namespace PanelWay_Backend.API.Payload.Requests.Users;

public class CreateUserRequest
{
    public Guid Id { get; set; }

    public string? FullName { get; set; }

    public string? Gender { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Status { get; set; }

    public bool? VerificationStatus { get; set; }
}