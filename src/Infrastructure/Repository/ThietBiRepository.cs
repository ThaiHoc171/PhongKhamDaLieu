using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository;

public class ThietBiRepository : IThietBiRepository
{
	private readonly string _connectionString;

	public ThietBiRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException();
	}

	public async Task<List<ThietBi>> GetAllAsync()
	{
		const string sql = @"
			SELECT 
				ThietBiID, TenTB, LoaiTB
			FROM ThietBi";

		var list = new List<ThietBi>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToEntity(reader));

		return list;
	}

	public async Task<ThietBi?> GetByIdAsync(int id)
	{
		const string sql = @"
			SELECT 
				ThietBiID, TenTB, LoaiTB
			FROM ThietBi
			WHERE ThietBiID = @Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Id", id);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}

	public async Task<List<ThietBi>> SearchByTenAsync(string tenTB)
	{
		const string sql = @"
			SELECT 
				ThietBiID, TenTB, LoaiTB
			FROM ThietBi
			WHERE TenTB LIKE @TenTB";

		var list = new List<ThietBi>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@TenTB", $"%{tenTB}%");

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToEntity(reader));

		return list;
	}

	public async Task AddAsync(ThietBi tb)
	{
		const string sql = @"
			INSERT INTO ThietBi (TenTB, LoaiTB)
			VALUES (@TenTB, @LoaiTB)";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@TenTB", tb.TenTB);
		cmd.Parameters.AddWithValue("@LoaiTB", tb.LoaiTB ?? (object)DBNull.Value);
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task UpdateAsync(ThietBi tb)
	{
		const string sql = @"
			UPDATE ThietBi
			SET TenTB = @TenTB,
			    LoaiTB = @LoaiTB
			WHERE ThietBiID = @Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@Id", tb.Id);
		cmd.Parameters.AddWithValue("@TenTB", tb.TenTB);
		cmd.Parameters.AddWithValue("@LoaiTB", tb.LoaiTB ?? (object)DBNull.Value);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task<string?> GetNameByIdAsync(int id)
	{
		const string sql = @"
			SELECT TenTB
			FROM ThietBi
			WHERE ThietBiID = @Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Id", id);

		await conn.OpenAsync();
		return await cmd.ExecuteScalarAsync() as string;
	}

	public async Task<List<(int Id, string Ten)>> GetIdAndNameAsync()
	{
		const string sql = @"
			SELECT ThietBiID, TenTB
			FROM ThietBi
			ORDER BY TenTB";

		var list = new List<(int, string)>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add((
				reader.GetInt32(reader.GetOrdinal("ThietBiID")),
				reader.GetString(reader.GetOrdinal("TenThietBi"))
			));
		}

		return list;
	}
	private static ThietBi MapToEntity(SqlDataReader r)
		=> new(
			id: r.GetInt32(r.GetOrdinal("ThietBiID")),
			tenTB: r.GetString(r.GetOrdinal("TenTB")),
			loaiTB: r["LoaiTB"] as string
		);
}
