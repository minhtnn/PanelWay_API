using AutoMapper;
using PanelWay_Backend.Domain.Entities;
using PanelWay_Backend.Repository.Interfaces;

namespace PanelWay_Backend.API.Services;

public abstract class BaseService<T> where T : class
{
    protected IUnitOfWork<PanelWayDbContext> _unitOfWork;
    protected ILogger<T> _logger;
    protected IMapper _mapper;
    protected IHttpContextAccessor _httpContextAccessor;

    public BaseService(IUnitOfWork<PanelWayDbContext> unitOfWork, ILogger<T> logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }
}