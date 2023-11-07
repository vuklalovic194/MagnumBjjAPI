using Magnum_API_web_application.Command.Training_Commands;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Repository.IRepository;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace Magnum_API_web_application.Handler.Training_Handlers
{
	public class CreateTrainingSessionHandler : IRequestHandler<CreateTrainingSessionCommand, ApiResponse>
	{
		private readonly IActiveMemberRepository _activeMemberRepository;
		private readonly IMemberRepository _memberRepository;
		private readonly ITrainingSessionRepository _trainingSessionRepository;
		private readonly IUnpaidMonthRepository _unpaidMonthRepository;
		private readonly IFeeRepository _feeRepository;
		private ApiResponse _apiResponse;

		public CreateTrainingSessionHandler(
			IActiveMemberRepository activeMemberRepository,
			IMemberRepository memberRepository,
			ITrainingSessionRepository trainingSessionRepository,
			IUnpaidMonthRepository unpaidMonthRepository,
			IFeeRepository feeRepository)
		{
			_activeMemberRepository = activeMemberRepository;
			_memberRepository = memberRepository;
			_trainingSessionRepository = trainingSessionRepository;
			_unpaidMonthRepository = unpaidMonthRepository;
			_feeRepository = feeRepository;
			_apiResponse = new ApiResponse();
		}

		public async Task<ApiResponse> Handle(CreateTrainingSessionCommand request, CancellationToken cancellationToken)
		{
			foreach (var memberId in request.MembersId)
			{
				if (await _memberRepository.GetByIdAsync(u => u.Id == memberId) == null)
				{
					return _apiResponse.BadRequest();
				}

				List<ActiveMember> activeMemberByMonths = await _activeMemberRepository.GetAllAsync(u => u.MemberId == memberId);
				List<TrainingSession> trainingSessions = await _trainingSessionRepository.GetAllAsync(u => u.MemberId == memberId);
				List<UnpaidMonth> unpaidMonths = await _unpaidMonthRepository.GetAllAsync(tracked: false);
				List<Fee> fees = await _feeRepository.GetAllAsync(u => u.MemberId == memberId);
				List<ActiveMember> listToAdd = new List<ActiveMember>();

				//Establishing if member trained more than 2 times per each month, and adding him to activeMembers list if true
				await CreateActiveMemberAsync(memberId, activeMemberByMonths, trainingSessions, listToAdd);

				//Comparing list of active months and list of paid fees, if its unmached, add unpaid month to list
				await CreateUnpaidMonthAsync(memberId, activeMemberByMonths, fees, unpaidMonths);

				TrainingSession trainingSession = new()
				{
					MemberId = memberId,
					SessionDate = DateTime.UtcNow
				};

				await _trainingSessionRepository.CreateAsync(trainingSession);
			}

			_apiResponse.StatusCode = HttpStatusCode.Created;
			return _apiResponse;
		}

		public async Task<bool> CreateActiveMemberAsync(
			int id,
			List<ActiveMember> activeMemberByMonths,
			List<TrainingSession> trainingSessions,
			List<ActiveMember> listToAdd)
		{
			try
			{
				bool isActive;
				var distinctMonths = trainingSessions
					.Select(session => new DateTime(session.SessionDate.Year, session.SessionDate.Month, 1))
					.Distinct();

				foreach (var month in distinctMonths)
				{
					isActive = trainingSessions
						.Count(session => new DateTime(session.SessionDate.Year, session.SessionDate.Month, 1) == month) >= 2;

					if (isActive)
					{
						ActiveMember newMember = new()
						{
							MemberId = id,
							Month = month,
						};

						int memberIdToCheck = id;
						DateTime monthToCheck = month;

						bool containsMember = activeMemberByMonths.Any(member =>
							member.MemberId == memberIdToCheck && member.Month == monthToCheck);

						if (!containsMember)
						{
							listToAdd.Add(newMember);

							await _activeMemberRepository.CreateAsync(newMember);
						}
					}
				}
				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<bool> CreateUnpaidMonthAsync(
			int member,
			List<ActiveMember> activeMemberByMonths,
			List<Fee> fees,
			List<UnpaidMonth> unpaidMonths)
		{
			List<DateTime> listOfMonth = new List<DateTime>();
			List<DateTime> listOfFees = new List<DateTime>();

			try
			{
				for (int i = 0; i < activeMemberByMonths.Count; i++)
				{
					listOfMonth.Add(activeMemberByMonths[i].Month);
				}
				for (int j = 0; j < fees.Count; j++)
				{
					listOfFees.Add(fees[j].DatePaid);
				}

				var unmatchedMonths = listOfMonth.Except(listOfFees).ToList();

				foreach (var date in unmatchedMonths)
				{
					if (!unpaidMonths.Any(unpaid => unpaid.Month == date && unpaid.MemberId == member))
					{
						UnpaidMonth unpaidMonth = new()
						{
							Month = date,
							MemberId = member,
						};

						await _unpaidMonthRepository.CreateAsync(unpaidMonth);
					}
				}
				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
