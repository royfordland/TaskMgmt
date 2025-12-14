using Service.Interfaces;
using Service.Models;

namespace Service
{
	public class UserService(IDbQueryHelper dbQueryHelper) : IUserService
	{
		// The table in the database was named "user" and needs the double quotes to avoid
		// conflicts with SQL reserved keywords. In order to avoid typos, use this const instead.
		private const string userTable = @"taskmgmt.public.""user""";

		public IEnumerable<User> GetUsers()
		{
			var sql = $@"
				SELECT id, username, email, is_active, is_admin
				FROM {userTable} ";

			return dbQueryHelper.QueryList<User>(sql);
		}

		public User? GetUser(long id)
		{
			var sql = $@"
				SELECT id, username, email, is_active, is_admin
				FROM {userTable}
				WHERE id = @id ";

			return dbQueryHelper.QuerySingle<User>(sql, new { id });
		}

		public long? InsertUser(string username, string email, string password)
		{
			var sql = $@"
				INSERT INTO {userTable} (username, email)
				VALUES (@username, @email) RETURNING ID ";

			var parameters = new
			{
				username,
				email
			};

			return dbQueryHelper.QueryScalar<long?>(sql, parameters);
		}

		public long UpdateUser(User user, long userId)
		{
			var sql = $@"
				UPDATE {userTable}
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

		public void DeleteUser(long id, long userId)
		{
			var sql = $@"
				UPDATE {userTable}
				SET is_active = false,
					updated_dt = NOW(),
					updated_by = @userId
				WHERE id = @id ";

			var parameters = new
			{
				userId,
				id
			};

			dbQueryHelper.Execute(sql, parameters);
		}
	}
}