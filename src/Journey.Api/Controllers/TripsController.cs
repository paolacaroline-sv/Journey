using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Journey.Communication.Requests;
using Journey.Application.UseCases.Trips.Register;
using Journey.Exception.ExceptionBase;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetById;

namespace Journey.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly GetAllTripsUseCase _getAllTripsUseCase;
        private readonly GetById _getById;

        private readonly RegisterTripUseCase _registerTripUseCase;

        public TripsController(GetAllTripsUseCase getAllTripsUseCase, GetById getById, RegisterTripUseCase registerTripUseCase)
        {
            _getAllTripsUseCase = getAllTripsUseCase;
            _getById = getById;
            _registerTripUseCase = registerTripUseCase;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_getAllTripsUseCase.Execute());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_getById.Execute(id));
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