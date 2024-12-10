namespace PanelWay_Backend.API.Payload.Requests.PanelTypes;

public class UpdatePanelTypeRequest
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }
}