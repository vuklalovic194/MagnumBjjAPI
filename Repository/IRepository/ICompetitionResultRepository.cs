using Magnum_API_web_application.Models;

namespace Magnum_API_web_application.Repository.IRepository
{
	public interface ICompetitionResultRepository : IRepository<CompetitionResult>
	{
		public Task<CompetitionResult> UpdateAsync(CompetitionResult competitionResult);
	}
}
