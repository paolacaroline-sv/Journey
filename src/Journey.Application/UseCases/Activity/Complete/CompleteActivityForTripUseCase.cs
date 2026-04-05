using Journey.Exception.ExceptionBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Enums;

namespace Journey.Application.UseCases.Activity.Complete
{
    public class CompleteActivityForTripUseCase
    {
        private readonly JourneyDbContext _dbContext;
        public CompleteActivityForTripUseCase(JourneyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Execute(Guid tripId, Guid activityId)
        {
            var activity = _dbContext
                .Activities
                .FirstOrDefault(act => act.Id == activityId && act.TripId == tripId)
                ?? throw new NotFoundException($"Activity with ID {activityId} not found for Trip with ID {tripId}.");

            if (activity.Status == ActivityStatus.Done)
            {
                throw new ErrorOnValidationException([$"Activity with ID {activityId} is already completed."]);
            }
            
            activity.Status = ActivityStatus.Done;
            _dbContext.SaveChanges();
            
        }
    }
}