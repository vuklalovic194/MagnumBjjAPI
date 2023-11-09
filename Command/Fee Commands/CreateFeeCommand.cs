using Magnum_API_web_application.Models;
using MediatR;

namespace Magnum_API_web_application.Command.Fee_Commands
{
	public class CreateFeeCommand : IRequest<ApiResponse>
	{
		public int MemberId { get; }

		public CreateFeeCommand(int memberId)
		{
			MemberId = memberId;
		}
	}
}
