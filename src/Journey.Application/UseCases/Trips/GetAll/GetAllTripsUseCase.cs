using Journey.Communication.Responses;
using Journey.Infrastructure;

namespace Journey.Application.UseCases.Trips.GetAll
{
    public class GetAllTripsUseCase
    {
        private readonly JourneyDbContext _dbContext;
        private readonly ILogger<GetAllTripsUseCase> _logger;
        public GetAllTripsUseCase(JourneyDbContext dbContext, ILogger<GetAllTripsUseCase> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public ResponseTripsJson Execute()
        {
            var trips = _dbContext.Trips.ToList();           

            _logger.LogInformation("Retrieved {Count} trips from the database.", trips.Count);

            return new ResponseTripsJson
            {
                Trips = trips.Select(t => new ResponseShortTripJson
                {
                    Id = t.Id,
                    Name = t.Name,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate
                }).ToList()
            };
          
        }
    }
}