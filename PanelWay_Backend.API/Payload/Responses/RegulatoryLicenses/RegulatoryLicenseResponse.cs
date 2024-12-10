namespace PanelWay_Backend.API.Payload.Responses.RegulatoryLicenses;

public class RegulatoryLicenseResponse
{
    public Guid Id { get; set; }

    public string? ImgUrl { get; set; }

    public Guid RegulatoryApprovalId { get; set; }
}