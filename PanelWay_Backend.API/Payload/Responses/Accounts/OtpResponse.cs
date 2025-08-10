namespace PanelWay_Backend.API.Payload.Responses.Accounts;

public class OtpResponse
{
    public int Status { get; set; }
    public String Message { get; set; }
    public String OtpCode { get; set; }
}