using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class ChucVuRepository : IChucVuRepository
{
	private readonly string _connectionString;

	public ChucVuRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}

	public async Task<List<ChucVu>> GetAllAsync()
	{
		const string sql = @" SELECT ChucVuID, TenChucVu, MoTa, NgayTao, TrangThai
							FROM ChucVu";

		var list = new List<ChucVu>();

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

	public async Task<ChucVu?> GetByIdAsync(int id)
	{
		const string sql ="SELECT ChucVuID, TenChucVu, MoTa, NgayTao, TrangThai FROM ChucVu WHERE ChucVuID = @ChucVuID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@ChucVuID", id);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}

	public async Task AddAsync(ChucVu cv)
	{
		const string sql = @"INSERT INTO ChucVu (TenChucVu, MoTa) VALUES (@TenChucVu, @MoTa)";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@TenChucVu", cv.TenChucVu);
		cmd.Parameters.AddWithValue("@MoTa", (object?)cv.MoTa ?? DBNull.Value);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task UpdateAsync(ChucVu cv)
	{
		const string sql = @"UPDATE ChucVu SET TenChucVu = @TenChucVu, MoTa = @MoTa, TrangThai = @TrangThai WHERE ChucVuID = @ChucVuID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@ChucVuID", cv.ChucVuID);
		cmd.Parameters.AddWithValue("@TenChucVu", cv.TenChucVu);
		cmd.Parameters.AddWithValue("@MoTa", (object?)cv.MoTa ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@TrangThai", cv.TrangThai);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task<string?> GetNameByIdAsync(int id)
	{
		const string sql = @"
			SELECT TenChucVu
			FROM ChucVu
			WHERE ChucVuID = @Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Id", id);

		await conn.OpenAsync();
		return await cmd.ExecuteScalarAsync() as string;
	}
	public async Task<List<(int Id, string Ten)>> GetIdAndNameAsync()
	{
		const string sql = @"
        SELECT ChucVuID, TenChucVu
        FROM ChucVu
        ORDER BY TenChucVu";

		var list = new List<(int Id, string Ten)>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add((
				Id: reader.GetInt32(0),
				Ten: reader.GetString(1)
			));
		}

		return list;
	}
	private static ChucVu MapToEntity(SqlDataReader reader)
	{
		return new ChucVu(
			chucVuID: reader.GetInt32(0),
			tenChucVu: reader.GetString(1),
			moTa: reader.IsDBNull(2) ? null : reader.GetString(2),
			ngayTao: reader.GetDateTime(3),
			trangThai: reader.GetString(4)
		);
	}
}
