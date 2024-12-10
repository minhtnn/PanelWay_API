using System;
using System.Collections.Generic;

namespace PanelWay_Backend.Domain.Entities;

public partial class Appointment
{
    public Guid Id { get; set; }

    public string? Code { get; set; }

    public DateTime? BookingDate { get; set; }

    public string? Place { get; set; }

    public int? Priority { get; set; }

    public string? Status { get; set; }

    public Guid? AdContentId { get; set; }

    public Guid? RentalLocationId { get; set; }

    public virtual AdContent? AdContent { get; set; }

    public virtual ICollection<AppointmentHistory> AppointmentHistories { get; set; } = new List<AppointmentHistory>();

    public virtual RentalLocation? RentalLocation { get; set; }
}
