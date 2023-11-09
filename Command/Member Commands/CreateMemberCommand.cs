using Magnum_API_web_application.Models;
using Magnum_API_web_application.Models.DTO;
using MediatR;

namespace Magnum_API_web_application.Command
{
	public class CreateMemberCommand : IRequest<ApiResponse>
	{
		public MemberDTO MemberDTO { get; }

		public CreateMemberCommand(MemberDTO memberDTO)
		{
			MemberDTO = memberDTO;
		}
    }
}
