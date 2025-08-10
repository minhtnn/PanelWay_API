namespace PanelWay_Backend.API.Payload.Responses.Accounts;

public class AccountResponse
{
    public Guid Id { get; set; }

    public string? AvatarUrl { get; set; }

    public string? Status { get; set; }

    public string? Role { get; set; }

    public int? IndividualPoint { get; set; }

    public Guid? UserId { get; set; }
    
    
    
    public string? FullName { get; set; }

    public string? Gender { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public int? Age { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UserStatus { get; set; }

    public bool? VerificationStatus { get; set; }
}