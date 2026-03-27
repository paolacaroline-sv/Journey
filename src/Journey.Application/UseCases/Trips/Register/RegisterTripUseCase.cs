using Journey.Communication.Requests;
using Journey.Exception.ExceptionBase;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripUseCase
    {
        public void Execute(RequestRegisterTripJson request)
        {
            Validate(request);
        }

        private void Validate(RequestRegisterTripJson request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var todayUtc = DateTime.UtcNow.Date;
            var startDate = request.StartDate.Date;
            var endDate = request.EndDate.Date;
           
            if (string.IsNullOrWhiteSpace(request.Name))
               throw new JourneyException("Name is required.");

            if (request.StartDate == default)
                throw new JourneyException("Start date is required.");

            if (request.EndDate == default)
                throw new JourneyException("End date is required.");

            if (startDate < todayUtc)
                throw new JourneyException("Start date cannot be in the past.");

            if (endDate < startDate)
                throw new JourneyException("End date cannot be before start date.");
        }
    }
}