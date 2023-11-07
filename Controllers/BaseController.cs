using Magnum_API_web_application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Magnum_API_web_application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BaseController : ControllerBase
	{
		protected readonly IMediator _mediator;
		protected ApiResponse apiResponse;

		public BaseController(IMediator mediator) 
		{
			apiResponse = new();
			_mediator = mediator;
		}
	}
}
