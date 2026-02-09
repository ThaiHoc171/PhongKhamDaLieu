using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Domain.Enums;
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
		const string sql = @" SELECT COUNT(*) FROM PhienKham_Benh WHERE PhienKhamID = @PhienKhamID";
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
							Where PhienKhamID = @PhienKhamID AND LoaiChanDoan = N'Chẩn đoán chính' ";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PhienKhamID", phienKhamID);
		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		var count = Convert.ToInt32(result);
		return count > 0;
	}
	public async Task<List<PhienKhamBenh>> GetByIdAsync(int phienKhamID)
	{
		const string sql = @"
		SELECT 
			PhienKham_BenhID,
			PhienKhamID,
			LoaiBenhID,
			LoaiChanDoan,
			GhiChu
		FROM PhienKham_Benh
		WHERE PhienKhamID = @PhienKhamID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PhienKhamID", phienKhamID);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		var results = new List<PhienKhamBenh>();

		while (await reader.ReadAsync())
		{
			results.Add(new PhienKhamBenh(
				phienKham_BenhID: reader.GetInt32(0),
				phienKhamID: reader.GetInt32(1),
				loaiBenhID: reader.GetInt32(2),
				loaiChanDoan: LoaiChanDoanEnumExtensions.ToEnum(reader.GetString(3)),
				ghiChu: reader.IsDBNull(4) ? null : reader.GetString(4)
			));
		}

		return results;
	}

	public async Task AddAsync(PhienKhamBenh pkb)
	{
		const string sql = @" INSERT INTO PhienKham_Benh (PhienKhamID, LoaiBenhID, LoaiChanDoan, GhiChu)
							VALUES (@PhienKhamID, @LoaiBenhID, @LoaiChanDoan, @GhiChu) ";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PhienKhamID", pkb.PhienKhamID);
		cmd.Parameters.AddWithValue("@LoaiBenhID", pkb.LoaiBenhID);
		cmd.Parameters.AddWithValue("@LoaiChanDoan",pkb.LoaiChanDoan.ToDbValue());
		cmd.Parameters.AddWithValue("@GhiChu", pkb.GhiChu ?? (object)DBNull.Value);
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task UpdateAsync(PhienKhamBenh pkb)
	{
		const string sql = @" UPDATE PhienKham_Benh
							SET LoaiBenhID = @LoaiBenhID ,LoaiChanDoan = @LoaiChanDoan,GhiChu = @GhiChu
							WHERE PhienKham_BenhID = @PhienKham_BenhID ";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@LoaiBenhID", pkb.LoaiBenhID);
		cmd.Parameters.AddWithValue("@LoaiChanDoan", pkb.LoaiChanDoan.ToDbValue());
		cmd.Parameters.AddWithValue("@PhienKham_BenhID", pkb.PhienKham_BenhID);
		cmd.Parameters.AddWithValue("@GhiChu", pkb.GhiChu ?? (object)DBNull.Value);
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
}
