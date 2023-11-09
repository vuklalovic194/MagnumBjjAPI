using System;
using System.Threading.Tasks;
using Magnum_API_web_application.Models.DTO;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Repository.IRepository;
using Magnum_API_web_application.Service;
using Moq;
using Xunit;
using Magnum_API_web_application; // Replace with the actual namespace of your code
using Magnum_API_web_application.Handler;
using Magnum_API_web_application.Command;
using System.Linq.Expressions;

public class MemberServiceTests
{
	//[Fact]
	//public async Task CreateMember_WhenMemberDoesNotExist_ShouldCreateAndReturnApiResponse()
	//{
	//	// Arrange
	//	var repositoryMock = new Mock<IMemberRepository>();
	//	var memberService = new MemberService(repositoryMock.Object); // Replace with your actual service

	//	MemberDTO memberDTO = new MemberDTO();
	//	memberDTO.Name = "NewMemberName";
		

	//	repositoryMock
	//		.Setup(repo => repo.GetByIdAsync(It.IsAny<Func<Member, bool>>(), null, true))
	//		.ReturnsAsync((Member)null); // Simulate that no member with the same name exists

	//	// Act
	//	var result = await memberService.CreateMemberIfValidAsync(memberDTO);

	//	// Assert
	//	repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Member>()), Times.Once); // Verify CreateAsync was called
	//	repositoryMock.Verify(repo => repo.SaveAsync(), Times.Once); // Verify SaveAsync was called
	//	Assert.IsType<ApiResponse<MemberDTO>>(result); // Check that the result is of the correct type
	//	var response = result as ApiResponse<MemberDTO>;
	//	Assert.Equal(request.MemberDTO, response.Data); // Check that the response contains the correct data
	//}

	//[Fact]
	//public async Task CreateMember_WhenMemberExists_ShouldReturnBadRequestApiResponse()
	//{
	//	// Arrange
	//	var repositoryMock = new Mock<IMemberRepository>();
	//	var memberService = new MemberService(repositoryMock.Object); // Replace with your actual service

	//	MemberDTO memberDTO = new MemberDTO();
	//	memberDTO.Name = "NewMemberName";

	//	repositoryMock
	//		.Setup(repo => repo.GetByIdAsync()
	//		.ReturnsAsync(new Member()); // Simulate that a member with the same name exists

	//	// Act
	//	var result = await memberService.CreateMember(request);

	//	// Assert
	//	repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Member>()), Times.Never); // Verify CreateAsync was not called
	//	repositoryMock.Verify(repo => repo.SaveAsync(), Times.Never); // Verify SaveAsync was not called
	//	Assert.IsType<ApiResponse>(result); // Check that the result is of the correct type
	//	var response = result as ApiResponse;
	//	Assert.Equal("Member with same name already exists", response.ErrorMessage); // Check the error message
	//	Assert.Equal(400, response.StatusCode); // Check the status code
	//}
}
