using Domain.DTO;
using Domain.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class BacSiProfileRepository : IBacSiProfileRepository
    {
        private readonly string _connectionString;

        public BacSiProfileRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<BacSiProfileDTO> DanhSachBacSiProfile()
        {
            var danhSach = new List<BacSiProfileDTO>();
            string sql = "SELECT * FROM BacSiProfile";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        danhSach.Add(new BacSiProfileDTO
                        {
                            BacSiProfileID = (int)reader["BacSiProfileID"],
                            NhanVienID = (int)reader["NhanVienID"],
                            GioiThieu = reader["GioiThieu"].ToString(),
                            ChuyenMon = reader["ChuyenMon"].ToString(),
                            ThanhTuu = reader["ThanhTuu"].ToString(),
                            HinhAnh = reader["HinhAnh"].ToString(),
                            KinhNghiem = reader["KinhNghiem"].ToString(),
                            NgayCapNhat = (DateTime)reader["NgayCapNhat"]
                        });
                    }
                }
            }
            return danhSach;
        }

        public BacSiProfileDTO LayBacSiProfileByID(int bacSiProfileID)
        {
            string sql = @"SELECT * FROM BacSiProfile 
                           WHERE BacSiProfileID = @BacSiProfileID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@BacSiProfileID", bacSiProfileID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new BacSiProfileDTO
                            {
                                BacSiProfileID = (int)reader["BacSiProfileID"],
                                NhanVienID = (int)reader["NhanVienID"],
                                GioiThieu = reader["GioiThieu"].ToString(),
                                ChuyenMon = reader["ChuyenMon"].ToString(),
                                ThanhTuu = reader["ThanhTuu"].ToString(),
                                HinhAnh = reader["HinhAnh"].ToString(),
                                KinhNghiem = reader["KinhNghiem"].ToString(),
                                NgayCapNhat = (DateTime)reader["NgayCapNhat"]
                            };
                        }
                    }
                }
            }
            return null;
        }

        public BacSiProfileDTO LayBacSiProfileByNhanVienID(int nhanVienID)
        {
            string sql = @"SELECT * FROM BacSiProfile 
                           WHERE NhanVienID = @NhanVienID";

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
                            return new BacSiProfileDTO
                            {
                                BacSiProfileID = (int)reader["BacSiProfileID"],
                                NhanVienID = (int)reader["NhanVienID"],
                                GioiThieu = reader["GioiThieu"].ToString(),
                                ChuyenMon = reader["ChuyenMon"].ToString(),
                                ThanhTuu = reader["ThanhTuu"].ToString(),
                                HinhAnh = reader["HinhAnh"].ToString(),
                                KinhNghiem = reader["KinhNghiem"].ToString(),
                                NgayCapNhat = (DateTime)reader["NgayCapNhat"]
                            };
                        }
                    }
                }
            }
            return null;
        }

        public bool ThemBacSiProfile(ThemBacSiProfileDTO bacSi)
        {
            string sql = @"
                INSERT INTO BacSiProfile
                (NhanVienID, GioiThieu, ChuyenMon, ThanhTuu, HinhAnh, KinhNghiem, NgayCapNhat)
                VALUES
                (@NhanVienID, @GioiThieu, @ChuyenMon, @ThanhTuu, @HinhAnh, @KinhNghiem, @NgayCapNhat)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NhanVienID", bacSi.NhanVienID);
                    cmd.Parameters.AddWithValue("@GioiThieu", bacSi.GioiThieu);
                    cmd.Parameters.AddWithValue("@ChuyenMon", bacSi.ChuyenMon);
                    cmd.Parameters.AddWithValue("@ThanhTuu", bacSi.ThanhTuu);
                    cmd.Parameters.AddWithValue("@HinhAnh", bacSi.HinhAnh);
                    cmd.Parameters.AddWithValue("@KinhNghiem", bacSi.KinhNghiem);
                    cmd.Parameters.AddWithValue("@NgayCapNhat", bacSi.NgayCapNhat);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool CapNhatBacSiProfile(BacSiProfileDTO bacSi)
        {
            string sql = @"
                UPDATE BacSiProfile
                SET GioiThieu = @GioiThieu,
                    ChuyenMon = @ChuyenMon,
                    ThanhTuu = @ThanhTuu,
                    HinhAnh = @HinhAnh,
                    KinhNghiem = @KinhNghiem,
                    NgayCapNhat = @NgayCapNhat
                WHERE BacSiProfileID = @BacSiProfileID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@BacSiProfileID", bacSi.BacSiProfileID);
                    cmd.Parameters.AddWithValue("@GioiThieu", bacSi.GioiThieu);
                    cmd.Parameters.AddWithValue("@ChuyenMon", bacSi.ChuyenMon);
                    cmd.Parameters.AddWithValue("@ThanhTuu", bacSi.ThanhTuu);
                    cmd.Parameters.AddWithValue("@HinhAnh", bacSi.HinhAnh);
                    cmd.Parameters.AddWithValue("@KinhNghiem", bacSi.KinhNghiem);
                    cmd.Parameters.AddWithValue("@NgayCapNhat", bacSi.NgayCapNhat);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
