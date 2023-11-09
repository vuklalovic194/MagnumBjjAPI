using Xunit;

namespace Magnum.API.Unittest.Members.Command
{
	public class CreateMemberCommandHandlerTest
	{
		[Fact]
		public void Handle_Should_ReturnFailure_WhenNameIsNotUnique()
		{
			//Arange

			var command = new CreateMemberCommand();

			//Act
			
			//Assert
		}
	}
}
