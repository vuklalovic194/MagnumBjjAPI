using AutoMapper.Execution;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Queries.Fee_Queries;
using Magnum_API_web_application.Repository.IRepository;
using MediatR;

namespace Magnum_API_web_application.Handler.Fee_Handlers
{
	public class GetFeesByIdHandler : IRequestHandler<GetFeesByIdQuery, ApiResponse>
	{
		private readonly IFeeRepository _feeRepository;
		public ApiResponse _apiResponse;

		public GetFeesByIdHandler(IFeeRepository feeRepository)
		{
			_feeRepository = feeRepository;
			_apiResponse = new ApiResponse();
		}

		public async Task<ApiResponse> Handle(GetFeesByIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				List<Fee> feeList = await _feeRepository.GetAllAsync(u => u.MemberId == request.MemberId);

				if (request.MemberId == 0)
				{
					_apiResponse.Response = await _feeRepository.GetAllAsync();
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
