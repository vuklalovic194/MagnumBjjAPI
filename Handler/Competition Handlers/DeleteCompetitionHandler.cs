using Magnum_API_web_application.Command.Competition_Commands;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Repository.IRepository;
using MediatR;
using System.Net;

namespace Magnum_API_web_application.Handler.Competition_Handlers
{
	public class DeleteCompetitionHandler : IRequestHandler<DeleteCompetitionCommand, ApiResponse>
	{
		private readonly ICompetitionRepository _repository;
		public ApiResponse _apiResponse;

        public DeleteCompetitionHandler(ICompetitionRepository repository)
        {
            _repository = repository;
            _apiResponse = new ApiResponse();
        }

		public async Task<ApiResponse> Handle(DeleteCompetitionCommand request, CancellationToken cancellationToken)
		{
			Competition competition = await _repository.GetByIdAsync(u => u.Id == request.Id);
			if (competition != null) 
			{ 
				await _repository.DeleteAsync(competition);
				await _repository.SaveAsync();
				
				_apiResponse.StatusCode = HttpStatusCode.NoContent;
				return _apiResponse;
			}

			return _apiResponse.NotFound(competition);
		}
	}
}
