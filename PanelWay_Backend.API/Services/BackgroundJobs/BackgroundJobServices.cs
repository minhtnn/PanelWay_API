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
                using (var scope = _serviceProvider.CreateScope())
                {
                    var appointmentService = scope.ServiceProvider.GetRequiredService<IAppointmentService>();
                    //Get the appointment's nearest booking datetime
                    var nearestBookingDate = await GetNearestBookingDate(appointmentService);
                    //Get current datetome
                    DateTime now = DateTime.UtcNow;
                    TimeSpan timeToWait = (TimeSpan)(nearestBookingDate - now)!;
                    await Task.Delay(timeToWait, stoppingToken);
                    await CancelAppointmentsByBookingDate(appointmentService, nearestBookingDate);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
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