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
			const string sql = @"
				SELECT KhungGioID, CaLamViec, GioBatDau, GioKetThuc, TenKhung
				FROM KhungGioKham
				ORDER BY CaLamViec, GioBatDau";
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
		public async Task<List<int>> GetKhungGioIdsByCaLamViecAsync(int caLamViec)
		{
			const string sql = @"
				SELECT KhungGioID
				FROM KhungGioKham
				WHERE CaLamViec = @CaLamViec
				ORDER BY GioBatDau";
			var list = new List<int>();
			await using var conn = new SqlConnection(_connectionString);
			await using var cmd = new SqlCommand(sql, conn);
			cmd.Parameters.AddWithValue("@CaLamViec", caLamViec);
			await conn.OpenAsync();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				list.Add(reader.GetInt32(0));
			}
			return list;
		}
		public async Task<KhungGioKham?> GetByIdAsync(int id)
		{
			const string sql = @"
				SELECT KhungGioID, CaLamViec, GioBatDau, GioKetThuc, TenKhung
				FROM KhungGioKham
				WHERE KhungGioID = @KhungGioID";
			await using var conn = new SqlConnection(_connectionString);
			await using var cmd = new SqlCommand(sql, conn);
			cmd.Parameters.AddWithValue("@KhungGioID", id);
			await conn.OpenAsync();
			await using var reader = await cmd.ExecuteReaderAsync();
			return await reader.ReadAsync() ? MapToEntity(reader) : null;
		}
		public async Task<int> CountKhungGioKhamAsync()
		{
			const string sql = @"SELECT COUNT(*) FROM KhungGioKham";
			await using var conn = new SqlConnection(_connectionString);
			await using var cmd = new SqlCommand(sql, conn);
			await conn.OpenAsync();
			return (int)await cmd.ExecuteScalarAsync();
		}
		public async Task AddAsync(KhungGioKham kg)
		{
			const string sql = @"
				INSERT INTO KhungGioKham (CaLamViec, GioBatDau, GioKetThuc, TenKhung)
				VALUES (@CaLamViec, @GioBatDau, @GioKetThuc, @TenKhung)";
			await using var conn = new SqlConnection(_connectionString);
			await using var cmd = new SqlCommand(sql, conn);
			cmd.Parameters.AddWithValue("@CaLamViec", kg.CaLamViec);
			cmd.Parameters.AddWithValue("@GioBatDau", kg.GioBatDau);
			cmd.Parameters.AddWithValue("@GioKetThuc", kg.GioKetThuc);
			cmd.Parameters.AddWithValue("@TenKhung", (object?)kg.TenKhung ?? DBNull.Value);
			await conn.OpenAsync();
			await cmd.ExecuteNonQueryAsync();
		}
		public async Task UpdateAsync(KhungGioKham kg)
		{
			const string sql = @"
				UPDATE KhungGioKham
				SET CaLamViec = @CaLamViec,
					GioBatDau = @GioBatDau,
					GioKetThuc = @GioKetThuc,
					TenKhung = @TenKhung
				WHERE KhungGioID = @KhungGioID";
			await using var conn = new SqlConnection(_connectionString);
			await using var cmd = new SqlCommand(sql, conn);
			cmd.Parameters.AddWithValue("@KhungGioID", kg.KhungGioID);
			cmd.Parameters.AddWithValue("@CaLamViec", kg.CaLamViec);
			cmd.Parameters.AddWithValue("@GioBatDau", kg.GioBatDau);
			cmd.Parameters.AddWithValue("@GioKetThuc", kg.GioKetThuc);
			cmd.Parameters.AddWithValue("@TenKhung", (object?)kg.TenKhung ?? DBNull.Value);
			await conn.OpenAsync();
			await cmd.ExecuteNonQueryAsync();
		}
		public async Task<List<(int Id, string Ten)>> GetIdAndNameAsync()
		{
			const string sql = @"
			SELECT KhungGioID, TenKhung
			FROM KhungGioKham
			ORDER BY TenKhung";
			var list = new List<(int, string)>();
			await using var conn = new SqlConnection(_connectionString);
			await using var cmd = new SqlCommand(sql, conn);
			await conn.OpenAsync();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				list.Add((
					reader.GetInt32(reader.GetOrdinal("KhungGioID")),
					reader.GetString(reader.GetOrdinal("TenKhung"))
				));
			}
			return list;
		}
		private static KhungGioKham MapToEntity(SqlDataReader reader)
		{
			return new KhungGioKham(
				khungGioID: reader.GetInt32(0),
				caLamViec: reader.GetInt32(1),
				gioBatDau: reader.GetTimeSpan(2),
				gioKetThuc: reader.GetTimeSpan(3),
				tenKhung: reader.IsDBNull(4) ? null : reader.GetString(4)
			);
		}
	}
}
