using Magnum_API_web_application.Models;
using MediatR;

namespace Magnum_API_web_application.Command
{
	public class DeleteMemberRequest : IRequest<ApiResponse>
	{
		public int Id { get; }

		public DeleteMemberRequest(int id)
		{
			Id = id;
		}
	}
}
