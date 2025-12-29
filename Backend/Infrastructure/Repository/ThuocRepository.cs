using Domain.DTO;
using Domain.Repository;
using System.Collections.Generic;
using System.Data.SqlClient;

using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
    public class ThuocRepository : IThuocRepository
    {
        private readonly string _connectionString;
        public ThuocRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        public List<ThuocDTO> DanhSachThuoc()
        {
            List<ThuocDTO> listThuoc = new List<ThuocDTO>();
            string sql = "SELECT * FROM Thuoc";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listThuoc.Add(new ThuocDTO
                            {
                                ThuocID = (int)reader["ThuocID"],
                                TenThuoc = reader["TenThuoc"].ToString(),
                                HoatChat = reader["HoatChat"].ToString()
                            });
                        }
                    }
                }
                return listThuoc;
            }
        }

        public List<ThuocDTO> DanhSachThuocTheoKeywords(string kw)
        {
            var listThuoc = new List<ThuocDTO>();
            string sql = @"SELECT * FROM Thuoc WHERE TenThuoc LIKE @kw OR HoatChat LIKE @kw";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@kw", "%" + kw + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listThuoc.Add(new ThuocDTO
                            {
                                ThuocID = (int)reader["ThuocID"],
                                TenThuoc = reader["TenThuoc"].ToString(),
                                HoatChat = reader["HoatChat"].ToString()
                            });
                        }
                    }
                }
                return listThuoc;
            }
        }

        public ThuocDTO LayThuocTheoID(int thuocid)
        {
            string sql = @"SELECT * FROM Thuoc WHERE ThuocID = @thuocid";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ThuocID", thuocid);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new ThuocDTO
                            {
                                ThuocID = (int)reader["ThuocID"],
                                TenThuoc = reader["TenThuoc"].ToString(),
                                HoatChat = reader["HoatChat"].ToString()
                            };
                        }
                    }
                }
                return null;
            }
        }

        public bool ThemThuoc(ThuocCreateDTO thuoc)
        {
            string sql = "INSERT INTO Thuoc (TenThuoc, HoatChat) VALUES (@TenThuoc, @HoatChat)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TenThuoc", thuoc.TenThuoc);
                    cmd.Parameters.AddWithValue("@HoatChat", thuoc.HoatChat);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public bool CapNhatThuoc(ThuocDTO thuoc)
        {
            string sql = @"UPDATE Thuoc 
                 SET TenThuoc =@TenThuoc, 
                     HoatChat =@HoatChat
                 WHERE ThuocID = ThuocID";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ThuocID", thuoc.ThuocID);
                    cmd.Parameters.AddWithValue("@TenThuoc", thuoc.TenThuoc);
                    cmd.Parameters.AddWithValue("@HoatChat", thuoc.HoatChat);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
