using Magnum_API_web_application.Models;
using MediatR;

namespace Magnum_API_web_application.Command.Competition_Commands
{
	public class DeleteCompetitionCommand : IRequest<ApiResponse>
	{
		public int Id { get; set; }

        public DeleteCompetitionCommand(int memberId)
        {
            Id = memberId;
        }
    }
}
