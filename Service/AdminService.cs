using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	public class AdminService(IDbQueryHelper dbQueryHelper) : IAdminService
	{
		/// <summary>
		/// Some sort of admin specific functionality that other services or users don't have access to.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="id"></param>
		/// <param name="userId"></param>
		public void UpdateUsername(string name, int id, int userId)
		{
			var sql = @"
				UPDATE taskmgmt.public.user
				SET username = @name,
					updated_dt = NOW(),
					updated_by = @userId
				WHERE id = @id ";

			dbQueryHelper.Execute(sql, new { name, userId, id });
		}
	}
}