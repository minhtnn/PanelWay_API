using Microsoft.OpenApi.Models;
using PanelWay_Backend.API.Configurations;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Extensions;

try
{
    var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddMyCors();
    builder.Services.AddDatabase();
    builder.Services.AddUnitOfWork();
    builder.Services.AddControllers();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddServices(builder.Configuration);
    builder.Services.AddMemoryCacheConfig();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddAutoMapperConfig(builder.Configuration);
    builder.Services.AddFirebase(FirebaseConfig.CredentialFilePath!);
    builder.Services.AddConfigSwagger();
    builder.Services.AddJwtValidation();
    builder.Services.AddBackgroundJobService();
    var app = builder.Build();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment() || app.Environment.IsProduction() || app.Environment.IsStaging())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseRouting();
    app.UseCors(CorsConfig.PolicyName);
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
    app.Run();
}
catch(Exception e)
{
    Console.WriteLine(e);
    throw;
}

