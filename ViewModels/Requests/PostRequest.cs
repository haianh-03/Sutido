using System.ComponentModel.DataAnnotations;

namespace ViewModels.Requests
{
    public class PostRequest
    {
        [Required]
        public long CreatorUserId { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string? Subject { get; set; }
        [Required]
        public string? StudentGrade { get; set; }
        [Required]
        public int? SessionsPerWeek { get; set; }
        [Required]
        public string? PreferredDays { get; set; }
        [Required]
        public string? PreferredTime { get; set; }
        [Required]
        public decimal? PricePerSession { get; set; }
        public string? Description { get; set; }
    }
}
