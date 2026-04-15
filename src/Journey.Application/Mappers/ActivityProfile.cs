using AutoMapper;
using Journey.Communication.Enums;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Infrastructure.Entities;
using ActivityStatusInfra = Journey.Infrastructure.Enums.ActivityStatus;
using ActivityStatusComm = Journey.Communication.Enums.ActivityStatus;

namespace Journey.Application.Mappers;

public class ActivityProfile : Profile
{
    public ActivityProfile()
    {
        CreateMap<Activity, ResponseActivityJson>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        CreateMap<RequestRegisterActivityJson, Activity>();
        CreateMap<ActivityStatusInfra, ActivityStatusComm>();
    }
}