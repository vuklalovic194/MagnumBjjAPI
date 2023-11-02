using Magnum_API_web_application.Models;
using Magnum_API_web_application.Models.DTO;
using Magnum_API_web_application.Repository;
using Magnum_API_web_application.Repository.IRepository;
using Magnum_API_web_application.Service.IServices;

namespace Magnum_API_web_application.Service
{
	public class RankService : IRankService
	{
		public ApiResponse _apiResponse;
		private readonly IRankRepository _rankRepository;

		public RankService(IRankRepository rankRepository)
		{
			_rankRepository = rankRepository;
			_apiResponse = new();
		}

		public async Task<ApiResponse> CreateRankAsync(RankDTO rankDTO)
		{
			List<Rank> ranks = await _rankRepository.GetAllAsync();
			Rank model = new()
			{
				SkillRank = rankDTO.SkillRank
			};

			foreach (var rank in ranks)
			{
				if(rank == model)
				{
					return _apiResponse.BadRequest();
				}
			}

			await _rankRepository.CreateAsync(model);
			return _apiResponse.Create(rankDTO);
		}

		public async Task<ApiResponse> GetAllRanksAsync()
		{
			List<Rank> ranks = await _rankRepository.GetAllAsync();
			return _apiResponse.Get(ranks);
		}
	}
}
