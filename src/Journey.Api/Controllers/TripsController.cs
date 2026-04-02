using Microsoft.AspNetCore.Mvc;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Application.UseCases.Trips.Register;
using Journey.Exception.ExceptionBase;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetTripById;

namespace Journey.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly GetAllTripsUseCase _getAllTripsUseCase;
        private readonly GetTripByIdUseCase _getTripByIdUseCase;
        private readonly RegisterTripUseCase _registerTripUseCase;

        public TripsController(GetAllTripsUseCase getAllTripsUseCase, GetTripByIdUseCase getTripByIdUseCase, RegisterTripUseCase registerTripUseCase)
        {
            _getAllTripsUseCase = getAllTripsUseCase;
            _getTripByIdUseCase = getTripByIdUseCase;
            _registerTripUseCase = registerTripUseCase;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            return Ok(_getAllTripsUseCase.Execute());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
        public IActionResult GetById([FromRoute] Guid id)
        {
            return Ok(_getTripByIdUseCase.Execute(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RequestRegisterTripJson request)
        {
            try
            {   
                var response = _registerTripUseCase.Execute(request);
                return Created(string.Empty, response);
            }
            catch (JourneyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {                
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

    }
}