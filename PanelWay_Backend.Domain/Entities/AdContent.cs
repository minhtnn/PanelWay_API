using System;
using System.Collections.Generic;

namespace PanelWay_Backend.Domain.Entities;

public partial class AdContent
{
    public Guid Id { get; set; }

    public string? Code { get; set; }

    public string? Type { get; set; }

    public string? Content { get; set; }

    public string? Size { get; set; }

    public string? ImgUrl { get; set; }

    public Guid? AdvertisingClientId { get; set; }

    public virtual Account? AdvertisingClient { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
