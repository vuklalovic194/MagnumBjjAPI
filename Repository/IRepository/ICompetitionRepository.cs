using Magnum_API_web_application.Models;

namespace Magnum_API_web_application.Repository.IRepository
{
	public interface ICompetitionRepository : IRepository<Competition>
	{
		Task<Competition> UpdateAsync(Competition competition);
	}
}
