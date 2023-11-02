using Magnum_API_web_application.Models;
using Magnum_API_web_application.Repository.IRepository;
using Magnum_API_web_application.Service.IServices;

namespace Magnum_API_web_application.Service
{
    public class UnpaidMonthService : IUnpaidMonthService
    {
        private readonly IUnpaidMonthRepository _unpaidMonthRepository;
        private readonly IActiveMemberRepository _activeMemberRepository;
        private readonly IFeeRepository _feeRepository;
        private ApiResponse _apiResponse;

        public UnpaidMonthService(
            IUnpaidMonthRepository unpaidMonthRepository,
            IActiveMemberRepository activeMemberRepository,
            IFeeRepository feeRepository
            )
        {
            _unpaidMonthRepository = unpaidMonthRepository;
            _activeMemberRepository = activeMemberRepository;
            _feeRepository = feeRepository;
			_apiResponse = new();
        }

        public async Task<ApiResponse> GetAllUnpaidMonthsAsync()
        {
            List<UnpaidMonth> unpaidMonths = await _unpaidMonthRepository.GetAllAsync();

            if (unpaidMonths.Count != 0)
            {
				_apiResponse.Get(unpaidMonths);
                return _apiResponse;
            }
			_apiResponse.NotFound(unpaidMonths);
            return _apiResponse;
        }

        public async Task<ApiResponse> GetUnpaidMonthsById(int memberId)
        {
            List<UnpaidMonth> unpaidMonths = await _unpaidMonthRepository.GetAllAsync(u => u.MemberId == memberId);
            if (unpaidMonths.Count != 0)
            {
                _apiResponse.Get(unpaidMonths);
                return _apiResponse;
            }
            _apiResponse.NotFound(unpaidMonths);
            return _apiResponse;
        }
    }
}
