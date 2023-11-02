using AutoMapper;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Models.DTO;

namespace Magnum_API_web_application
{
	public class MapConfig : Profile
	{
        public MapConfig()
        {
            CreateMap<Member, MemberDTO>().ReverseMap();
            CreateMap<Fee, FeeDTO>().ReverseMap();
		}
    }
}
