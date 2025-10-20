using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Sutido.Model.Entites;

namespace Sutido.Model;

public partial class SutidoProjectContext : DbContext
{
    public SutidoProjectContext()
    {
    }

    public SutidoProjectContext(DbContextOptions<SutidoProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ad> Ads { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<BankAccount> BankAccounts { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<ChatRoom> ChatRooms { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<MarketplaceItem> MarketplaceItems { get; set; }

    public virtual DbSet<Point> Points { get; set; }

    public virtual DbSet<PointTransaction> PointTransactions { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Referral> Referrals { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Tracking> Trackings { get; set; }

    public virtual DbSet<TutorProfile> TutorProfiles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Verification> Verifications { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    public virtual DbSet<VoucherUsage> VoucherUsages { get; set; }

    public virtual DbSet<Wallet> Wallets { get; set; }

    public virtual DbSet<WalletExternalTransaction> WalletExternalTransactions { get; set; }

    public virtual DbSet<WalletTransaction> WalletTransactions { get; set; }


    public virtual DbSet<Message> Messages { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // ⚙️ Chuỗi kết nối tạm thời để EF tạo migration (sửa lại tên DB nếu cần)
            optionsBuilder.UseSqlServer("Server=(local);Database=SutidoProject;User Id=sa;Password=12345;TrustServerCertificate=True");
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ad>(entity =>
        {
            entity.HasKey(e => e.AdId).HasName("PK__Ad__7130D5AEFD321A04");

            entity.ToTable("Ad");

            entity.Property(e => e.AdType).HasMaxLength(100);
            entity.Property(e => e.AdvertiserName).HasMaxLength(200);
            entity.Property(e => e.ContentUrl).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditId).HasName("PK__AuditLog__A17F2398AB6621E4");

            entity.ToTable("AuditLog");

            entity.Property(e => e.Action).HasMaxLength(50);
            entity.Property(e => e.Entity).HasMaxLength(100);
            entity.Property(e => e.EntityId).HasMaxLength(100);
            entity.Property(e => e.PerformedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.PerformedByUser).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.PerformedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AuditLog__Perfor__2DE6D218");
        });

        modelBuilder.Entity<BankAccount>(entity =>
        {
            entity.HasKey(e => e.BankAccountId).HasName("PK__BankAcco__4FC8E4A11448D1FB");

            entity.ToTable("BankAccount");

            entity.Property(e => e.AccountHolder).HasMaxLength(200);
            entity.Property(e => e.AccountNumber).HasMaxLength(100);
            entity.Property(e => e.BankName).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.User).WithMany(p => p.BankAccounts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BankAccou__UserI__3F466844");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951AED7A9AD38C");

            entity.ToTable("Booking");

            entity.Property(e => e.AgreedDays).HasMaxLength(200);
            entity.Property(e => e.AgreedPricePerSession).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AgreedTime).HasMaxLength(100);
            entity.Property(e => e.BookingStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.SecurityCode).HasMaxLength(50);

            entity.HasOne(d => d.ChatRoom).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ChatRoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__ChatRoo__6A30C649");
        });

        modelBuilder.Entity<ChatRoom>(entity =>
        {
            entity.HasKey(e => e.ChatRoomId).HasName("PK__ChatRoom__69733CF7FB8D3EC9");

            entity.ToTable("ChatRoom");

            entity.Property(e => e.CancelReason).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.ParentPost).WithMany(p => p.ChatRoomParentPosts)
                .HasForeignKey(d => d.ParentPostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChatRoom__Parent__5DCAEF64");

            entity.HasOne(d => d.ParentUser).WithMany(p => p.ChatRoomParentUsers)
                .HasForeignKey(d => d.ParentUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChatRoom__Parent__5FB337D6");

            entity.HasOne(d => d.TutorPost).WithMany(p => p.ChatRoomTutorPosts)
                .HasForeignKey(d => d.TutorPostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChatRoom__TutorP__5EBF139D");

            entity.HasOne(d => d.TutorUser).WithMany(p => p.ChatRoomTutorUsers)
                .HasForeignKey(d => d.TutorUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChatRoom__TutorU__60A75C0F");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__1ABEEF0F1ECA779B");

            entity.ToTable("Document");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.DocumentType).HasMaxLength(100);
            entity.Property(e => e.FileUrl).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.OwnerUser).WithMany(p => p.Documents)
                .HasForeignKey(d => d.OwnerUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Document__OwnerU__797309D9");
        });

        modelBuilder.Entity<MarketplaceItem>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Marketpl__727E838B0456B75A");

            entity.ToTable("MarketplaceItem");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.FileUrl).HasMaxLength(500);
            entity.Property(e => e.IsPublished).HasDefaultValue(true);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.SellerUser).WithMany(p => p.MarketplaceItems)
                .HasForeignKey(d => d.SellerUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Marketpla__Selle__7D439ABD");
        });

        modelBuilder.Entity<Point>(entity =>
        {
            entity.HasKey(e => e.PointId).HasName("PK__Point__40A977E16D9C2789");

            entity.ToTable("Point");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Reason).HasMaxLength(200);

            entity.HasOne(d => d.User).WithMany(p => p.Points)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Point__UserId__07C12930");
        });

        modelBuilder.Entity<PointTransaction>(entity =>
        {
            entity.HasKey(e => e.PointTransactionId).HasName("PK__PointTra__4D5BCB7FCB6543CB");

            entity.ToTable("PointTransaction");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Reason).HasMaxLength(200);

            entity.HasOne(d => d.Point).WithMany(p => p.PointTransactions)
                .HasForeignKey(d => d.PointId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PointTran__Point__123EB7A3");

            entity.HasOne(d => d.Referral).WithMany(p => p.PointTransactions)
                .HasForeignKey(d => d.ReferralId)
                .HasConstraintName("FK__PointTran__Refer__1332DBDC");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Post__AA1260180B04E4DB");

            entity.ToTable("Post");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.IsPublished).HasDefaultValue(true);
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.PostType).HasMaxLength(50);
            entity.Property(e => e.PreferredDays).HasMaxLength(200);
            entity.Property(e => e.PreferredTime).HasMaxLength(100);
            entity.Property(e => e.PricePerSession).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.StudentGrade).HasMaxLength(100);
            entity.Property(e => e.Subject).HasMaxLength(200);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.CreatorUser).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CreatorUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Post__CreatorUse__59063A47");
        });

        modelBuilder.Entity<Referral>(entity =>
        {
            entity.HasKey(e => e.ReferralId).HasName("PK__Referral__A2C4A9665A0FDCD4");

            entity.ToTable("Referral");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.ReferredEmail).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.ReferredUser).WithMany(p => p.ReferralReferredUsers)
                .HasForeignKey(d => d.ReferredUserId)
                .HasConstraintName("FK__Referral__Referr__0C85DE4D");

            entity.HasOne(d => d.ReferrerUser).WithMany(p => p.ReferralReferrerUsers)
                .HasForeignKey(d => d.ReferrerUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Referral__Referr__0B91BA14");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Review__74BC79CEC2B04C07");

            entity.ToTable("Review");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Booking).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Review__BookingI__73BA3083");

            entity.HasOne(d => d.FromUser).WithMany(p => p.ReviewFromUsers)
                .HasForeignKey(d => d.FromUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Review__FromUser__74AE54BC");

            entity.HasOne(d => d.ToUser).WithMany(p => p.ReviewToUsers)
                .HasForeignKey(d => d.ToUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Review__ToUserId__75A278F5");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1AD1258896");

            entity.ToTable("Role");

            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.SubscriptionId).HasName("PK__Subscrip__9A2B249DAF80ACF1");

            entity.ToTable("Subscription");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PackageName).HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Subscriptions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Subscript__UserI__02FC7413");
        });

        modelBuilder.Entity<Tracking>(entity =>
        {
            entity.HasKey(e => e.TrackingId).HasName("PK__Tracking__3C19EDF1CC090E74");

            entity.ToTable("Tracking");

            entity.Property(e => e.Action).HasMaxLength(200);
            entity.Property(e => e.ActionAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.SecurityCodeUsed).HasMaxLength(50);

            entity.HasOne(d => d.Booking).WithMany(p => p.Trackings)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tracking__Bookin__6EF57B66");

            entity.HasOne(d => d.TutorUser).WithMany(p => p.Trackings)
                .HasForeignKey(d => d.TutorUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tracking__TutorU__6FE99F9F");
        });

        modelBuilder.Entity<TutorProfile>(entity =>
        {
            entity.HasKey(e => e.TutorProfileId).HasName("PK__TutorPro__CE969F683A55E287");

            entity.ToTable("TutorProfile");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Education).HasMaxLength(500);

            entity.HasOne(d => d.User).WithMany(p => p.TutorProfiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TutorProf__UserI__534D60F1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4CB21C8C24");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D105349C379654").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__RoleId__3B75D760");
        });

        modelBuilder.Entity<Verification>(entity =>
        {
            entity.HasKey(e => e.VerificationId).HasName("PK__Verifica__306D490753D10F80");

            entity.ToTable("Verification");

            entity.Property(e => e.DocumentType).HasMaxLength(100);
            entity.Property(e => e.FileUrl).HasMaxLength(500);
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.SubmittedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.ReviewedByNavigation).WithMany(p => p.VerificationReviewedByNavigations)
                .HasForeignKey(d => d.ReviewedBy)
                .HasConstraintName("FK__Verificat__Revie__4F7CD00D");

            entity.HasOne(d => d.User).WithMany(p => p.VerificationUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Verificat__UserI__4D94879B");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PK__Voucher__3AEE7921F0C88711");

            entity.ToTable("Voucher");

            entity.HasIndex(e => e.Code, "UQ__Voucher__A25C5AA7899263F3").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.DiscountType).HasMaxLength(50);
            entity.Property(e => e.DiscountValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MaxDiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MinOrderAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<VoucherUsage>(entity =>
        {
            entity.HasKey(e => e.VoucherUsageId).HasName("PK__VoucherU__4264F80B3C601F66");

            entity.ToTable("VoucherUsage");

            entity.Property(e => e.UsedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.User).WithMany(p => p.VoucherUsages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VoucherUs__UserI__25518C17");

            entity.HasOne(d => d.Voucher).WithMany(p => p.VoucherUsages)
                .HasForeignKey(d => d.VoucherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VoucherUs__Vouch__245D67DE");

            entity.HasOne(d => d.WalletTransaction).WithMany(p => p.VoucherUsages)
                .HasForeignKey(d => d.WalletTransactionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VoucherUs__Walle__2645B050");
        });

        modelBuilder.Entity<Wallet>(entity =>
        {
            entity.HasKey(e => e.WalletId).HasName("PK__Wallet__84D4F90E94E5E2C5");

            entity.ToTable("Wallet");

            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.User).WithMany(p => p.Wallets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Wallet__UserId__4316F928");
        });

        modelBuilder.Entity<WalletExternalTransaction>(entity =>
        {
            entity.HasKey(e => e.ExternalTransactionId).HasName("PK__WalletEx__535E5F63F3B48CEE");

            entity.ToTable("WalletExternalTransaction");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.PaymentMethod).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TransactionCode).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.BankAccount).WithMany(p => p.WalletExternalTransactions)
                .HasForeignKey(d => d.BankAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WalletExt__BankA__48CFD27E");

            entity.HasOne(d => d.Wallet).WithMany(p => p.WalletExternalTransactions)
                .HasForeignKey(d => d.WalletId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WalletExt__Walle__47DBAE45");
        });

        modelBuilder.Entity<WalletTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__WalletTr__55433A6B2E6FB6B7");

            entity.ToTable("WalletTransaction");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.DiscountAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FinalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.RelatedEntity).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransactionType).HasMaxLength(50);

            entity.HasOne(d => d.FromUser).WithMany(p => p.WalletTransactionFromUsers)
                .HasForeignKey(d => d.FromUserId)
                .HasConstraintName("FK__WalletTra__FromU__1DB06A4F");

            entity.HasOne(d => d.ToUser).WithMany(p => p.WalletTransactionToUsers)
                .HasForeignKey(d => d.ToUserId)
                .HasConstraintName("FK__WalletTra__ToUse__1EA48E88");

            entity.HasOne(d => d.Wallet).WithMany(p => p.WalletTransactions)
                .HasForeignKey(d => d.WalletId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WalletTra__Walle__1CBC4616");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId);
            entity.Property(e => e.Content).HasColumnType("nvarchar(max)");
            entity.Property(e => e.MessageType).HasMaxLength(50).HasDefaultValue("text");
            entity.Property(e => e.FileUrl).HasMaxLength(500);
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.SentAt).HasDefaultValueSql("SYSDATETIMEOFFSET()");

            entity.HasOne(d => d.ChatRoom)
                .WithMany(p => p.Messages)
                .HasForeignKey(d => d.ChatRoomId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Message_ChatRoom");

            entity.HasOne(d => d.Sender)
                .WithMany()
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Message_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
