using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Repository
{
	public interface ITaiKhoanRepository
	{
		TaiKhoanDTO LayTaiKhoanTheoEmail(string email);
		List<TaiKhoanDTO> LayDanhSachTaiKhoan();
	}

	public class TaiKhoanRepository : ITaiKhoanRepository
	{
		private readonly string _connectionString;

		public TaiKhoanRepository(IConfiguration config)
		{
			_connectionString = config.GetConnectionString("DefaultConnection");
		}

		public TaiKhoanDTO LayTaiKhoanTheoEmail(string email)
		{
			string sql = @"SELECT * FROM TaiKhoan WHERE Email = @Email";

			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@Email", email);
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							return new TaiKhoanDTO
							{
								TaiKhoanID = (int)reader["TaiKhoanID"],
								Email = reader["Email"].ToString(),
								PasswordHash = reader["PasswordHash"].ToString(),
								Role = reader["Role"].ToString(),
								Status = reader["Status"].ToString(),
								NgayTao = (DateTime)reader["NgayTao"],
								NgayCapNhat = (DateTime)reader["NgayCapNhat"]
							};
						}
					}
				}
			}
			return null;
		}

		public List<TaiKhoanDTO> LayDanhSachTaiKhoan()
		{
			List<TaiKhoanDTO> list = new List<TaiKhoanDTO>();
			string sql = @"
                SELECT TaiKhoanID, Email, Role, Status, NgayTao, NgayCapNhat
                FROM TaiKhoan
            ";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							list.Add(new TaiKhoanDTO
							{
								TaiKhoanID = (int)reader["TaiKhoanID"],
								Email = reader["Email"].ToString(),
								Role = reader["Role"].ToString(),
								Status = reader["Status"].ToString(),
								NgayTao = (DateTime)reader["NgayTao"],
								NgayCapNhat = (DateTime)reader["NgayCapNhat"]
							});
						}
					}
					return list;
				}
			}


		}
	}
}
