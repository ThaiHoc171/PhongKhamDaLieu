using Domain.DTO;
using Domain.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class NgayNghiNhanVienRepository : INgayNghiNhanVienRepository
    {
        private readonly string _connectionString;

        public NgayNghiNhanVienRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<NgayNghiNhanVienDTO> DanhSach()
        {
            var list = new List<NgayNghiNhanVienDTO>();
            string sql = "SELECT * FROM NgayNghiNhanVien";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new NgayNghiNhanVienDTO
                        {
                            NgayNghiID = (int)reader["NgayNghiID"],
                            NhanVienID = (int)reader["NhanVienID"],
                            Ngay = (DateTime)reader["Ngay"],
                            LyDo = reader["LyDo"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public NgayNghiNhanVienDTO LayNgayNghiByID(int ngayNghiID)
        {
            string sql = "SELECT * FROM NgayNghiNhanVien WHERE NgayNghiID = @NgayNghiID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NgayNghiID", ngayNghiID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new NgayNghiNhanVienDTO
                            {
                                NgayNghiID = (int)reader["NgayNghiID"],
                                NhanVienID = (int)reader["NhanVienID"],
                                Ngay = (DateTime)reader["Ngay"],
                                LyDo = reader["LyDo"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<NgayNghiNhanVienDTO> LayNgayNghiByNhanVien(int nhanVienID)
        {
            var list = new List<NgayNghiNhanVienDTO>();
            string sql = "SELECT * FROM NgayNghiNhanVien WHERE NhanVienID = @NhanVienID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NhanVienID", nhanVienID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new NgayNghiNhanVienDTO
                            {
                                NgayNghiID = (int)reader["NgayNghiID"],
                                NhanVienID = (int)reader["NhanVienID"],
                                Ngay = (DateTime)reader["Ngay"],
                                LyDo = reader["LyDo"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }

        public bool ThemNgayNghi(ThemNgayNghiNhanVienDTO nn)
        {
            string sql = @"
				INSERT INTO NgayNghiNhanVien (NhanVienID, Ngay, LyDo)
				VALUES (@NhanVienID, @Ngay, @LyDo)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NhanVienID", nn.NhanVienID);
                    cmd.Parameters.AddWithValue("@Ngay", nn.Ngay);
                    cmd.Parameters.AddWithValue("@LyDo", nn.LyDo);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool CapNhatNgayNghi(NgayNghiNhanVienDTO nn)
        {
            string sql = @"
				UPDATE NgayNghiNhanVien
				SET Ngay = @Ngay,
					LyDo = @LyDo
				WHERE NgayNghiID = @NgayNghiID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NgayNghiID", nn.NgayNghiID);
                    cmd.Parameters.AddWithValue("@Ngay", nn.Ngay);
                    cmd.Parameters.AddWithValue("@LyDo", nn.LyDo);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
