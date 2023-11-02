﻿using Magnum_API_web_application.Models;

namespace Magnum_API_web_application.Repository.IRepository
{
	public interface IActiveMemberRepository : IRepository<ActiveMember>
	{
		Task <List<ActiveMember>> GroupAndCreateActiveMembers(List<ActiveMember> activeMembers, List<TrainingSession> trainingSession, ActiveMember activeMember);
	}
}
