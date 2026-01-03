using Domain.DTO;
using Domain.Repository;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Infrastructure.Repositories
{
	public class CanLamSangRepository : ICanLamSangRepositories
	{
		public readonly string _connectionString;
		public CanLamSangRepository(IConfiguration config)
		{
			_connectionString = config.GetConnectionString("DefaultConnection");
		}
		public List<CanLamSangListDTO> DanhSachCanLamSang()
		{
			string sql = @"SELECT CanLamSangID, TenCLS, LoaiXetNghiem, Gia FROM CanLamSang";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				List<CanLamSangListDTO> danhSachCLS = new List<CanLamSangListDTO>();
				while (reader.Read())
				{
					CanLamSangListDTO cls = new CanLamSangListDTO
					{
						CanLamSangID = reader.GetInt32(0),
						TenCLS = reader.GetString(1),
						LoaiXetNghiem = reader.GetString(2),
						Gia = reader.GetDecimal(3)
					};
					danhSachCLS.Add(cls);
				}
				return danhSachCLS;
			}
		}
		public CanLamSangDetailDTO LayChiTietCanLamSang(int CanLamSangID)
		{
			string sql = @"SELECT CanLamSangID, TenCLS, MoTa, Gia, LoaiXetNghiem, NgayTao 
						   FROM CanLamSang WHERE CanLamSangID = @CanLamSangID";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				cmd.Parameters.AddWithValue("@CanLamSangID", CanLamSangID);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					return new CanLamSangDetailDTO
					{
						CanLamSangID = reader.GetInt32(0),
						TenCLS = reader.GetString(1),
						MoTa = reader.IsDBNull(2) ? null : reader.GetString(2),
						Gia = reader.IsDBNull(3) ? (decimal?)null : reader.GetDecimal(3),
						LoaiXetNghiem = reader.GetString(4),
						NgayTao = reader.GetDateTime(5)
					};
				}
				return null;
			}
		}
		public bool ThemCanLamSang(ThemCanLamSangDTO cls)
		{
			string sql = @"INSERT INTO CanLamSang (TenCLS, MoTa, Gia, LoaiXetNghiem, NgayTao) 
						   VALUES (@TenCLS, @MoTa, @Gia, @LoaiXetNghiem, GETDATE())";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				cmd.Parameters.AddWithValue("@TenCLS", cls.TenCLS);
				cmd.Parameters.AddWithValue("@MoTa", cls.MoTa);
				cmd.Parameters.AddWithValue("@Gia", cls.Gia);
				cmd.Parameters.AddWithValue("@LoaiXetNghiem", cls.LoaiXetNghiem);
				conn.Open();
				int rowsAffected = cmd.ExecuteNonQuery();
				return rowsAffected > 0;
			}
		}
		public bool CapNhatCanLamSang(CapNhatCanLamSangDTO cls)
		{
			string sql = @"UPDATE CanLamSang 
						   SET TenCLS = @TenCLS, MoTa = @MoTa, Gia = @Gia, LoaiXetNghiem = @LoaiXetNghiem 
						   WHERE CanLamSangID = @CanLamSangID";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				cmd.Parameters.AddWithValue("@CanLamSangID", cls.CanLamSangID);
				cmd.Parameters.AddWithValue("@TenCLS", cls.TenCLS);
				cmd.Parameters.AddWithValue("@MoTa", cls.MoTa);
				cmd.Parameters.AddWithValue("@Gia", cls.Gia);
				cmd.Parameters.AddWithValue("@LoaiXetNghiem", cls.LoaiXetNghiem);
				conn.Open();
				int rowsAffected = cmd.ExecuteNonQuery();
				return rowsAffected > 0;
			}
		}
		public bool ChuyenTrangThai(Status stt)
		{
			string sql = @"UPDATE CanLamSang 
						   SET TrangThai = @TrangThai 
						   WHERE CanLamSangID = @CanLamSangID";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				cmd.Parameters.AddWithValue("@CanLamSangID", stt.Id);
				cmd.Parameters.AddWithValue("@TrangThai", stt.TrangThai);
				conn.Open();
				int rowsAffected = cmd.ExecuteNonQuery();
				return rowsAffected > 0;
			}
		}
	}
}
