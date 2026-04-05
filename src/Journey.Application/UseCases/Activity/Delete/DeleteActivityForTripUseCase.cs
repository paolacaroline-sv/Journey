using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Journey.Exception.ExceptionBase;
using Journey.Infrastructure;

namespace Journey.Application.UseCases.Activity.Delete
{
    public class DeleteActivityForTripUseCase
    {
        private readonly JourneyDbContext _dbContext;
        public DeleteActivityForTripUseCase(JourneyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Execute(Guid tripId, Guid activityId)
        {
            var activity = _dbContext
                .Activities
                .FirstOrDefault(act => act.Id == activityId && act.TripId == tripId)
                ?? throw new NotFoundException($"Activity with ID {activityId} not found for Trip with ID {tripId}.");

            _dbContext.Activities.Remove(activity);
            _dbContext.SaveChanges();
        }

    }
}