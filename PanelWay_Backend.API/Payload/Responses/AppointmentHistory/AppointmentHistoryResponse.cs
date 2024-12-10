namespace PanelWay_Backend.API.Payload.Responses.AppointmentHistory;

public class AppointmentHistoryResponse
{
    public Guid Id { get; set; }

    public DateTime? IssueDate { get; set; }

    public string? FromStatus { get; set; }

    public string? ToStatus { get; set; }

    public Guid AdvertisingClientId { get; set; }

    public Guid SpaceProviderId { get; set; }

    public Guid AppointmentId { get; set; }
}