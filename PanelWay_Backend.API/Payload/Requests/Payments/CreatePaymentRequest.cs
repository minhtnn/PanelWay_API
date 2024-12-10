namespace PanelWay_Backend.API.Payload.Requests.Payments;

public class CreatePaymentRequest
{
    public Guid Id { get; set; }

    public Guid PaymentTypeId { get; set; }

    public string? Details { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

}