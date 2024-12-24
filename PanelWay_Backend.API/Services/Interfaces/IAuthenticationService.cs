using PanelWay_Backend.API.Payload.Requests.Authentication;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface IAuthenticationService
{ 
    Task<string?> Login(LoginRequest request);
    Task<string?> SignUpForCustomer(SignUpRequest request);
    Task<bool> ChangePasswordForCustomer(ChangePasswordRequest request);
}