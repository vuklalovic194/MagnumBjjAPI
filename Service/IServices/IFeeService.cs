using Magnum_API_web_application.Models;

namespace Magnum_API_web_application.Service.IServices
{
	public interface IFeeService
	{
		public Task<ApiResponse> GetFeesByMemberIdAsync(int memberId);
		public Task<ApiResponse> CreateFeeAsync(int memberId);
		public Task<ApiResponse> DeleteFeeAsync(int memberId);
	}
}
