    using Domain.DTO;
    using Domain.Repository;
    using System;
    using System.Collections.Generic;
    using Microsoft.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;

    namespace Infrastructure.Repositories
    {
        public class LoaiBenhRepository : ILoaiBenhRepository
        {
            private readonly string _connectionString;
            public LoaiBenhRepository(IConfiguration config)
            {
                _connectionString = config.GetConnectionString("DefaultConnection");
            }
            public List<LoaiBenhDTO> DanhSachLoaiBenh()
            {
                List<LoaiBenhDTO> lishLoaiBenh = new List<LoaiBenhDTO>();
                string sql = "SELECT * FROM LoaiBenh";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lishLoaiBenh.Add(new LoaiBenhDTO
                                {
                                    LoaiBenhID = (int)reader["LoaiBenhID"],
                                    TenBenh = reader["TenBenh"].ToString(),
                                    TenKhoaHoc = reader["TenKhoaHoc"].ToString(),
                                    NhomBenh = reader["NhomBenh"].ToString(),
                                    MoTa = reader["MoTa"].ToString(),
                                    DoPhoBien = reader["DoPhoBien"].ToString(),
                                    MucDoNghiemTrong = reader["MucDoNghiemTrong"].ToString(),
                                    NgayTao = (DateTime)reader["NgayTao"]
                                });
                            }
                        }
                    }
                    return lishLoaiBenh;
                }
            }
            public LoaiBenhDTO LayLoaiBenhByID(int loaiBenhID)
            {
                string sql = "SELECT * FROM LoaiBenh WHERE LoaiBenhID = @loaiBenhID";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@loaiBenhID", loaiBenhID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return new LoaiBenhDTO
                                {
                                    LoaiBenhID = (int)reader["LoaiBenhID"],
                                    TenBenh = reader["TenBenh"].ToString(),
                                    TenKhoaHoc = reader["TenKhoaHoc"].ToString(),
                                    NhomBenh = reader["NhomBenh"].ToString(),
                                    MoTa = reader["MoTa"].ToString(),
                                    DoPhoBien = reader["DoPhoBien"].ToString(),
                                    MucDoNghiemTrong = reader["MucDoNghiemTrong"].ToString(),
                                    NgayTao = (DateTime)reader["NgayTao"]
                                };
                            }
                        }
                    }
                    return null;
                }
            }
            public List<LoaiBenhDTO> LayLoaiBenhByTen(string tenBenh)
            {
                var danhSach = new List<LoaiBenhDTO>();

                string sql = @"
            SELECT *
            FROM LoaiBenh
            WHERE TenBenh LIKE @TenBenh
        ";

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenBenh", "%" + tenBenh + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                danhSach.Add(new LoaiBenhDTO
                                {
                                    LoaiBenhID = (int)reader["LoaiBenhID"],
                                    TenBenh = reader["TenBenh"].ToString(),
                                    TenKhoaHoc = reader["TenKhoaHoc"].ToString(),
                                    NhomBenh = reader["NhomBenh"].ToString(),
                                    MoTa = reader["MoTa"].ToString(),
                                    DoPhoBien = reader["DoPhoBien"].ToString(),
                                    MucDoNghiemTrong = reader["MucDoNghiemTrong"].ToString(),
                                    NgayTao = (DateTime)reader["NgayTao"]
                                });
                            }
                        }
                    }
                }
                return danhSach;
            }

            public bool ThemLoaiBenh(ThemLoaiBenhDTO lb)
            {
                string sql = @"
                INSERT INTO LoaiBenh
                (TenBenh, TenKhoaHoc, NhomBenh, MoTa, DoPhoBien, MucDoNghiemTrong, NgayTao)
                VALUES
                (@TenBenh, @TenKhoaHoc, @NhomBenh, @MoTa, @DoPhoBien, @MucDoNghiemTrong, @NgayTao)";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@TenBenh", lb.TenBenh);
                        cmd.Parameters.AddWithValue("@TenKhoaHoc", lb.TenKhoaHoc);
                        cmd.Parameters.AddWithValue("@NhomBenh", lb.NhomBenh);
                        cmd.Parameters.AddWithValue("@MoTa", lb.MoTa);
                        cmd.Parameters.AddWithValue("@DoPhoBien", lb.DoPhoBien);
                        cmd.Parameters.AddWithValue("@MucDoNghiemTrong", lb.MucDoNghiemTrong);
                        cmd.Parameters.AddWithValue("@NgayTao", lb.NgayTao);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            public bool CapNhatLoaiBenh(LoaiBenhDTO lb)
            {
                string sql = @"
                UPDATE LoaiBenh
                SET TenBenh = @TenBenh,
                    TenKhoaHoc = @TenKhoaHoc,
                    NhomBenh = @NhomBenh,
                    MoTa = @MoTa,
                    DoPhoBien = @DoPhoBien,
                    MucDoNghiemTrong = @MucDoNghiemTrong,
                    NgayTao = @NgayTao
                WHERE LoaiBenhID = @LoaiBenhID";
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@LoaiBenhID", lb.LoaiBenhID);
                        cmd.Parameters.AddWithValue("@TenBenh", lb.TenBenh);
                        cmd.Parameters.AddWithValue("@TenKhoaHoc", lb.TenKhoaHoc);
                        cmd.Parameters.AddWithValue("@NhomBenh", lb.NhomBenh);
                        cmd.Parameters.AddWithValue("@MoTa", lb.MoTa);
                        cmd.Parameters.AddWithValue("@DoPhoBien", lb.DoPhoBien);
                        cmd.Parameters.AddWithValue("@MucDoNghiemTrong", lb.MucDoNghiemTrong);
                        cmd.Parameters.AddWithValue("@NgayTao", lb.NgayTao);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
        }
    }
