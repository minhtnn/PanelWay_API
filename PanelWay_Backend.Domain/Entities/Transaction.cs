using System;
using System.Collections.Generic;

namespace PanelWay_Backend.Domain.Entities;

public partial class Transaction
{
    public Guid Id { get; set; }

    public Guid? SubscriptionId { get; set; }

    public Guid? UserSubscriptionId { get; set; }

    public Guid? PaymentId { get; set; }

    public double? Amount { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? Status { get; set; }

    public virtual Payment? Payment { get; set; }

    public virtual Subscription? Subscription { get; set; }

    public virtual UserSubscription? UserSubscription { get; set; }
}
