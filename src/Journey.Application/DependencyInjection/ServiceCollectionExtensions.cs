using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetTripById;
using Journey.Application.UseCases.Trips.Register;

namespace Journey.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<RegisterTripUseCase>();
        services.AddScoped<GetAllTripsUseCase>();
        services.AddScoped<GetTripByIdUseCase>();
        
        return services;
    }
}