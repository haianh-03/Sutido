using Sutido.Model.Entites;
using Sutido.Model.Enums;
using Sutido.Repository.UnitOfWork;
using Sutido.Service.Interfaces;

namespace Sutido.Service.Implementations
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _iUnitOfWork;

        public PostService(IUnitOfWork iUnitOfWork)
        {
            _iUnitOfWork = iUnitOfWork;
        }

        public async Task<Post?> AddAsync(Post p)
        {
            var user = await _iUnitOfWork.Users.GetByIdAsync(p.CreatorUserId);
            if (user == null)
                return null;
            if (user.Role == Model.Enums.RoleType.Customer)
            {
                p.PostType = Model.Enums.PostType.FindTutor;
            }
            else if (user.Role == Model.Enums.RoleType.Tutor)
            {
                p.PostType = Model.Enums.PostType.FindStudent;
            }
            if (string.IsNullOrEmpty(user.District) || string.IsNullOrEmpty(user.City))
                throw new Exception("Location of user is empty.");
            p.Location = user.District + ", " + user.City;
            await _iUnitOfWork.Posts.AddAsync(p);
            var result = await _iUnitOfWork.SaveAsync();
            if (result <= 0) return null;
            return p;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var post = await _iUnitOfWork.Posts.GetByIdAsync(id);
            if (post == null) return -1;

            post.IsActive = false;
            _iUnitOfWork.Posts.Update(post);
            return await _iUnitOfWork.SaveAsync();
        }

        //public async Task<IEnumerable<Post>> GetAllAsync(string? keyword, string? sortBy, string sortOrder, int page, int pageSize, Dictionary<string, string>? filters)
        //{
        //    var query = await _iUnitOfWork.Posts.GetAllAsync(keyword, sortBy, sortOrder, page, pageSize, filters);

        //    return query;
        //}

        public async Task<IEnumerable<Post>> GetAllAsync(string? keyword, string? sortBy, string? sortOrder, int page, int pageSize, Dictionary<string, string>? filters, PostType type)
        {
            var query = await _iUnitOfWork.Posts.GetAllAsync(keyword, sortBy, sortOrder, page, pageSize, filters, type);

            return query;
        }

        public async Task<Post?> GetDetailsByIdAsync(int id)
        {
            return await _iUnitOfWork.Posts.GetByIdAsync(id);
        }

        public async Task<int> UpdateAsync(int id, Post p)
        {
            var post = await _iUnitOfWork.Posts.GetByIdAsync(id);
            if (post == null) return -1;

            post.Title = p.Title;
            post.Description = p.Description;

            _iUnitOfWork.Posts.Update(post);
            return await _iUnitOfWork.SaveAsync();
        }
    }
}
