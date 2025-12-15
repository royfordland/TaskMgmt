using Moq;
using Service.Interfaces;
using TaskStatus = Service.Models.TaskStatus;

namespace Service.Tests
{
	[TestClass]
	public class TaskStatusServiceTests
	{
		private Mock<IDbQueryHelper> _dbQueryHelper = null!;
		private Mock<TaskStatusService> _serviceToTest = null!;

		[TestInitialize]
		public void TestInitialize()
		{
			_dbQueryHelper = new Mock<IDbQueryHelper>();
			_serviceToTest = new Mock<TaskStatusService>(_dbQueryHelper.Object);
		}

		[TestMethod]
		public void GetStatuses_Returns_List_And_Calls_QueryList()
		{
			// Arrange
			var expected = new List<TaskStatus>
			{
				new() { Id = 1, Status = "Open", IsActive = true }
			};

			_dbQueryHelper.Setup(m => m.QueryList<TaskStatus>(It.IsAny<string>()))
				.Returns(expected)
				.Verifiable();

			// Act
			var result = _serviceToTest.Object.GetStatuses().ToList();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("Open", result[0].Status);
		}

		[TestMethod]
		public void GetStatus_Returns_Status_And_Passes_Id()
		{
			// Arrange
			var expected = new TaskStatus { Id = 3, Status = "Done", IsActive = true };

			_dbQueryHelper.Setup(m => m.QuerySingle<TaskStatus>(It.IsAny<string>(), It.IsAny<object>()))
				.Returns(expected)
				.Verifiable();

			// Act
			var result = _serviceToTest.Object.GetStatus(3);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result!.Id);
		}

		[TestMethod]
		public void InsertStatus_Returns_New_Id()
		{
			// Arrange
			_dbQueryHelper.Setup(m => m.QueryScalar<long>(It.IsAny<string>(), It.IsAny<object>()))
				.Returns(900L)
				.Verifiable();

			// Act
			var id = _serviceToTest.Object.InsertStatus("New", userId: 4);

			// Assert
			Assert.AreEqual(900L, id);
		}

		[TestMethod]
		public void UpdateStatus_Calls_Execute_And_Returns_Id()
		{
			// Arrange
			var status = new TaskStatus { Id = 5, Status = "S", IsActive = false };

			_dbQueryHelper.Setup(m => m.Execute(It.IsAny<string>(), It.IsAny<object>()))
				.Verifiable();

			// Act
			var returned = _serviceToTest.Object.UpdateStatus(status, userId: 10);

			// Assert
			Assert.AreEqual(5, returned);
			_dbQueryHelper.Verify(x => x.Execute(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
		}

		[TestMethod]
		public void DeleteStatus_Calls_Execute()
		{
			// Arrange
			_dbQueryHelper.Setup(m => m.Execute(It.IsAny<string>(), It.IsAny<object>()))
				.Verifiable();

			// Act
			_serviceToTest.Object.DeleteStatus(8, userId: 11);

			// Assert
			_dbQueryHelper.Verify(x => x.Execute(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
		}
	}
}