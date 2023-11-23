using Magnum_API_web_application.Models;
using Magnum_API_web_application.Models.DTO;
using MediatR;

namespace Magnum_API_web_application.Command.Competition_Commands
{
	public class UpdateCompetitionCommand : IRequest<ApiResponse>
	{
		public int Id { get; set; }
        public CompetitionDTO Dto { get; set; }

        public UpdateCompetitionCommand(int memberId, CompetitionDTO dto)
        {
            Dto = dto;
            Id = memberId;
        }
    }
}
