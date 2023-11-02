using Magnum_API_web_application.Models;
using Magnum_API_web_application.Models.DTO;
using Magnum_API_web_application.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Magnum_API_web_application.Controllers
{
	[Route("api/Rank")]
	[ApiController]
	public class RankController : ControllerBase
	{
		public ApiResponse apiResponse;
		private readonly IRankService rankService;

		public RankController(
			IRankService rankService)
		{
			this.apiResponse = new();
			this.rankService = rankService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllRanks()
		{
			apiResponse = await rankService.GetAllRanksAsync();
			return Ok(apiResponse);
		}

		[HttpPost]
		public async Task<IActionResult> CreateRank([FromBody] RankDTO rankDTO)
		{
			apiResponse = await rankService.CreateRankAsync(rankDTO);
			return Ok(apiResponse);
		}
	}
}
