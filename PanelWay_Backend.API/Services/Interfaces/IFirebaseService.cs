namespace PanelWay_Backend.API.Services.Interfaces;

public interface IFirebaseService
{
    Task<string> Upload(IFormFile file, string? folderName);
    Task<string> Download(string? fileName, string? folderName);
}