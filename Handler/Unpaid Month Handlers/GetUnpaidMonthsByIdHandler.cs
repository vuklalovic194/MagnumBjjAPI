using Magnum_API_web_application.Models;
using Magnum_API_web_application.Queries.Unpaid_Month_Queries;
using Magnum_API_web_application.Repository.IRepository;
using MediatR;

namespace Magnum_API_web_application.Handler.Unpaid_Month_Handlers
{
	public class GetUnpaidMonthsByIdHandler : IRequestHandler<GetUnpaidMonthsByIdQuery, ApiResponse>
	{
		private readonly IUnpaidMonthRepository _unpaidMonthRepository;
		private ApiResponse _apiResponse;

		public GetUnpaidMonthsByIdHandler(IUnpaidMonthRepository unpaidMonthRepository)
		{
			_unpaidMonthRepository = unpaidMonthRepository;
			_apiResponse = new ApiResponse();
		}

		public async Task<ApiResponse> Handle(GetUnpaidMonthsByIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				List<UnpaidMonth> unpaidMonths = await _unpaidMonthRepository.GetAllAsync(u => u.MemberId == request.MemberId);
				if (unpaidMonths.Count != 0)
				{
					_apiResponse.Get(unpaidMonths);
					return _apiResponse;
				}
				_apiResponse.NotFound(unpaidMonths);
				return _apiResponse;
			}
			catch (Exception e)
			{
				return _apiResponse.Unauthorize(e);
			}
		}
	}
}
