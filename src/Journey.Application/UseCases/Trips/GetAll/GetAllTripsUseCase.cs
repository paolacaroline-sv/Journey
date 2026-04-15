using AutoMapper;
using Journey.Communication.Responses;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Journey.Application.UseCases.Trips.GetAll
{
    public class GetAllTripsUseCase
    {
        private readonly JourneyDbContext _dbContext;
        private readonly ILogger<GetAllTripsUseCase> _logger;
        private readonly IMapper _mapper;
        public GetAllTripsUseCase(JourneyDbContext dbContext, ILogger<GetAllTripsUseCase> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ResponseTripsJson> Execute()
        {
            var trips = await _dbContext.Trips.ToListAsync();

            _logger.LogInformation("Retrieved {Count} trips from the database.", trips.Count);

            return _mapper.Map<ResponseTripsJson>(trips);
        }
    }
}