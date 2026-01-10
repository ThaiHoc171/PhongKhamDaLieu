using Domain.DTO;
using Domain.Repository;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class PhongChucNang_ThietBiRepository : IPhongChucNang_ThietBiRepository
    {
        private readonly string _connectionString;
        public PhongChucNang_ThietBiRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<PhongChucNang_ThietBiDTO> DanhSach()
        {
            List<PhongChucNang_ThietBiDTO> listPCN_TB = new List<PhongChucNang_ThietBiDTO>();
            string sql = "SELECT * FROM PhongChucNang_ThietBi";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listPCN_TB.Add(new PhongChucNang_ThietBiDTO
                            {
                                PCN_TB_ID = (int)reader["PCN_TB_ID"],
                                PhongChucNangID = (int)reader["PhongChucNangID"],
                                ThietBiID = (int)reader["ThietBiID"],
                                SoLuong = (int)reader["SoLuong"],
                                GhiChu = reader["GhiChu"].ToString()
                            });
                        }
                    }
                }
                return listPCN_TB;
            }
        }

        public PhongChucNang_ThietBiDTO LayPhongChucNang_ThietBiByID(int PCN_TB_ID)
        {
            string sql = "SELECT * FROM PhongChucNang_ThietBi WHERE PCN_TB_ID = @PCN_TB_ID";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PCN_TB_ID", PCN_TB_ID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new PhongChucNang_ThietBiDTO
                            {
                                PCN_TB_ID = (int)reader["PCN_TB_ID"],
                                PhongChucNangID = (int)reader["PhongChucNangID"],
                                ThietBiID = (int)reader["ThietBiID"],
                                SoLuong = (int)reader["SoLuong"],
                                GhiChu = reader["GhiChu"].ToString()
                            };
                        }
                    }
                }
                return null;
            }
        }

        public List<PhongChucNang_ThietBiDTO> LayPhongChucNang_ThietBiByPhong(int PhongChucNangID)
        {
            var danhSach = new List<PhongChucNang_ThietBiDTO>();

            string sql = @"
        SELECT *
        FROM PhongChucNang_ThietBi
        WHERE PhongChucNangID = @PhongChucNangID
    ";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PhongChucNangID", PhongChucNangID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            danhSach.Add(new PhongChucNang_ThietBiDTO
                            {
                                PCN_TB_ID = (int)reader["PCN_TB_ID"],
                                PhongChucNangID = (int)reader["PhongChucNangID"],
                                ThietBiID = (int)reader["ThietBiID"],
                                SoLuong = (int)reader["SoLuong"],
                                GhiChu = reader["GhiChu"].ToString()
                            });
                        }
                    }
                }
            }
            return danhSach;
        }

        public bool ThemPhongChucNang_ThietBi(ThemPhongChucNang_ThietBiDTO pcn_tb)
        {
            string sql = @"
            INSERT INTO PhongChucNang_ThietBi
            (PhongChucNangID, ThietBiID, SoLuong, GhiChu)
            VALUES
            (@PhongChucNangID, @ThietBiID, @SoLuong, @GhiChu)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PhongChucNangID", pcn_tb.PhongChucNangID);
                    cmd.Parameters.AddWithValue("@ThietBiID", pcn_tb.ThietBiID);
                    cmd.Parameters.AddWithValue("@SoLuong", pcn_tb.SoLuong);
                    cmd.Parameters.AddWithValue("@GhiChu", pcn_tb.GhiChu);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool CapNhatPhongChucNang_ThietBi(PhongChucNang_ThietBiDTO pcn_tb)
        {
            string sql = @"
            UPDATE PhongChucNang_ThietBi
            SET PhongChucNangID = @PhongChucNangID,
                ThietBiID = @ThietBiID,
                SoLuong = @SoLuong,
                GhiChu = @GhiChu
            WHERE PCN_TB_ID = @PCN_TB_ID";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PCN_TB_ID", pcn_tb.PCN_TB_ID);
                    cmd.Parameters.AddWithValue("@PhongChucNangID", pcn_tb.PhongChucNangID);
                    cmd.Parameters.AddWithValue("@ThietBiID", pcn_tb.ThietBiID);
                    cmd.Parameters.AddWithValue("@SoLuong", pcn_tb.SoLuong);
                    cmd.Parameters.AddWithValue("@GhiChu", pcn_tb.GhiChu);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
