namespace PanelWay_Backend.API.Payload.Requests.RentalLocations;

public class UpdateRentalLocationRequest
{
    public double? LocationX { get; set; }

    public double? LocationY { get; set; }

    public string? Address { get; set; }

    public string? PanelSize { get; set; }

    public string? Description { get; set; }

    public DateTime? AvailableDate { get; set; }

    public double? Price { get; set; }

    public string? Status { get; set; }

    public Guid SpaceProviderId { get; set; }

    public Guid ManagerId { get; set; }
}