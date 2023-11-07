using Magnum_API_web_application.Command;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Models.DTO;
using Magnum_API_web_application.Repository.IRepository;
using MediatR;

namespace Magnum_API_web_application.Handler
{
	public class UpdateMemberHandler : IRequestHandler<UpdateMemberRequest, ApiResponse>
	{
		private readonly IMemberRepository _repository;
		private readonly ApiResponse _apiResponse;

		public UpdateMemberHandler(IMemberRepository repository)
		{
			_repository = repository;
			_apiResponse = new ApiResponse();
		}

		public async Task<ApiResponse> Handle(UpdateMemberRequest request, CancellationToken cancellationToken)
		{
			try
			{
				Member member = await _repository.GetByIdAsync(u => u.Id == request.Id);
				if (member == null)
				{
					return _apiResponse.NotFound(member);
				}

				request.MemberDTO.mapMember(request.MemberDTO, member);
				await _repository.Update(member);
				await _repository.SaveAsync();

				return _apiResponse.Update(member);
			}
			catch (Exception e)
			{
				return _apiResponse.Unauthorize(e);
			}
		}
	}
}
