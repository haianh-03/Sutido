namespace ViewModels.Responses
{
    public class PostDetailsResponse
    {
        public long PostId { get; set; }

        public string Title { get; set; } = null!;

        public long CreatorUserId { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public string? StudentGrade { get; set; }

        public int? SessionsPerWeek { get; set; }

        public string? PreferredDays { get; set; }

        public string? PreferredTime { get; set; }

        public decimal? PricePerSession { get; set; }

        public string? Location { get; set; }

        public string? Description { get; set; }
    }
}
