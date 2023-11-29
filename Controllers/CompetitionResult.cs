using Magnum_API_web_application.Command.Competition_Result_Commands;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Queries.Competition_Results_Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Magnum_API_web_application.Controllers
{
	[Route("api/CompetitionResult")]
	[ApiController]
	public class CompetitionResult : BaseController
	{

		public CompetitionResult(IMediator mediator) : base(mediator)
        {
		}

        [HttpGet]
		public async Task<ActionResult<ApiResponse>> GetAllResults()
		{
			var query = new GetCompetitionResultsQuery();
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<ApiResponse>> GetResultsByMemberId(int memberId)
		{
			var query = new GetCompetitionResultsQuery();
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<ApiResponse>> CreateCompetitionResult([FromBody] Models.CompetitionResult competitionResult)
		{
			var command = new CreateCompetitionResultCommand(competitionResult);
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		
	}
}
