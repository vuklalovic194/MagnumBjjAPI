using Magnum_API_web_application.Models;
using Magnum_API_web_application.Repository.IRepository;
using Magnum_API_web_application.Service.IServices;
using System.Net;

namespace Magnum_API_web_application.Service
{
	public class FeeService : IFeeService
	{
		public ApiResponse _apiResponse;
		private readonly IFeeRepository _repository;
		private readonly IUnpaidMonthRepository _unpaidMonthRepository;

		public FeeService(
			IFeeRepository repository,
			IUnpaidMonthRepository unpaidMonthRepository)
		{
			_apiResponse = new ApiResponse();
			_repository = repository;
			_unpaidMonthRepository = unpaidMonthRepository;
		}

		public async Task<ApiResponse> CreateFeeAsync(int memberId)
		{
			try
			{
				List<UnpaidMonth> unpaidMonths = await _unpaidMonthRepository.GetAllAsync(u => u.MemberId == memberId);
				Fee fee = new Fee();

				if (unpaidMonths.Count > 0)
				{
					fee = fee.CreateFee(memberId);

					await _repository.CreateAsync(fee);
					await _unpaidMonthRepository.DeleteAsync(unpaidMonths[0]);
					await _repository.SaveAsync();

					return _apiResponse.Create(fee);
				}

				_apiResponse.NotFound(fee);
				_apiResponse.ErrorMessage = "This member has no debt";
				return _apiResponse;
			}
			catch (Exception e)
			{
				return _apiResponse.Unauthorize(e);
			}
		}

		public async Task<ApiResponse> DeleteFeeAsync(int memberId)
		{
			try
			{
				List<Fee> fees = await _repository.GetAllAsync(u => u.MemberId == memberId);
				if (fees.Count <= 0)
				{
					return _apiResponse.NotFound(fees);
				}

				await _repository.DeleteAsync(fees.Last());
				await _repository.SaveAsync();

				_apiResponse.StatusCode = HttpStatusCode.NoContent;
				return _apiResponse;
			}
			catch (Exception e)
			{
				return _apiResponse.Unauthorize(e);
			}
		}
		
		public async Task<ApiResponse> GetFeesByMemberIdAsync(int memberId)
		{
			try
			{
				List<Fee> feeList = await _repository.GetAllAsync(u => u.MemberId == memberId);

				if (memberId == 0)
				{
					_apiResponse.Response = await _repository.GetAllAsync();
					return _apiResponse;
				}

				if (feeList.Count != 0)
				{
					return _apiResponse.Get(feeList);
				}

				return _apiResponse.NotFound(feeList);
			}
			catch (Exception e)
			{
				return _apiResponse.Unauthorize(e);
			}
		}
	}
}
