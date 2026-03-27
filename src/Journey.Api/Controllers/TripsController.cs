using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Journey.Communication.Requests;
using Journey.Application.UseCases.Trips.Register;
using Journey.Exception.ExceptionBase;

namespace Journey.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly RegisterTripUseCase _registerTripUseCase;

        public TripsController(RegisterTripUseCase registerTripUseCase)
        {
            _registerTripUseCase = registerTripUseCase;
        }

        [HttpPost]
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