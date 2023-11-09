using Magnum_API_web_application.Command.Fee_Commands;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Repository.IRepository;
using MediatR;

namespace Magnum_API_web_application.Handler.Fee_Handlers
{
	public class CreateFeeHandler : IRequestHandler<CreateFeeCommand, ApiResponse>
	{
		private readonly IUnpaidMonthRepository _unpaidMonthRepository;
		private readonly IFeeRepository _feeRepository;
		public ApiResponse _apiResponse;

		public CreateFeeHandler(IFeeRepository feeRepository, IUnpaidMonthRepository unpaidMonthRepository)
		{
			_feeRepository = feeRepository;
			_unpaidMonthRepository = unpaidMonthRepository;
			_apiResponse = new ApiResponse();
		}

		public async Task<ApiResponse> Handle(CreateFeeCommand request, CancellationToken cancellationToken)
		{
			try
			{
				List<UnpaidMonth> unpaidMonths = await _unpaidMonthRepository.GetAllAsync(u => u.MemberId == request.MemberId);
				Fee fee = new Fee();

				if (unpaidMonths.Count > 0)
				{
					fee = fee.CreateFee(request.MemberId);

					await _feeRepository.CreateAsync(fee);
					await _unpaidMonthRepository.DeleteAsync(unpaidMonths[0]);
					await _feeRepository.SaveAsync();

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
	}
}
