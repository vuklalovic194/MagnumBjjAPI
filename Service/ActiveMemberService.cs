//using Magnum_API_web_application.Models;
//using Magnum_API_web_application.Repository.IRepository;
//using Magnum_API_web_application.Service.IServices;

//namespace Magnum_API_web_application.Service
//{
//	public class ActiveMemberService : IActiveMemberService
//	{
//		private readonly IActiveMemberRepository _repository;
//		private readonly ITrainingSessionRepository _trainingSessionRepository;
//		public ApiResponse _apiResponse;

//		public ActiveMemberService(IActiveMemberRepository repository, ITrainingSessionRepository trainingSessionRepository)
//		{
//			_repository = repository;
//			_apiResponse = new ApiResponse();
//			_trainingSessionRepository = trainingSessionRepository;
//		}

//		public async Task<ApiResponse> GetActiveMemberAsync(int id)
//		{
//			try
//			{
//				List<ActiveMember> activeMembers = await _repository.GetAllAsync(u => u.MemberId == id);

//				if (activeMembers.Count != 0)
//				{
//					return _apiResponse.Get(activeMembers);
//				}

//				_apiResponse.NotFound(activeMembers);
//				_apiResponse.ErrorMessage = "Member is not active";
//				return _apiResponse;
//			}
//			catch (Exception e)
//			{
//				return _apiResponse.Unauthorize(e);
//			}
//		}

//		public async Task<ApiResponse> GetAllActiveMembersAsync()
//		{
//			try
//			{
//				List<ActiveMember> activeMembers = await _repository.GetAllAsync();

//				if (activeMembers.Count != 0)
//				{
//					return _apiResponse.Get(activeMembers);
//				}

//				return _apiResponse.NotFound(activeMembers);
//			}
//			catch (Exception e)
//			{
//				return _apiResponse.Unauthorize(e);
//			}
//		}
//	}
//}
