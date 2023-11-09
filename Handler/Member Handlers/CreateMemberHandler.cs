using Magnum_API_web_application.Command;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Models.DTO;
using Magnum_API_web_application.Repository.IRepository;
using MediatR;

namespace Magnum_API_web_application.Handler
{
	public class CreateMemberHandler : IRequestHandler<CreateMemberCommand, ApiResponse>
	{
		private readonly IMemberRepository _repository;
		public ApiResponse _apiResponse;

		public CreateMemberHandler(IMemberRepository repository)
		{
			_repository = repository;
			_apiResponse = new ApiResponse();
		}

		public async Task<ApiResponse> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
		{
			try
			{
				if (await _repository.GetByIdAsync(u => u.Name == request.MemberDTO.Name) != null)
				{
					_apiResponse.BadRequest();
					_apiResponse.ErrorMessage = "Member with same name already exists";
					return _apiResponse;
				}

				Member model = new();
				request.MemberDTO.mapMember(request.MemberDTO, model);

				await _repository.CreateAsync(model);
				await _repository.SaveAsync();

				return _apiResponse.Create(request.MemberDTO);
			}
			catch (Exception e)
			{
				return _apiResponse.Unauthorize(e);
			}
		}
	}
}
