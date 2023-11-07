using Magnum_API_web_application.Models;
using Magnum_API_web_application.Queries;
using Magnum_API_web_application.Repository.IRepository;
using MediatR;

namespace Magnum_API_web_application.Handler
{
	public class GetMemberByIdHandler : IRequestHandler<GetMemberByIdQuery, ApiResponse>
	{
		private readonly IMemberRepository _repository;
		private readonly ApiResponse _apiResponse;

		public GetMemberByIdHandler(IMemberRepository repository)
        {
			_repository = repository;
			_apiResponse = new ApiResponse();
		}

        public async Task<ApiResponse> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				Member member = await _repository.GetByIdAsync(u => u.Id == request.MemberId);
				if (member == null)
				{
					return _apiResponse.NotFound(member);
				}

				return _apiResponse.Get(member);
			}
			catch (Exception e)
			{
				return _apiResponse.Unauthorize(e);
			}
		}
	}
}
