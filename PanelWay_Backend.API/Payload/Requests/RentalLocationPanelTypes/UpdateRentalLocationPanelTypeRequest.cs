namespace PanelWay_Backend.API.Payload.Requests.RentalLocationPanelTypes;

public class UpdateRentalLocationPanelTypeRequest
{
    public Guid RentalLocationId { get; set; }

    public Guid PanelTypeId { get; set; }
}