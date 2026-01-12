using Domain.DTO;
using Domain.Repository;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;


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
					SELECT TNC.ThongTinID, BN.BenhNhanID, TNC.HoTen, TNC.NgaySinh, TNC.GioiTinh, TNC.SDT, BN.TrangThaiTheoDoi
					FROM BenhNhan BN
					JOIN ThongTinCaNhan TNC ON BN.ThongTinID = TNC.ThongTinID
					WHERE TNC.Loai = N'Bệnh nhân'
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
								SDT = reader["SDT"].ToString(),
								TrangThaiTheoDoi = reader["TrangThaiTheoDoi"].ToString()	
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
					SELECT TNC.ThongTinID, BN.BenhNhanID, TNC.HoTen, TNC.NgaySinh, TNC.GioiTinh, TNC.SDT, BN.TrangThaiTheoDoi
					FROM BenhNhan BN
					JOIN ThongTinCaNhan TNC ON BN.ThongTinID = TNC.ThongTinID
					WHERE TNC.Loai = N'Bệnh nhân'
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
								SDT = reader["SDT"].ToString(),
								TrangThaiTheoDoi = reader["TrangThaiTheoDoi"].ToString()
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
					SELECT BN.BenhNhanID, BN.LoaiDa, BN.TrangThaiTheoDoi, BN.GhiChu, TNC.ThongTinID, 
							TNC.TaiKhoanID, TNC.HoTen, TNC.NgaySinh, TNC.GioiTinh, TNC.SDT,
							TNC.EmailLienHe, TNC.DiaChi, TNC.Avatar, TNC.NgayTao, TNC.NgayCapNhat, 
							TNC.Loai
					FROM BenhNhan BN
					JOIN ThongTinCaNhan TNC ON BN.ThongTinID = TNC.ThongTinID
					WHERE BN.BenhNhanID = @BenhNhanID AND TNC.Loai = N'Bệnh nhân'
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
								Loai = reader["Loai"].ToString(),
								NgayTao = (DateTime)reader["NgayTao"],
								NgayCapNhat = (DateTime)reader["NgayCapNhat"]
							};
						}
					}
				}
			}
			return null;
		}
		public bool ThemThongTinBenhNhan(HoSoBenhNhanDTO hs)
		{
			string sql = @"
				INSERT INTO ThongTinCaNhan (HoTen, NgaySinh, GioiTinh, SDT, EmailLienHe, DiaChi, Avatar, Loai)
				VALUES (@HoTen, @NgaySinh, @GioiTinh, @SDT, @EmailLienHe, @DiaChi, @Avatar, N'Bệnh nhân');
			";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@HoTen", hs.HoTen);
					cmd.Parameters.AddWithValue("@NgaySinh", (object)hs.NgaySinh ?? DBNull.Value);
					cmd.Parameters.AddWithValue("@GioiTinh", hs.GioiTinh);
					cmd.Parameters.AddWithValue("@SDT", hs.SDT);
					cmd.Parameters.AddWithValue("@EmailLienHe", hs.EmailLienHe);
					cmd.Parameters.AddWithValue("@DiaChi", hs.DiaChi);
					cmd.Parameters.AddWithValue("@Avatar", hs.Avatar);
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected > 0;
				}
			}
		}
		public bool ThemBenhNhan(ThemBenhNhanDTO bn)
		{
			string sql = @"
				INSERT INTO BenhNhan (ThongTinID, LoaiDa, TrangThaiTheoDoi, GhiChu)
				VALUES (@ThongTinID, @LoaiDa, @TrangThaiTheoDoi, @GhiChu);
			";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@ThongTinID", bn.ThongTinID);
					cmd.Parameters.AddWithValue("@LoaiDa", bn.LoaiDa);
					cmd.Parameters.AddWithValue("@TrangThaiTheoDoi", bn.TrangThaiTheoDoi);
					cmd.Parameters.AddWithValue("@GhiChu", bn.GhiChu);
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected > 0;
				}
			}
		}
		public bool CapNhatBenhNhan(BenhNhanUpdateDTO bn)
		{
			string sql = @"
				UPDATE TNC
				SET TNC.HoTen = @HoTen, TNC.NgaySinh = @NgaySinh, TNC.GioiTinh = @GioiTinh,TNC.SDT = @SDT,
					TNC.EmailLienHe = @EmailLienHe, TNC.DiaChi = @DiaChi, TNC.Avatar = @Avatar,
					TNC.NgayCapNhat = GETDATE()
				From ThongTinCaNhan TNC
				JOIN BenhNhan BN ON TNC.ThongTinID = BN.ThongTinID
				WHERE BN.BenhNhanID = @BenhNhanID;
				
				UPDATE BenhNhan
				SET LoaiDa = @LoaiDa, TrangThaiTheoDoi = @TrangThaiTheoDoi, GhiChu = @GhiChu	
				WHERE BenhNhanID = @BenhNhanID;
			";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@BenhNhanID", bn.BenhNhanID);
					cmd.Parameters.AddWithValue("@HoTen", bn.HoTen);
					cmd.Parameters.AddWithValue("@NgaySinh", (object)bn.NgaySinh ?? DBNull.Value);
					cmd.Parameters.AddWithValue("@GioiTinh", bn.GioiTinh);
					cmd.Parameters.AddWithValue("@SDT", bn.SDT);
					cmd.Parameters.AddWithValue("@EmailLienHe", bn.EmailLienHe);
					cmd.Parameters.AddWithValue("@DiaChi", bn.DiaChi);
					cmd.Parameters.Add("@Avatar", SqlDbType.NVarChar, 255).Value = string.IsNullOrEmpty(bn.Avatar) ? DBNull.Value : (object)bn.Avatar;
					cmd.Parameters.AddWithValue("@LoaiDa", bn.LoaiDa);
					cmd.Parameters.AddWithValue("@TrangThaiTheoDoi", bn.TrangThaiTheoDoi);
					cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 500).Value = bn.GhiChu ?? (object)DBNull.Value;
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected > 0;
				}
			}
		}
		public bool ChuyenTrangThai(Status stt)
		{
			string sql = @"
				UPDATE BN
				SET BN.TrangThaiTheoDoi = @TrangThai, BN.NgayCapNhat = GETDATE(),
					TCN.NgayCapNhat = GETDATE()
				FROM BenhNhan BN
				JOIN ThongTinCaNhan TCN ON BN.ThongTinID = TCN.ThongTinID
				WHERE BN.BenhNhanID = @BenhNhanID
			";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@BenhNhanID", stt.Id);
					cmd.Parameters.AddWithValue("@TrangThai", stt.TrangThai);
					int rowsAffected = cmd.ExecuteNonQuery();
					return rowsAffected > 0;
				}
			}
		}
	}

}
