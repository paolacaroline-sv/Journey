using Journey.Communication.Responses;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.GetTripById
{

    public class GetTripByIdUseCase
    {
        private readonly JourneyDbContext _dbContext;
        private readonly ILogger<GetTripByIdUseCase> _logger;
        public GetTripByIdUseCase(JourneyDbContext dbContext, ILogger<GetTripByIdUseCase> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public ResponseTripJson Execute(Guid id)
        {
            var trip = _dbContext.Trips
            .Include(trip => trip.Activities)
            .FirstOrDefault(trip => trip.Id == id);

            if (trip == null)
            {
                _logger.LogWarning("Trip with ID {Id} not found.", id);
                return null!; 
            }

            _logger.LogInformation("Retrieved trip with ID {Id} from the database.", id);

            return new ResponseTripJson
            {
                Id = trip.Id,
                Name = trip.Name,
                StartDate = trip.StartDate,
                EndDate = trip.EndDate,
                Activities = trip.Activities.Select(activity => new ResponseActivityJson
                {
                    Id = activity.Id,
                    Name = activity.Name,                                      
                    Date = activity.Date,
                    Status = (Communication.Enums.ActivityStatus)activity.Status
                }).ToList()
            };
        }

    }
}