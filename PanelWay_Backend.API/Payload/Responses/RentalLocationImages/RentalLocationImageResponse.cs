namespace PanelWay_Backend.API.Payload.Responses.RentalLocationImages;

public class RentalLocationImageResponse
{
    public Guid Id { get; set; }

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }

    public bool? IsDaylight { get; set; }

    public Guid? RentalLocationId { get; set; }
}