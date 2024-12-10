using PanelWay_Backend.API.Payload.Requests.AdContents;
using PanelWay_Backend.API.Payload.Responses.AdContents;

namespace PanelWay_Backend.API.Services.Interfaces;

public interface IAdContentService
{
    Task<AdContentResponse> GetAdContentById(Guid id);
    Task<ICollection<AdContentResponse>> GetAdContentByAdvertisingClientId(Guid advertisingClientId);
    Task<AdContentResponse?> CreateNewAdContent(CreateAdContentRequest request);
    Task<AdContentResponse?> UpdateAdContent(UpdateAdContentRequest request);
}