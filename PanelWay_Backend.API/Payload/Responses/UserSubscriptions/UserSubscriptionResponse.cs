namespace PanelWay_Backend.API.Payload.Responses.UserSubscriptions;

public class UserSubscriptionResponse
{
    public Guid Id { get; set; }

    public Guid AccountId { get; set; }

    public Guid SubscriptionId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }
}