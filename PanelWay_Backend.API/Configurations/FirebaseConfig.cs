namespace PanelWay_Backend.API.Configurations;

public static class FirebaseConfig
{
    public static string? Bucket { get; set; }
    public static string? CredentialFilePath { get; set; }
    public static string UploadFileUrl(string objectName)
    {
        return "https://storage.googleapis.com/" + Bucket + "/" + objectName;
    }
    public static void GetFirebase()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        var bucket = configuration.GetValue<string>("FirebaseStorage:Bucket");
        var credentialFilePath = configuration.GetValue<string>("FirebaseStorage:CredentialFilePath");
        FirebaseConfig.Bucket = bucket;
        FirebaseConfig.CredentialFilePath = credentialFilePath;
    }
}