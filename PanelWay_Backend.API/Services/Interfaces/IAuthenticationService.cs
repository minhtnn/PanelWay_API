using PanelWay_Backend.API.Payload.Requests.Authentication;
using PanelWay_Backend.API.Payload.Requests.Firebase;
using PanelWay_Backend.API.Payload.Responses;
using PanelWay_Backend.API.Payload.Responses.Accounts;
using PanelWay_Backend.API.Payload.Responses.Authentication;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface IAuthenticationService
{ 
    Task<AuthenticationResponse?> Login(LoginRequest request);
    Task<AuthenticationResponse?> SignUpForCustomer(SignUpRequest request);
    Task<bool?> ChangePasswordForCustomer(ChangePasswordRequest request);
    Task<DataReponse> GetUser(VerifyTokenRequest request);
    Task<DataReponse> SaveNewUser(AuthenticationRequest request);
}