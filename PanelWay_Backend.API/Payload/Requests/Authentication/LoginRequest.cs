using PanelWay_Backend.API.Enums;

namespace PanelWay_Backend.API.Payload.Requests.Authentication;

public class LoginRequest
{
    public string? PhoneNumber { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; } = nameof(RoleEnum.AdvertisingClient);
}