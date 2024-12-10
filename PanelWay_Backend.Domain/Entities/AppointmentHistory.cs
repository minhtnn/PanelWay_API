using System;
using System.Collections.Generic;

namespace PanelWay_Backend.Domain.Entities;

public partial class AppointmentHistory
{
    public Guid Id { get; set; }

    public DateTime? IssueDate { get; set; }

    public string? FromStatus { get; set; }

    public string? ToStatus { get; set; }

    public Guid? AdvertisingClientId { get; set; }

    public Guid? SpaceProviderId { get; set; }

    public Guid? AppointmentId { get; set; }

    public virtual Account? AdvertisingClient { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual Account? SpaceProvider { get; set; }
}
