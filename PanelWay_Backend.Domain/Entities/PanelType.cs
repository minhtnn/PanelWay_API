using System;
using System.Collections.Generic;

namespace PanelWay_Backend.Domain.Entities;

public partial class PanelType
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<RentalLocationPanelType> RentalLocationPanelTypes { get; set; } = new List<RentalLocationPanelType>();
}
