namespace PanelWay_Backend.API.Services.Interfaces;

public interface IInfobipService
{
    Task<string> SendOtpAsync(string phoneNumber, string otpCode);
    
}