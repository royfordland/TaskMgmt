using Service.Interfaces;
using Service.Models;

namespace Service
{
	public class UserService(IDbQueryHelper dbQueryHelper) : IUserService
	{
		public IEnumerable<User> GetUsers()
		{
			var sql = @"
				SELECT id, title, description, status_id, assigned_user_id
				FROM taskmgmt.public.task ";

			return dbQueryHelper.QueryList<User>(sql);
		}

		public int InsertUser(string username, string email, string password)
		{
			var sql = @"
				INSERT INTO taskmgmt.public.user (username, email)
				VALUES (@username, @email) ";

			var parameters = new
			{
				username,
				email
			};

			return dbQueryHelper.QueryScalar<int>(sql, parameters);
		}

		public int UpdateUser(User user, int userId)
		{
			var sql = @"
				UPDATE taskmgmt.public.user
				SET is_admin = @IsAdmin,
					is_active = @IsActive,
					updated_dt = NOW(),
					updated_by = @userId
				WHERE id = @Id ";

			var parameters = new
			{
				user.IsAdmin,
				user.IsActive,
				userId,
				user.Id
			};

			dbQueryHelper.Execute(sql, parameters);

			return user.Id;
		}
	}
}