using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Application.Interfaces;
namespace Infrastructure.Repositories;
public class ChucVuQuyenRepository : IChucVuQuyenRepository
{
	private readonly string _connectionString;
	public ChucVuQuyenRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")!;
	}
	public async Task<List<int>> GetByChucVuAsync(int chucVuId)
	{
		const string sql = @"
            SELECT QuyenID
            FROM ChucVuQuyen
            WHERE ChucVuID = @ChucVuID
        ";
		var list = new List<int>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@ChucVuID", SqlDbType.Int).Value = chucVuId;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
			list.Add(reader.GetInt32(0));
		return list;
	}
	public async Task<List<string>> GetNameByChucVuAsync(int chucVuId)
	{
		const string sql = @"
			SELECT q.MaQuyen
			FROM ChucVuQuyen cq
			JOIN Quyen q ON q.QuyenID = cq.QuyenID
			WHERE cq.ChucVuID = @ChucVuID
        ";
		var list = new List<string>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@ChucVuID", SqlDbType.Int).Value = chucVuId;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
			list.Add(reader.GetString(0));
		return list;
	}
	public async Task AddRangeAsync(int chucVuId, IEnumerable<int> quyenIds)
	{
		var ids = quyenIds.ToList();
		if (!ids.Any()) return;

		var values = string.Join(",", ids.Select(id => $"({chucVuId},{id})"));

		var sql = $@"
			INSERT INTO ChucVuQuyen (ChucVuID, QuyenID)
			VALUES {values}
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task DeleteRangeAsync(int chucVuId, IEnumerable<int> quyenIds)
	{
		var ids = quyenIds.ToList();
		if (!ids.Any()) return;

		var idList = string.Join(",", ids);

		var sql = $@"
			DELETE FROM ChucVuQuyen
			WHERE ChucVuID = @ChucVuID
			AND QuyenID IN ({idList})
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@ChucVuID", SqlDbType.Int).Value = chucVuId;

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
}