using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository
{
	public class KhungGioKhamRepository : IKhungGioKhamRepository
	{
		private readonly string _connectionString;

		public KhungGioKhamRepository(IConfiguration config)
		{
			_connectionString = config.GetConnectionString("DefaultConnection")
				?? throw new ArgumentNullException("Connection string not found");
		}

		public async Task<List<KhungGioKham>> GetAllAsync()
		{
			const string sql = @"SELECT KhungGioID, GioBatDau, GioKetThuc, TenKhung, MaxSlot
								FROM KhungGioKham";

			var list = new List<KhungGioKham>();

			await using var conn = new SqlConnection(_connectionString);
			await using var cmd = new SqlCommand(sql, conn);

			await conn.OpenAsync();
			await using var reader = await cmd.ExecuteReaderAsync();

			while (await reader.ReadAsync())
			{
				list.Add(MapToEntity(reader));
			}

			return list;
		}

		public async Task<KhungGioKham?> GetByIdAsync(int id)
		{
			const string sql = @"SELECT KhungGioID, GioBatDau, GioKetThuc, TenKhung, MaxSlot
								FROM KhungGioKham
								WHERE KhungGioID = @KhungGioID";

			await using var conn = new SqlConnection(_connectionString);
			await using var cmd = new SqlCommand(sql, conn);
			cmd.Parameters.AddWithValue("@KhungGioID", id);

			await conn.OpenAsync();
			await using var reader = await cmd.ExecuteReaderAsync();

			return await reader.ReadAsync() ? MapToEntity(reader) : null;
		}

		public async Task AddAsync(KhungGioKham kg)
		{
			// MaxSlot KHÔNG truyền → DB dùng DEFAULT 5
			const string sql = @"INSERT INTO KhungGioKham (GioBatDau, GioKetThuc, TenKhung)
								VALUES (@GioBatDau, @GioKetThuc, @TenKhung)";

			await using var conn = new SqlConnection(_connectionString);
			await using var cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@GioBatDau", kg.GioBatDau);
			cmd.Parameters.AddWithValue("@GioKetThuc", kg.GioKetThuc);
			cmd.Parameters.AddWithValue("@TenKhung", (object?)kg.TenKhung ?? DBNull.Value);

			await conn.OpenAsync();
			await cmd.ExecuteNonQueryAsync();
		}

		public async Task UpdateAsync(KhungGioKham kg)
		{
			const string sql = @"UPDATE KhungGioKham
								SET GioBatDau = @GioBatDau, GioKetThuc = @GioKetThuc, TenKhung = @TenKhung
								WHERE KhungGioID = @KhungGioID";

			await using var conn = new SqlConnection(_connectionString);
			await using var cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@KhungGioID", kg.KhungGioID);
			cmd.Parameters.AddWithValue("@GioBatDau", kg.GioBatDau);
			cmd.Parameters.AddWithValue("@GioKetThuc", kg.GioKetThuc);
	

			await conn.OpenAsync();
			await cmd.ExecuteNonQueryAsync();
		}

		private static KhungGioKham MapToEntity(SqlDataReader reader)
		{
			return new KhungGioKham(
				khungGioID: reader.GetInt32(0),
				gioBatDau: reader.GetTimeSpan(1),
				gioKetThuc: reader.GetTimeSpan(2),
				tenKhung: reader.IsDBNull(3) ? null : reader.GetString(3),
				maxSlot: reader.GetInt32(4)
			);
		}
	}
}
