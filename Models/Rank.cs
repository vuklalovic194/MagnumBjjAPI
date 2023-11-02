
namespace Magnum_API_web_application.Models
{
	public class Rank
	{
		public int Id { get; set; }
		public string SkillRank { get; set; }

		public ICollection<Member> Members { get; set; }
	}
}
