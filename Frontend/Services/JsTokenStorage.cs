using Frontend.Services.Interfaces;
using Microsoft.JSInterop;

namespace Frontend.Services
{
	public class JsTokenStorage(IJSRuntime js) : IJsTokenStorage
	{
		public Task<string?> GetTokenAsync(string key)
		{
			return js.InvokeAsync<string?>("localStorage.getItem", key).AsTask();
		}

		public Task SetTokenAsync(string key, string value)
		{
			return js.InvokeVoidAsync("localStorage.setItem", key, value).AsTask();
		}

		public Task RemoveTokenAsync(string key)
		{
			return js.InvokeVoidAsync("localStorage.removeItem", key).AsTask();
		}
	}
}