using System;
using System.Collections.Generic;

namespace PanelWay_Backend.Domain.Entities;

public partial class RentalLocationImage
{
    public Guid Id { get; set; }

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }

    public bool? IsDaylight { get; set; }

    public Guid? RentalLocationId { get; set; }

    public virtual RentalLocation? RentalLocation { get; set; }
}
