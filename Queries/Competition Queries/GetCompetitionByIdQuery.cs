using Magnum_API_web_application.Models;
using MediatR;

namespace Magnum_API_web_application.Queries.Competition_Queries
{
	public class GetCompetitionByIdQuery : IRequest<ApiResponse>
	{
        public int Id { get; set; }

        public GetCompetitionByIdQuery(int competitionId)
        {
            Id = competitionId;
        }
    }
}
