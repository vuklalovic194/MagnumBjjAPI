namespace Magnum_API_web_application.Models.DTO
{
	public class MemberDTO
	{
		public string Name { get; set; }
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public string ImageUrl { get; set; }
		public int PhoneNumber { get; set; }
		public int Age { get; set; }
		public int Rank { get; set; }

		public void mapMember(MemberDTO memberDTO, Member member)
		{
			member.StreetAddress = memberDTO.StreetAddress;
			member.City = memberDTO.City;
			member.Country = memberDTO.Country;
			member.Name = memberDTO.Name;
			member.ImageUrl = memberDTO.ImageUrl;
			member.PhoneNumber = memberDTO.PhoneNumber;
			member.Age = memberDTO.Age;
			member.RankId = memberDTO.Rank;
		}
	}
}

