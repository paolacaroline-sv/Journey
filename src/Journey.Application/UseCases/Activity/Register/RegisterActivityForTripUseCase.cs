using Journey.Communication.Requests;
using Journey.Exception.ExceptionBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using FluentValidation.Results;
using Journey.Communication.Responses;


namespace Journey.Application.UseCases.Activity.Register
{
    public class RegisterActivityForTripUseCase
    {
        private readonly JourneyDbContext _dbContext;
        public RegisterActivityForTripUseCase(JourneyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ResponseActivityJson Execute(Guid tripId, RequestRegisterActivityJson request)
        {
            var trip = _dbContext.Trips.Include(t => t.Activities).FirstOrDefault(t => t.Id == tripId) ?? throw new NotFoundException($"Trip with ID {tripId} not found.");
            Validate(trip, request);

            var activity = new Infrastructure.Entities.Activity 
            {
                Name = request.Name,
                Date = request.Date
            };

            trip.Activities.Add(activity);
            _dbContext.Trips.Update(trip);
            _dbContext.SaveChanges();
            
            
            return new ResponseActivityJson
            {
                Id = activity.Id,
                Name = activity.Name,
                Date = activity.Date,
                Status = (Communication.Enums.ActivityStatus)activity.Status
            };
        }
        
        private void Validate(Trip trip, RequestRegisterActivityJson request)
        {
            var validator = new RegisterActivityForTripValidator();
            var result = validator.Validate(request);

            if (request.Date >= trip.StartDate && request.Date <= trip.EndDate)
            {
                result.Errors.Add(new ValidationFailure(nameof(request.Date), "The activity date must be within the trip's start and end dates."));
            }

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}