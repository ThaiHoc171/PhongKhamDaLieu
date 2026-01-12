using Domain.DTO;
using Domain.Repository;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class LichLamViecNhanVienRepository : ILichLamViecNhanVienRepository
    {
        private readonly string _connectionString;
        public LichLamViecNhanVienRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<LichLamViecNhanVienDTO> DanhSach()
        {
            List<LichLamViecNhanVienDTO> listLich = new List<LichLamViecNhanVienDTO>();
            string sql = "SELECT * FROM LichLamViecNhanVien";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listLich.Add(new LichLamViecNhanVienDTO
                            {
                                LichLamViecID = (int)reader["LichLamViecID"],
                                NhanVienID = (int)reader["NhanVienID"],
                                Ngay = (DateTime)reader["Ngay"],
                                CaLamViec = (int)reader["CaLamViec"],
                                GhiChu = reader["GhiChu"].ToString()
                            });
                        }
                    }
                }
                return listLich;
            }
        }

        public LichLamViecNhanVienDTO LayLichLamViecNhanVienByID(int LichLamViecID)
        {
            string sql = "SELECT * FROM LichLamViecNhanVien WHERE LichLamViecID = @LichLamViecID";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@LichLamViecID", LichLamViecID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new LichLamViecNhanVienDTO
                            {
                                LichLamViecID = (int)reader["LichLamViecID"],
                                NhanVienID = (int)reader["NhanVienID"],
                                Ngay = (DateTime)reader["Ngay"],
                                CaLamViec = (int)reader["CaLamViec"],
                                GhiChu = reader["GhiChu"].ToString()
                            };
                        }
                    }
                }
                return null;
            }
        }

        public List<LichLamViecNhanVienDTO> LayLichLamViecNhanVienByNhanVien(int NhanVienID)
        {
            var danhSach = new List<LichLamViecNhanVienDTO>();

            string sql = @"
        SELECT *
        FROM LichLamViecNhanVien
        WHERE NhanVienID = @NhanVienID
    ";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NhanVienID", NhanVienID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSach.Add(new LichLamViecNhanVienDTO
                            {
                                LichLamViecID = (int)reader["LichLamViecID"],
                                NhanVienID = (int)reader["NhanVienID"],
                                Ngay = (DateTime)reader["Ngay"],
                                CaLamViec = (int)reader["CaLamViec"],
                                GhiChu = reader["GhiChu"].ToString()
                            });
                        }
                    }
                }
            }
            return danhSach;
        }

        public bool ThemLichLamViecNhanVien(ThemLichLamViecNhanVienDTO lich)
        {
            string sql = @"
            INSERT INTO LichLamViecNhanVien
            (NhanVienID, Ngay, CaLamViec, GhiChu)
            VALUES
            (@NhanVienID, @Ngay, @CaLamViec, @GhiChu)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NhanVienID", lich.NhanVienID);
                    cmd.Parameters.AddWithValue("@Ngay", lich.Ngay);
                    cmd.Parameters.AddWithValue("@CaLamViec", lich.CaLamViec);
                    cmd.Parameters.AddWithValue("@GhiChu", lich.GhiChu);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool CapNhatLichLamViecNhanVien(LichLamViecNhanVienDTO lich)
        {
            string sql = @"
            UPDATE LichLamViecNhanVien
            SET NhanVienID = @NhanVienID,
                Ngay = @Ngay,
                CaLamViec = @CaLamViec,
                GhiChu = @GhiChu
            WHERE LichLamViecID = @LichLamViecID";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@LichLamViecID", lich.LichLamViecID);
                    cmd.Parameters.AddWithValue("@NhanVienID", lich.NhanVienID);
                    cmd.Parameters.AddWithValue("@Ngay", lich.Ngay);
                    cmd.Parameters.AddWithValue("@CaLamViec", lich.CaLamViec);
                    cmd.Parameters.AddWithValue("@GhiChu", lich.GhiChu);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
