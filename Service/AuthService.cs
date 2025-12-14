using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	public class AuthService(IDbQueryHelper dbQueryHelper, IUserService userService) : IAuthService
	{
		public long? RegisterUser(string username, string email, string password)
		{
			return userService.InsertUser(username, email, password);
		}

		public User? GetUser(string username)
		{
			var sql = $@"
				SELECT id, username, email, password, is_active, is_admin
				FROM taskmgmt.public.""user""
				WHERE username = @username ";

			return dbQueryHelper.QuerySingle<User>(sql, new { username });
		}
	}
}