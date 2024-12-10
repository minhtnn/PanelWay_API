namespace PanelWay_Backend.API.Payload.Requests.Appointments;

public class CreateAppointmentRequest
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Code { get; set; }

    public DateTime? BookingDate { get; set; }

    public string? Place { get; set; }

    public int? Priority { get; set; }

    public string? Status { get; set; }

    public Guid AdContentId { get; set; }

    public Guid RentalLocationId { get; set; }

    public void GetNewId()
    {
        Id = Guid.NewGuid();
    }
}