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
		TaiKhoanDTO LayTaiKhoanTheoID(int taiKhoanID);
		List<TaiKhoanDTO> LayDanhSachTaiKhoan();
		bool ThemTaiKhoan(TaiKhoanDTO tk);
		bool CapNhatMatKhau(int taiKhoanID, string newPasswordHash);
		bool VoHieuHoaTaiKhoan(int taiKhoanID);
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
		public TaiKhoanDTO LayTaiKhoanTheoID(int taiKhoanID)
		{
			string sql = @"SELECT * FROM TaiKhoan WHERE TaiKhoanID = @TaiKhoanID";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@TaiKhoanID", taiKhoanID);
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
		public bool ThemTaiKhoan(TaiKhoanDTO tk)
		{
			string sql = @"
				INSERT INTO TaiKhoan (Email, PasswordHash, Role, Status, NgayTao, NgayCapNhat)
				VALUES (@Email, @PasswordHash, @Role, @Status, @NgayTao, @NgayCapNhat)
			";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@Email", tk.Email);
					cmd.Parameters.AddWithValue("@PasswordHash", tk.PasswordHash);
					cmd.Parameters.AddWithValue("@Role", tk.Role);
					cmd.Parameters.AddWithValue("@Status", tk.Status);
					cmd.Parameters.AddWithValue("@NgayTao", tk.NgayTao);
					cmd.Parameters.AddWithValue("@NgayCapNhat", tk.NgayCapNhat);
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected > 0;
				}
			}
		}
		public bool CapNhatMatKhau(int taiKhoanID, string newPasswordHash)
		{
			string sql = @"
				UPDATE TaiKhoan
				SET PasswordHash = @PasswordHash, NgayCapNhat = @NgayCapNhat
				WHERE TaiKhoanID = @TaiKhoanID
			";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@PasswordHash", newPasswordHash);
					cmd.Parameters.AddWithValue("@NgayCapNhat", DateTime.Now);
					cmd.Parameters.AddWithValue("@TaiKhoanID", taiKhoanID);
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected > 0;
				}
			}
		}
		public bool VoHieuHoaTaiKhoan(int taiKhoanID)
		{
			string sql = @"
				UPDATE TaiKhoan
				SET Status = 'Inactive', NgayCapNhat = @NgayCapNhat
				WHERE TaiKhoanID = @TaiKhoanID
			";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@NgayCapNhat", DateTime.Now);
					cmd.Parameters.AddWithValue("@TaiKhoanID", taiKhoanID);
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected > 0;
				}
			}
		}
	}
}
