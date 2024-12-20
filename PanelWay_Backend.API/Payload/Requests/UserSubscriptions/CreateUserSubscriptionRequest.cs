using PanelWay_Backend.API.Enums;

namespace PanelWay_Backend.API.Payload.Requests.UserSubscriptions;

public class CreateUserSubscriptionRequest
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public Guid AccountId { get; set; }

    public Guid SubscriptionId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; private set; }

    public string? Status { get; } = nameof(UserSubcriptionStatusEnum.Active);

    public void GetNewPrimaryKey()
    {
        Id = Guid.NewGuid();
    }

    public void SetEndDate(int period)
    {
        DateTime endDate = StartDate;
        var dateTime = endDate.AddDays(period);
        EndDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
    }
}