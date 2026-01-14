using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class ThuocRepository : IThuocRepository
{
	private readonly string _connectionString;

	public ThuocRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")!;
	}

	public async Task<List<Thuoc>> GetAllAsync()
	{
		const string sql = @"
			SELECT ThuocID, TenThuoc, HoatChat
			FROM Thuoc";

		var list = new List<Thuoc>();

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

	public async Task<List<Thuoc>> SearchAsync(string keyword)
	{
		const string sql = @"
			SELECT ThuocID, TenThuoc, HoatChat
			FROM Thuoc
			WHERE TenThuoc LIKE @kw OR HoatChat LIKE @kw";

		var list = new List<Thuoc>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add(MapToEntity(reader));
		}

		return list;
	}

	public async Task<Thuoc?> GetByIdAsync(int id)
	{
		const string sql = @"
			SELECT ThuocID, TenThuoc, HoatChat
			FROM Thuoc
			WHERE ThuocID = @id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@id", id);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}

	public async Task AddAsync(Thuoc thuoc)
	{
		const string sql = @"
			INSERT INTO Thuoc (TenThuoc, HoatChat)
			VALUES (@TenThuoc, @HoatChat)";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@TenThuoc", thuoc.TenThuoc);
		cmd.Parameters.AddWithValue("@HoatChat", (object?)thuoc.HoatChat ?? DBNull.Value);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task UpdateAsync(Thuoc thuoc)
	{
		const string sql = @"
			UPDATE Thuoc
			SET TenThuoc = @TenThuoc,
				HoatChat = @HoatChat
			WHERE ThuocID = @Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@Id", thuoc.ThuocID);
		cmd.Parameters.AddWithValue("@TenThuoc", thuoc.TenThuoc);
		cmd.Parameters.AddWithValue("@HoatChat", (object?)thuoc.HoatChat ?? DBNull.Value);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	private static Thuoc MapToEntity(SqlDataReader r)
	{
		return new Thuoc(
			id: r.GetInt32(0),
			tenThuoc: r.GetString(1),
			hoatChat: r.IsDBNull(2) ? null : r.GetString(2)
		);
	}
}
