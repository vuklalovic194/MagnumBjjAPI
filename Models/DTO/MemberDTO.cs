namespace Magnum_API_web_application.Models.DTO
{
	public class MemberDTO
	{
		public string Name { get; set; }
		public string Address { get; set; }
		public string ImageUrl { get; set; }
		public int PhoneNumber { get; set; }
		public int Age { get; set; }
		public int Rank { get; set; }

		public void mapMember(MemberDTO memberDTO, Member member)
		{
			member.Address = memberDTO.Address;
			member.Name = memberDTO.Name;
			member.ImageUrl = memberDTO.ImageUrl;
			member.PhoneNumber = memberDTO.PhoneNumber;
			member.Age = memberDTO.Age;
			member.RankId = memberDTO.Rank;
		}
	}
}

