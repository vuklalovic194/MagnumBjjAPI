namespace Magnum_API_web_application.Models
{
	public class ActiveMember
	{
		public int Id { get; set; }
		public DateTime Month { get; set; }

		public Member Member { get; set; }
		public int MemberId { get; set; }
	}
}
