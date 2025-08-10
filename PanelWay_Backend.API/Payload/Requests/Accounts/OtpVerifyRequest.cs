namespace PanelWay_Backend.API.Payload.Requests.Accounts;

public class OtpVerifyRequest
{
    public string PhoneNumber { get; set; }
    public string OtpCode { get; set; }
}