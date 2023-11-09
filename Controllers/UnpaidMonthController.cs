using Magnum_API_web_application.Models;
using Magnum_API_web_application.Queries.Unpaid_Month_Queries;
using Magnum_API_web_application.Service;
using Magnum_API_web_application.Service.IServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Magnum_API_web_application.Controllers
{
    [Route("api/UnpaidMonth")]
	[ApiController]
	public class UnpaidMonthController : BaseController
	{
        public UnpaidMonthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("GetAll")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetUnpaidMonths()
		{
			var query = new GetUnpaidMonthsQuery();
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpGet("Get/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetUnpaidMonthsByMemberId(int id)
		{
			var query = new GetUnpaidMonthsByIdQuery(id);
			var result = await _mediator.Send(query);
			return Ok(result);
		}
	}
}
