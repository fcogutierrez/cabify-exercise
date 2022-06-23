using Cabify.CarPooling.Application.Commands;
using Cabify.CarPooling.Application.Dtos;
using Cabify.CarPooling.Application.Exceptions;
using Cabify.CarPooling.Application.Handlers;
using Cabify.CarPooling.Application.Interfaces;
using Cabify.CarPooling.Application.Query;
using Cabify.CarPooling.Infra.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cabify.CarPooling.Api.Controllers
{
    [ApiController]
    [Route("")]
    public class CarPoolingController 
        : ControllerBase
    {
        private readonly ICommandHandler<AddCarsCommand> _addCarsCommandHandler;
        private readonly ICommandHandler<AddJourneyCommand> _addJourneyCommandHandler;
        private readonly ICommandHandler<DropOffJourneyCommand> _dropOffJourneyCommandHandler;
        private readonly IQueryHandler<LocateJourneyCarQuery, CarDto> _locateCarQueryHandler;
        private readonly IHealthService _healthService;

        public CarPoolingController(
            ICommandHandler<AddCarsCommand> addCarsCommandHandler,
            ICommandHandler<AddJourneyCommand> addJourneyCommandHandler,
            ICommandHandler<DropOffJourneyCommand> dropOffJourneyCommandHandler,
            IQueryHandler<LocateJourneyCarQuery, CarDto> locateCarQueryHandler,
            IHealthService healthService)
        {
            _addCarsCommandHandler = addCarsCommandHandler;
            _addJourneyCommandHandler = addJourneyCommandHandler;
            _dropOffJourneyCommandHandler = dropOffJourneyCommandHandler;
            _locateCarQueryHandler = locateCarQueryHandler;
            _healthService = healthService;
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            var result = await _healthService.IsInfrastructureHealthy();
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        [HttpPut("cars")]
        public async Task<IActionResult> Update([FromBody] IEnumerable<CarDto> cars)
        {
            var command = new AddCarsCommand(cars);
            await _addCarsCommandHandler.Handle(command);

            return Ok();
        }

        [HttpPost("journey")]
        public async Task<IActionResult> AddJourney([FromBody] JourneyDto journeyDto)
        {
            var command = new AddJourneyCommand(journeyDto.Id, journeyDto.People);
            await _addJourneyCommandHandler.Handle(command);

            return Ok();
        }

        [HttpPost("dropoff")]
        public async Task<IActionResult> DropOffJourney([FromForm] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var command = new DropOffJourneyCommand(id.Value);

            try
            {
                await _dropOffJourneyCommandHandler.Handle(command);
            }
            catch (JourneyNotFoundException)
            {
                return NotFound(default);
            }

            return Ok();
        }

        [HttpPost("locate")]
        public async Task<IActionResult> Locate([FromForm] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var query = new LocateJourneyCarQuery(id.Value);

            try
            {
                var carDto = await _locateCarQueryHandler.Handle(query);
                return Ok(carDto);
            }
            catch (JourneyNotFoundException)
            {
                return NotFound(default);
            }
            catch (JourneyWithUnassignedCarException)
            {
                return NoContent();
            }
        }
    }
}
