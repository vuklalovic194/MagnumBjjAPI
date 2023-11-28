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

		
	}
}
