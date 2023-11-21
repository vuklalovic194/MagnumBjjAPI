using Magnum_API_web_application.Models;
using Magnum_API_web_application.Queries.Competition_Queries;
using Magnum_API_web_application.Repository.IRepository;
using MediatR;

namespace Magnum_API_web_application.Handler.Competition_Handlers
{
	public class GetCompetitionByIdHandler : IRequestHandler<GetCompetitionByIdQuery, ApiResponse>
	{
		private readonly ICompetitionRepository _repository;
		public ApiResponse _apiResponse;

		public GetCompetitionByIdHandler(ICompetitionRepository repository)
        {
			_repository = repository;
			_apiResponse = new ApiResponse();
		}
        public async Task<ApiResponse> Handle(GetCompetitionByIdQuery request, CancellationToken cancellationToken)
		{
			Competition competition = await _repository.GetByIdAsync(u => u.Id == request.Id);
			if (request.Id == 0)
			{
				return _apiResponse.BadRequest();
			}
			
			if (competition == null)
			{
				return _apiResponse.NotFound(competition);
			}
			
			return _apiResponse.Get(competition);
		}
	}
}
