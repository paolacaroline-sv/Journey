using AutoMapper;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Infrastructure.Entities;

namespace Journey.Application.Mappers;

public class TripProfile : Profile
{
    public TripProfile()
    {
        CreateMap<IEnumerable<Trip>, ResponseTripsJson>()
            .ForMember(dest => dest.Trips, opt => opt.MapFrom(src => src));

        CreateMap<Trip, ResponseShortTripJson>();
        CreateMap<Trip, ResponseTripJson>();
        CreateMap<RequestRegisterTripJson, Trip>();
    }
}