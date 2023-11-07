using Magnum_API_web_application.Command.Training_Commands;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Repository.IRepository;
using Magnum_API_web_application.Service.IServices;
using MediatR;
using System.Net;

namespace Magnum_API_web_application.Handler.Training_Handlers
{
	public class DeleteTrainingSessionHandler : IRequestHandler<DeleteTrainingSessionCommand, ApiResponse>
	{
		private readonly ITrainingSessionRepository _trainingSessionRepository;
		private ApiResponse _apiResponse;

		public async Task<ApiResponse> Handle(DeleteTrainingSessionCommand request, CancellationToken cancellationToken)
		{
			TrainingSession trainingSession = await _trainingSessionRepository.GetByIdAsync(u => u.SessionDate == request.Date);
			if (trainingSession == null)
			{
				return _apiResponse.NotFound(trainingSession);
			}

			await _trainingSessionRepository.DeleteAsync(trainingSession);
			await _trainingSessionRepository.SaveAsync();

			_apiResponse.StatusCode = HttpStatusCode.NoContent;
			return _apiResponse;
		}
	}
}
