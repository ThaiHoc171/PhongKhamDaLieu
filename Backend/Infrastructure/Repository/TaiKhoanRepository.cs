using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
	public class TaiKhoanRepository : ITaiKhoanRepository
	{
		private readonly string _connectionString;

		public TaiKhoanRepository(IConfiguration config)
		{
			_connectionString = config.GetConnectionString("DefaultConnection");
		}

		public TaiKhoan GetByEmail(string email)
		{
			const string sql = @"
            SELECT TaiKhoanID, Email, MatKhau, VaiTro, TrangThai, NgayTao
            FROM TaiKhoan
            WHERE Email = @Email
        ";

			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@Email", email);
					conn.Open();
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							return MapToEntity(reader);
						}
						else
						{
							return null;
						}
					}
				}
			}
		}
		public TaiKhoan GetById(int id)
		{
			string sql = @"
                SELECT TaiKhoanID, Email, MatKhau, VaiTro, TrangThai, NgayTao
                FROM TaiKhoan
                WHERE TaiKhoanID = @Id
            ";

			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@Id", id);
					conn.Open();
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							return MapToEntity(reader);
						}
						else
						{
							return null;
						}
					}
				}
			}
		}

		public List<TaiKhoan> GetAll()
		{
			List<TaiKhoan> list = new List<TaiKhoan>();
			string sql = @"
                SELECT TaiKhoanID, Email, MatKhau, VaiTro, TrangThai, NgayTao
                FROM TaiKhoan
            ";

			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					conn.Open();
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							list.Add(MapToEntity(reader));
						}
					}
				}
			}

			return list;
		}
		public void Add(TaiKhoan taiKhoan)
		{
			string sql = @"
                INSERT INTO TaiKhoan (Email, MatKhau, VaiTro, TrangThai, NgayTao)
                VALUES (@Email, @MatKhau, @VaiTro, @TrangThai, @NgayTao)
            ";

			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@Email", taiKhoan.Email);
					cmd.Parameters.AddWithValue("@MatKhau", taiKhoan.MatKhau);
					cmd.Parameters.AddWithValue("@VaiTro", taiKhoan.Vaitro);
					cmd.Parameters.AddWithValue("@TrangThai", taiKhoan.TrangThai);
					cmd.Parameters.AddWithValue("@NgayTao", taiKhoan.NgayTao);

					conn.Open();
					cmd.ExecuteNonQuery();
				}
			}
		}
		public void Update(TaiKhoan taiKhoan)
		{
			string sql = @"
                UPDATE TaiKhoan
                SET MatKhau = @MatKhau,
                    TrangThai = @TrangThai,
                    NgayCapNhat = GETDATE()
                WHERE TaiKhoanID = @Id
            ";

			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@MatKhau", taiKhoan.MatKhau);
					cmd.Parameters.AddWithValue("@TrangThai", taiKhoan.TrangThai);
					cmd.Parameters.AddWithValue("@Id", taiKhoan.Id);

					conn.Open();
					cmd.ExecuteNonQuery();
				}
			}
		}

		private TaiKhoan MapToEntity(SqlDataReader reader)
		{
			return new TaiKhoan(
				id: (int)reader["TaiKhoanID"],
				email: reader["Email"].ToString(),
				matkhau: reader["MatKhau"].ToString(),
				vaitro: reader["VaiTro"].ToString(),
				trangthai: reader["TrangThai"].ToString(),
				ngaytao: (DateTime)reader["NgayTao"]
			);
		}
	}
}
