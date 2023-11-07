using Magnum_API_web_application.Models;
using MediatR;

namespace Magnum_API_web_application.Queries.Training_Queries
{
	public class GetSessionsByMemberIdQuery : IRequest<ApiResponse>
	{
		public int MemberId { get; }
		public int Month { get; } 

		public GetSessionsByMemberIdQuery(int memberId, int month)
		{
			MemberId = memberId;
			Month = month;
		}
	}
}
