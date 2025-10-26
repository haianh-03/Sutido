using Sutido.Model.Enums;

namespace Sutido.API.ViewModels.Requests
{
    public class CertificationReviewRequest
    {
        public long CertificationId { get; set; }
        public long? ReviewedBy { get; set; }
        public StatusType Status { get; set; }
    }
}
