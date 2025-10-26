using Sutido.Model.Enums;

namespace Sutido.API.ViewModels.Responses
{
    public class CertificationResponse
    {
        public long CertificationId { get; set; }

        public string DocumentType { get; set; } = null!;

        public string FileUrl { get; set; } = null!;

        public string? Note { get; set; }

        public StatusType Status { get; set; }

        public DateTimeOffset SubmittedAt { get; set; }

        public long? ReviewedBy { get; set; }

        public DateTimeOffset? ReviewedAt { get; set; }
    }
}
