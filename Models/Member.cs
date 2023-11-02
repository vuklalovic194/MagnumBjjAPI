using System.ComponentModel.DataAnnotations;

namespace Magnum_API_web_application.Models
{
	public class Member
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		[MaxLength(100)]
		public string Address { get; set; }
		public string ImageUrl { get; set; }
		public int PhoneNumber { get; set; }
		[Required]
		public int Age { get; set; }
		public DateTime DateUpdated { get; set; }
		public DateTime DateCreated { get; set; } = DateTime.UtcNow;
		public int RankId { get; set; }
		
		public DateTime LastActiveDate { get; set; }
		public List<DateTime> ActiveDates { get; set; }

		//navigation properties

		public ICollection<Fee> Fee { get; set; }
		public ICollection<TrainingSession> TrainingSession { get; set; }
		public ICollection<ActiveMember> ActiveMember{ get; set; }
		public ICollection<UnpaidMonth> UnpaidMonth { get; set; }
		public Rank Rank { get; set; }
		public ICollection<Competition_Member_Result> Competition_Member_Result { get; set; }
	}
}
