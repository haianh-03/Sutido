using Sutido.Model.Enums;

namespace Sutido.Model.Entites;

public partial class User
{
    public long UserId { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public DateTimeOffset DateOfBirth { get; set; }

    public string? Street { get; set; }

    public string? Ward { get; set; }

    public string? District { get; set; }

    public string? City { get; set; }

    public bool IsActive { get; set; } = true;

    public RoleType Role { get; set; } = RoleType.Customer;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public DateTimeOffset? UpdatedAt { get; set; }

    // AuditLog
    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    // BankAccount
    public virtual ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();

    // ChatRoom
    public virtual ICollection<ChatRoom> ChatRoomParentUsers { get; set; } = new List<ChatRoom>();

    public virtual ICollection<ChatRoom> ChatRoomTutorUsers { get; set; } = new List<ChatRoom>();

    // Document
    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    // MarketplaceItem
    public virtual ICollection<MarketplaceItem> MarketplaceItems { get; set; } = new List<MarketplaceItem>();

    // Point
    public virtual Point Point { get; set; } = null!;

    // Post
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    // Referral
    public virtual ICollection<Referral> ReferralReferredUsers { get; set; } = new List<Referral>();

    public virtual ICollection<Referral> ReferralReferrerUsers { get; set; } = new List<Referral>();

    // Review
    public virtual ICollection<Review> ReviewFromUsers { get; set; } = new List<Review>();

    public virtual ICollection<Review> ReviewToUsers { get; set; } = new List<Review>();

    // Subscription
    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    // Tracking
    public virtual ICollection<Tracking> Trackings { get; set; } = new List<Tracking>();

    // TutorProfile
    public virtual TutorProfile? TutorProfile { get; set; }

    public virtual ICollection<TutorProfile> ReviewedTutorProfiles { get; set; } = new List<TutorProfile>();

    // Certification
    public virtual ICollection<Certification> ReviewedCertifications { get; set; } = new List<Certification>();

    // VoucherUsage
    public virtual ICollection<VoucherUsage> VoucherUsages { get; set; } = new List<VoucherUsage>();

    // WalletTransaction
    public virtual ICollection<WalletTransaction> WalletTransactionFromUsers { get; set; } = new List<WalletTransaction>();

    public virtual ICollection<WalletTransaction> WalletTransactionToUsers { get; set; } = new List<WalletTransaction>();

    // Wallet
    public virtual Wallet Wallet { get; set; } = null!;
}
