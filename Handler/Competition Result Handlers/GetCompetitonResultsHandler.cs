using Magnum_API_web_application.Models;
using Magnum_API_web_application.Models.DTO;
using Magnum_API_web_application.Queries.Competition_Results_Queries;
using Magnum_API_web_application.Repository.IRepository;
using MediatR;

namespace Magnum_API_web_application.Handler.Competition_Result_Handlers
{
	public class GetCompetitonResultsHandler : IRequestHandler<GetCompetitionResultsQuery, ApiResponse>
	{
		private readonly ICompetitionResultRepository _repository;
		public ApiResponse _apiResponse;

		public GetCompetitonResultsHandler(ICompetitionResultRepository repository)
        {
			_repository = repository;
			_apiResponse = new ApiResponse();
		}

        public async Task<ApiResponse> Handle(GetCompetitionResultsQuery request, CancellationToken cancellationToken)
		{
			try
			{
				List<CompetitionResult> results = await _repository.GetAllAsync();

				if (results.Count != 0)
				{
					return _apiResponse.Get(results);
				}

				return _apiResponse.NotFound(results);
			}
			catch (Exception e)
			{
				return _apiResponse.Unauthorize(e);
			}
			
		}
	}
}
