namespace PanelWay_Backend.API.Payload.Requests.RegulatoryLicenses;

public class UpdateRegulatoryLicenseRequest
{
    public Guid Id { get; set; }

    public string? ImgUrl { get; set; }

    public Guid RegulatoryApprovalId { get; set; }
}