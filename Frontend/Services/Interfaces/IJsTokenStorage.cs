namespace Frontend.Services.Interfaces
{
	public interface IJsTokenStorage
	{
		Task<string?> GetTokenAsync(string key);

		Task SetTokenAsync(string key, string value);

		Task RemoveTokenAsync(string key);
	}
}