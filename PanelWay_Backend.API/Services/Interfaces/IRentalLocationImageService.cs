using PanelWay_Backend.API.Payload.Requests.RentalLocationImage;
using PanelWay_Backend.API.Payload.Responses.RentalLocationImages;
namespace PanelWay_Backend.API.Services.Interfaces;

public interface IRentalLocationImageService
{
    Task<ICollection<RentalLocationImageResponse>> GetAllImagesByRentalLocationId(Guid id);
    Task<RentalLocationImageResponse> AddImageByRentalLocationId(CreateRentalLocationImageRequest request);
    Task<RentalLocationImageResponse> UpdateImageByRentalLocationId(UpdateRentalLocationImageRequest request);

}