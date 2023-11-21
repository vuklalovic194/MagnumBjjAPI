using Magnum_API_web_application.Models;
using Magnum_API_web_application.Queries.Competition_Queries;
using Magnum_API_web_application.Repository;
using Magnum_API_web_application.Repository.IRepository;
using MediatR;

namespace Magnum_API_web_application.Handler.Competition_Handlers
{
	public class GetCompetitionsHandler : IRequestHandler<GetCompetitionsQuery, ApiResponse>
	{
		private readonly ICompetitionRepository _repository;
		public ApiResponse _apiResponse;

		public GetCompetitionsHandler(ICompetitionRepository repository)
        {
			_repository = repository;
			_apiResponse = new ApiResponse();
		}
        public async Task<ApiResponse> Handle(GetCompetitionsQuery request, CancellationToken cancellationToken)
		{
			//Logic
			List<Competition> list = await _repository.GetAllAsync();
			
			if(list.Count != 0) 
			{
				return _apiResponse.Get(list);
			}
			
			return _apiResponse.NotFound(list);
		}
	}
}
