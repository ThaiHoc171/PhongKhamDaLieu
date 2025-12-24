using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Repository
{
	public interface IChucVuRepository
	{
		List<ChucVuDTO> DanhSachChucVu ();
		ChucVuDTO LayChucVuByID(int chucvuID);
		bool ThemChucVu(ThemChucVuDTO cv);
		bool CapNhatChucVu(ChucVuDTO cv);
		bool VoHieuHoaChucVu(int chucvuID);
	}
	public class ChucVuRepository : IChucVuRepository
	{
		private readonly string _connectionString;
		public ChucVuRepository(IConfiguration config)
		{
			_connectionString = config.GetConnectionString("DefaultConnection");

		}
		public List<ChucVuDTO> DanhSachChucVu()
		{
			List<ChucVuDTO> listChucVu = new List<ChucVuDTO>();
			string sql = "SELECT * FROM ChucVu WHERE TrangThai = 1";

			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							listChucVu.Add(new ChucVuDTO
							{
								ChucVuID = (int)reader["ChucVuID"],
								TenChucVu = reader["TenChucVu"].ToString(),
								MoTa = reader["MoTa"].ToString(),
								NgayTao = (DateTime)reader["NgayTao"],
								TrangThai = (bool)reader["TrangThai"]
							});
						}
					}
				}
				return listChucVu;
			}
		}
		public ChucVuDTO LayChucVuByID(int chucvuID)
		{
			string sql = "SELECT * FROM ChucVu WHERE ChucVuID = @ChucVuID AND TrangThai = 1";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@ChucVuID", chucvuID);
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							return new ChucVuDTO
							{
								ChucVuID = (int)reader["ChucVuID"],
								TenChucVu = reader["TenChucVu"].ToString(),
								MoTa = reader["MoTa"].ToString(),
								NgayTao = (DateTime)reader["NgayTao"],
								TrangThai = (bool)reader["TrangThai"]
							};
						}
					}
				}
				return null;
			}
		}
		public bool ThemChucVu(ThemChucVuDTO cv)
		{
			string sql = @"INSERT INTO ChucVu (TenChucVu, MoTa, NgayTao, TrangThai)
						   VALUES (@TenChucVu, @MoTa, @NgayTao, @TrangThai)";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@TenChucVu", cv.TenChucVu);
					cmd.Parameters.AddWithValue("@MoTa", cv.MoTa);
					cmd.Parameters.AddWithValue("@NgayTao", cv.NgayTao);
					cmd.Parameters.AddWithValue("@TrangThai", cv.TrangThai);
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected > 0;
				}
			}
		}
		public bool CapNhatChucVu(ChucVuDTO cv)
		{
			string sql = @"UPDATE ChucVu
						   SET TenChucVu = @TenChucVu,
							   MoTa = @MoTa,
							   TrangThai = @TrangThai
						   WHERE ChucVuID = @ChucVuID";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@TenChucVu", cv.TenChucVu);
					cmd.Parameters.AddWithValue("@MoTa", cv.MoTa);
					cmd.Parameters.AddWithValue("@TrangThai", cv.TrangThai);
					cmd.Parameters.AddWithValue("@ChucVuID", cv.ChucVuID);
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected > 0;
				}
			}
		}
		public bool VoHieuHoaChucVu(int chucvuID)
		{
			string sql = @"UPDATE ChucVu
						   SET TrangThai = 0
						   WHERE ChucVuID = @ChucVuID";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@ChucVuID", chucvuID);
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected > 0;
				}
			}
		}
	}
}
