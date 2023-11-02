using Microsoft.Identity.Client;

namespace Magnum_API_web_application.Models
{
	public class Competition
	{
		public int Id { get; set; }
		public string Place { get; set; }
		public string Organisation { get; set; }
		public DateTime Date { get; set; }

		public ICollection<Competition_Member_Result> Competition_Member_Result { get; set; }
	}
}
