
namespace Magnum_API_web_application.Models
{
	public class Competition
	{
		public int Id { get; set; }
		public string Place { get; set; }
		public string Organisation { get; set; }
		public DateTime Date { get; set; }

		public DateTime DateCreated { get; set; }
		public DateTime DateUpdated { get; set; }

		public ICollection<Competition_Member_Result> Competition_Member_Result { get; set; }
		enum Results
		{
			Gold,
			Silver,
			Bronze,
			Participation,

			GoldAbs,
			SilverAbs,
			BronzeAbs,
			ParticipationAbs
		}
	}
}
