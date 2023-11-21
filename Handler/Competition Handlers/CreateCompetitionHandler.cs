using Magnum_API_web_application.Command.Competition_Commands;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Repository.IRepository;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Magnum_API_web_application.Handler.Competition_Handlers
{
	public class CreateCompetitionHandler : IRequestHandler<CreateCompetitionCommand, ApiResponse>
	{
		private readonly ICompetitionRepository _repository;
		public ApiResponse _apiResponse;

		public CreateCompetitionHandler(ICompetitionRepository repository)
        {
			_repository = repository;
			_apiResponse = new ApiResponse();
		}
        public async Task<ApiResponse> Handle(CreateCompetitionCommand request, CancellationToken cancellationToken)
		{
			//logic
			try
			{
				Competition competition = new();

				if (await _repository.GetByIdAsync
					(u => u.Place == request.CompetitionDTO.Place
					&& u.Organisation == request.CompetitionDTO.Organisation
					&& u.Date == request.CompetitionDTO.Date) != null)
				{
					_apiResponse.BadRequest();
					_apiResponse.ErrorMessage = "There is already Competition with same parameters";
					return _apiResponse;
				}

				request.CompetitionDTO.CompetitionMapper(request.CompetitionDTO, competition);
				await _repository.CreateAsync(competition);
				await _repository.SaveAsync();

				return _apiResponse.Create(request.CompetitionDTO);
			}
			catch (Exception e)
			{
				return _apiResponse.Unauthorize(e);
			}
			
		}
	}
}
