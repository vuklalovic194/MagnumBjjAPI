using Magnum_API_web_application.Models;
using Magnum_API_web_application.Models.DTO;
using MediatR;

namespace Magnum_API_web_application.Command
{
	public class UpdateMemberRequest : IRequest<ApiResponse>
	{
		public int Id { get; }
		public MemberDTO MemberDTO { get; }

		public UpdateMemberRequest(MemberDTO member, int id)
		{
			MemberDTO = member;
			Id = id;
		}
	}
}
