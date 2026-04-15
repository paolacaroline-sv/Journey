using Journey.Communication.Responses;
using Journey.Exception.ExceptionBase;
using Journey.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Journey.Application.UseCases.Trips.GetTripById
{

    public class GetTripByIdUseCase
    {
        private readonly JourneyDbContext _dbContext;
        private readonly ILogger<GetTripByIdUseCase> _logger;
        private readonly IMapper _mapper;
        public GetTripByIdUseCase(JourneyDbContext dbContext, ILogger<GetTripByIdUseCase> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

        }

        public ResponseTripJson Execute(Guid id)
        {
            var trip = _dbContext.Trips
            .Include(trip => trip.Activities)
            .FirstOrDefault(trip => trip.Id == id);

            if (trip == null)
            {
                _logger.LogWarning("Trip with ID {Id} not found.", id);
                throw new NotFoundException($"Trip with ID {id} not found.");
            }

            _logger.LogInformation("Retrieved trip with ID {Id} from the database.", id);

            return _mapper.Map<ResponseTripJson>(trip);

        }

    }
}