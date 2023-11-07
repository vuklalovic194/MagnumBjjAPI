using Magnum_API_web_application.Command;
using Magnum_API_web_application.Handler;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Models.DTO;
using Magnum_API_web_application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Magnum_API_web_application.Controllers
{
    [Route("api/Members")]
	[ApiController]
	public class MemberController : BaseController
	{
		public MemberController(IMediator mediator) : base(mediator)
		{
		}

		[HttpGet(Name = "GetMembers")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		//[Authorize]
		public async Task<ActionResult<ApiResponse>> GetMembers()
		{
			var query = new GetAllMembersQuery();
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpGet("{id}", Name = "GetMember")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ApiResponse>> GetMember(int id)
		{
			var query = new GetMemberByIdQuery(id);
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpPost(Name = "CreateMember")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public async Task<IActionResult> Create([FromBody] MemberDTO memberDTO)
		{
			if(ModelState.IsValid)
			{
				var command =  new CreateMemberRequest(memberDTO);
				var result = await _mediator.Send(command);
				return Ok(result);
			}
			return Ok(_apiResponse.BadRequest());
		}

		[HttpPut("{id}", Name = "UpdateMember")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Update([FromBody] MemberDTO updateDTO, int id)
		{
			if (ModelState.IsValid)
			{
				var command = new UpdateMemberRequest(updateDTO, id);
				var result = await _mediator.Send(command);
				return Ok(result);
			}
			return Ok(_apiResponse.BadRequest());
		}

		[HttpDelete("{id}", Name = "DeleteMember")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> Delete(int id)
		{
			var command = new DeleteMemberRequest(id);
			var result = await _mediator.Send(command);
			return Ok(result);
		}
	}
}
