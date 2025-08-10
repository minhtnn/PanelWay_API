using System;
using System.Collections.Generic;

namespace PanelWay_Backend.Domain.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string? FullName { get; set; }

    public string? Gender { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public int? Age { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Status { get; set; }

    public bool? VerificationStatus { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
