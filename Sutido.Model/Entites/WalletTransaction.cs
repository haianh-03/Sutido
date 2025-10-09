using System;
using System.Collections.Generic;

namespace Sutido.Model.Entites;

public partial class WalletTransaction
{
    public long TransactionId { get; set; }

    public long WalletId { get; set; }

    public long? FromUserId { get; set; }

    public long? ToUserId { get; set; }

    public string TransactionType { get; set; } = null!;

    public string? RelatedEntity { get; set; }

    public long? RelatedId { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal DiscountAmount { get; set; }

    public decimal FinalAmount { get; set; }

    public string Status { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? CompletedAt { get; set; }

    public string? Note { get; set; }

    public virtual User? FromUser { get; set; }

    public virtual User? ToUser { get; set; }

    public virtual ICollection<VoucherUsage> VoucherUsages { get; set; } = new List<VoucherUsage>();

    public virtual Wallet Wallet { get; set; } = null!;
}
