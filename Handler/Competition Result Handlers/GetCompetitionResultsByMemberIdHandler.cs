using Magnum_API_web_application.Models;
using Magnum_API_web_application.Queries.Competition_Results_Queries;
using Magnum_API_web_application.Repository.IRepository;
using MediatR;

namespace Magnum_API_web_application.Handler.Competition_Result_Handlers
{
	public class GetCompetitionResultsByMemberIdHandler : IRequestHandler<GetCompetitionResultsByMemberIdQuery, ApiResponse>
	{
		private readonly ICompetitionResultRepository _repository;
		private readonly IMemberRepository _memberRepository;
		public ApiResponse _apiResponse;

		public GetCompetitionResultsByMemberIdHandler(
			ICompetitionResultRepository repository,
			IMemberRepository memberRepository
			)
        {
			_repository = repository;
			_memberRepository = memberRepository;
			_apiResponse = new ApiResponse();
		}

        public async Task<ApiResponse> Handle(GetCompetitionResultsByMemberIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				List<CompetitionResult> results = await _repository.GetAllAsync();

				if(results.Count !=  0 )
				{
					var myList = results
					.Where(u => u.Member.Id == request.MemberId);

					return _apiResponse.Get(myList);
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
