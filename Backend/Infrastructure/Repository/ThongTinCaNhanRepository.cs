using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using Application.Interfaces;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
	public class ThongTinCaNhanRepository : IThongTinCaNhanRepository
	{
		private readonly string _connectionString;

		public ThongTinCaNhanRepository(IConfiguration config)
		{
			_connectionString = config.GetConnectionString("DefaultConnection");
		}

		public ThongTinCaNhan GetById(int thongTinID)
		{
			string sql = @"
				SELECT ThongTinID, TaiKhoanID, HoTen, NgaySinh, GioiTinh, SDT, EmailLienHe, DiaChi, Avatar, Loai, NgayTao, NgayCapNhat
				FROM ThongTinCaNhan
				WHERE ThongTinID = @ThongTinID
			";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				cmd.Parameters.AddWithValue("@ThongTinID", thongTinID);
				conn.Open();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						return MapToEntityFull(reader);
					}
					else
					{
						return null;
					}
				}
			}
		}
		public List<ThongTinCaNhan> GetAllByLoai(string loai)
		{
			string sql = @"
				SELECT ThongTinID, TaiKhoanID, HoTen, SDT, EmailLienHe, Loai, NgayTao, NgayCapNhat
				FROM ThongTinCaNhan
				WHERE Loai = @Loai
			";
			List<ThongTinCaNhan> list = new List<ThongTinCaNhan>();
			using (SqlConnection conn = new SqlConnection(_connectionString))
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				cmd.Parameters.AddWithValue("@Loai", loai);
				conn.Open();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						list.Add(MapToEntityLite(reader));
					}
				}
			}
			return list;
		}
		public int Add(ThongTinCaNhan tt)
		{
			const string sql = @"
				INSERT INTO ThongTinCaNhan
				(HoTen, NgaySinh, GioiTinh, SDT, EmailLienHe, DiaChi, Avatar, Loai)
				OUTPUT INSERTED.ThongTinID
				VALUES
				(@HoTen, @NgaySinh, @GioiTinh, @SDT, @Email, @DiaChi, @Avatar, @Loai)
			";

			using (SqlConnection conn = new SqlConnection(_connectionString))
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				cmd.Parameters.AddWithValue("@HoTen", tt.HoTen);
				cmd.Parameters.AddWithValue("@NgaySinh", (object)tt.NgaySinh ?? DBNull.Value);
				cmd.Parameters.AddWithValue("@GioiTinh", tt.GioiTinh);
				cmd.Parameters.AddWithValue("@SDT", tt.SDT);
				cmd.Parameters.AddWithValue("@Email", tt.EmailLienHe);
				cmd.Parameters.AddWithValue("@DiaChi", tt.DiaChi);
				cmd.Parameters.AddWithValue("@Avatar", (object)tt.Avatar ?? DBNull.Value);
				cmd.Parameters.AddWithValue("@Loai", tt.Loai);

				conn.Open();
				int insertedId = (int)cmd.ExecuteScalar();
				return insertedId;
			}
		}

		public void Update(ThongTinCaNhan tt)
		{
			string sql = @"
				UPDATE ThongTinCaNhan
				SET HoTen = @HoTen,
					NgaySinh = @NgaySinh,
					GioiTinh = @GioiTinh,
					SDT = @SDT,
					EmailLienHe = @Email,
					DiaChi = @DiaChi,
					Avatar = @Avatar,
					NgayCapNhat = GETDATE()
				WHERE ThongTinID = @ThongTinID
			";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				cmd.Parameters.AddWithValue("@HoTen", tt.HoTen);
				cmd.Parameters.AddWithValue("@NgaySinh", (object)tt.NgaySinh ?? DBNull.Value);
				cmd.Parameters.AddWithValue("@GioiTinh", tt.GioiTinh);
				cmd.Parameters.AddWithValue("@SDT", tt.SDT);
				cmd.Parameters.AddWithValue("@Email", tt.EmailLienHe);
				cmd.Parameters.AddWithValue("@DiaChi", tt.DiaChi);
				cmd.Parameters.AddWithValue("@Avatar", (object)tt.Avatar ?? DBNull.Value);
				cmd.Parameters.AddWithValue("@ThongTinID", tt.ThongTinID);
				conn.Open();
				cmd.ExecuteNonQuery();
			}
		}
		private ThongTinCaNhan MapToEntityFull(SqlDataReader reader)
		{
			return new ThongTinCaNhan(
				thongTinID: (int)reader["ThongTinID"],
				taiKhoanID: reader["TaiKhoanID"] == DBNull.Value ? null : (int?)reader["TaiKhoanID"],
				hoTen: reader["HoTen"].ToString(),
				ngaySinh: reader["NgaySinh"] == DBNull.Value ? null : (DateTime?)reader["NgaySinh"],
				gioiTinh: reader["GioiTinh"].ToString(),
				sdt: reader["SDT"].ToString(),
				emailLienHe: reader["EmailLienHe"].ToString(),
				diaChi: reader["DiaChi"].ToString(),
				avatar: reader["Avatar"] == DBNull.Value ? null : reader["Avatar"].ToString(),
				loai: reader["Loai"].ToString(),
				ngayTao: (DateTime)reader["NgayTao"],
				ngayCapNhat: (DateTime)reader["NgayCapNhat"]
			);
		}
		private ThongTinCaNhan MapToEntityLite(SqlDataReader reader)
		{
			return new ThongTinCaNhan(
				thongTinID: (int)reader["ThongTinID"],
				taiKhoanID: reader["TaiKhoanID"] == DBNull.Value ? null : (int?)reader["TaiKhoanID"],
				hoTen: reader["HoTen"].ToString(),
				ngaySinh: null,
				gioiTinh: null,
				sdt: reader["SDT"].ToString(),
				emailLienHe: reader["EmailLienHe"].ToString(),
				diaChi: null,
				avatar:null,
				loai: reader["Loai"].ToString(),
				ngayTao: (DateTime)reader["NgayTao"],
				ngayCapNhat: (DateTime)reader["NgayCapNhat"]
			);
		}

	}
}
