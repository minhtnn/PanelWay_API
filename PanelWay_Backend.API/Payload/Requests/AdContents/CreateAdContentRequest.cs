namespace PanelWay_Backend.API.Payload.Requests.AdContents;

public class CreateAdContentRequest
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public required string Code { get; set; }

    public string? Type { get; set; }

    public string? Content { get; set; }

    public string? Size { get; set; }

    public string? ImgUrl { get; set; }

    public Guid AdvertisingClientId { get; set; }

    public void NewGuid()
    {
        Id = Guid.NewGuid();
    }
}