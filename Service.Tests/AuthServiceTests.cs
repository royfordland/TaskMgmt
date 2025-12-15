using Moq;
using Service.Interfaces;
using Service.Models;

namespace Service.Tests
{
	[TestClass]
	public class AuthServiceTests
	{
		private Mock<IDbQueryHelper> _dbQueryHelper = null!;
		private Mock<IUserService> _userService = null!;
		private Mock<AuthService> _serviceToTest = null!;

		[TestInitialize]
		public void TestInitialize()
		{
			_dbQueryHelper = new Mock<IDbQueryHelper>();
			_userService = new Mock<IUserService>();
			_serviceToTest = new Mock<AuthService>(_dbQueryHelper.Object, _userService.Object);
		}

		[TestMethod]
		public void RegisterUser_Delegates_To_UserService_InsertUser()
		{
			// Arrange
			_userService.Setup(m => m.InsertUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
				.Returns(55L)
				.Verifiable();

			// Act
			var id = _serviceToTest.Object.RegisterUser("u", "e@x.com", "pw");

			// Assert
			Assert.AreEqual(55L, id);
			_userService.Verify(x => x.InsertUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
		}

		[TestMethod]
		public void GetUser_Returns_User_From_DbQueryHelper()
		{
			// Arrange
			var expected = new User { Id = 7, Username = "bob", Email = "b@b.com", Password = "p", IsActive = true, IsAdmin = false };

			_dbQueryHelper.Setup(m => m.QuerySingle<User>(It.IsAny<string>(), It.IsAny<object>()))
				.Returns(expected)
				.Verifiable();

			// Act
			var result = _serviceToTest.Object.GetUser("bob");

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(7, result!.Id);
		}
	}
}