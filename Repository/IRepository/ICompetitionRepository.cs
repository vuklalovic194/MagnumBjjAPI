using Magnum_API_web_application.Models;

namespace Magnum_API_web_application.Repository.IRepository
{
	public interface ICompetitionRepository : IRepository<Competition>
	{
		public Task<Competition> UpdateAsync(Competition competition);
	}
}
