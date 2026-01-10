using Domain.Entities;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{

	public class ChucVuRepository : IChucVuRepository
	{
		private readonly string _connectionString;
		public ChucVuRepository(IConfiguration config)
		{
			_connectionString = config.GetConnectionString("DefaultConnection");

		}
		public List<ChucVu> GetAll()
		{
			List<ChucVu> list = new List<ChucVu>();
			string sql = "SELECT * FROM ChucVu WHERE TrangThai = N'Hoạt động'";

			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							list.Add(MapToEntity(reader));
						}
					}
				}
			}
			return list;
		}
		public ChucVu GetById(int id)
		{
			string sql = "SELECT * FROM ChucVu WHERE ChucVuID = @ChucVuID AND TrangThai = N'Hoạt động'";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@ChucVuID", id);
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							return MapToEntity(reader);
						}
					}
				}
				return null;
			}
		}
		public void Add(ChucVu cv)
		{
			string sql = @"INSERT INTO ChucVu (TenChucVu, MoTa)
						   VALUES (@TenChucVu, @MoTa)";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@TenChucVu", cv.TenChucVu);
					cmd.Parameters.AddWithValue("@MoTa", cv.MoTa);
					cmd.ExecuteNonQuery();
				}
			}
		}
		public void Update(ChucVu cv)
		{
			string sql = @"UPDATE ChucVu
						   SET TenChucVu = @TenChucVu, MoTa = @MoTa, TrangThai = @TrangThai
						   WHERE ChucVuID = @ChucVuID";
			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@ChucVuID", cv.ChucVuID);
					cmd.Parameters.AddWithValue("@TenChucVu", cv.TenChucVu);
					cmd.Parameters.AddWithValue("@MoTa", cv.MoTa);
					cmd.Parameters.AddWithValue("@TrangThai", cv.TrangThai);
					cmd.ExecuteNonQuery();
				}
			}
		}
		private ChucVu MapToEntity(SqlDataReader reader)
		{
			return new ChucVu(
				(int)reader["ChucVuID"],
				reader["TenChucVu"].ToString(),
				reader["MoTa"].ToString(),
				(DateTime)reader["NgayTao"],
				reader["TrangThai"].ToString()
			);
		}
	}
}
