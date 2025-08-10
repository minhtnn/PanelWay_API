namespace PanelWay_Backend.API.Payload.Requests.Authentication;

public class SignUpRequest
{
    public int Age { get; set; }
    public string? FullName { get; set; }

    public string? Gender { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }
}