using System.Text;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PanelWay_Backend.API.Configurations;
using PanelWay_Backend.API.Constants;
using PanelWay_Backend.API.Services.BackgroundJobs;
using PanelWay_Backend.API.Services.Implements;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Domain.Paginate;
using PanelWay_Backend.Repository.Implement;
using PanelWay_Backend.Repository.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PanelWay_Backend.API.Extensions;

public static class DependencyServices
{
    public static IServiceCollection AddMemoryCacheConfig(this IServiceCollection services)
    {
        services.AddMemoryCache();
        return services;
    }
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork<PanelWayDbContext>, UnitOfWork<PanelWayDbContext>>();
        return services;
    }
    public static IServiceCollection AddMyCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
           options.AddPolicy(name: CorsConfig.PolicyName,
               policy =>
               { 
                   policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
               });
        });
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
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IAppointmentHistoryService, AppointmentHistoryService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IFirebaseService, FirebaseService>();
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
        services.AddScoped<IInfobipService, InfobipService>();
        services.AddScoped<IRentalLocationImageService, RentalLocationImageService>();
        return services;
    }
    public static IServiceCollection AddJwtValidation(this IServiceCollection services)
    {
        var secretKey = JwtConfig.SecretKey;
        var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.SaveToken = true;
            opt.RequireHttpsMetadata = false;
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                //Tự cấp token
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),
                ClockSkew = TimeSpan.Zero
            };
        });
        return services;
    }
    public static IServiceCollection AddConfigSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = SystemConstant.Name,
                Version = "v1"
            });
            options.MapType<TimeOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "time",
                Example = OpenApiAnyFactory.CreateFromJson("\"13:45:42.0000000\"")
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
        return services;
    }
    public static IServiceCollection AddFirebase(this IServiceCollection services, string credentialFilePath)
    {
        FirebaseConfig.GetFirebase();
        // Initialize FirebaseApp
        services.AddSingleton<FirebaseApp>(provider =>
        {
            var appOptions = new AppOptions()
            {
                Credential = GoogleCredential.FromFile(credentialFilePath)
            };

            return FirebaseApp.Create(appOptions);
        });
        // Register StorageClient as a singleton service
        services.AddSingleton(provider => StorageClient.Create());
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