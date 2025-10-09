using System;
using System.Collections.Generic;

namespace Sutido.Model.Entites;

public partial class User
{
    public long UserId { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? Phone { get; set; }

    public bool IsActive { get; set; }

    public int RoleId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();

    public virtual ICollection<ChatRoom> ChatRoomParentUsers { get; set; } = new List<ChatRoom>();

    public virtual ICollection<ChatRoom> ChatRoomTutorUsers { get; set; } = new List<ChatRoom>();

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<MarketplaceItem> MarketplaceItems { get; set; } = new List<MarketplaceItem>();

    public virtual ICollection<Point> Points { get; set; } = new List<Point>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Referral> ReferralReferredUsers { get; set; } = new List<Referral>();

    public virtual ICollection<Referral> ReferralReferrerUsers { get; set; } = new List<Referral>();

    public virtual ICollection<Review> ReviewFromUsers { get; set; } = new List<Review>();

    public virtual ICollection<Review> ReviewToUsers { get; set; } = new List<Review>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual ICollection<Tracking> Trackings { get; set; } = new List<Tracking>();

    public virtual ICollection<TutorProfile> TutorProfiles { get; set; } = new List<TutorProfile>();

    public virtual ICollection<Verification> VerificationReviewedByNavigations { get; set; } = new List<Verification>();

    public virtual ICollection<Verification> VerificationUsers { get; set; } = new List<Verification>();

    public virtual ICollection<VoucherUsage> VoucherUsages { get; set; } = new List<VoucherUsage>();

    public virtual ICollection<WalletTransaction> WalletTransactionFromUsers { get; set; } = new List<WalletTransaction>();

    public virtual ICollection<WalletTransaction> WalletTransactionToUsers { get; set; } = new List<WalletTransaction>();

    public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
}
