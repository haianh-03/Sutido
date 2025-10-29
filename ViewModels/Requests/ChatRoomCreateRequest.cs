using System.ComponentModel.DataAnnotations;

namespace Sutido.API.ViewModels.Requests
{
    public class ChatRoomCreateRequest
    {
        [Required]
        public long ParentPostId { get; set; }

        [Required]
        public long TutorPostId { get; set; }

        [Required]
        public long ParentUserId { get; set; }

        [Required]
        public long TutorUserId { get; set; }
    }
}