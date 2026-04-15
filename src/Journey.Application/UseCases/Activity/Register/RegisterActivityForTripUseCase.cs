using Journey.Communication.Requests;
using Journey.Exception.ExceptionBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using FluentValidation.Results;
using Journey.Communication.Responses;
using AutoMapper;


namespace Journey.Application.UseCases.Activity.Register
{
    public class RegisterActivityForTripUseCase
    {
        private readonly JourneyDbContext _dbContext;
        private readonly IMapper _mapper;
        public RegisterActivityForTripUseCase(JourneyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ResponseActivityJson> Execute(Guid tripId, RequestRegisterActivityJson request)
        {
            var trip = await _dbContext.Trips.FirstOrDefaultAsync(t => t.Id == tripId) ?? throw new NotFoundException($"Trip with ID {tripId} not found.");
            Validate(trip, request);

            var activity = _mapper.Map<Infrastructure.Entities.Activity>(request);
            activity.TripId = tripId;            

            _dbContext.Activities.Add(activity);
            await _dbContext.SaveChangesAsync();            
            
            return _mapper.Map<ResponseActivityJson>(activity);
        }
        
        private void Validate(Trip trip, RequestRegisterActivityJson request)
        {
            var validator = new RegisterActivityForTripValidator();
            var result =  validator.Validate(request);

            var activityDate = request.Date.Date;
            var tripStartDate = trip.StartDate.Date;
            var tripEndDate = trip.EndDate.Date;

            if (activityDate < tripStartDate || activityDate > tripEndDate)
            {
                result.Errors.Add(new ValidationFailure("Date", "The activity date must be within the trip's start and end dates."));
            }

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}