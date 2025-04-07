namespace PanelWay_Backend.API.Configurations;

public static class CorsConfig
{
    public const string PolicyName = "MyDefaultPolicy";

    public static void AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(PolicyName, builder =>
            {
                builder.WithOrigins("http://localhost:5173")
                       .AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials(); // Quan trọng khi frontend dùng withCredentials
            });
        });
    }
}
