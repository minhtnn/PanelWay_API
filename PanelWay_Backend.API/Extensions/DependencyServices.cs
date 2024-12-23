using Microsoft.EntityFrameworkCore;
using PanelWay_Backend.API.Services.BackgroundJobs;
using PanelWay_Backend.API.Services.Implements;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Domain.Paginate;
using PanelWay_Backend.Repository.Implement;
using PanelWay_Backend.Repository.Interfaces;

namespace PanelWay_Backend.API.Extensions;

public static class DependencyServices
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork<PanelWayDbContext>, UnitOfWork<PanelWayDbContext>>();
        return services;
    }
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        services.AddDbContext<PanelWayDbContext>(options => options.UseSqlServer(CreateConnectionString(configuration)));
        return services;
    }
    private static string CreateConnectionString(IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("ConnectionStrings:PanelWaySystemDatabase");
        return connectionString;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAdContentService, AdContentService>();
        services.AddScoped<IAppointmentHistoryService, AppointmentHistoryService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IPanelTypeService, PanelTypeService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IPaymentTypeService, PaymentTypeService>();
        services.AddScoped<IPayOSService, PayOSService>();
        services.AddScoped<IRegulatoryApprovalService, RegulatoryApprovalService>();
        services.AddScoped<IRegulatoryLicenseService, RegulatoryLicenseService>();
        services.AddScoped<IRentalLocationService, RentalLocationService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();
        services.AddScoped<IRentalLocationPanelTypeService, RentalLocationPanelTypeService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserSubscriptionService, UserSubscriptionService>();
        return services;
    }

    public static IServiceCollection AddAutoMapperConfig(this IServiceCollection services, IConfiguration config)
    {
        services.AddAutoMapper(typeof(PaginateMapper));
        return services;
    }
    public static IServiceCollection AddBackgroundJobService(this IServiceCollection services)
    {
        services.AddHostedService<BackgroundJobServices>();
        return services;
    }
}