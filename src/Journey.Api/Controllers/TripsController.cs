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
        [HttpPost]
        [ProducesResponseType(201)]
        public IActionResult Register([FromBody] RequestRegisterTripJson request)
        {
           try
           {
               var useCase = new RegisterTripUseCase();
               useCase.Execute(request);
               return Created();
           }
           catch (JourneyException ex)
           {
                return BadRequest(ex.Message);
           }
        }
        
    }
}