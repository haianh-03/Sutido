using System;
using System.Collections.Generic;

namespace Sutido.Model.Entites;

public partial class Post
{
    public long PostId { get; set; }

    public long CreatorUserId { get; set; }

    public string PostType { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Subject { get; set; }

    public string? StudentGrade { get; set; }

    public int? SessionsPerWeek { get; set; }

    public string? PreferredDays { get; set; }

    public string? PreferredTime { get; set; }

    public decimal? PricePerSession { get; set; }

    public string? Location { get; set; }

    public bool IsPublished { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public virtual ICollection<ChatRoom> ChatRoomParentPosts { get; set; } = new List<ChatRoom>();

    public virtual ICollection<ChatRoom> ChatRoomTutorPosts { get; set; } = new List<ChatRoom>();

    public virtual User CreatorUser { get; set; } = null!;
}
