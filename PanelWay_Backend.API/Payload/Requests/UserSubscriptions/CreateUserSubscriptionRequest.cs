namespace PanelWay_Backend.API.Payload.Requests.UserSubscriptions;

public class CreateUserSubscriptionRequest
{
    public Guid Id { get; set; }

    public Guid AccountId { get; set; }

    public Guid SubscriptionId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }
}