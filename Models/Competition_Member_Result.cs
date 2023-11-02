namespace Magnum_API_web_application.Models
{
	public class Competition_Member_Result
	{
		public int Id { get; set; }

		public int MemberId { get; set; }
		public int CompetitionId { get; set; }
		public int ResultId { get; set; }

		public Member Member { get; set; }
		public Competition Competition { get; set; }
		public CompetitionResult CompetitionResult { get; set; }
	}
}
