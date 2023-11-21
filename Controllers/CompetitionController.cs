using Magnum_API_web_application.Command.Competition_Commands;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Models.DTO;
using Magnum_API_web_application.Queries.Competition_Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Magnum_API_web_application.Controllers
{
	[Route("api/Competitions")]
	[ApiController]
	public class CompetitionController : BaseController
	{
        public CompetitionController(IMediator mediator) : base(mediator) 
        {
        }

		[HttpGet]
		public async Task<ActionResult<ApiResponse>> GetCompetitions() 
		{
			var query = new GetCompetitionsQuery();
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<ApiResponse>> GetCompetition(int id)
		{
			var query = new GetCompetitionByIdQuery(id);
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpPost(Name = "CreateCompetition")]
		public async Task<ActionResult<ApiResponse>> CreateCompetition([FromBody]CompetitionDTO dto)
		{
			var command = new CreateCompetitionCommand(dto);
			var result = await _mediator.Send(command);
			return Ok(result);
		}
    }
}
