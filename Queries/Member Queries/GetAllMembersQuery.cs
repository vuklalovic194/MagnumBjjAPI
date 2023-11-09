using Magnum_API_web_application.Models;
using MediatR;

namespace Magnum_API_web_application.Queries
{
	public class GetAllMembersQuery : IRequest<ApiResponse>
	{
		public int Page { get; } = 1;

		public GetAllMembersQuery(int page)
		{
			Page = page;
		}
	}
}
