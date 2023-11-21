namespace Magnum_API_web_application.Models.DTO
{
	public class CompetitionDTO
	{
		public string Place { get; set; }
		public string Organisation { get; set; }
		public DateTime Date { get; set; }

		public void CompetitionMapper(CompetitionDTO dto, Competition competition)
		{
			competition.Date = dto.Date;
			competition.Place = dto.Place;
			competition.Organisation = dto.Organisation;
			competition.DateCreated = DateTime.UtcNow;
		}
	}
}
