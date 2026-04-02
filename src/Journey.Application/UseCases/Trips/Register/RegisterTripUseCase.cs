using System.Data.Common;
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
            Validate(request);

            var trip = new Trip
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            try
            {
                _dbContext.Trips.Add(trip);
                _dbContext.SaveChanges();
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "Error occurred while saving the trip to the database.");
                throw;
            }

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
            ArgumentNullException.ThrowIfNull(request);
            var todayUtc = DateTime.UtcNow.Date;
            var startDate = request.StartDate.Date;
            var endDate = request.EndDate.Date;

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ErrorOnValidationException("Name is required.");

            if (request.StartDate == default)
                throw new ErrorOnValidationException("Start date is required.");

            if (request.EndDate == default)
                throw new ErrorOnValidationException("End date is required.");

            if (startDate < todayUtc)
                throw new ErrorOnValidationException("Start date cannot be in the past.");

            if (endDate < startDate)
                throw new ErrorOnValidationException("End date cannot be before start date.");
        }
    }
}