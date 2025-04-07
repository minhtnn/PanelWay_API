using Net.payOS.Types;

namespace PanelWay_Backend.API.Payload.Requests.PayOS;

public class CreatePayOSRequest
{
    public int OrderCode { get; private set; } = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
    public int Amount { get; set; }
    public string Description { get; set; }
    public List<ItemData> Items { get; set; }
    public string ReturnUrl { get; set; }
    public string CancelUrl{ get; set; }
    
    public string SubcriptionId { get; set; }
    public string AccountId { get; set; }
}