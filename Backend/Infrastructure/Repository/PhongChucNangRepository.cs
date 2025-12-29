using Domain.DTO;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
	public class PhongChucNangRepository : IPhongChucNangRepository
	{
		private readonly string _connectionString;
		public PhongChucNangRepository(IConfiguration config)
		{
			_connectionString = config.GetConnectionString("DefaultConnection");
		}
		public List<PhongChucNangDTO> DanhSachPhongChucNang()
		{
			List<PhongChucNangDTO> listPhongChucNang = new List<PhongChucNangDTO>();
			string sql = "SELECT * FROM PhongChucNang WHERE TrangThai = 'Hoạt Động'";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							listPhongChucNang.Add(new PhongChucNangDTO
							{
								PhongChucNangID = (int)reader["PhongChucNangID"],
								TenPhong = reader["TenPhong"]?.ToString(),
								LoaiPhong = reader["LoaiPhong"]?.ToString(),
								MoTa = reader["MoTa"]?.ToString(),
								TrangThai = reader["TrangThai"]?.ToString(),
								NgayNhap = (DateTime)reader["NgayNhap"]
							});
						}
					}
				}
			}
			return listPhongChucNang;
		}
		public List<PhongChucNangDTO> TimKiem(string keyword)
		{
			List<PhongChucNangDTO> listPhongChucNang = new List<PhongChucNangDTO>();
			string sql = @"SELECT * FROM PhongChucNang WHERE TrangThai = 'Hoạt Động' 
						AND (TenPhong LIKE @Keyword OR CAST(PhongChucNangID AS NVarchar) LIKE @keyword )";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							listPhongChucNang.Add(new PhongChucNangDTO
							{
								PhongChucNangID = (int)reader["PhongChucNangID"],
								TenPhong = reader["TenPhong"]?.ToString(),
								LoaiPhong = reader["LoaiPhong"]?.ToString(),
								MoTa = reader["MoTa"]?.ToString(),
								TrangThai = reader["TrangThai"]?.ToString(),
								NgayNhap = (DateTime)reader["NgayNhap"]
							});
						}
					}
				}
			}
			return listPhongChucNang;
		}
		public PhongChucNangDTO GetPhongByID(int ID)
		{
			PhongChucNangDTO pcn = null;
			string sql = @"SELECT * FROM PhongChucNang WHERE PhongChucNangID = @ID AND TrangThai = 'Hoạt Động'";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@ID", ID);
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							pcn = new PhongChucNangDTO
							{
								PhongChucNangID = (int)reader["PhongChucNangID"],
								TenPhong = reader["TenPhong"]?.ToString(),
								LoaiPhong = reader["LoaiPhong"]?.ToString(),
								MoTa = reader["MoTa"]?.ToString(),
								TrangThai = reader["TrangThai"]?.ToString(),
								NgayNhap = (DateTime)reader["NgayNhap"]
							};
						}
					}
				}
			}
			return pcn;
		}
		public bool ThemPhongChucNang(PhongChucNangCreateDTO pcn)
		{
			string sql = @"INSERT INTO PhongChucNang (TenPhong, LoaiPhong, MoTa, NgayNhap) 
						 VALUES (@TenPhong, @LoaiPhong, @MoTa, GETDATE())";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@TenPhong", pcn.TenPhong);
					cmd.Parameters.AddWithValue("@LoaiPhong", pcn.LoaiPhong);
					cmd.Parameters.AddWithValue("@MoTa", pcn.MoTa ?? (object)DBNull.Value);
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected > 0;
				}
			}
		}
		public bool CapNhatPhongChucNang(PhongChucNangDTO pcn)
		{
			string sql = @"UPDATE PhongChucNang 
						   SET TenPhong = @TenPhong, LoaiPhong = @LoaiPhong, MoTa = @MoTa 
						   WHERE PhongChucNangID = @PhongChucNangID AND TrangThai = 'Hoạt Động'";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@TenPhong", pcn.TenPhong);
					cmd.Parameters.AddWithValue("@LoaiPhong", pcn.LoaiPhong);
					cmd.Parameters.AddWithValue("@MoTa", pcn.MoTa ?? (object)DBNull.Value);
					cmd.Parameters.AddWithValue("@PhongChucNangID", pcn.PhongChucNangID);
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected > 0;
				}
			}
		}
		public bool XoaPhongChucNang(int phongChucNangID)
		{
			string sql = @"UPDATE PhongChucNang 
						   SET TrangThai = 'Ngừng Hoạt Động' 
						   WHERE PhongChucNangID = @PhongChucNangID AND TrangThai = 'Hoạt Động'";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@PhongChucNangID", phongChucNangID);
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected > 0;
				}
			}
		}
	}
}
