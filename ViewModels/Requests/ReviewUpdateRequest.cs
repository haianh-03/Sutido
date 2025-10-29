using System.ComponentModel.DataAnnotations;

namespace Sutido.API.ViewModels.Requests
{
    public class ReviewUpdateRequest
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string? Comment { get; set; }
    }
}