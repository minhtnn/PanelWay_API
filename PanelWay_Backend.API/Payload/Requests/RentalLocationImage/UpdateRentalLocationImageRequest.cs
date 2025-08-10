namespace PanelWay_Backend.API.Payload.Requests.RentalLocationImage;

public class UpdateRentalLocationImageRequest
{
    public Guid Id { get; set; }

    public string? ImageUrl { get; set; }

    public string? Description { get; set; }

    public bool? IsDaylight { get; set; }

    public Guid? RentalLocationId { get; set; }
}