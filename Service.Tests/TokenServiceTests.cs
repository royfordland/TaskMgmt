using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Service.Models;

namespace Service.Tests
{
	[TestClass]
	public class TokenServiceTests
	{
		private IConfiguration _configuration = null!;
		private TokenService _serviceToTest = null!;

		[TestInitialize]
		public void TestInitialize()
		{
			var inMemory = new Dictionary<string, string?>
			{
				["Jwt:Key"] = "supersecretkey_for_tests_1234567890",
				["Jwt:Issuer"] = "test-issuer",
				["Jwt:Audience"] = "test-audience",
				["Jwt:ExpiryMinutes"] = "60"
			};

			_configuration = new ConfigurationBuilder()
				.AddInMemoryCollection(inMemory!)
				.Build();

			_serviceToTest = new TokenService(_configuration);
		}

		[TestMethod]
		public void CreateToken_Returns_Valid_Jwt_With_Expected_Claims()
		{
			// Arrange
			var user = new User { Id = 123, Username = "bob", Email = "bob@example.com", IsAdmin = true };

			// Act
			var token = _serviceToTest.CreateToken(user);

			// Assert
			Assert.IsFalse(string.IsNullOrEmpty(token));

			var handler = new JwtSecurityTokenHandler();
			var jwt = handler.ReadJwtToken(token);

			Assert.IsNotNull(jwt);
			// sub claim
			var sub = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
			Assert.IsNotNull(sub);
			Assert.AreEqual(user.Id.ToString(), sub!.Value);

			// unique name
			var uniqueName = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.UniqueName);
			Assert.IsNotNull(uniqueName);
			Assert.AreEqual(user.Username, uniqueName!.Value);

			// email
			var email = jwt.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email);
			Assert.IsNotNull(email);
			Assert.AreEqual(user.Email, email!.Value);

			// is_admin custom claim
			var isAdmin = jwt.Claims.FirstOrDefault(c => c.Type == "is_admin");
			Assert.IsNotNull(isAdmin);
			Assert.AreEqual("1", isAdmin!.Value);
		}
	}
}