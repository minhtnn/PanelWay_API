namespace PanelWay_Backend.API.Payload.Requests.RentalLocationPanelTypes;

public class CreateRentalLocationPanelTypeRequest
{
    public Guid Id { get;} = Guid.NewGuid();
    public required Guid RentalLocationId { get; set; }

    public required Guid PanelTypeId { get; set; }
}