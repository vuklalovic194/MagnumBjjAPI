using Magnum_API_web_application.Models;
using MediatR;

namespace Magnum_API_web_application.Queries.Training_Queries
{
	public class GetSessionsHistoryQuery : IRequest<ApiResponse>
	{
		public int Id { get; }

		public GetSessionsHistoryQuery(int id)
		{
			Id = id;
		}
	}
}
