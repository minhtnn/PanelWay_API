using System.Text.Json;
using AutoMapper;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Repository.Interfaces;
using RestSharp;

namespace PanelWay_Backend.API.Services.Implements;

public class InfobipService : BaseService<InfobipService>,IInfobipService
{
    private readonly string _apiKey;
    private readonly string _baseUrl = "https://nmv12e.api.infobip.com"; 
    private readonly string _sender; 

    public InfobipService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<InfobipService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
        _apiKey = configuration["Infobip:ApiKey"];
        _sender = configuration["Infobip:Sender"];
    }
    public async Task<string> SendOtpAsync(string phoneNumber, string otpCode)
    {
        if (string.IsNullOrEmpty(_baseUrl))
        {
            throw new ArgumentException("Base URL của Infobip không được để trống.");
        }
        var client = new RestClient(_baseUrl);
        var request = new RestRequest("sms/2/text/advanced", Method.Post);
        request.AddHeader("Authorization", $"App {_apiKey}");
        request.AddHeader("Content-Type", "application/json");

        var payload = new
        {
            messages = new[]
            {
                new
                {
                    from = _sender,
                    destinations = new[] { new { to = phoneNumber } },
                    text = $"Mã OTP của bạn là: {otpCode}. Vui lòng không chia sẻ mã này với bất kỳ ai."
                }
            }
        };

        request.AddParameter("application/json", JsonSerializer.Serialize(payload), ParameterType.RequestBody);
        var response = await client.ExecuteAsync(request);

        return response.Content;
    }
}