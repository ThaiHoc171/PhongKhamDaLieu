using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository;

public class PhienKhamThietBiRepository : IPhienKhamThietBiRepository
{
	private readonly string _connectionString;

	public PhienKhamThietBiRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}

	public async Task AddAsync(PhienKhamThietBi pk)
	{
		const string sql = @"
			INSERT INTO PhienKham_ThietBi (PhienKhamID, ChiTietID, GhiChu)
			VALUES (@PhienKhamID, @ChiTietID, @GhiChu)";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@PhienKhamID", pk.PhienKhamID);
		cmd.Parameters.AddWithValue("@ChiTietID", pk.ChiTietID);
		cmd.Parameters.AddWithValue("@GhiChu", (object?)pk.GhiChu ?? DBNull.Value);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task<List<PhienKhamThietBi>> GetByPhienKhamAsync(int phienKhamID)
	{
		const string sql = @"
			SELECT PhienKham_ThietBiID, PhienKhamID, ChiTietID, GhiChu
			FROM PhienKham_ThietBi
			WHERE PhienKhamID = @PhienKhamID";

		var list = new List<PhienKhamThietBi>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PhienKhamID", phienKhamID);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add(MapToEntity(reader));
		}
		return list;
	}

	public async Task<PhienKhamThietBi?> GetByPhienKhamAndChiTietAsync(int phienKhamID, int chiTietID)
	{
		const string sql = @"
			SELECT PhienKham_ThietBiID, PhienKhamID, ChiTietID, GhiChu
			FROM PhienKham_ThietBi
			WHERE PhienKhamID = @PhienKhamID AND ChiTietID = @ChiTietID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@PhienKhamID", phienKhamID);
		cmd.Parameters.AddWithValue("@ChiTietID", chiTietID);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		if (!await reader.ReadAsync()) return null;

		return MapToEntity(reader);
	}

	public async Task<PhienKhamThietBi?> GetByIdAsync(int id)
	{
		const string sql = @"
			SELECT PhienKham_ThietBiID, PhienKhamID, ChiTietID, GhiChu
			FROM PhienKham_ThietBi
			WHERE PhienKham_ThietBiID = @ID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@ID", id);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		if (!await reader.ReadAsync()) return null;

		return MapToEntity(reader);
	}

	public async Task UpdateAsync(PhienKhamThietBi pk)
	{
		const string sql = @"
			UPDATE PhienKham_ThietBi
			SET GhiChu = @GhiChu
			WHERE PhienKham_ThietBiID = @ID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@GhiChu", (object?)pk.GhiChu ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@ID", pk.PhienKhamThietBiID);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	private PhienKhamThietBi MapToEntity(SqlDataReader reader)
	{
		return new PhienKhamThietBi(
			reader.GetInt32(0),
			reader.GetInt32(1),
			reader.GetInt32(2),
			reader.IsDBNull(3) ? null : reader.GetString(3)
		);
	}
}
