namespace ViewModels.Responses
{
    public class PostResponse
    {
        public long PostId { get; set; }
        public string Title { get; set; } = null!;
        public string? Subject { get; set; }
        public string? StudentGrade { get; set; }
        public int? SessionsPerWeek { get; set; }
        public string? PreferredDays { get; set; }
        public string? PreferredTime { get; set; }
        public decimal? PricePerSession { get; set; }
        public string? Location { get; set; }
        public bool IsPublished { get; set; } = true;
    }
}
