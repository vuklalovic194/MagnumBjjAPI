using Magnum_API_web_application.Models;
using MediatR;

namespace Magnum_API_web_application.Queries.Competition_Results_Queries
{
	public class GetCompetitionResultsByMemberIdQuery : IRequest<ApiResponse>
	{
		public int MemberId { get; set; }

		public GetCompetitionResultsByMemberIdQuery(int memberId)
		{
			MemberId = memberId;
		}
	}
}
