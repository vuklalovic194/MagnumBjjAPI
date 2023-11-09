using Magnum_API_web_application.Models;
using MediatR;

namespace Magnum_API_web_application.Queries.Fee_Queries
{
	public class GetFeesByIdQuery : IRequest<ApiResponse>
	{
		public int MemberId { get; }

		public GetFeesByIdQuery(int memberId)
		{
			MemberId = memberId;
		}
	}
}
