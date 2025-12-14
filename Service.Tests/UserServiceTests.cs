using Moq;
using Service.Interfaces;
using Service.Models;

namespace Service.Tests
{
	[TestClass]
	public class UserServiceTests
	{
		private Mock<IDbQueryHelper> _dbQueryHelper = null!;
		private Mock<UserService> _serviceToTest = null!;

		[TestInitialize]
		public void TestInitialize()
		{
			_dbQueryHelper = new Mock<IDbQueryHelper>();
			_serviceToTest = new Mock<UserService>(_dbQueryHelper.Object);
		}

		[TestMethod]
		public void GetUsers_Returns_List_And_Calls_QueryList()
		{
			// Arrange
			var username = "bob";

			var expected = new List<User>
			{
				new() { Id = 1, Username = username, Email = "bob@example.com", IsActive = true, IsAdmin = false }
			};

			_dbQueryHelper.Setup(m => m.QueryList<User>(It.IsAny<string>()))
				.Returns(expected)
				.Verifiable();

			// Act
			var result = _serviceToTest.Object.GetUsers().ToList();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(username, result[0].Username);
		}

		[TestMethod]
		public void GetUser_Returns_User_And_Passes_Id_Parameter()
		{
			// Arrange
			var expected = new User { Id = 42, Username = "alice", Email = "alice@example.com", IsActive = true, IsAdmin = false };

			_dbQueryHelper.Setup(m => m.QuerySingle<User>(It.IsAny<string>(), It.IsAny<object>()))
				  .Returns(expected)
				  .Verifiable();

			// Act
			var result = _serviceToTest.Object.GetUser(42);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(42, result!.Id);
		}

		[TestMethod]
		public void InsertUser_Returns_New_Id_And_Passes_Parameters()
		{
			// Arrange
			_dbQueryHelper.Setup(m => m.QueryScalar<long?>(It.IsAny<string>(), It.IsAny<object>()))
				  .Returns(123L)
				  .Verifiable();

			// Act
			var id = _serviceToTest.Object.InsertUser("newuser", "n@u.com", "ignored");

			// Assert
			Assert.AreEqual(123L, id);
		}

		[TestMethod]
		public void UpdateUser_Calls_Execute_And_Returns_Id()
		{
			// Arrange
			var user = new User { Id = 5, Username = "u", Email = "e", IsActive = true, IsAdmin = true };

			_dbQueryHelper.Setup(m => m.Execute(It.IsAny<string>(), It.IsAny<object>()))
				  .Verifiable();

			// Act
			var returned = _serviceToTest.Object.UpdateUser(user, userId: 99);

			// Assert
			Assert.AreEqual(5, returned);
		}

		[TestMethod]
		public void DeleteUser_Calls_Execute_And_Passes_Parameters()
		{
			// Arrange
			_dbQueryHelper.Setup(m => m.Execute(It.IsAny<string>(), It.IsAny<object>()))
				.Verifiable();

			// Act
			_serviceToTest.Object.DeleteUser(7, userId: 100);

			// Assert
			_dbQueryHelper.Verify(x => x.Execute(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
		}
	}
}