using System;
using System.Collections.Generic;

namespace PanelWay_Backend.Domain.Entities;

public partial class RegulatoryLicense
{
    public Guid Id { get; set; }

    public string? ImgUrl { get; set; }

    public Guid? RegulatoryApprovalId { get; set; }

    public virtual RegulatoryApproval? RegulatoryApproval { get; set; }
}
