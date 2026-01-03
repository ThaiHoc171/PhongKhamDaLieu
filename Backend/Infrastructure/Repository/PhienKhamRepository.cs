using Domain.DTO;
using Domain.Repository;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Infrastructure.Repositories
{
	public class PhienKhamRepository : IPhienKhamRepositories
	{
		private readonly string _connectionString;
		public PhienKhamRepository(IConfiguration config)
		{
			_connectionString = config.GetConnectionString("DefaultConnection");
		}
		public List<ListPhienKhamDTO> DanhSachPhienKham(int? Id)
		{
			List<ListPhienKhamDTO> listPhienKham = new List<ListPhienKhamDTO>();
			string sql = @"
					SELECT pk.PhienKhamID, pk.CaKhamID,
						   bnTNC.HoTen AS TenBenhNhan,
						   bsTNC.HoTen AS TenBacSi,
						   pk.TrieuChung, pk.NgayKham, pk.TrangThai
					FROM PhienKham pk
					JOIN BenhNhan bn ON pk.BenhNhanID = bn.BenhNhanID
					JOIN ThongTinCaNhan bnTNC ON bn.ThongTinID = bnTNC.ThongTinID
					JOIN NhanVien nv ON pk.NhanVienID = nv.NhanVienID
					JOIN ThongTinCaNhan bsTNC ON nv.ThongTinID = bsTNC.ThongTinID
					WHERE (@NhanVienID IS NULL OR pk.NhanVienID = @NhanVienID)
				";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = Id.HasValue ? Id.Value : (object)DBNull.Value;

				conn.Open();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						listPhienKham.Add(new ListPhienKhamDTO
						{
							PhienKhamID = reader.GetInt32(0),
							CaKhamID = reader.GetInt32(1),
							TenBenhNhan = reader.GetString(2),
							TenBacSi = reader.GetString(3),
							TrieuChung = reader.IsDBNull(4) ? null : reader.GetString(4),
							NgayKham = reader.GetDateTime(5),
							TrangThai = reader.GetString(6)
						});
					}
				}
			}
			return listPhienKham;
		}
		public PhienKhamDetailDTO GetPhienKhambyID(int phienKhamID)
		{
			PhienKhamDetailDTO result = null;

			string sql = @"
				SELECT pk.PhienKhamID, pk.CaKhamID, pk.BenhNhanID,
					   bnTNC.HoTen AS TenBenhNhan,
					   nv.NhanVienID AS BacSiID,
					   bsTNC.HoTen AS TenBacSi,
					   pcn.TenPhong AS TenPhongChucNang,
					   pk.TrieuChung, pk.GhiChu, pk.HinhAnhJSON,
					   pk.ChuanDoanCuoi, pk.NgayKham, pk.TrangThai
				FROM PhienKham pk
				JOIN BenhNhan bn ON pk.BenhNhanID = bn.BenhNhanID
				JOIN ThongTinCaNhan bnTNC ON bn.ThongTinID = bnTNC.ThongTinID
				JOIN NhanVien nv ON pk.NhanVienID = nv.NhanVienID
				JOIN ThongTinCaNhan bsTNC ON nv.ThongTinID = bsTNC.ThongTinID
				LEFT JOIN PhongChucNang pcn ON pk.PhongChucNangID = pcn.PhongChucNangID
				WHERE pk.PhienKhamID = @PhienKhamID
				";

			using (SqlConnection conn = new SqlConnection(_connectionString))
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = phienKhamID;

				conn.Open();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						result = new PhienKhamDetailDTO
						{
							PhienKhamID = reader.GetInt32(0),
							CaKhamID = reader.GetInt32(1),
							BenhNhanID = reader.GetInt32(2),
							TenBenhNhan = reader.GetString(3),
							BacSiID = reader.GetInt32(4),
							TenBacSi = reader.GetString(5),
							TenPhongChucNang = reader.IsDBNull(6) ? null : reader.GetString(6),
							TrieuChung = reader.IsDBNull(7) ? null : reader.GetString(7),
							GhiChu = reader.IsDBNull(8) ? null : reader.GetString(8),
							HinhAnhJSON = reader.IsDBNull(9) ? null : reader.GetString(9),
							ChanDoanCuoi = reader.IsDBNull(10) ? null : reader.GetString(10),
							NgayKham = reader.GetDateTime(11),
							TrangThai = reader.GetString(12)
						};
					}
				}
			}

			return result;
		}
		public bool TaoPhienKham(TaoPhienKhamDTO pk)
		{
			string sql = @"
				INSERT INTO PhienKham (CaKhamID, BenhNhanID, NhanVienID, PhongChucNangID, NgayKham, TrangThai)
				VALUES (@CaKhamID, @BenhNhanID, @NhanVienID, @PhongChucNangID, @NgayKham, N'Đang khám')
				";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				cmd.Parameters.Add("@CaKhamID", SqlDbType.Int).Value = pk.CaKhamID;
				cmd.Parameters.Add("@BenhNhanID", SqlDbType.Int).Value = pk.BenhNhanID;
				cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = pk.NhanVienID;
				cmd.Parameters.Add("@PhongChucNangID", SqlDbType.Int).Value = pk.PhongChucNangID;
				cmd.Parameters.Add("@NgayKham", SqlDbType.DateTime).Value = pk.NgayKham;
				conn.Open();
				int rowsAffected = cmd.ExecuteNonQuery();
				return rowsAffected > 0;
			}
		}
		public bool KetThucPhienKham(KetThucPhienKhamDTO pk)
		{
			string sql = @"
				UPDATE PhienKham
				SET TrieuChung = @TrieuChung,
					GhiChu = @GhiChu,
					HinhAnhJSON = @HinhAnhJSON,
					ChuanDoanCuoi = @ChuanDoanCuoi,
					TrangThai = N'Hoàn thành'
				WHERE PhienKhamID = @PhienKhamID
				";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = pk.PhienKhamID;
				cmd.Parameters.Add("@TrieuChung", SqlDbType.NVarChar).Value = (object)pk.TrieuChung ?? DBNull.Value;
				cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = (object)pk.GhiChu ?? DBNull.Value;
				cmd.Parameters.Add("@HinhAnhJSON", SqlDbType.NVarChar).Value = (object)pk.HinhAnhJSON ?? DBNull.Value;
				cmd.Parameters.Add("@ChuanDoanCuoi", SqlDbType.NVarChar).Value = (object)pk.ChanDoanCuoi ?? DBNull.Value;
				conn.Open();
				int rowsAffected = cmd.ExecuteNonQuery();
				return rowsAffected > 0;
			}
		}
		public bool HuyPhienKham(int phienKhamID)
		{
			string sql = @"
				UPDATE PhienKham
				SET TrangThai = N'Đã hủy'
				WHERE PhienKhamID = @PhienKhamID
				";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = phienKhamID;
				conn.Open();
				int rowsAffected = cmd.ExecuteNonQuery();
				return rowsAffected > 0;
			}
		}
	}
}
