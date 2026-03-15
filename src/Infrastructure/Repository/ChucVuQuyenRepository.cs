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
	public async Task AddAsync(int chucVuId, int quyenId)
	{
		const string sql = """
            INSERT INTO ChucVuQuyen (ChucVuID, QuyenID)
            VALUES (@ChucVuID, @QuyenID)
        """;
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@ChucVuID", SqlDbType.Int).Value = chucVuId;
		cmd.Parameters.Add("@QuyenID", SqlDbType.Int).Value = quyenId;
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task DeleteAsync(int chucVuId, int quyenId)
	{
		const string sql = """
            DELETE FROM ChucVuQuyen
            WHERE ChucVuID = @ChucVuID
            AND QuyenID = @QuyenID
        """;
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@ChucVuID", SqlDbType.Int).Value = chucVuId;
		cmd.Parameters.Add("@QuyenID", SqlDbType.Int).Value = quyenId;
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task DeleteAllAsync(int chucVuId)
	{
		const string sql = """
            DELETE FROM ChucVuQuyen
            WHERE ChucVuID = @ChucVuID
        """;
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@ChucVuID", SqlDbType.Int).Value = chucVuId;
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
}