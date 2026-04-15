using AutoMapper;
using Journey.Application.Mappers;
using Journey.Application.UseCases.Activity.Complete;
using Journey.Application.UseCases.Activity.Delete;
using Journey.Application.UseCases.Activity.Register;
using Journey.Application.UseCases.Trips.Delete;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetTripById;
using Journey.Application.UseCases.Trips.Register;
using Microsoft.Extensions.DependencyInjection;


namespace Journey.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<RegisterTripUseCase>();
        services.AddScoped<GetAllTripsUseCase>();
        services.AddScoped<GetTripByIdUseCase>();
        services.AddScoped<DeleteTripByIdUseCase>();
        services.AddScoped<RegisterActivityForTripUseCase>();
        services.AddScoped<CompleteActivityForTripUseCase>();
        services.AddScoped<DeleteActivityForTripUseCase>();
        
        services.AddAutoMapper(typeof(TripProfile), typeof(ActivityProfile));
        
        return services;
    }
}