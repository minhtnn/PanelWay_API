using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Repository.Interfaces;
using RestSharp;

namespace PanelWay_Backend.API.Services.Implements;

public class InfobipService : BaseService<InfobipService>,IInfobipService
{
    private readonly string _apiKey;
    private readonly string _baseUrl = "https://ypjevp.api.infobip.com"; 
    private readonly string _sender; 
    private readonly IMemoryCache _cache;
    public InfobipService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<InfobipService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IMemoryCache cache) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
        _cache = cache;
        _apiKey = configuration["Infobip:ApiKey"];
        _sender = configuration["Infobip:Sender"];
    }
    public async Task<string> SendOtpAsync(string phoneNumber, string otpCode)
    {
        string cacheKey = $"LastSent_{phoneNumber}";
        if (_cache.TryGetValue(cacheKey, out DateTime lastSentTime))
        {
            var timeSinceLastSent = DateTime.UtcNow - lastSentTime;
            if (timeSinceLastSent.TotalSeconds < 30)
            {
                return "Vui lòng đợi 30 giây trước khi gửi lại OTP.";
            }
        }

        // Kiểm tra URL
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

        // Nếu gửi thành công, lưu timestamp vào cache
        if (response.IsSuccessful)
        {
            _cache.Set(cacheKey, DateTime.UtcNow, TimeSpan.FromSeconds(30)); // Cache sẽ hết hạn sau 30s
        }

        return response.Content;
    }
}
