using Magnum_API_web_application.Data;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Repository.IRepository;

namespace Magnum_API_web_application.Repository
{
	public class CompetitionResultRepository : Repository<CompetitionResult>, ICompetitionResultRepository
	{
		private readonly ApplicationDbContext _context;

		public CompetitionResultRepository(ApplicationDbContext context) : base(context) 
		{
			_context = context;
		}

		public async Task<CompetitionResult> UpdateAsync(CompetitionResult competitionResult)
		{
			competitionResult.DateUpdated = DateTime.UtcNow;
			_context.Update(competitionResult);
			_context.SaveChanges();
			return competitionResult;
		}
	}
}
