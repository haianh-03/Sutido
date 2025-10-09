using System;
using System.Collections.Generic;

namespace Sutido.Model.Entites;

public partial class VoucherUsage
{
    public long VoucherUsageId { get; set; }

    public long VoucherId { get; set; }

    public long UserId { get; set; }

    public long WalletTransactionId { get; set; }

    public DateTimeOffset UsedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual Voucher Voucher { get; set; } = null!;

    public virtual WalletTransaction WalletTransaction { get; set; } = null!;
}
