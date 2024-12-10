namespace PanelWay_Backend.API.Payload.Requests.PanelTypes;

public class CreatePanelTypeRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public string? Description { get; set; }

    public void GetNewId()
    {
        Id = Guid.NewGuid();
    }
}