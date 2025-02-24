using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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
            entity.HasKey(e => e.Id).HasName("PK__Accounts__3214EC07E4C486CF");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AvatarUrl)
                .IsUnicode(false)
                .HasColumnName("AvatarURL");
            entity.Property(e => e.Role).IsUnicode(false);
            entity.Property(e => e.Status).IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Accounts__UserId__7BF04F28");
        });

        modelBuilder.Entity<AdContent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AdConten__3214EC07FC68DBF7");

            entity.ToTable("AdContent");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).IsUnicode(false);
            entity.Property(e => e.ImgUrl).IsUnicode(false);

            entity.HasOne(d => d.AdvertisingClient).WithMany(p => p.AdContents)
                .HasForeignKey(d => d.AdvertisingClientId)
                .HasConstraintName("FK__AdContent__Adver__15B0212B");
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Appointm__3214EC07F8770382");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.Code).IsUnicode(false);

            entity.HasOne(d => d.AdContent).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.AdContentId)
                .HasConstraintName("FK__Appointme__AdCon__188C8DD6");

            entity.HasOne(d => d.RentalLocation).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.RentalLocationId)
                .HasConstraintName("FK__Appointme__Renta__1980B20F");
        });

        modelBuilder.Entity<AppointmentHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Appointm__3214EC07E5D3F463");

            entity.ToTable("AppointmentHistory");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IssueDate).HasColumnType("datetime");

            entity.HasOne(d => d.AdvertisingClient).WithMany(p => p.AppointmentHistoryAdvertisingClients)
                .HasForeignKey(d => d.AdvertisingClientId)
                .HasConstraintName("FK__Appointme__Adver__1C5D1EBA");

            entity.HasOne(d => d.Appointment).WithMany(p => p.AppointmentHistories)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__Appointme__Appoi__1E45672C");

            entity.HasOne(d => d.SpaceProvider).WithMany(p => p.AppointmentHistorySpaceProviders)
                .HasForeignKey(d => d.SpaceProviderId)
                .HasConstraintName("FK__Appointme__Space__1D5142F3");
        });

        modelBuilder.Entity<PanelType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PanelTyp__3214EC078C8C4013");

            entity.ToTable("PanelType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasColumnType("ntext");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payments__3214EC07C0FE7BD1");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Details).HasColumnType("ntext");
            entity.Property(e => e.Status).IsUnicode(false);

            entity.HasOne(d => d.PaymentType).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentTypeId)
                .HasConstraintName("FK__Payments__Paymen__066DDD9B");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentT__3214EC070059A7D9");

            entity.ToTable("PaymentType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.ImgUrl).IsUnicode(false);
        });

        modelBuilder.Entity<RegulatoryApproval>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Regulato__3214EC07ACA70843");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.IssueDate).HasColumnType("datetime");
            entity.Property(e => e.PermitNumber).IsUnicode(false);

            entity.HasOne(d => d.RentalLocation).WithMany(p => p.RegulatoryApprovals)
                .HasForeignKey(d => d.RentalLocationId)
                .HasConstraintName("FK__Regulator__Renta__2121D3D7");
        });

        modelBuilder.Entity<RegulatoryLicense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Regulato__3214EC07A46C3C59");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ImgUrl).IsUnicode(false);

            entity.HasOne(d => d.RegulatoryApproval).WithMany(p => p.RegulatoryLicenses)
                .HasForeignKey(d => d.RegulatoryApprovalId)
                .HasConstraintName("FK__Regulator__Regul__23FE4082");
        });

        modelBuilder.Entity<RentalLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RentalLo__3214EC0771B0F445");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AvailableDate).HasColumnType("datetime");
            entity.Property(e => e.Code).IsUnicode(false);
            entity.Property(e => e.Description).HasColumnType("ntext");
            entity.Property(e => e.PostDate).HasColumnType("datetime");

            entity.HasOne(d => d.Manager).WithMany(p => p.RentalLocationManagers)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__RentalLoc__Manag__0F03239C");

            entity.HasOne(d => d.SpaceProvider).WithMany(p => p.RentalLocationSpaceProviders)
                .HasForeignKey(d => d.SpaceProviderId)
                .HasConstraintName("FK__RentalLoc__Space__0E0EFF63");
        });

        modelBuilder.Entity<RentalLocationPanelType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RentalLo__3214EC076A0859B7");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.PanelType).WithMany(p => p.RentalLocationPanelTypes)
                .HasForeignKey(d => d.PanelTypeId)
                .HasConstraintName("FK__RentalLoc__Panel__12D3B480");

            entity.HasOne(d => d.RentalLocation).WithMany(p => p.RentalLocationPanelTypes)
                .HasForeignKey(d => d.RentalLocationId)
                .HasConstraintName("FK__RentalLoc__Renta__11DF9047");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subscrip__3214EC07D1EF587E");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).IsUnicode(false);
            entity.Property(e => e.Features).HasColumnType("ntext");
            entity.Property(e => e.Status).IsUnicode(false);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3214EC0742C9B38A");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Status).IsUnicode(false);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");

            entity.HasOne(d => d.Payment).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK__Transacti__Payme__094A4A46");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07C76FCE83");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Password).IsUnicode(false);
            entity.Property(e => e.PhoneNumber).IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserName).IsUnicode(false);
        });

        modelBuilder.Entity<UserSubscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserSubs__3214EC07ECDF3AF9");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status).IsUnicode(false);

            entity.HasOne(d => d.Account).WithMany(p => p.UserSubscriptions)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__UserSubsc__Accou__00B50445");

            entity.HasOne(d => d.Subscription).WithMany(p => p.UserSubscriptions)
                .HasForeignKey(d => d.SubscriptionId)
                .HasConstraintName("FK__UserSubsc__Subsc__01A9287E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
