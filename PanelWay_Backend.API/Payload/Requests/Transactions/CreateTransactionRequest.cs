namespace PanelWay_Backend.API.Payload.Requests.Transactions;

public class CreateTransactionRequest
{
    public Guid Id { get; set; }

    public Guid SubscriptionId { get; set; }

    public Guid UserSubscriptionId { get; set; }

    public Guid PaymentId { get; set; }

    public double? Amount { get; set; }

    public DateTime? TransactionDate { get; set; }
    public int Duration { get; set; }

    public string? Status { get; set; }
}