using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Application.ReadModels;
using Domain.Entities;
using Application.Interfaces;


namespace Infrastructure.Repository;

public class PhienKhamBenhRepository : IPhienKhamBenhRepository
{
	private readonly string _connectionString;
	public PhienKhamBenhRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}
	public async Task<int> CountPKBenhAsync(int phienKhamID)
	{
		const string sql = @" SELECT COUNT(*) FROM PhienKham_Benh WHERE PhienKhamID = @PhienKhamID ";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PhienKhamID", phienKhamID);
		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		return Convert.ToInt32(result);
	}
	public async Task<bool> PrimaryPKBenhExitsAsync(int phienKhamID)
	{
		const string sql = @"Select Count(*) From PhienKham_Benh
							Where PhienKhamID = @PhienKhamID AND LoaiChuanDoan = 'Chẩn đoán chính' ";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PhienKhamID", phienKhamID);
		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		var count = Convert.ToInt32(result);
		return count > 0;
	}
	public async Task<List<PhienKhamBenhReadModel>> GetByIdAsync(int phienKhamID)
	{
		const string sql = @" SELECT pk.PhienKham_BenhID, pk.PhienKhamID, pk.LoaiBenhID, lb.TenBenh, pk.LoaiChuanDoan, pk.GhiChu
							FROM PhienKham_Benh pk
							JOIN LoaiBenh lb ON pk.LoaiBenhID = lb.LoaiBenhID
							WHERE pk.PhienKhamID = @PhienKhamID ";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PhienKhamID", phienKhamID);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		var results = new List<PhienKhamBenhReadModel>();
		while (await reader.ReadAsync())
		{
			results.Add(new PhienKhamBenhReadModel
			{
				Id = reader.GetInt32(0),
				PhienKhamID =  reader.GetInt32(1),
				LoaiBenhID = reader.GetInt32(2),
				TenLoaiBenh = reader.GetString(3),
				LoaiChuanDoan = reader.GetString(4),
				GhiChu = reader.IsDBNull(5) ? null : reader.GetString(5)
			});
		}
		return results;
	}
	public async Task AddAsync(PhienKhamBenh pkb)
	{
		const string sql = @" INSERT INTO PhienKham_Benh (PhienKhamID, LoaiBenhID, LoaiChuanDoan, GhiChu)
							VALUES (@PhienKhamID, @LoaiBenhID, @LoaiChuanDoan, @GhiChu) ";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PhienKhamID", pkb.PhienKhamID);
		cmd.Parameters.AddWithValue("@LoaiBenhID", pkb.LoaiBenhID);
		cmd.Parameters.AddWithValue("@LoaiChuanDoan", pkb.LoaiChuanDoan ?? (object)DBNull.Value);
		cmd.Parameters.AddWithValue("@GhiChu", pkb.GhiChu ?? (object)DBNull.Value);
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task UpdateAsync(PhienKhamBenh pkb)
	{
		const string sql = @" UPDATE PhienKham_Benh
							SET LoaiBenhID = @LoaiBenhID ,LoaiChuanDoan = @LoaiChuanDoan,GhiChu = @GhiChu
							WHERE PhienKham_BenhID = @PhienKham_BenhID ";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@LoaiBenhID", pkb.LoaiBenhID);
		cmd.Parameters.AddWithValue("@LoaiChuanDoan", pkb.LoaiChuanDoan ?? (object)DBNull.Value);
		cmd.Parameters.AddWithValue("@PhienKham_BenhID", pkb.PhienKham_BenhID);
		cmd.Parameters.AddWithValue("@GhiChu", pkb.GhiChu ?? (object)DBNull.Value);
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
}
