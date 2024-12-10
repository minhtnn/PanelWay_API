namespace PanelWay_Backend.API.Constants;

public class FirebaseConstant
{
    public const string Bucket = "capstoneproject-b5349.appspot.com";
    public const string CredentialFilePath = "capstoneproject-b5349-33507b187d3a.json";
    public static string UploadFileUrl(string objectName)
    {
        return "https://storage.googleapis.com/" + Bucket + "/" + objectName;
    }
}