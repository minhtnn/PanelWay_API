using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PanelWay_Backend.Domain.Entities;

public partial class PanelWayDbContext : DbContext
{
    public PanelWayDbContext()
    {
    }

    public PanelWayDbContext(DbContextOptions<PanelWayDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AdContent> AdContents { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<AppointmentHistory> AppointmentHistories { get; set; }

    public virtual DbSet<PanelType> PanelTypes { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<RegulatoryApproval> RegulatoryApprovals { get; set; }

    public virtual DbSet<RegulatoryLicense> RegulatoryLicenses { get; set; }

    public virtual DbSet<RentalLocation> RentalLocations { get; set; }

    public virtual DbSet<RentalLocationPanelType> RentalLocationPanelTypes { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =(local);database=PanelWayDB;uid=sa;pwd=123456;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Accounts__3214EC073D7062DE");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AvatarUrl)
                .IsUnicode(false)
                .HasColumnName("AvatarURL");
            entity.Property(e => e.Role).IsUnicode(false);
            entity.Property(e => e.Status).IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Accounts__UserId__3414ACBA");
        });

        modelBuilder.Entity<AdContent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AdConten__3214EC0733DF2158");

            entity.ToTable("AdContent");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).IsUnicode(false);
            entity.Property(e => e.ImgUrl).IsUnicode(false);

            entity.HasOne(d => d.AdvertisingClient).WithMany(p => p.AdContents)
                .HasForeignKey(d => d.AdvertisingClientId)
                .HasConstraintName("FK__AdContent__Adver__4FBCC72F");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Appointm__3214EC07628BEF69");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.Code).IsUnicode(false);

            entity.HasOne(d => d.AdContent).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.AdContentId)
                .HasConstraintName("FK__Appointme__AdCon__529933DA");

            entity.HasOne(d => d.RentalLocation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.RentalLocationId)
                .HasConstraintName("FK__Appointme__Renta__538D5813");
        });

        modelBuilder.Entity<AppointmentHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Appointm__3214EC077D77D700");

            entity.ToTable("AppointmentHistory");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IssueDate).HasColumnType("datetime");

            entity.HasOne(d => d.AdvertisingClient).WithMany(p => p.AppointmentHistoryAdvertisingClients)
                .HasForeignKey(d => d.AdvertisingClientId)
                .HasConstraintName("FK__Appointme__Adver__5669C4BE");

            entity.HasOne(d => d.Appointment).WithMany(p => p.AppointmentHistories)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__Appointme__Appoi__58520D30");

            entity.HasOne(d => d.SpaceProvider).WithMany(p => p.AppointmentHistorySpaceProviders)
                .HasForeignKey(d => d.SpaceProviderId)
                .HasConstraintName("FK__Appointme__Space__575DE8F7");
        });

        modelBuilder.Entity<PanelType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PanelTyp__3214EC0794F8D01B");

            entity.ToTable("PanelType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasColumnType("ntext");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payments__3214EC07B0D05B2C");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Details).HasColumnType("ntext");
            entity.Property(e => e.Status).IsUnicode(false);

            entity.HasOne(d => d.PaymentType).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentTypeId)
                .HasConstraintName("FK__Payments__Paymen__3E923B2D");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentT__3214EC07A8750941");

            entity.ToTable("PaymentType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.ImgUrl).IsUnicode(false);
        });

        modelBuilder.Entity<RegulatoryApproval>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Regulato__3214EC072E0A0B5F");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.IssueDate).HasColumnType("datetime");
            entity.Property(e => e.PermitNumber).IsUnicode(false);

            entity.HasOne(d => d.RentalLocation).WithMany(p => p.RegulatoryApprovals)
                .HasForeignKey(d => d.RentalLocationId)
                .HasConstraintName("FK__Regulator__Renta__5B2E79DB");
        });

        modelBuilder.Entity<RegulatoryLicense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Regulato__3214EC07736DD6F7");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ImgUrl).IsUnicode(false);

            entity.HasOne(d => d.RegulatoryApproval).WithMany(p => p.RegulatoryLicenses)
                .HasForeignKey(d => d.RegulatoryApprovalId)
                .HasConstraintName("FK__Regulator__Regul__5E0AE686");
        });

        modelBuilder.Entity<RentalLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RentalLo__3214EC0755F3B28E");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AvailableDate).HasColumnType("datetime");
            entity.Property(e => e.Code).IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.PostDate).HasColumnType("datetime");

            entity.HasOne(d => d.Manager).WithMany(p => p.RentalLocationManagers)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__RentalLoc__Manag__490FC9A0");

            entity.HasOne(d => d.SpaceProvider).WithMany(p => p.RentalLocationSpaceProviders)
                .HasForeignKey(d => d.SpaceProviderId)
                .HasConstraintName("FK__RentalLoc__Space__481BA567");
        });

        modelBuilder.Entity<RentalLocationPanelType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RentalLo__3214EC07A3392991");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.PanelType).WithMany(p => p.RentalLocationPanelTypes)
                .HasForeignKey(d => d.PanelTypeId)
                .HasConstraintName("FK__RentalLoc__Panel__4CE05A84");

            entity.HasOne(d => d.RentalLocation).WithMany(p => p.RentalLocationPanelTypes)
                .HasForeignKey(d => d.RentalLocationId)
                .HasConstraintName("FK__RentalLoc__Renta__4BEC364B");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subscrip__3214EC07A22543A5");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).IsUnicode(false);
            entity.Property(e => e.Features).HasColumnType("ntext");
            entity.Property(e => e.Status).IsUnicode(false);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC07A8E8CC07");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Status).IsUnicode(false);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");

            entity.HasOne(d => d.Payment).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__Transacti__Payme__4356F04A");

            entity.HasOne(d => d.Subscription).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.SubscriptionId)
                .HasConstraintName("FK__Transacti__Subsc__416EA7D8");

            entity.HasOne(d => d.UserSubscription).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserSubscriptionId)
                .HasConstraintName("FK__Transacti__UserS__4262CC11");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07FDFA3CF8");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Password).IsUnicode(false);
            entity.Property(e => e.PhoneNumber).IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserName).IsUnicode(false);
        });

        modelBuilder.Entity<UserSubscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserSubs__3214EC0792BC5EFF");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status).IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.UserSubscriptions)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__UserSubsc__Accou__38D961D7");

            entity.HasOne(d => d.Subscription).WithMany(p => p.UserSubscriptions)
                .HasForeignKey(d => d.SubscriptionId)
                .HasConstraintName("FK__UserSubsc__Subsc__39CD8610");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
