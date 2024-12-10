using System;
using System.Collections.Generic;

namespace PanelWay_Backend.Domain.Entities;

public partial class RentalLocationPanelType
{
    public Guid Id { get; set; }

    public Guid? RentalLocationId { get; set; }

    public Guid? PanelTypeId { get; set; }

    public virtual PanelType? PanelType { get; set; }

    public virtual RentalLocation? RentalLocation { get; set; }
}
