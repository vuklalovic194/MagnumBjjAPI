using Magnum_API_web_application.Data;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Repository.IRepository;

namespace Magnum_API_web_application.Repository
{
	public class CompetitionRepository : Repository<Competition>, ICompetitionRepository
	{
		private readonly ApplicationDbContext _context;

		public CompetitionRepository(ApplicationDbContext context) : base(context)
        {
			_context = context;
		}

        public async Task<Competition> UpdateAsync(Competition competition)
		{
			competition.DateUpdated = DateTime.UtcNow;
			_context.Update(competition);
			_context.SaveChanges();
			return competition;
		}
	}
}
