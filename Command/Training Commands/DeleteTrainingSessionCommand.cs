using Magnum_API_web_application.Models;
using MediatR;

namespace Magnum_API_web_application.Command.Training_Commands
{
	public class DeleteTrainingSessionCommand : IRequest<ApiResponse>
	{
		public DateTime Date { get; }

		public DeleteTrainingSessionCommand(DateTime date)
		{
			Date = date;
		}
	}
}
