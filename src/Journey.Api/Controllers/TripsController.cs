global using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Application.UseCases.Trips.Register;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetTripById;
using Journey.Application.UseCases.Trips.Delete;
using Journey.Application.UseCases.Activity.Register;
using Journey.Application.UseCases.Activity.Complete;
using Journey.Application.UseCases.Activity.Delete;



namespace Journey.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly GetAllTripsUseCase _getAllTripsUseCase;
        private readonly GetTripByIdUseCase _getTripByIdUseCase;
        private readonly RegisterTripUseCase _registerTripUseCase;
        private readonly DeleteTripByIdUseCase _deleteTripByIdUseCase;

        private readonly RegisterActivityForTripUseCase _registerActivityForTripUseCase;
        private readonly CompleteActivityForTripUseCase _completeActivityForTripUseCase;
        private readonly DeleteActivityForTripUseCase _deleteActivityForTripUseCase;

        public TripsController(GetAllTripsUseCase getAllTripsUseCase, GetTripByIdUseCase getTripByIdUseCase, RegisterTripUseCase registerTripUseCase, DeleteTripByIdUseCase deleteTripByIdUseCase, RegisterActivityForTripUseCase registerActivityForTripUseCase, CompleteActivityForTripUseCase completeActivityForTripUseCase, DeleteActivityForTripUseCase deleteActivityForTripUseCase)
        {
            _getAllTripsUseCase = getAllTripsUseCase;
            _getTripByIdUseCase = getTripByIdUseCase;
            _registerTripUseCase = registerTripUseCase;
            _deleteTripByIdUseCase = deleteTripByIdUseCase;

            _registerActivityForTripUseCase = registerActivityForTripUseCase;
            _completeActivityForTripUseCase = completeActivityForTripUseCase;
            _deleteActivityForTripUseCase = deleteActivityForTripUseCase;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _getAllTripsUseCase.Execute());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]   
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            return Ok(await _getTripByIdUseCase.Execute(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RequestRegisterTripJson request)
        {
            var response = await _registerTripUseCase.Execute(request);
            return Created(string.Empty, response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _deleteTripByIdUseCase.Execute(id);
            return NoContent();
        }

        [HttpPost]
        [Route("{tripId}/activity")]
        [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RegisterActivity([FromRoute] Guid tripId, [FromBody] RequestRegisterActivityJson request)
        {            
            var response = await _registerActivityForTripUseCase.Execute(tripId, request);
            return Created(string.Empty, response);
        }

        [HttpPut]
        [Route("{tripId}/activity/{activityId}/complete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CompleteActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId)
        {            
            await _completeActivityForTripUseCase.Execute(tripId, activityId);
            return NoContent();
        }

        [HttpDelete]
        [Route("{tripId}/activity/{activityId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId)
        {            
            await _deleteActivityForTripUseCase.Execute(tripId, activityId);
            return NoContent();
        }
    }
}