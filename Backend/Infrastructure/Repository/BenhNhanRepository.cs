using Microsoft.Extensions.Configuration;
using Domain.DTO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using Domain.Repository;


namespace Infrastructure.Repositories
{
	public class BenhNhanRepository : IBenhNhanRepository
	{
		private readonly string _connectionString;
		public BenhNhanRepository(IConfiguration config)
		{
			_connectionString = config.GetConnectionString("DefaultConnection");
		}
		public List<BenhNhanListDTO> LayDanhSachBenhNhan()
		{
			string sql = @"
					SELECT TNC.ThongTinID, BN.BenhNhanID, TNC.HoTen, TNC.NgaySinh, TNC.GioiTinh, TNC.SDT
					FROM BenhNhan BN
					JOIN ThongTinCaNhan TNC ON BN.ThongTinID = TNC.ThongTinID
					WHERE TNC.Loai = 'benhnhan' AND TNC.TrangThai = 'active'
			";
			List<BenhNhanListDTO> listBenhNhan = new List<BenhNhanListDTO>();
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							listBenhNhan.Add(new BenhNhanListDTO
							{
								BenhNhanID = (int)reader["BenhNhanID"],
								ThongTinID = (int)reader["ThongTinID"],
								HoTen = reader["HoTen"].ToString(),
								NgaySinh = (DateTime)reader["NgaySinh"],
								GioiTinh = reader["GioiTinh"].ToString(),
								SDT = reader["SDT"].ToString()
							});
						}

					}
				}
			}
			return listBenhNhan;
		}
		public List<BenhNhanListDTO> LayBenhNhanByKeyWord(string keyword)
		{
			string sql = @"
					SELECT TNC.ThongTinID, BN.BenhNhanID, TNC.HoTen, TNC.NgaySinh, TNC.GioiTinh, TNC.SDT
					FROM BenhNhan BN
					JOIN ThongTinCaNhan TNC ON BN.ThongTinID = TNC.ThongTinID
					WHERE TNC.loai = 'benhnhan' 
					AND ( TNC.HoTen LIKE @keyword OR TNC.EmailLienHe LIKE @keyword OR CAST(BN.BenhNhanID AS NVARCHAR) LIKE @keyword )
					ORDER BY BN.BenhNhanID ASC
				";
			List<BenhNhanListDTO> listBenhNhan = new List<BenhNhanListDTO>();
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							listBenhNhan.Add(new BenhNhanListDTO
							{
								BenhNhanID = (int)reader["BenhNhanID"],
								ThongTinID = (int)reader["ThongTinID"],
								HoTen = reader["HoTen"].ToString(),
								NgaySinh = reader["NgaySinh"] == DBNull.Value
									? (DateTime?)null
									: (DateTime)reader["NgaySinh"],
								GioiTinh = reader["GioiTinh"].ToString(),
								SDT = reader["SDT"].ToString()
							});
						}
					}
				}
			}
			return listBenhNhan;
		}
		public BenhNhanDetailDTO LayBenhNhanByID(int benhNhanID)
		{
			string sql = @"
					SELECT BN.BenhNhanID, BN.LoaiDa, BN.TrangThaiTheoDoi, BN.GhiChu,
							TNC.ThongTinID, TNC.TaiKhoanID, TNC.HoTen, TNC.NgaySinh, TNC.GioiTinh, TNC.SDT, TNC.EmailLienHe, TNC.DiaChi, TNC.Avatar, TNC.TrangThai, TNC.NgayTao, TNC.NgayCapNhat
					FROM BenhNhan BN
					JOIN ThongTinCaNhan TNC ON BN.ThongTinID = TNC.ThongTinID
					WHERE BN.BenhNhanID = @BenhNhanID AND TNC.Loai = 'benhnhan' AND TNC.TrangThai = 'active'
			";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@BenhNhanID", benhNhanID);
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							return new BenhNhanDetailDTO
							{
								BenhNhanID = (int)reader["BenhNhanID"],
								LoaiDa = reader["LoaiDa"].ToString(),
								TrangThaiTheoDoi = reader["TrangThaiTheoDoi"].ToString(),
								GhiChu = reader["GhiChu"].ToString(),
								ThongTinID = (int)reader["ThongTinID"],
								TaiKhoanID = reader["TaiKhoanID"] as int?,
								HoTen = reader["HoTen"].ToString(),
								NgaySinh = reader["NgaySinh"] as DateTime?,
								GioiTinh = reader["GioiTinh"].ToString(),
								SDT = reader["SDT"].ToString(),
								EmailLienHe = reader["EmailLienHe"].ToString(),
								DiaChi = reader["DiaChi"].ToString(),
								Avatar = reader["Avatar"].ToString(),
								TrangThai = reader["TrangThai"].ToString(),
								NgayTao = (DateTime)reader["NgayTao"],
								NgayCapNhat = (DateTime)reader["NgayCapNhat"]
							};
						}
					}
				}
			}
			return null;
		}
		public bool ThemBenhNhan(BenhNhanCreateDTO bn)
		{
			string sql = @"
				INSERT INTO ThongTinCaNhan (HoTen, NgaySinh, GioiTinh, SDT, EmailLienHe, DiaChi, Avatar, Loai, TrangThai, NgayTao, NgayCapNhat)
				VALUES (@HoTen, @NgaySinh, @GioiTinh, @SDT, @EmailLienHe, @DiaChi, @Avatar, 'benhnhan', 'active', GETDATE(), GETDATE());
				SELECT SCOPE_IDENTITY();

				INSERT INTO BenhNhan (ThongTinID,LoaiDa,GhiChu)	
				VALUES (SCOPE_IDENTITY(),@LoaiDa,@GhiChu);
			";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@HoTen", bn.HoTen);
					cmd.Parameters.AddWithValue("@NgaySinh", (object)bn.NgaySinh ?? DBNull.Value);
					cmd.Parameters.AddWithValue("@GioiTinh", bn.GioiTinh);
					cmd.Parameters.AddWithValue("@SDT", bn.SDT);
					cmd.Parameters.AddWithValue("@EmailLienHe", bn.EmailLienHe);
					cmd.Parameters.AddWithValue("@DiaChi", bn.DiaChi);
					cmd.Parameters.AddWithValue("@Avatar", bn.Avatar);
					cmd.Parameters.AddWithValue("@LoaiDa", bn.LoaiDa);
					cmd.Parameters.AddWithValue("@GhiChu", bn.GhiChu);
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected > 0;
				}
			}
		}
		public bool CapNhatBenhNhan(BenhNhanCreateDTO bn)
		{
			string sql = @"
				UPDATE ThongTinCaNhan
				SET HoTen = @HoTen,
					NgaySinh = @NgaySinh,
					GioiTinh = @GioiTinh,
					SDT = @SDT,
					EmailLienHe = @EmailLienHe,
					DiaChi = @DiaChi,
					Avatar = @Avatar,
					NgayCapNhat = GETDATE()
				WHERE ThongTinID = @ThongTinID;
				UPDATE BenhNhan
				SET LoaiDa = @LoaiDa,
					GhiChu = @GhiChu
				WHERE BenhNhanID = @BenhNhanID;
			";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@HoTen", bn.HoTen);
					cmd.Parameters.AddWithValue("@NgaySinh", (object)bn.NgaySinh ?? DBNull.Value);
					cmd.Parameters.AddWithValue("@GioiTinh", bn.GioiTinh);
					cmd.Parameters.AddWithValue("@SDT", bn.SDT);
					cmd.Parameters.AddWithValue("@EmailLienHe", bn.EmailLienHe);
					cmd.Parameters.AddWithValue("@DiaChi", bn.DiaChi);
					cmd.Parameters.AddWithValue("@Avatar", bn.Avatar);
					cmd.Parameters.AddWithValue("@LoaiDa", bn.LoaiDa);
					cmd.Parameters.AddWithValue("@GhiChu", bn.GhiChu);
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected > 0;
				}
			}
		}
		public bool XoaBenhNhan(int benhNhanID)
		{
			string sql = @"
				UPDATE ThongTinCaNhan
				SET TrangThai = 'inactive',
					NgayCapNhat = GETDATE()
				WHERE ThongTinID = (SELECT ThongTinID FROM BenhNhan WHERE BenhNhanID = @BenhNhanID);
			";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@BenhNhanID", benhNhanID);
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected > 0;
				}
			}
		}
	}

}
