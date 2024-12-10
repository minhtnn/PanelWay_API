namespace PanelWay_Backend.API.Payload.Responses.Subscriptions;

public class SubscriptionResponse
{
    public Guid Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public double? Price { get; set; }

    public string? Features { get; set; }

    public string? Status { get; set; }
}