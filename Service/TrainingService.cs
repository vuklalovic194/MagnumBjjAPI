using Magnum_API_web_application.Models;
using Magnum_API_web_application.Repository;
using Magnum_API_web_application.Repository.IRepository;
using Magnum_API_web_application.Service.IServices;
using System.Net;

namespace Magnum_API_web_application.Service
{
	public class TrainingService : ITrainingService
	{
		public ApiResponse _apiResponse;
		private readonly ITrainingSessionRepository _trainingSessionRepository;
		private readonly IActiveMemberRepository _activeMemberRepository;
		private readonly IUnpaidMonthRepository _unpaidMonthRepository;
		private readonly IFeeRepository _feeRepository;
		private readonly IMemberRepository _memberRepository;

		public TrainingService(
			ITrainingSessionRepository trainingSessionRepository,
			IActiveMemberRepository activeMemberRepository,
			IUnpaidMonthRepository unpaidMonthRepository,
			IFeeRepository feeRepository,
			IMemberRepository memberRepository)
		{
			_apiResponse = new();
			_trainingSessionRepository = trainingSessionRepository;
			_activeMemberRepository = activeMemberRepository;
			_unpaidMonthRepository = unpaidMonthRepository;
			_feeRepository = feeRepository;
			_memberRepository = memberRepository;
		}

		public async Task<ApiResponse> CreateSessionAsync(List<int> memberIds)
		{
			foreach (var member in memberIds)
			{

				if (await _memberRepository.GetByIdAsync(u => u.Id == member) == null)
				{
					return _apiResponse.BadRequest();
				}

				List<ActiveMember> activeMemberByMonths = await _activeMemberRepository.GetAllAsync(u => u.MemberId == member);
				List<TrainingSession> trainingSessions = await _trainingSessionRepository.GetAllAsync(u => u.MemberId == member);
				List<UnpaidMonth> unpaidMonths = await _unpaidMonthRepository.GetAllAsync(tracked: false);
				List<Fee> fees = await _feeRepository.GetAllAsync(u => u.MemberId == member);
				List<ActiveMember> listToAdd = new List<ActiveMember>();

				//Create Active Member
				await CreateActiveMemberAsync(member, activeMemberByMonths, trainingSessions, listToAdd);

				//Create Unpaid Month
				await CreateUnpaidMonthAsync(member, activeMemberByMonths, fees, unpaidMonths);

				TrainingSession trainingSession = new()
				{
					MemberId = member,
					SessionDate = DateTime.UtcNow
				};

				await _trainingSessionRepository.CreateAsync(trainingSession);
			}

			_apiResponse.StatusCode = HttpStatusCode.Created;
			return _apiResponse;
		}

		public async Task<ApiResponse> DeleteSessionAsync(DateTime date)
		{
			TrainingSession trainingSession = await _trainingSessionRepository.GetByIdAsync(u => u.SessionDate == date);
			if (trainingSession == null)
			{
				return _apiResponse.NotFound(trainingSession);
			}

			await _trainingSessionRepository.DeleteAsync(trainingSession);
			await _trainingSessionRepository.SaveAsync();

			_apiResponse.StatusCode = HttpStatusCode.NoContent;
			return _apiResponse;
		}

		public async Task<ApiResponse> GetSessionHistoryAsync(int memberId)
		{
			List<TrainingSession> trainingSessions = await _trainingSessionRepository.GetAllAsync(u => u.MemberId == memberId);

			if (trainingSessions.Count != 0)
			{
				return _apiResponse.Get(trainingSessions);
			}
			return _apiResponse.NotFound(trainingSessions);
		}

		public async Task<ApiResponse> GetSessionsByMemberIdAsync(int memberId, int month = 0)
		{
			List<TrainingSession> trainingSession = await _trainingSessionRepository.GetAllAsync(u => u.MemberId == memberId);
			if (trainingSession.Count != 0)
			{
				if (month != 0)
				{
					trainingSession = await _trainingSessionRepository.GetAllAsync(u => u.SessionDate.Month == month && u.MemberId == memberId);

					return _apiResponse.Get(trainingSession.Count);
				}

				return _apiResponse.Get(trainingSession.Count);
			}

			return _apiResponse.NotFound(trainingSession);
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
