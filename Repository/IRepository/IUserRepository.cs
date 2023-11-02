using Magnum_API_web_application.Models.DTO;

namespace Magnum_API_web_application.Repository.IRepository
{
	public interface IUserRepository
	{
		bool IsUnique(string username);
		Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
	}
}
