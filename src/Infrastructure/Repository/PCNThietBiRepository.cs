using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class PCNThietBiRepository : IPCNThietBiRepository
{
	private readonly string _connectionString;

	public PCNThietBiRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}

	public async Task<List<PCNThietBi>> GetAllAsync()
	{
		const string sql = @"
			SELECT PCN_TB_ID, PhongChucNangID, ThietBiID, TongSoLuong
			FROM PhongChucNang_ThietBi";

		var list = new List<PCNThietBi>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(Map(reader));

		return list;
	}

	public async Task<PCNThietBi?> GetByIdAsync(int pcnTbId)
	{
		const string sql = @"
			SELECT PCN_TB_ID, PhongChucNangID, ThietBiID, TongSoLuong
			FROM PhongChucNang_ThietBi
			WHERE PCN_TB_ID = @PCN_TB_ID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PCN_TB_ID", pcnTbId);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		return await reader.ReadAsync() ? Map(reader) : null;
	}

	public async Task<PCNThietBi?> GetByPhongAndThietBiAsync(int phongChucNangId, int thietBiId)
	{
		const string sql = @"
			SELECT PCN_TB_ID, PhongChucNangID, ThietBiID, TongSoLuong
			FROM PhongChucNang_ThietBi
			WHERE PhongChucNangID = @PhongChucNangID
			  AND ThietBiID = @ThietBiID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PhongChucNangID", phongChucNangId);
		cmd.Parameters.AddWithValue("@ThietBiID", thietBiId);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		return await reader.ReadAsync() ? Map(reader) : null;
	}

	public async Task AddAsync(PCNThietBi entity)
	{
		const string sql = @"
			INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID)
			VALUES (@PhongChucNangID, @ThietBiID)";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@PhongChucNangID", entity.PhongChucNangID);
		cmd.Parameters.AddWithValue("@ThietBiID", entity.ThietBiID);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task UpdateAsync(PCNThietBi entity)
	{
		const string sql = @"
			UPDATE PhongChucNang_ThietBi
			SET TongSoLuong = @TongSoLuong
			WHERE PCN_TB_ID = @PCN_TB_ID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@PCN_TB_ID", entity.PCN_TB_ID);
		cmd.Parameters.AddWithValue("@TongSoLuong", entity.TongSoLuong);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task DeleteAsync(int pcnTbId)
	{
		const string sql = @"DELETE FROM PhongChucNang_ThietBi WHERE PCN_TB_ID = @PCN_TB_ID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PCN_TB_ID", pcnTbId);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	private static PCNThietBi Map(SqlDataReader r)
	{
		return new PCNThietBi(
			r.GetInt32(0),
			r.GetInt32(1),
			r.GetInt32(2),
			r.GetInt32(3)
		);
	}
}
