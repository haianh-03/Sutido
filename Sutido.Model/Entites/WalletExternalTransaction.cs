using System;
using System.Collections.Generic;

namespace Sutido.Model.Entites;

public partial class WalletExternalTransaction
{
    public long ExternalTransactionId { get; set; }

    public long WalletId { get; set; }

    public long BankAccountId { get; set; }

    public string Type { get; set; } = null!;

    public string? PaymentMethod { get; set; }

    public decimal Amount { get; set; }

    public string? TransactionCode { get; set; }

    public string Status { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public virtual BankAccount BankAccount { get; set; } = null!;

    public virtual Wallet Wallet { get; set; } = null!;
}
