using Magnum_API_web_application.Command.Competition_Commands;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Repository.IRepository;
using MediatR;

namespace Magnum_API_web_application.Handler.Competition_Handlers
{
	public class UpdateCompetitionHandler : IRequestHandler<UpdateCompetitionCommand, ApiResponse>
	{
		private readonly ICompetitionRepository _repository;
		public ApiResponse _apiResponse;

		public UpdateCompetitionHandler(ICompetitionRepository repository)
        {
			_repository = repository;
			_apiResponse = new ApiResponse();
		}
        public async Task<ApiResponse> Handle(UpdateCompetitionCommand request, CancellationToken cancellationToken)
		{
			Competition competition = await _repository.GetByIdAsync(u => u.Id == request.Id);
			if (competition != null) 
			{
				request.Dto.CompetitionMapper(request.Dto, competition);

				await _repository.UpdateAsync(competition);
				await _repository.SaveAsync();

				return _apiResponse.Update(competition);
			}
			
			return _apiResponse.NotFound(competition);
		}
	}
}
