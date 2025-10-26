using Sutido.Model.Enums;

namespace Sutido.API.ViewModels.Requests
{
    public class TutorProfileReviewRequest
    {
        public long TutorProfileId { get; set; }
        public long ReviewerBy { get; set; }
        public StatusType status { get; set; }
        public string? Reason { get; set; }
    }
}
