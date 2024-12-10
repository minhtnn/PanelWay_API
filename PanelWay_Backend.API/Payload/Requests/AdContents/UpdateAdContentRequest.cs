namespace PanelWay_Backend.API.Payload.Requests.AdContents;

public class UpdateAdContentRequest
{
    public Guid Id { get; set; }

    public string? Code { get; set; }

    public string? Type { get; set; }

    public string? Content { get; set; }

    public string? Size { get; set; }

    public string? ImgUrl { get; set; }

    public Guid AdvertisingClientId { get; private set; }
}