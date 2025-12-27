using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Repository
{
	public interface IPhongChucNangRepository
	{
		List<PhongChucNangDTO> DanhSachPhongChucNang();
	}
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
		public bool ThemPhongChucNang(ThemPhongChucNangDTO pcn)
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
	}
}
