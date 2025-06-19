using MediatR;
using Microsoft.AspNetCore.Mvc;
using Patients.Application.Models;
using Patients.Application.Patients;

namespace Patients.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/patients")]
    [Produces("application/json")]
    public sealed class PatientsController : ControllerBase
    {
        private readonly IMediator mediator;

        public PatientsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet(Name = "GetAllPatients")]
        public async Task<ActionResult<IEnumerable<PatientModel>>> GetPatients([FromQuery] string[] date)
        {
            var request = new GetPatientsQuery {
                Filters = date
            };
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("{id:Guid}", Name = "GetPatient")]
        public async Task<ActionResult<PatientModel>> GetPatient(Guid id)
        {
            var request = new GetPatientByIdQuery { Id = id };
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpPost(Name = "CreatePatient")]
        public async Task<ActionResult<PatientModel>> Post([FromBody] CreatePatientCommand request)
        {
            var result = await mediator.Send(request);
            return Created(
                $"{result.Name.Id}",
                result);
        }

        [HttpPut("{id:guid}", Name = "UpdatePatient")]
        public async Task<ActionResult<PatientModel>> Put(Guid id, [FromBody] UpdatePatientCommand request)
        {
            var result = await mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("{id:guid}", Name = "DeletePatient")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var request = new DeletePatientCommand { Id = id };
            await mediator.Send(request);
            return NoContent();
        }
    }
}
