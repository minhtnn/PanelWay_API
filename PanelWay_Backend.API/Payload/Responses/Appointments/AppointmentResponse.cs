namespace PanelWay_Backend.API.Payload.Responses.Appointments;

public class AppointmentResponse
{
    public Guid Id { get; set; }

    public string? Code { get; set; }

    public DateTime? BookingDate { get; set; }

    public string? Place { get; set; }

    public int? Priority { get; set; }

    public string? Status { get; set; }

    public Guid AdContentId { get; set; }

    public Guid RentalLocationId { get; set; }
}