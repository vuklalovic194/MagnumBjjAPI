using Magnum_API_web_application.Models;
using MediatR;

namespace Magnum_API_web_application.Command.Training_Commands
{
	public class CreateTrainingSessionCommand : IRequest<ApiResponse>
	{
		public List<int> MembersId { get; }

		public CreateTrainingSessionCommand(List<int> membersId)
		{
			MembersId = membersId;
		}
	}
}
