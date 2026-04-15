using System.Data.Common;
using FluentValidation;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using AutoMapper;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripUseCase
    {
        private readonly JourneyDbContext _dbContext;
        private readonly ILogger<RegisterTripUseCase> _logger;
        private readonly IMapper _mapper;

        public RegisterTripUseCase(JourneyDbContext dbContext, ILogger<RegisterTripUseCase> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public ResponseShortTripJson Execute(RequestRegisterTripJson request)
        {
            Validate(request);

            var trip = _mapper.Map<Trip>(request);           

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
            return _mapper.Map<ResponseShortTripJson>(trip);
        }

        private void Validate(RequestRegisterTripJson request)
        {
            var validator = new RegisterTripValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errors);
            }
        }
    }
}