using System;
using System.Collections.Generic;

namespace PanelWay_Backend.Domain.Entities;

public partial class RegulatoryApproval
{
    public Guid Id { get; set; }

    public string? PermitNumber { get; set; }

    public string? IssueBy { get; set; }

    public DateTime? IssueDate { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public Guid? RentalLocationId { get; set; }

    public virtual ICollection<RegulatoryLicense> RegulatoryLicenses { get; set; } = new List<RegulatoryLicense>();

    public virtual RentalLocation? RentalLocation { get; set; }
}
