using Magnum_API_web_application.Models;
using Magnum_API_web_application.Models.DTO;
using MediatR;

namespace Magnum_API_web_application.Queries.Competition_Results_Queries
{
	public class GetCompetitionResultsQuery : IRequest<ApiResponse>
	{
		public CompetitionResultDTO CompetitionResultDTO { get; set; }
        
        public GetCompetitionResultsQuery(CompetitionResultDTO dto)
        {
            CompetitionResultDTO = dto;
        }
    }
}
