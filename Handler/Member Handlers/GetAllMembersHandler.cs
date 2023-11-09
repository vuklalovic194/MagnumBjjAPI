using Magnum_API_web_application.Models;
using Magnum_API_web_application.Queries;
using Magnum_API_web_application.Repository.IRepository;
using MediatR;

namespace Magnum_API_web_application.Handler
{
	public class GetAllMembersHandler : IRequestHandler<GetAllMembersQuery, ApiResponse>
	{
		private readonly IMemberRepository _repository;
		private readonly ApiResponse _apiResponse;

		public GetAllMembersHandler(IMemberRepository repository)
        {
			_repository = repository;
			_apiResponse = new ApiResponse();
		}

        public async Task<ApiResponse> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
		{
			try
			{
				List<Member> memberList = await _repository.GetAllAsync();

				if (memberList.Count <= 0)
				{
					return _apiResponse.NotFound(memberList);
				}

				//pagination
				int pageSize = 10;
				
				List<Member> paginatedMembers = 
					memberList
					.Skip((request.Page - 1) * pageSize)
					.Take(pageSize)
					.ToList();

				_apiResponse.Get(paginatedMembers);

				return _apiResponse;
			}
			catch (Exception e)
			{
				return _apiResponse.Unauthorize(e);
			}
		}
	}
}
