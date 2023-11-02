using Magnum_API_web_application.Models;
using Magnum_API_web_application.Models.DTO;
using Magnum_API_web_application.Repository.IRepository;
using Magnum_API_web_application.Service.IServices;
using System.Net;

namespace Magnum_API_web_application.Service
{
    public class MemberService : IMemberService
    {
        public ApiResponse _apiResponse;
        private readonly IMemberRepository _repository;

        public MemberService(IMemberRepository repository)
        {
			_apiResponse = new();
			_repository = repository;
        }

        public async Task<ApiResponse> CreateMemberIfValidAsync(MemberDTO memberDTO)
        {
            try
            {
                if (await _repository.GetByIdAsync(u => u.Name == memberDTO.Name) != null)
                {
                    _apiResponse.BadRequest();
					_apiResponse.ErrorMessage = "Member with same name already exists";
                    return _apiResponse;
                }

                Member model = new();
                memberDTO.mapMember(memberDTO, model);

                await _repository.CreateAsync(model);
                await _repository.SaveAsync();

                return _apiResponse.Create(memberDTO);
            }
            catch (Exception e)
            {
                return _apiResponse.Unauthorize(e);
            }
        }

        public async Task<ApiResponse> DeleteMemberAsync(int memberId)
        {
            try
            {
                Member member = await _repository.GetByIdAsync(u => u.Id == memberId);
                if (member == null)
                {
                    return _apiResponse.NotFound(member);
                }

                await _repository.DeleteAsync(member);
                await _repository.SaveAsync();

				_apiResponse.StatusCode = HttpStatusCode.NoContent;
                return _apiResponse;
            }
            catch (Exception e)
            {
                return _apiResponse.Unauthorize(e);
            }
        }

        public async Task<ApiResponse> GetMemberByIdAsync(int memberId)
        {
            try
            {
                Member member = await _repository.GetByIdAsync(u => u.Id == memberId);
                if (member == null)
                {
                    return _apiResponse.NotFound(member);
                }

                return _apiResponse.Get(member);
            }
            catch (Exception e)
            {
                return _apiResponse.Unauthorize(e);
            }
        }

        public async Task<ApiResponse> GetMembersAsync()
        {
            try
            {
                List<Member> memberList = await _repository.GetAllAsync();

                if (memberList.Count <= 0)
                {
					_apiResponse.NotFound(memberList);
                }

                return _apiResponse.Get(memberList);
            }
            catch (Exception e)
            {
                return _apiResponse.Unauthorize(e);
            }
        }

        public async Task<ApiResponse> UpdateMemberAsync(MemberDTO memberDTO, int memberId)
        {
            try
            {
                Member member = await _repository.GetByIdAsync(u => u.Id == memberId);
                if (member == null)
                {
                    return _apiResponse.NotFound(member);
                }

                memberDTO.mapMember(memberDTO, member);
				await _repository.Update(member);
                await _repository.SaveAsync();

                return _apiResponse.Update(member);
            }
            catch (Exception e)
            {
                return _apiResponse.Unauthorize(e);
            }
        }
    }
}
