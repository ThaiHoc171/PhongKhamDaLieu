using Domain.DTO;
using Domain.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class ThietBiRepository : IThietBiRepository
    {
        private readonly string _connectionString;

        public ThietBiRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<ThietBiDTO> DanhSachThietBi()
        {
            var danhSach = new List<ThietBiDTO>();
            string sql = "SELECT * FROM ThietBi";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        danhSach.Add(new ThietBiDTO
                        {
                            ThietBiID = (int)reader["ThietBiID"],
                            TenTB = reader["TenTB"].ToString(),
                            LoaiTB = reader["LoaiTB"].ToString(),
                            TinhTrang = reader["TinhTrang"].ToString(),
                            NgayNhap = (DateTime)reader["NgayNhap"]
                        });
                    }
                }
            }
            return danhSach;
        }

        public ThietBiDTO LayThietBiByID(int thietBiID)
        {
            string sql = "SELECT * FROM ThietBi WHERE ThietBiID = @ThietBiID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ThietBiID", thietBiID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ThietBiDTO
                            {
                                ThietBiID = (int)reader["ThietBiID"],
                                TenTB = reader["TenTB"].ToString(),
                                LoaiTB = reader["LoaiTB"].ToString(),
                                TinhTrang = reader["TinhTrang"].ToString(),
                                NgayNhap = (DateTime)reader["NgayNhap"]
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<ThietBiDTO> LayThietBiByTen(string tenTB)
        {
            var danhSach = new List<ThietBiDTO>();
            string sql = "SELECT * FROM ThietBi WHERE TenTB LIKE @TenTB";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TenTB", "%" + tenTB + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSach.Add(new ThietBiDTO
                            {
                                ThietBiID = (int)reader["ThietBiID"],
                                TenTB = reader["TenTB"].ToString(),
                                LoaiTB = reader["LoaiTB"].ToString(),
                                TinhTrang = reader["TinhTrang"].ToString(),
                                NgayNhap = (DateTime)reader["NgayNhap"]
                            });
                        }
                    }
                }
            }
            return danhSach;
        }

        public bool ThemThietBi(ThemThietBiDTO tb)
        {
            string sql = @"
                INSERT INTO ThietBi (TenTB, LoaiTB, TinhTrang, NgayNhap)
                VALUES (@TenTB, @LoaiTB, @TinhTrang, @NgayNhap)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TenTB", tb.TenTB);
                    cmd.Parameters.AddWithValue("@LoaiTB", tb.LoaiTB);
                    cmd.Parameters.AddWithValue("@TinhTrang", tb.TinhTrang);
                    cmd.Parameters.AddWithValue("@NgayNhap", tb.NgayNhap);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool CapNhatThietBi(ThietBiDTO tb)
        {
            string sql = @"
                UPDATE ThietBi
                SET TenTB = @TenTB,
                    LoaiTB = @LoaiTB,
                    TinhTrang = @TinhTrang,
                    NgayNhap = @NgayNhap
                WHERE ThietBiID = @ThietBiID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ThietBiID", tb.ThietBiID);
                    cmd.Parameters.AddWithValue("@TenTB", tb.TenTB);
                    cmd.Parameters.AddWithValue("@LoaiTB", tb.LoaiTB);
                    cmd.Parameters.AddWithValue("@TinhTrang", tb.TinhTrang);
                    cmd.Parameters.AddWithValue("@NgayNhap", tb.NgayNhap);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
