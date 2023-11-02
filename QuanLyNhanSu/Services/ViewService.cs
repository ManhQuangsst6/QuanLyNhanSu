using Npgsql;
using Microsoft.AspNetCore.Mvc;

using NpgsqlTypes;
using QuanLyNhanSu.Interfaces;
using QuanLyNhanSu.Models;
using System.Data;
using QuanLyNhanSu.Models.ViewModel;
using Microsoft.Extensions.Configuration;

namespace QuanLyNhanSu.Services
{
	public class ViewService : IViewService

	{
		private readonly IConfiguration _configuration;
		public ViewService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public async Task<List<View_DepartmentList>> GetView_DepartmentList()
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));

			await conn.OpenAsync();
			var views = new List<View_DepartmentList>();
			using (var cmd = new NpgsqlCommand("select * from View_DepartmentList", conn))
			{
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						string? id = reader["ID"].ToString();
						string? name = reader["Name"].ToString();

						views.Add(new View_DepartmentList { ID = id, Name = name});

					}
				}
			}
			return views;
		}

		public async Task<List<View_PositionList>> GetView_PositionList()
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));

			await conn.OpenAsync();
			var views = new List<View_PositionList>();
			using (var cmd = new NpgsqlCommand("select * from View_PositionList", conn))
			{
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						string? id = reader["ID"].ToString();
						string? name = reader["Name"].ToString();

						views.Add(new View_PositionList { ID = id, Name = name });

					}
				}
			}
			return views;
		}

		public async Task<List<View_ProjectList>> GetView_ProjectList()
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));

			await conn.OpenAsync();
			var views = new List<View_ProjectList>();
			using (var cmd = new NpgsqlCommand("select * from View_ProjectList", conn))
			{
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						string? id = reader["ID"].ToString();
						string? name = reader["Name"].ToString();

						views.Add(new View_ProjectList { ID = id, Name = name });

					}
				}
			}
			return views;
		}

		public async Task<List<View_SkillList>> GetView_SkillList()
		{
			await using var conn = new NpgsqlConnection(_configuration.GetConnectionString("EmployeeAppCon"));

			await conn.OpenAsync();
			var views = new List<View_SkillList>();
			using (var cmd = new NpgsqlCommand("select * from View_SkillList", conn))
			{
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						string? id = reader["ID"].ToString();
						string? name = reader["Name"].ToString();

						views.Add(new View_SkillList { ID = id, Name = name });

					}
				}
			}
			return views;
		}
	}
}
