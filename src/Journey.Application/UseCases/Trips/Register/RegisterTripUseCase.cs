using System.Data.Common;
using FluentValidation;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripUseCase
    {
        private readonly JourneyDbContext _dbContext;
        private readonly ILogger<RegisterTripUseCase> _logger;

        public RegisterTripUseCase(JourneyDbContext dbContext, ILogger<RegisterTripUseCase> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public ResponseShortTripJson Execute(RequestRegisterTripJson request)
        {
            _logger.LogInformation("Starting trip registration process for trip: {TripName}", request.Name);
            Validate(request);
            _logger.LogInformation("Validation successful for trip: {TripName}", request.Name);
            var trip = new Trip
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            _logger.LogInformation("Attempting to save trip: {TripName} to the database", request.Name);
            try
            {
                _dbContext.Trips.Add(trip);
                _dbContext.SaveChanges();
                _logger.LogInformation("Trip: {TripName} successfully saved to the database with ID: {TripId}", trip.Name, trip.Id);
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "Error occurred while saving the trip to the database.");
                throw;
            }
            _logger.LogInformation("Trip registration process completed for trip: {TripName}", request.Name);
            return new ResponseShortTripJson
            {
                Name = trip.Name,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Id = trip.Id
            };
        }

        private void Validate(RequestRegisterTripJson request)
        {
            var validator = new RegisterTripValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for trip: {TripName}. Errors: {Errors}", request.Name, validationResult.Errors);
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }    
        }
    }
}