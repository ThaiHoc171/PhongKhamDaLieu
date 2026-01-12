using Domain.DTO;
using Domain.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class KhungGioKhamRepository : IKhungGioKhamRepository
    {
        private readonly string _connectionString;

        public KhungGioKhamRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<KhungGioKhamDTO> DanhSach()
        {
            var danhSach = new List<KhungGioKhamDTO>();
            string sql = "SELECT * FROM KhungGioKham";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        danhSach.Add(new KhungGioKhamDTO
                        {
                            KhungGioID = (int)reader["KhungGioID"],
                            GioBatDau = (TimeSpan)reader["GioBatDau"],
                            GioKetThuc = (TimeSpan)reader["GioKetThuc"],
                            TenKhung = reader["TenKhung"].ToString(),
                            MaxSlot = (int)reader["MaxSlot"]
                        });
                    }
                }
            }
            return danhSach;
        }

        public KhungGioKhamDTO LayKhungGioKhamByID(int khungGioID)
        {
            string sql = "SELECT * FROM KhungGioKham WHERE KhungGioID = @KhungGioID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@KhungGioID", khungGioID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new KhungGioKhamDTO
                            {
                                KhungGioID = (int)reader["KhungGioID"],
                                GioBatDau = (TimeSpan)reader["GioBatDau"],
                                GioKetThuc = (TimeSpan)reader["GioKetThuc"],
                                TenKhung = reader["TenKhung"].ToString(),
                                MaxSlot = (int)reader["MaxSlot"]
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<KhungGioKhamDTO> LayKhungGioKhamByTen(string tenKhung)
        {
            var danhSach = new List<KhungGioKhamDTO>();
            string sql = @"SELECT * FROM KhungGioKham WHERE TenKhung LIKE @TenKhung";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TenKhung", "%" + tenKhung + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSach.Add(new KhungGioKhamDTO
                            {
                                KhungGioID = (int)reader["KhungGioID"],
                                GioBatDau = (TimeSpan)reader["GioBatDau"],
                                GioKetThuc = (TimeSpan)reader["GioKetThuc"],
                                TenKhung = reader["TenKhung"].ToString(),
                                MaxSlot = (int)reader["MaxSlot"]
                            });
                        }
                    }
                }
            }
            return danhSach;
        }

        public bool ThemKhungGioKham(ThemKhungGioKhamDTO kg)
        {
            string sql = @"
			INSERT INTO KhungGioKham
			(GioBatDau, GioKetThuc, TenKhung, MaxSlot)
			VALUES
			(@GioBatDau, @GioKetThuc, @TenKhung, @MaxSlot)";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@GioBatDau", kg.GioBatDau);
                    cmd.Parameters.AddWithValue("@GioKetThuc", kg.GioKetThuc);
                    cmd.Parameters.AddWithValue("@TenKhung", kg.TenKhung);
                    cmd.Parameters.AddWithValue("@MaxSlot", kg.MaxSlot);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool CapNhatKhungGioKham(KhungGioKhamDTO kg)
        {
            string sql = @"
			UPDATE KhungGioKham
			SET GioBatDau = @GioBatDau,
			    GioKetThuc = @GioKetThuc,
			    TenKhung = @TenKhung,
			    MaxSlot = @MaxSlot
			WHERE KhungGioID = @KhungGioID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@KhungGioID", kg.KhungGioID);
                    cmd.Parameters.AddWithValue("@GioBatDau", kg.GioBatDau);
                    cmd.Parameters.AddWithValue("@GioKetThuc", kg.GioKetThuc);
                    cmd.Parameters.AddWithValue("@TenKhung", kg.TenKhung);
                    cmd.Parameters.AddWithValue("@MaxSlot", kg.MaxSlot);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
