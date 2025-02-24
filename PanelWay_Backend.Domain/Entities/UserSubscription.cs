using System;
using System.Collections.Generic;

namespace PanelWay_Backend.Domain.Entities;

public partial class UserSubscription
{
    public Guid Id { get; set; }

    public Guid? AccountId { get; set; }

    public Guid? SubscriptionId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Subscription? Subscription { get; set; }
}
