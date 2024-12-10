namespace PanelWay_Backend.API.Payload.Requests.RegulatoryApproval;

public class CreateRegulatoryApprovalRequest
{
    public Guid Id { get; set; }

    public string? PermitNumber { get; set; }

    public string? IssueBy { get; set; }

    public DateTime? IssueDate { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public Guid RentalLocationId { get; set; }
}