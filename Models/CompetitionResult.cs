namespace Magnum_API_web_application.Models
{
	public class CompetitionResult
	{
		public int Id { get; set; }
		public string Result { get; set; }
		public int CompetitionId { get; set; }
		public Member Member { get; set; }

		public DateTime DateCreated { get; set; }
		public DateTime DateUpdated { get; set; }

		public ICollection<Competition_Member_Result> Competition_Member_Result { get; set; }
	}
}
