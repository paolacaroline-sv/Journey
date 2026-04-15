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
        CreateMap<ActivityStatusInfra, ActivityStatusComm>();
        CreateMap<Activity, ResponseActivityJson>();
        CreateMap<RequestRegisterActivityJson, Activity>();
    }
}