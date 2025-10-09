using System;
using System.Collections.Generic;

namespace Sutido.Model.Entites;

public partial class Document
{
    public long DocumentId { get; set; }

    public long OwnerUserId { get; set; }

    public string DocumentType { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string FileUrl { get; set; } = null!;

    public long SizeBytes { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public virtual User OwnerUser { get; set; } = null!;
}
