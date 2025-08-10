using PanelWay_Backend.API.Payload.Responses.Accounts;

namespace PanelWay_Backend.API.Payload.Responses.Authentication;

public class AuthenticationResponse
{
    public AccountResponse? AccountResponse { get; set; }
    public string? JwtToken { get; set; }
}