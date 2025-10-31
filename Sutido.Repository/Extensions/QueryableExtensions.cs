using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace PRN232.Lab2.CoffeeStore.Repositories.Extensions
{
    public static class QueryableExtensions
    {
        //Search
        public static IQueryable<T> Search<T>(this IQueryable<T> query, string? keyword, string? searchFields)
        {
            if (string.IsNullOrEmpty(keyword) || string.IsNullOrEmpty(searchFields))
            {
                return query;
            }

            var fields = searchFields.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (fields.Length == 1)
            {
                return query.Where($"{fields[0]}.Contains(@0)", keyword);
            }

            var conditions = string.Join(" OR ", fields.Select(f => $"{f}.Contains(@0)"));

            return query.Where(conditions, keyword);

        }

        //Sort
        public static IQueryable<T> Sort<T>(this IQueryable<T> query, string? sortBy, string? sortOrder, params string[] searchFields)
        {
            if (string.IsNullOrEmpty(sortBy))
                return query;

            var fields = sortBy.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var directions = (sortOrder ?? "asc").Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (directions.Length < fields.Length)
            {
                directions = directions.Concat(Enumerable.Repeat(directions.FirstOrDefault() ?? "asc", fields.Length - directions.Length)).ToArray();
            }

            var sortExpression = string.Join(", ", fields.Select((f, i) =>
                $"{f} {(directions[i].Equals("desc", StringComparison.OrdinalIgnoreCase) ? "descending" : "ascending")}"));

            return query.OrderBy(sortExpression);
        }

        //Paging
        public static IQueryable<T> Paging<T>(this IQueryable<T> query, int page, int pageSize)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize < 1 ? 10 : pageSize;

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        //Filter
        public static IQueryable<T> Filter<T>(this IQueryable<T> query, Dictionary<string, string>? filters)
        {
            if (filters == null || filters.Count == 0)
                return query;

            //foreach (var filter in filters)
            //{
            //    var field = filter.Key;
            //    var value = filter.Value;
            //    query = query.Where($"{field} == @0", value);
            //}

            foreach (var filter in filters)
            {
                if (string.IsNullOrEmpty(filter.Value))
                    continue; // bỏ qua filter null hoặc rỗng

                var property = typeof(T).GetProperty(filter.Key);
                if (property == null)
                    continue; // bỏ qua nếu không tồn tại property trong entity

                query = query.Where(e => EF.Property<string>(e, filter.Key).Contains(filter.Value));
            }
            return query;
        }
    }
}
