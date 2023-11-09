using Magnum_API_web_application.Command.Fee_Commands;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Queries.Fee_Queries;
using Magnum_API_web_application.Service.IServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Magnum_API_web_application.Controllers
{
	[Route("api/Fees")]
	[ApiController]
	public class FeeController : BaseController
	{
        public FeeController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		//[Authorize]
		public async Task<ActionResult<ApiResponse>> GetFeesByMemberId(int id)
		{
			var query = new GetFeesByIdQuery(id);
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		//[Authorize]
		public async Task<ActionResult<ApiResponse>> CreateFee(int memberId)
		{
			var command = new CreateFeeCommand(memberId);
			var result = await _mediator.Send(command);
			return Ok(result);
		}

	}
}
