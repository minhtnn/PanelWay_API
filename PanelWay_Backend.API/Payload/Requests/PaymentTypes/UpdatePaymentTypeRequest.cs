namespace PanelWay_Backend.API.Payload.Requests.PaymentTypes;

public class UpdatePaymentTypeRequest
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImgUrl { get; set; }
}