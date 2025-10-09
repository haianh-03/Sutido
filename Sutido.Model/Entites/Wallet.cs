using System;
using System.Collections.Generic;

namespace Sutido.Model.Entites;

public partial class Wallet
{
    public long WalletId { get; set; }

    public long UserId { get; set; }

    public decimal Balance { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<WalletExternalTransaction> WalletExternalTransactions { get; set; } = new List<WalletExternalTransaction>();

    public virtual ICollection<WalletTransaction> WalletTransactions { get; set; } = new List<WalletTransaction>();
}
