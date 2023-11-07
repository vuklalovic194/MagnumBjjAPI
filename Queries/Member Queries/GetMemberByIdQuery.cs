using Magnum_API_web_application.Models;
using MediatR;

namespace Magnum_API_web_application.Queries
{
	public class GetMemberByIdQuery : IRequest<ApiResponse>
	{
        public int MemberId { get; }

        public GetMemberByIdQuery(int memberId)
        {
            MemberId = memberId;
        }
    }
}
