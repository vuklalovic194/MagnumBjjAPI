using Magnum_API_web_application.Models;
using MediatR;

namespace Magnum_API_web_application.Command.Competition_Result_Commands
{
	public class CreateCompetitionResultCommand : IRequest<ApiResponse>
	{
		public CompetitionResult CompetitionResult { get; set; }

		public CreateCompetitionResultCommand(CompetitionResult competitionResult)
		{
			CompetitionResult = competitionResult;
		}
	}
}
