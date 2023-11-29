namespace Magnum_API_web_application.Models.DTO
{
	public class CompetitionResultDTO
	{
		public int Id { get; set; }
		public string Result { get; set; }
		public int CompetitionId { get; set; }
		public List<Member> Members { get; set; }


		public void Mapper(CompetitionResult result, CompetitionResultDTO dto)
		{
			result.Id = dto.Id;
			result.Result = dto.Result;
			result.CompetitionId = dto.CompetitionId;
			foreach (Member member in Members)
			{
				result.Member = member;
			}
		}
	}
}
