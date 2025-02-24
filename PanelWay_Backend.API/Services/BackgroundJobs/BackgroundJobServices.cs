using PanelWay_Backend.API.Enums;
using PanelWay_Backend.API.Services.Implements;
using PanelWay_Backend.API.Services.Interfaces;
using PanelWay_Backend.Domain.Entities;

namespace PanelWay_Backend.API.Services.BackgroundJobs;

public class BackgroundJobServices : BackgroundService
{
    private readonly ILogger<BackgroundJobServices> _logger;
    private readonly IServiceProvider _serviceProvider;
    public BackgroundJobServices(ILogger<BackgroundJobServices> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var appointmentService = scope.ServiceProvider.GetRequiredService<IAppointmentService>();
                        var nearestBookingDate = await GetNearestBookingDate(appointmentService);

                        if (nearestBookingDate.HasValue)
                        {
                            DateTime now = DateTime.UtcNow;
                            if (nearestBookingDate > now)
                            {
                                TimeSpan timeToWait = (nearestBookingDate - now).Value;
                                await Task.Delay(timeToWait, stoppingToken);
                            }
                            await CancelAppointmentsByBookingDate(appointmentService, nearestBookingDate);
                        }
                        else
                        {
                            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Dừng vì token hủy, không cần log lỗi.
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred in BackgroundJobServices.");
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "BackgroundJobServices encountered a fatal error and will terminate.");
        }
    }


    private async Task<DateTime?> GetNearestBookingDate(IAppointmentService service)
    {
        var nearestBookingDate = await service.GetIsNearestBookingDate();
        return nearestBookingDate;
    }

    private async Task<bool> CancelAppointmentsByBookingDate(IAppointmentService service, DateTime? bookingDate)
    {
        if (bookingDate != null)
        {
            var appointments = await service.GetAppointmentsByBookingDate(bookingDate);
            if (appointments!= null && appointments.Count > 0)
            {
                foreach (var appointment in appointments)
                {
                    appointment.Status = nameof(AppointmentStatusEnum.Expired);
                }
                return await service.UpdateAppointments(appointments);
            }
        }
        return false;
    }
}