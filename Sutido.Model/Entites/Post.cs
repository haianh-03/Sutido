using Sutido.Model.Enums;

namespace Sutido.Model.Entites;

public partial class Post
{
    public long PostId { get; set; }

    public long CreatorUserId { get; set; }

    public PostType PostType { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Subject { get; set; }

    public string? StudentGrade { get; set; }

    public int? SessionsPerWeek { get; set; }

    public string? PreferredDays { get; set; }

    public string? PreferredTime { get; set; }

    public decimal? PricePerSession { get; set; }

    public string Location { get; set; } = null!;

    public bool IsPublished { get; set; } = true;

    public bool IsActive { get; set; } = true;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? UpdatedAt { get; set; }

    public virtual ICollection<ChatRoom> ChatRoomParentPosts { get; set; } = new List<ChatRoom>();

    public virtual ICollection<ChatRoom> ChatRoomTutorPosts { get; set; } = new List<ChatRoom>();

    public virtual User CreatorUser { get; set; } = null!;
}
