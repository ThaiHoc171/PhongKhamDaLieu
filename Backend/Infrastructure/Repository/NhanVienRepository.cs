using Domain.Entities;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{ 
	public class NhanVienRepository : INhanVienRepository
	{
		private readonly string _connectionString;
		public NhanVienRepository(IConfiguration config)
		{
			_connectionString = config.GetConnectionString("DefaultConnection");
		}
		public NhanVien GetById(int nhanVienID)
		{
			const string sql = @"
					SELECT 
						nv.NhanVienID, nv.ThongTinID, nv.ChucVuID, nv.NgayVaoLam,
						nv.BangCap, nv.KinhNghiem, nv.TrangThai,
						tt.HoTen, tt.NgaySinh, tt.GioiTinh, tt.SDT, tt.EmailLienHe,
						tt.DiaChi, tt.Avatar, tt.Loai, tt.NgayTao, tt.NgayCapNhat,          
						cv.TenChucVu
						FROM NhanVien nv
						JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
						JOIN ChucVu cv ON nv.ChucVuID = cv.ChucVuID
						WHERE nv.NhanVienID = @NhanVienID
				";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@NhanVienID", nhanVienID);
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
		public List<NhanVien> GetAll()
		{
			const string sql = @"
					SELECT
						nv.NhanVienID, nv.TrangThai,
						tt.ThongTinID, tt.HoTen, tt.SDT, tt.EmailLienHe, 
						cv.TenChucVu
					FROM NhanVien nv
					JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
					JOIN ChucVu cv ON nv.ChucVuID = cv.ChucVuID
					ORDER BY nv.NhanVienID
				";

			var list = new List<NhanVien>();
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					conn.Open();
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							list.Add(MapToEntityList(reader));
						}
					}
				}
			}
			return list;
		}
		public List<NhanVien> GetNhanViens(string keyword)
		{
			const string sql = @"
					SELECT
						nv.NhanVienID, nv.TrangThai,
						tt.ThongTinID, tt.HoTen, tt.SDT, tt.EmailLienHe, 
						cv.TenChucVu
					FROM NhanVien nv
					JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
					JOIN ChucVu cv ON nv.ChucVuID = cv.ChucVuID
					WHERE (tt.HoTen LIKE @Keyword) OR (tt.EmailLienHe LIKE @Keyword)
					ORDER BY nv.NhanVienID
				";
			var list = new List<NhanVien>();
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
					conn.Open();
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							list.Add(MapToEntityList(reader));
						}
					}
				}
			}
			return list;
		}
		public void Add(NhanVien nv)
		{
			const string sql = @"
					INSERT INTO NhanVien
					( ThongTinID, ChucVuID, NgayVaoLam, BangCap, KinhNghiem )
					VALUES ( @ThongTinID, @ChucVuID, @NgayVaoLam, @BangCap, @KinhNghiem )
				";

			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@ThongTinID", nv.ThongTinID);
					cmd.Parameters.AddWithValue("@ChucVuID", nv.ChucVuID);
					cmd.Parameters.AddWithValue("@NgayVaoLam", (object)nv.NgayVaoLam ?? DBNull.Value);
					cmd.Parameters.AddWithValue("@BangCap", (object)nv.BangCap ?? DBNull.Value);
					cmd.Parameters.AddWithValue("@KinhNghiem", (object)nv.KinhNghiem ?? DBNull.Value);
					conn.Open();
					cmd.ExecuteNonQuery();
				}
			}
		}
		public void Update(NhanVien nv)
		{
			const string sql = @"
					UPDATE NhanVien
					SET ChucVuID = @ChucVuID, NgayVaoLam = @NgayVaoLam, BangCap = @BangCap,
						KinhNghiem = @KinhNghiem, TrangThai = @TrangThai
					WHERE NhanVienID = @NhanVienID
				";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@ChucVuID", nv.ChucVuID);
					cmd.Parameters.AddWithValue("@NgayVaoLam", (object)nv.NgayVaoLam ?? DBNull.Value);
					cmd.Parameters.AddWithValue("@BangCap", (object)nv.BangCap ?? DBNull.Value);
					cmd.Parameters.AddWithValue("@KinhNghiem", (object)nv.KinhNghiem ?? DBNull.Value);
					cmd.Parameters.AddWithValue("@TrangThai", nv.TrangThai);
					cmd.Parameters.AddWithValue("@NhanVienID", nv.NhanVienID);
					conn.Open();
					cmd.ExecuteNonQuery();
				}
			}
		}
		public void UpdateTrangThai(int nhanVienID, string trangThai)
		{
			const string sql = @"
				UPDATE NhanVien
				SET TrangThai = @TrangThai
				WHERE NhanVienID = @NhanVienID
			";

			using (SqlConnection conn = new SqlConnection(_connectionString))
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				cmd.Parameters.AddWithValue("@TrangThai", trangThai);
				cmd.Parameters.AddWithValue("@NhanVienID", nhanVienID);
				conn.Open();
				cmd.ExecuteNonQuery();
			}
		}


		private NhanVien MapToEntity(SqlDataReader r)
		{
			var thongTin = new ThongTinCaNhan(
			thongTinID: (int)r["ThongTinID"],
			taiKhoanID: null,
			hoTen: r["HoTen"].ToString(),
			ngaySinh: r["NgaySinh"] == DBNull.Value ? null : (DateTime?)r["NgaySinh"],
			gioiTinh: r["GioiTinh"]?.ToString(),
			sdt: r["SDT"]?.ToString(),
			emailLienHe: r["EmailLienHe"]?.ToString(),
			diaChi: r["DiaChi"]?.ToString(),
			avatar: r["Avatar"] == DBNull.Value ? null : r["Avatar"].ToString(),
			loai: r["Loai"].ToString(),
			ngayTao: (DateTime)r["NgayTao"],
			ngayCapNhat: (DateTime)r["NgayCapNhat"]
			);

			return new NhanVien(
				nhanVienID: (int)r["NhanVienID"],
				thongTinID: thongTin.ThongTinID,
				chucVuID: (int)r["ChucVuID"],
				ngayVaoLam: r["NgayVaoLam"] == DBNull.Value ? null : (DateTime?)r["NgayVaoLam"],
				bangCap: r["BangCap"]?.ToString(),
				kinhNghiem: r["KinhNghiem"]?.ToString(),
				trangThai: r["TrangThai"].ToString(),
				tenChucVu: r["TenChucVu"].ToString(),
				thongTinCaNhan: thongTin
			);
		}

		private NhanVien MapToEntityList(SqlDataReader r)
		{
			var thongTin = new ThongTinCaNhan(
				thongTinID: (int)r["ThongTinID"],
				taiKhoanID: null,
				hoTen: r["HoTen"].ToString(),
				ngaySinh: null,
				gioiTinh: null,
				sdt: r["SDT"].ToString(),
				emailLienHe: r["EmailLienHe"].ToString(),
				diaChi: null,
				avatar: null,
				loai: "Nhân viên",
				ngayTao: DateTime.MinValue,
				ngayCapNhat: DateTime.MinValue
			);

			return new NhanVien(
				nhanVienID: (int)r["NhanVienID"],
				thongTinID: thongTin.ThongTinID,
				chucVuID: 0,
				ngayVaoLam: null,
				bangCap: null,
				kinhNghiem: null,
				trangThai: r["TrangThai"].ToString(),
				tenChucVu: r["TenChucVu"].ToString(),
				thongTinCaNhan: thongTin
			);
		}

	}
}
