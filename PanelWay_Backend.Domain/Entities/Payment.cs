using System;
using System.Collections.Generic;

namespace PanelWay_Backend.Domain.Entities;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid? PaymentTypeId { get; set; }

    public long? PayOsOrderCode { get; set; }

    public string? Details { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual PaymentType? PaymentType { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
