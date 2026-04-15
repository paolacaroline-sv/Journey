using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Journey.Exception.ExceptionBase;
using Journey.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.Delete
{
    public class DeleteTripByIdUseCase
    {
        private readonly JourneyDbContext _dbContext;
        private readonly ILogger<DeleteTripByIdUseCase> _logger;
        public DeleteTripByIdUseCase(JourneyDbContext dbContext, ILogger<DeleteTripByIdUseCase> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task Execute(Guid id)
        {
            var trip = await _dbContext.Trips
            .Include(trip => trip.Activities)
            .FirstOrDefaultAsync(trip => trip.Id == id);

            if (trip == null)            
                throw new NotFoundException($"Trip with ID {id} not found.");            

            _dbContext.Trips.Remove(trip);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Deleted trip with ID {Id} from the database.", id);
            
        }        
    }
}