using Magnum_API_web_application.Models;
using Magnum_API_web_application.Queries.Training_Queries;
using Magnum_API_web_application.Repository.IRepository;
using MediatR;

namespace Magnum_API_web_application.Handler.Training_Handlers
{
	public class GetSessionsHistoryHandler : IRequestHandler<GetSessionsHistoryQuery, ApiResponse>
	{
		private readonly ITrainingSessionRepository _trainingSessionRepository;
		private ApiResponse _apiResponse;

		public GetSessionsHistoryHandler(ITrainingSessionRepository trainingSessionRepository)
		{
			_trainingSessionRepository = trainingSessionRepository;
			_apiResponse = new ApiResponse();
		}

		public async Task<ApiResponse> Handle(GetSessionsHistoryQuery request, CancellationToken cancellationToken)
		{
			List<TrainingSession> trainingSessions = await _trainingSessionRepository.GetAllAsync(u => u.MemberId == request.Id);

			if (trainingSessions.Count != 0)
			{
				return _apiResponse.Get(trainingSessions);
			}
			return _apiResponse.NotFound(trainingSessions);
		}
	}
}
