using Magnum_API_web_application.Models;
using Magnum_API_web_application.Models.DTO;

namespace Magnum_API_web_application.Service.IServices
{
	public interface IRankService
	{
		Task<ApiResponse> GetAllRanksAsync();
		Task<ApiResponse> CreateRankAsync(RankDTO rankDTO);
	}
}
