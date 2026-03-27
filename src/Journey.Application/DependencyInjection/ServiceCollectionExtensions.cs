using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetById;
using Journey.Application.UseCases.Trips.Register;
using Microsoft.Extensions.DependencyInjection;

namespace Journey.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<RegisterTripUseCase>();
        services.AddScoped<GetAllTripsUseCase>();
        services.AddScoped<GetById>();

        return services;
    }
}