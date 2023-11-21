using Magnum_API_web_application.Models;
using Magnum_API_web_application.Models.DTO;
using MediatR;

namespace Magnum_API_web_application.Command.Competition_Commands
{
	public class CreateCompetitionCommand : IRequest<ApiResponse>
	{
		public CompetitionDTO CompetitionDTO { get; set; }

        public CreateCompetitionCommand(CompetitionDTO competitionDTO)
        {
            CompetitionDTO = competitionDTO;
        }
    }
}
