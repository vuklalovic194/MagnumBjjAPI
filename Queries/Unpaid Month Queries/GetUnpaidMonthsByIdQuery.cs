using Magnum_API_web_application.Models;
using MediatR;

namespace Magnum_API_web_application.Queries.Unpaid_Month_Queries
{
	public class GetUnpaidMonthsByIdQuery : IRequest<ApiResponse>
	{
		public int MemberId { get; }

		public GetUnpaidMonthsByIdQuery(int memberId)
		{
			MemberId = memberId;
		}
	}
}
