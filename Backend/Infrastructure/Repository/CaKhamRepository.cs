using Domain.DTO;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class CaKhamRepository : ICaKhamRepository
    {
        public readonly string _connectionString;
        public CaKhamRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        public List<CaKhamDTO> DanhSach()
        {
            List<CaKhamDTO> listCaKham = new List<CaKhamDTO>();
            string sql = "SELECT * FROM CaKham";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listCaKham.Add(new CaKhamDTO
                            {
                                CaKhamID = (int)reader["CaKhamID"],
                                LichLamViecID = (int)reader["LichLamViecID"],
                                PhongChucNangID = reader["PhongChucNangID"] as int?,
                                NgayKham = (DateTime)reader["NgayKham"],
                                KhungGioID = (int)reader["KhungGioID"],
                                BenhNhanID = reader["BenhNhanID"] as int?,
                                LyDoKham = reader["LyDoKham"].ToString(),
                                TrangThai = reader["TrangThai"].ToString(),
                                NgayDat = (DateTime)reader["NgayDat"],
                                GhiChu = reader["GhiChu"].ToString()
                            });
                        }
                    }
                }
            }
            return listCaKham;
        }
        public CaKhamDTO GetByID(int id)
        {
            CaKhamDTO ck = null;
            string sql = "SELECT * FROM CaKham WHERE CaKhamID = @id";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ck = new CaKhamDTO
                            {
                                CaKhamID = (int)reader["CaKhamID"],
                                LichLamViecID = (int)reader["LichLamViecID"],
                                PhongChucNangID = reader["PhongChucNangID"] as int?,
                                NgayKham = (DateTime)reader["NgayKham"],
                                KhungGioID = (int)reader["KhungGioID"],
                                BenhNhanID = reader["BenhNhanID"] as int?,
                                LyDoKham = reader["LyDoKham"].ToString(),
                                TrangThai = reader["TrangThai"].ToString(),
                                NgayDat = (DateTime)reader["NgayDat"],
                                GhiChu = reader["GhiChu"].ToString()
                            };
                        }
                    }
                }
            }
            return ck;
        }
        public List<CaKhamDTO> TimKiem(string danhmuc, string keyword)
        {
            List<CaKhamDTO> list = new List<CaKhamDTO>();
            string sql = "SELECT * FROM CaKham WHERE 1 = 1";

            if (danhmuc == "Ngày khám")
                sql += " AND CAST(NgayKham AS DATE) = @Keyword";
            else if (danhmuc == "Ngày đặt")
                sql += " AND CAST(NgayDat AS DATE) = @Keyword";
            else if (danhmuc == "Bệnh nhân")
                sql += " AND CAST(BenhNhanID AS NVARCHAR) LIKE @Keyword";
            else if (danhmuc == "Khung giờ")
                sql += " AND CAST(KhungGioID AS NVARCHAR) LIKE @Keyword";
            else if (danhmuc == "Trạng thái")
                sql += " AND TrangThai LIKE @Keyword";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (danhmuc == "Ngày khám" || danhmuc == "Ngày đặt")
                        cmd.Parameters.AddWithValue("@Keyword", DateTime.Parse(keyword));
                    else
                        cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new CaKhamDTO
                            {
                                CaKhamID = (int)reader["CaKhamID"],
                                LichLamViecID = (int)reader["LichLamViecID"],
                                PhongChucNangID = reader["PhongChucNangID"] as int?,
                                NgayKham = (DateTime)reader["NgayKham"],
                                KhungGioID = (int)reader["KhungGioID"],
                                BenhNhanID = reader["BenhNhanID"] as int?,
                                LyDoKham = reader["LyDoKham"].ToString(),
                                TrangThai = reader["TrangThai"].ToString(),
                                NgayDat = (DateTime)reader["NgayDat"],
                                GhiChu = reader["GhiChu"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }
        public bool ThemCaKham(ThemCaKhamDTO ck)
        {
            string sql = @"INSERT INTO CaKham
                   (LichLamViecID, PhongChucNangID, NgayKham, KhungGioID,
                    BenhNhanID, LyDoKham, TrangThai, NgayDat, GhiChu)
                   VALUES
                   (@LichLamViecID, @PhongChucNangID, @NgayKham, @KhungGioID,
                    @BenhNhanID, @LyDoKham, @TrangThai, @NgayDat, @GhiChu)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@LichLamViecID", ck.LichLamViecID);
                    cmd.Parameters.AddWithValue("@PhongChucNangID", ck.PhongChucNangID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayKham", ck.NgayKham);
                    cmd.Parameters.AddWithValue("@KhungGioID", ck.KhungGioID);
                    cmd.Parameters.AddWithValue("@BenhNhanID", ck.BenhNhanID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@LyDoKham", ck.LyDoKham);
                    cmd.Parameters.AddWithValue("@TrangThai", ck.TrangThai);
                    cmd.Parameters.AddWithValue("@NgayDat", ck.NgayDat);
                    cmd.Parameters.AddWithValue("@GhiChu", ck.GhiChu);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        public bool CapNhatCaKham(CaKhamDTO ck)
        {
            string sql = @"UPDATE CaKham
                    SET LichLamViecID = @LichLamViecID, PhongChucNangID = @PhongChucNangID, NgayKham = @NgayKham, KhungGioID = @KhungGioID,
                    BenhNhanID = @BenhNhanID, LyDoKham = @LyDoKham, TrangThai = @TrangThai, NgayDat = @NgayDat, GhiChu = @GhiChu
                    WHERE CaKhamID = @CaKhamID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CaKhamID", ck.CaKhamID);
                    cmd.Parameters.AddWithValue("@LichLamViecID", ck.LichLamViecID);
                    cmd.Parameters.AddWithValue("@PhongChucNangID", ck.PhongChucNangID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayKham", ck.NgayKham);
                    cmd.Parameters.AddWithValue("@KhungGioID", ck.KhungGioID);
                    cmd.Parameters.AddWithValue("@BenhNhanID", ck.BenhNhanID ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@LyDoKham", ck.LyDoKham);
                    cmd.Parameters.AddWithValue("@TrangThai", ck.TrangThai);
                    cmd.Parameters.AddWithValue("@NgayDat", ck.NgayDat);
                    cmd.Parameters.AddWithValue("@GhiChu", ck.GhiChu);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

    }
}
