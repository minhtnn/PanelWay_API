using System;
using System.Collections.Generic;

namespace PanelWay_Backend.Domain.Entities;

public partial class RentalLocation
{
    public Guid Id { get; set; }

    public string? Code { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public string? Address { get; set; }

    public string? PanelSize { get; set; }

    public string? Description { get; set; }

    public DateTime? PostDate { get; set; }

    public DateTime? AvailableDate { get; set; }

    public double? Price { get; set; }

    public string? Status { get; set; }

    public Guid? SpaceProviderId { get; set; }

    public Guid? ManagerId { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Account? Manager { get; set; }

    public virtual ICollection<RegulatoryApproval> RegulatoryApprovals { get; set; } = new List<RegulatoryApproval>();

    public virtual ICollection<RentalLocationImage> RentalLocationImages { get; set; } = new List<RentalLocationImage>();

    public virtual ICollection<RentalLocationPanelType> RentalLocationPanelTypes { get; set; } = new List<RentalLocationPanelType>();

    public virtual Account? SpaceProvider { get; set; }
}
