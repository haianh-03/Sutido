using System;
using System.Collections.Generic;

namespace Sutido.Model.Entites;

public partial class BankAccount
{
    public long BankAccountId { get; set; }

    public long UserId { get; set; }

    public string BankName { get; set; } = null!;

    public string AccountNumber { get; set; } = null!;

    public string AccountHolder { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<WalletExternalTransaction> WalletExternalTransactions { get; set; } = new List<WalletExternalTransaction>();
}
