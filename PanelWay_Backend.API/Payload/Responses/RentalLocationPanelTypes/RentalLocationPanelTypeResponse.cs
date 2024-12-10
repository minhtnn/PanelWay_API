using PanelWay_Backend.API.Payload.Responses.PanelTypes;

namespace PanelWay_Backend.API.Payload.Responses.RentalLocationPanelTypes;

public class RentalLocationPanelTypeResponse
{
    public Guid? RentalLocationId { get; set; }

    public Guid? PanelTypeId { get; set; }
}