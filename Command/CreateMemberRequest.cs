using Magnum_API_web_application.Models;
using Magnum_API_web_application.Models.DTO;
using MediatR;

namespace Magnum_API_web_application.Command
{
	public class CreateMemberRequest : IRequest<ApiResponse>
	{
		public MemberDTO MemberDTO { get; }

		public CreateMemberRequest(MemberDTO memberDTO)
		{
			MemberDTO = memberDTO;
		}
    }
}
