using Magnum_API_web_application.Command;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Repository.IRepository;
using MediatR;
using System.Net;

namespace Magnum_API_web_application.Handler
{
	public class DeleteMemberHandler : IRequestHandler<DeleteMemberCommand, ApiResponse>
	{
		private readonly IMemberRepository _repository;
		private readonly ApiResponse _apiResponse;

		public DeleteMemberHandler(IMemberRepository repository)
		{
			_repository = repository;
			_apiResponse = new ApiResponse();
		}

		public async Task<ApiResponse> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
		{
			try
			{
				Member member = await _repository.GetByIdAsync(u => u.Id == request.Id);
				if (member == null)
				{
					return _apiResponse.NotFound(member);
				}

				await _repository.DeleteAsync(member);
				await _repository.SaveAsync();

				_apiResponse.StatusCode = HttpStatusCode.NoContent;
				return _apiResponse;
			}
			catch (Exception e)
			{
				return _apiResponse.Unauthorize(e);
			}
		}
	}
}
