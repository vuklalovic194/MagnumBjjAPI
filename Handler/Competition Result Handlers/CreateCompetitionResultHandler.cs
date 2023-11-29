using Magnum_API_web_application.Command.Competition_Result_Commands;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Repository.IRepository;
using MediatR;

namespace Magnum_API_web_application.Handler.Competition_Result_Handlers
{
	public class CreateCompetitionResultHandler : IRequestHandler<CreateCompetitionResultCommand, ApiResponse>
	{
		private readonly ICompetitionResultRepository _repository;
		public ApiResponse _apiResponse;

		public CreateCompetitionResultHandler(ICompetitionResultRepository repository)
        {
			_repository = repository;
			_apiResponse = new ApiResponse();
		}

		public async Task<ApiResponse> Handle(CreateCompetitionResultCommand request, CancellationToken cancellationToken)
		{
			try
			{
				CompetitionResult competitionResult = new CompetitionResult();
				competitionResult.Result = request.CompetitionResult.Result;
				competitionResult.CompetitionId = request.CompetitionResult.CompetitionId;
				competitionResult.Member = request.CompetitionResult.Member;

				await _repository.CreateAsync(competitionResult);
				await _repository.SaveAsync();

				return _apiResponse.Create(competitionResult);
			}
			catch (Exception e)
			{
				return _apiResponse.Unauthorize(e);
			}
		}
	}
}
