using System;
using System.Collections.Generic;

namespace PanelWay_Backend.Domain.Entities;

public partial class Subscription
{
    public Guid Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public double? Price { get; set; }

    public string? Features { get; set; }

    public string? Status { get; set; }

    public int? Duration { get; set; }

    public int? Priority { get; set; }

    public virtual ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
}
