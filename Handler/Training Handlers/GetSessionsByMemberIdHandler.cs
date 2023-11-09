using Magnum_API_web_application.Models;
using Magnum_API_web_application.Queries.Training_Queries;
using Magnum_API_web_application.Repository.IRepository;
using MediatR;

namespace Magnum_API_web_application.Handler.Training_Handlers
{
	public class GetSessionsByMemberIdHandler : IRequestHandler<GetSessionsByMemberIdQuery, ApiResponse>
	{
		private readonly ITrainingSessionRepository _trainingSessionRepository;
		private ApiResponse _apiResponse;

		public GetSessionsByMemberIdHandler(ITrainingSessionRepository trainingSessionRepository)
		{
			_trainingSessionRepository = trainingSessionRepository;
			_apiResponse = new ApiResponse();
		}

		public async Task<ApiResponse> Handle(GetSessionsByMemberIdQuery request, CancellationToken cancellationToken)
		{
			List<TrainingSession> trainingSession = await _trainingSessionRepository.GetAllAsync(u => u.MemberId == request.MemberId);
			if (trainingSession.Count != 0)
			{
				if (request.Month != 0)
				{
					trainingSession = await _trainingSessionRepository.GetAllAsync(u => u.SessionDate.Month == request.Month && u.MemberId == request.MemberId);

					return _apiResponse.Get(trainingSession.Count);
				}

				return _apiResponse.Get(trainingSession.Count);
			}

			return _apiResponse.NotFound(trainingSession);
		}
	}
}
