using Domain.DTO;
using Domain.Repository;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{

    public class ChiTietToaThuocRepository : IChiTietToaThuocRepository
    {
        private readonly string _connectionString;

        public ChiTietToaThuocRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<ChiTietToaThuocDTO> DanhSachChiTietToaThuoc()
        {
            var danhSach = new List<ChiTietToaThuocDTO>();
            string sql = "SELECT * FROM ChiTietToaThuoc";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        danhSach.Add(new ChiTietToaThuocDTO
                        {
                            ChiTietToaThuocID = (int)reader["ChiTietToaThuocID"],
                            ToaThuocID = (int)reader["ToaThuocID"],
                            ThuocID = (int)reader["ThuocID"],
                            LieuDung = reader["LieuDung"].ToString(),
                            SoLuong = (int)reader["SoLuong"],
                        });
                    }
                }
            }
            return danhSach;
        }

        public ChiTietToaThuocDTO LayChiTietToaThuocByID(int chiTietToaThuocID)
        {
            string sql = @"SELECT * FROM ChiTietToaThuoc 
                           WHERE ChiTietToaThuocID = @ChiTietToaThuocID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ChiTietToaThuocID", chiTietToaThuocID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ChiTietToaThuocDTO
                            {
                                ChiTietToaThuocID = (int)reader["ChiTietToaThuocID"],
                                ToaThuocID = (int)reader["ToaThuocID"],
                                ThuocID = (int)reader["ThuocID"],
                                LieuDung = reader["LieuDung"].ToString(),
                                SoLuong = (int)reader["SoLuong"],
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<ChiTietToaThuocDTO> LayChiTietToaThuocByToaThuocID(int toaThuocID)
        {
            var danhSach = new List<ChiTietToaThuocDTO>();
            string sql = @"SELECT * FROM ChiTietToaThuoc 
                           WHERE ToaThuocID = @ToaThuocID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ToaThuocID", toaThuocID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSach.Add(new ChiTietToaThuocDTO
                            {
                                ChiTietToaThuocID = (int)reader["ChiTietToaThuocID"],
                                ToaThuocID = (int)reader["ToaThuocID"],
                                ThuocID = (int)reader["ThuocID"],
                                LieuDung = reader["LieuDung"].ToString(),
                                SoLuong = (int)reader["SoLuong"],
                            });
                        }
                    }
                }
            }
            return danhSach;
        }

        public bool ThemChiTietToaThuoc(ChiTietToaThuocCreateDTO ct)
        {
            string sql = @"
                INSERT INTO ChiTietToaThuoc
                (ToaThuocID, ThuocID, LieuDung, SoLuong)
                VALUES
                (@ToaThuocID, @ThuocID, @LieuDung, @SoLuong)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ToaThuocID", ct.ToaThuocID);
                    cmd.Parameters.AddWithValue("@ThuocID", ct.ThuocID);
                    cmd.Parameters.AddWithValue("@LieuDung", ct.LieuDung);
                    cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool CapNhatChiTietToaThuoc(ChiTietToaThuocDTO ct)
        {
            string sql = @"
                UPDATE ChiTietToaThuoc
                SET ToaThuocID = @ToaThuocID,
                    ThuocID = @ThuocID,
                    LieuDung = @LieuDung,
                    SoLuong = @SoLuong
                WHERE ChiTietToaThuocID = @ChiTietToaThuocID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ChiTietToaThuocID", ct.ChiTietToaThuocID);
                    cmd.Parameters.AddWithValue("@ToaThuocID", ct.ToaThuocID);
                    cmd.Parameters.AddWithValue("@ThuocID", ct.ThuocID);
                    cmd.Parameters.AddWithValue("@LieuDung", ct.LieuDung);
                    cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}

