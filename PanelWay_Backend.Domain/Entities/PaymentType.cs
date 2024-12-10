using System;
using System.Collections.Generic;

namespace PanelWay_Backend.Domain.Entities;

public partial class PaymentType
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? ImgUrl { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
