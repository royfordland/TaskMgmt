namespace Service.Interfaces
{
	public interface IDbQueryHelper
	{
		IEnumerable<T> QueryList<T>(string sql);

		IEnumerable<T> QueryList<T>(string sql, object parameters);
	}
}