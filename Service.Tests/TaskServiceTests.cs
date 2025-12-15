using Moq;
using Service.Interfaces;
using Service.Models;
using Task = Service.Models.Task;

namespace Service.Tests
{
	[TestClass]
	public class TaskServiceTests
	{
		private Mock<IDbQueryHelper> _dbQueryHelper = null!;
		private Mock<TaskService> _serviceToTest = null!;

		[TestInitialize]
		public void TestInitialize()
		{
			_dbQueryHelper = new Mock<IDbQueryHelper>();
			_serviceToTest = new Mock<TaskService>(_dbQueryHelper.Object);
		}

		[TestMethod]
		public void GetTasks_Returns_List_And_Calls_QueryList()
		{
			// Arrange
			var expected = new List<Task>
			{
				new() { Id = 1, Title = "T1", Description = "d", Status = "open", AssignedUser = "u" }
			};

			_dbQueryHelper.Setup(m => m.QueryList<Task>(It.IsAny<string>()))
				.Returns(expected)
				.Verifiable();

			// Act
			var result = _serviceToTest.Object.GetTasks().ToList();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("T1", result[0].Title);
		}

		[TestMethod]
		public void GetTask_Returns_UpsertTask_And_Passes_Id()
		{
			// Arrange
			var expected = new UpsertTask { Id = 1, Title = "X", Description = "d", StatusId = 2, AssignedUserId = 3 };

			_dbQueryHelper.Setup(m => m.QuerySingle<UpsertTask>(It.IsAny<string>(), It.IsAny<object>()))
				.Returns(expected)
				.Verifiable();

			// Act
			var result = _serviceToTest.Object.GetTask(9);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result!.Id);
		}

		[TestMethod]
		public void InsertTask_Returns_New_Id()
		{
			// Arrange
			_dbQueryHelper.Setup(m => m.QueryScalar<long>(It.IsAny<string>(), It.IsAny<object>()))
				.Returns(101L)
				.Verifiable();

			var task = new UpsertTask { Title = "new task", Description = "the description", StatusId = 1, AssignedUserId = 2 };

			// Act
			var id = _serviceToTest.Object.InsertTask(task, userId: 5);

			// Assert
			Assert.AreEqual(101L, id);
		}

		[TestMethod]
		public void UpdateTask_Calls_Execute_And_Returns_Id()
		{
			// Arrange
			var task = new UpsertTask { Id = 11, Title = "updated title", Description = "updated description", StatusId = 1, AssignedUserId = 2 };

			_dbQueryHelper.Setup(m => m.Execute(It.IsAny<string>(), It.IsAny<object>()))
				.Verifiable();

			// Act
			var returned = _serviceToTest.Object.UpdateTask(task, userId: 77);

			// Assert
			Assert.AreEqual(11, returned);
			_dbQueryHelper.Verify(x => x.Execute(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
		}
	}
}