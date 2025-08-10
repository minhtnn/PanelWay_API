namespace PanelWay_Backend.API.Payload.Responses.Accounts;

public class ManageAccountResponse
{
    public Guid Id { get; set; }

    public string? AvatarUrl { get; set; }

    public string? Status { get; set; }

    public string? Role { get; set; }

    public int? IndividualPoint { get; set; }

    public Guid? UserId { get; set; }
    
    
}