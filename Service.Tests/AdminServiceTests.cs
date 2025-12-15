using Moq;
using Service.Interfaces;

namespace Service.Tests
{
	[TestClass]
	public class AdminServiceTests
	{
		private Mock<IDbQueryHelper> _dbQueryHelper = null!;
		private Mock<AdminService> _serviceToTest = null!;

		[TestInitialize]
		public void TestInitialize()
		{
			_dbQueryHelper = new Mock<IDbQueryHelper>();
			_serviceToTest = new Mock<AdminService>(_dbQueryHelper.Object);
		}

		[TestMethod]
		public void UpdateUsername_Calls_Execute_With_Parameters()
		{
			// Arrange
			_dbQueryHelper.Setup(m => m.Execute(It.IsAny<string>(), It.IsAny<object>()))
				.Verifiable();

			// Act
			_serviceToTest.Object.UpdateUsername("newname", id: 12, userId: 99);

			// Assert
			_dbQueryHelper.Verify(x => x.Execute(It.IsAny<string>(), It.IsAny<object>()), Times.Once);
		}
	}
}