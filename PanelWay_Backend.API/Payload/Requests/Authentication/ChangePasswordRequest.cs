namespace PanelWay_Backend.API.Payload.Requests.Authentication;

public class ChangePasswordRequest
{
    public string? Email { get; set; }
    public string? OldPassword { get; set; }
    public required string NewPassword { get; set; }
}