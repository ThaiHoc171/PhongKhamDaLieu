using Domain.DTO;
using Domain.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class PhongKhamRepository : IPhongKhamRepository
    {
        private readonly string _connectionString;

        public PhongKhamRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<PhongKhamDTO> DanhSachPhongKham()
        {
            var danhSach = new List<PhongKhamDTO>();
            string sql = "SELECT * FROM PhongKham";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        danhSach.Add(new PhongKhamDTO
                        {
                            PhongKhamID = (int)reader["PhongKhamID"],
                            TenPhongKham = reader["TenPhongKham"].ToString(),
                            GioiThieu = reader["GioiThieu"].ToString(),
                            DiaChi = reader["DiaChi"].ToString(),
                            Hotline = reader["Hotline"].ToString(),
                            Email = reader["Email"].ToString(),
                            Website = reader["Website"].ToString(),
                            GioMoCua = reader["GioMoCua"].ToString(),
                            NgayCapNhat = (DateTime)reader["NgayCapNhat"]
                        });
                    }
                }
            }
            return danhSach;
        }

        public bool CapNhatPhongKham(PhongKhamDTO phongKham)
        {
            string sql = @"
                UPDATE PhongKham
                SET TenPhongKham = @TenPhongKham,
                    GioiThieu = @GioiThieu,
                    DiaChi = @DiaChi,
                    Hotline = @Hotline,
                    Email = @Email,
                    Website = @Website,
                    GioMoCua = @GioMoCua,
                    NgayCapNhat = @NgayCapNhat
                WHERE PhongKhamID = @PhongKhamID";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PhongKhamID", phongKham.PhongKhamID);
                    cmd.Parameters.AddWithValue("@TenPhongKham", phongKham.TenPhongKham);
                    cmd.Parameters.AddWithValue("@GioiThieu", phongKham.GioiThieu);
                    cmd.Parameters.AddWithValue("@DiaChi", phongKham.DiaChi);
                    cmd.Parameters.AddWithValue("@Hotline", phongKham.Hotline);
                    cmd.Parameters.AddWithValue("@Email", phongKham.Email);
                    cmd.Parameters.AddWithValue("@Website", phongKham.Website);
                    cmd.Parameters.AddWithValue("@GioMoCua", phongKham.GioMoCua);
                    cmd.Parameters.AddWithValue("@NgayCapNhat", phongKham.NgayCapNhat);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
