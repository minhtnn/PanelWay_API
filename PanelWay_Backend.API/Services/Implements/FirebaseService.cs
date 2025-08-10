using AutoMapper;
using Google.Cloud.Storage.V1;
using PanelWay_Backend.API.Configurations;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Repository.Interfaces;

namespace PanelWay_Backend.API.Services.Implements;

public class FirebaseService : BaseService<FirebaseService>, IFirebaseService
{
    private readonly StorageClient _storageClient;
    public FirebaseService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<FirebaseService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, StorageClient storageClient) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
        _storageClient = storageClient;
    }
    
    public async Task<string> Upload(IFormFile file, string? folderName)
    {
        if (file == null || file.Length == 0) throw new BadHttpRequestException(MessageConstant.Firebase.NoFileUpload);
        var objectName = string.IsNullOrEmpty(folderName)?
            (Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)):
            ($"{folderName}/{Guid.NewGuid().ToString() + Path.GetExtension(file.FileName)}");
        // Upload file to Firebase Storage
        using (var stream = file.OpenReadStream())
        {
            await _storageClient.UploadObjectAsync(FirebaseConfig.Bucket, objectName, null, stream);
        }
        // Create a public URL for the uploaded file
        var downloadUrl = FirebaseConfig.UploadFileUrl(objectName);
        // Return the URL of the uploaded file
        return objectName;
    }

    public async Task<string> Download(string? fileName, string? folderName)
    {
        var memoryStream = new MemoryStream();
        var fileDownload = string.IsNullOrEmpty(folderName) ? fileName : ($"{folderName}/{fileName}");
        await _storageClient.DownloadObjectAsync(FirebaseConfig.Bucket, fileDownload, memoryStream);
        memoryStream.Position = 0;
        var base64String = Convert.ToBase64String(memoryStream.ToArray());
        return base64String;
    }
}