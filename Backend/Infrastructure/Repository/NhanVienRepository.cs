using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Domain.DTO;
using Domain.Repository;

namespace Infrastructure.Repositories
{ 
	public class NhanVienRepository : INhanVienRepository
	{
		private readonly string _connectionString;
		public NhanVienRepository(IConfiguration config)
		{
			_connectionString = config.GetConnectionString("DefaultConnection");
		}
		public List<NhanVienListDTO> LayDanhSachNhanVien()
		{
			string sql = @"
					SELECT NV.NhanVienID, TNC.HoTen,CV.TenChucVu,TNC.SDT,TNC.EmailLienHe,NV.NgayVaoLam
					FROM NhanVien NV
					JOIN ThongTinCaNhan TNC ON NV.ThongTinID = TNC.ThongTinID
					JOIN ChucVu CV ON NV.ChucVuID = CV.ChucVuID
					WHERE TNC.Loai = 'nhanvien'
				";
			List<NhanVienListDTO> list = new List<NhanVienListDTO>();
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{

							list.Add(new NhanVienListDTO
							{
								NhanVienID = (int)reader["NhanVienID"],
								HoTen = reader["HoTen"].ToString(),
								TenChucVu = reader["TenChucVu"].ToString(),
								SDT = reader["SDT"].ToString(),
								EmailLienHe = reader["EmailLienHe"].ToString(),
								NgayVaoLam = reader["NgayVaoLam"] as DateTime?
							});
						}
					}
				}
			}
			return list;
		}

		public NhanVienDetailDTO LayNhanVienByID(int nhanVienID)
		{
			string sql = @"
					SELECT NV.NhanVienID, NV.ThongTinID, NV.ChucVuID, CV.TenChucVu,
						   NV.Luong, NV.NgayVaoLam, NV.BangCap, NV.KinhNghiem,
						   TNC.HoTen, TNC.NgaySinh, TNC.GioiTinh,
						   TNC.SDT, TNC.EmailLienHe, TNC.DiaChi, TNC.Avatar,
						   TNC.TaiKhoanID, TNC.NgayTao, TNC.NgayCapNhat
					FROM NhanVien NV
					JOIN ThongTinCaNhan TNC ON NV.ThongTinID = TNC.ThongTinID
					JOIN ChucVu CV ON NV.ChucVuID = CV.ChucVuID
					WHERE NV.NhanVienID = @NhanVienID AND Loai = 'nhanvien' And TNC.TrangThai = 'active'
				";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@NhanVienID", nhanVienID);
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							return new NhanVienDetailDTO
							{
								NhanVienID = (int)reader["NhanVienID"],
								ThongTinID = (int)reader["ThongTinID"],
								ChucVuID = (int)reader["ChucVuID"],
								TenChucVu = reader["TenChucVu"].ToString(),
								Luong = reader["Luong"] as decimal?,
								NgayVaoLam = reader["NgayVaoLam"] as DateTime?,
								BangCap = reader["BangCap"].ToString(),
								KinhNghiem = reader["KinhNghiem"].ToString(),
								HoTen = reader["HoTen"].ToString(),
								NgaySinh = reader["NgaySinh"] as DateTime?,
								GioiTinh = reader["GioiTinh"].ToString(),
								SDT = reader["SDT"].ToString(),
								EmailLienHe = reader["EmailLienHe"].ToString(),
								DiaChi = reader["DiaChi"].ToString(),
								Avatar = reader["Avatar"].ToString(),
								TaiKhoanID = reader["TaiKhoanID"] as int?,
								NgayTao = (DateTime)reader["NgayTao"],
								NgayCapNhat = (DateTime)reader["NgayCapNhat"]
							};
						}
					}
				}
			}
			return null;
		}
		public List<NhanVienListDTO> LayNhanVienByKeyWord(string keyword)
		{
			string sql= @"
					SELECT NV.NhanVienID, TNC.HoTen,CV.TenChucVu,TNC.SDT,TNC.EmailLienHe,NV.NgayVaoLam
					FROM NhanVien NV
					JOIN ThongTinCaNhan TNC ON NV.ThongTinID = TNC.ThongTinID
					JOIN ChucVu CV ON NV.ChucVuID = CV.ChucVuID
					WHERE TNC.Loai = 'nhanvien'
					AND ( TNC.HoTen LIKE @keyword OR TNC.EmailLienHe LIKE @keyword OR CAST(NV.NhanVienID AS NVARCHAR) LIKE @keyword )
					ORDER BY NV.NhanVienID ASC
				";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				List<NhanVienListDTO> list = new List<NhanVienListDTO>();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							while (reader.Read())
							{
								list.Add(new NhanVienListDTO
								{
									NhanVienID = (int)reader["NhanVienID"],
									HoTen = reader["HoTen"]?.ToString(),
									TenChucVu = reader["TenChucVu"]?.ToString(),
									SDT = reader["SDT"]?.ToString(),
									EmailLienHe = reader["EmailLienHe"]?.ToString(),
									NgayVaoLam = (DateTime)reader["NgayVaoLam"]
								});
							}
						}
					}
				}
				return list;
			}
		}
		public bool ThemNhanVien(NhanVienCreateDTO nv)
		{
			string sql = @"
				INSERT INTO ThongTinCaNhan
				(HoTen, NgaySinh, GioiTinh, SDT, EmailLienHe, DiaChi, Avatar, Loai)
				VALUES
				(@HoTen, @NgaySinh, @GioiTinh, @SDT, @EmailLienHe, @DiaChi, @Avatar, 'nhanvien');

				DECLARE @ThongTinID INT = SCOPE_IDENTITY();

				INSERT INTO NhanVien
				(ThongTinID, ChucVuID, Luong, NgayVaoLam, BangCap, KinhNghiem)
				VALUES
				(@ThongTinID, @ChucVuID, @Luong, @NgayVaoLam, @BangCap, @KinhNghiem);
			";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@HoTen", nv.HoTen);
					cmd.Parameters.AddWithValue("@NgaySinh", nv.NgaySinh);
					cmd.Parameters.AddWithValue("@GioiTinh",nv.GioiTinh);
					cmd.Parameters.AddWithValue("@SDT", nv.SDT);
					cmd.Parameters.AddWithValue("@EmailLienHe", nv.EmailLienHe);
					cmd.Parameters.AddWithValue("@DiaChi", nv.DiaChi);
					cmd.Parameters.AddWithValue("@Avatar", nv.Avatar);

					cmd.Parameters.AddWithValue("@ChucVuID", nv.ChucVuID);
					cmd.Parameters.AddWithValue("@Luong", nv.Luong);
					cmd.Parameters.AddWithValue("@NgayVaoLam", nv.NgayVaoLam);
					cmd.Parameters.AddWithValue("@BangCap", nv.BangCap);
					cmd.Parameters.AddWithValue("@KinhNghiem", nv.KinhNghiem);

					return cmd.ExecuteNonQuery() > 0;
				}
			}
		}
		public bool CapNhatNhanVien(NhanVienDetailDTO nv)
		{
			string sql = @"
				UPDATE ThongTinCaNhan
				SET HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, SDT = @SDT, EmailLienHe = @EmailLienHe,
					DiaChi = @DiaChi, Avatar = @Avatar, NgayCapNhat = GETDATE()
				WHERE ThongTinID = @ThongTinID;
				UPDATE NhanVien
				SET ChucVuID = @ChucVuID, Luong = @Luong, NgayVaoLam = @NgayVaoLam, BangCap = @BangCap, KinhNghiem = @KinhNghiem
				WHERE NhanVienID = @NhanVienID;
			";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@HoTen", nv.HoTen);
					cmd.Parameters.AddWithValue("@NgaySinh", nv.NgaySinh);
					cmd.Parameters.AddWithValue("@GioiTinh", nv.GioiTinh);
					cmd.Parameters.AddWithValue("@SDT", nv.SDT);
					cmd.Parameters.AddWithValue("@EmailLienHe", nv.EmailLienHe);
					cmd.Parameters.AddWithValue("@DiaChi", nv.DiaChi);
					cmd.Parameters.AddWithValue("@Avatar", nv.Avatar);
					cmd.Parameters.AddWithValue("@ThongTinID", nv.ThongTinID);
					cmd.Parameters.AddWithValue("@ChucVuID", nv.ChucVuID);
					cmd.Parameters.AddWithValue("@Luong", nv.Luong);
					cmd.Parameters.AddWithValue("@NgayVaoLam", nv.NgayVaoLam);
					cmd.Parameters.AddWithValue("@BangCap", nv.BangCap);
					cmd.Parameters.AddWithValue("@KinhNghiem", nv.KinhNghiem);
					cmd.Parameters.AddWithValue("@NhanVienID", nv.NhanVienID);
					return cmd.ExecuteNonQuery() > 0;
				}
			}
		}
		public bool XoaNhanVien(int nhanVienID)
		{
			string sql = @"
				UPDATE ThongTinCaNhan
				SET TrangThai = 'inactive', NgayCapNhat = GETDATE()
				WHERE ThongTinID = (SELECT ThongTinID FROM NhanVien WHERE NhanVienID = @NhanVienID);
			";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@NhanVienID", nhanVienID);
					return cmd.ExecuteNonQuery() > 0;
				}
			}
		}

	}
}
