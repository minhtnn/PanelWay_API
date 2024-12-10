using System;
using System.Collections.Generic;

namespace PanelWay_Backend.Domain.Entities;

public partial class Account
{
    public Guid Id { get; set; }

    public string? AvatarUrl { get; set; }

    public string? Status { get; set; }

    public string? Role { get; set; }

    public int? IndividualPoint { get; set; }

    public Guid? UserId { get; set; }

    public virtual ICollection<AdContent> AdContents { get; set; } = new List<AdContent>();

    public virtual ICollection<AppointmentHistory> AppointmentHistoryAdvertisingClients { get; set; } = new List<AppointmentHistory>();

    public virtual ICollection<AppointmentHistory> AppointmentHistorySpaceProviders { get; set; } = new List<AppointmentHistory>();

    public virtual ICollection<RentalLocation> RentalLocationManagers { get; set; } = new List<RentalLocation>();

    public virtual ICollection<RentalLocation> RentalLocationSpaceProviders { get; set; } = new List<RentalLocation>();

    public virtual User? User { get; set; }

    public virtual ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
}
