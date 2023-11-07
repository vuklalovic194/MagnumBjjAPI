using Magnum_API_web_application.Command.Training_Commands;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Queries.Training_Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Magnum_API_web_application.Controllers
{
	[Route("api/Trainings")]
	[ApiController]
	public class TrainingController : BaseController
	{
        public TrainingController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task <ActionResult<ApiResponse>> GetSessionsByMemberId(int id, int month = 0)
		{
			var query = new GetSessionsByMemberIdQuery(id, month);
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpGet("SessionsHistory/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetSessionsHistory(int id)
		{
			var query = new GetSessionsHistoryQuery(id);
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Create(List<int> membersId)
		{
			var command = new CreateTrainingSessionCommand(membersId);
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task <ActionResult> Delete(DateTime date)
		{
			var command = new DeleteTrainingSessionCommand(date);
			var result = await _mediator.Send(command);
			return Ok(result);
		}
	}
}
