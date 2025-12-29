using Domain.DTO;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{


    public class ToaThuocRepository : IToaThuocRepository
    {
        private readonly string _connectionString;

        public ToaThuocRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<ToaThuocDTO> DanhSachToaThuoc()
        {
            var danhSach = new List<ToaThuocDTO>();
            string sql = "SELECT * FROM ToaThuoc";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        danhSach.Add(new ToaThuocDTO
                        {
                            ToaThuocID = (int)reader["ToaThuocID"],
                            PhienKhamID = (int)reader["PhienKhamID"],
                            NhanVienKeDonID = (int)reader["NhanVienKeDonID"],
                            NgayLap = (DateTime)reader["NgayLap"],
                            GhiChu = reader["GhiChu"].ToString()
                        });
                    }
                }
            }
            return danhSach;
        }

        public ToaThuocDTO LayToaThuocByID(int toaThuocID)
        {
            string sql = "SELECT * FROM ToaThuoc WHERE ToaThuocID = @ToaThuocID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ToaThuocID", toaThuocID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ToaThuocDTO
                            {
                                ToaThuocID = (int)reader["ToaThuocID"],
                                PhienKhamID = (int)reader["PhienKhamID"],
                                NhanVienKeDonID = (int)reader["NhanVienKeDonID"],
                                NgayLap = (DateTime)reader["NgayLap"],
                                GhiChu = reader["GhiChu"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<ToaThuocDTO> LayToaThuocByPhienKhamID(int phienKhamID)
        {
            var danhSach = new List<ToaThuocDTO>();

            string sql = @"SELECT * FROM ToaThuoc WHERE PhienKhamID = @PhienKhamID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PhienKhamID", phienKhamID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSach.Add(new ToaThuocDTO
                            {
                                ToaThuocID = (int)reader["ToaThuocID"],
                                PhienKhamID = (int)reader["PhienKhamID"],
                                NhanVienKeDonID = (int)reader["NhanVienKeDonID"],
                                NgayLap = (DateTime)reader["NgayLap"],
                                GhiChu = reader["GhiChu"].ToString()
                            });
                        }
                    }
                }
            }
            return danhSach;
        }


        public bool ThemToaThuoc(ToaThuocCreateDTO toaThuoc)
        {
            string sql = @"
                INSERT INTO ToaThuoc
                (PhienKhamID, NhanVienKeDonID, NgayLap, GhiChu)
                VALUES
                (@PhienKhamID, @NhanVienKeDonID, @NgayLap, @GhiChu)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PhienKhamID", toaThuoc.PhienKhamID);
                    cmd.Parameters.AddWithValue("@NhanVienKeDonID", toaThuoc.NhanVienKeDonID);
                    cmd.Parameters.AddWithValue("@NgayLap", toaThuoc.NgayLap);
                    cmd.Parameters.AddWithValue("@GhiChu", toaThuoc.GhiChu);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool CapNhatToaThuoc(ToaThuocDTO toaThuoc)
        {
            string sql = @"
                UPDATE ToaThuoc
                SET PhienKhamID = @PhienKhamID,
                    NhanVienKeDonID = @NhanVienKeDonID,
                    NgayLap = @NgayLap,
                    GhiChu = @GhiChu
                WHERE ToaThuocID = @ToaThuocID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ToaThuocID", toaThuoc.ToaThuocID);
                    cmd.Parameters.AddWithValue("@PhienKhamID", toaThuoc.PhienKhamID);
                    cmd.Parameters.AddWithValue("@NhanVienKeDonID", toaThuoc.NhanVienKeDonID);
                    cmd.Parameters.AddWithValue("@NgayLap", toaThuoc.NgayLap);
                    cmd.Parameters.AddWithValue("@GhiChu", toaThuoc.GhiChu);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
