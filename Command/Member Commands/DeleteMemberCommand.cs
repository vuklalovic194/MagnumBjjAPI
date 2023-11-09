using Magnum_API_web_application.Models;
using MediatR;

namespace Magnum_API_web_application.Command
{
	public class DeleteMemberCommand : IRequest<ApiResponse>
	{
		public int Id { get; }

		public DeleteMemberCommand(int id)
		{
			Id = id;
		}
	}
}
