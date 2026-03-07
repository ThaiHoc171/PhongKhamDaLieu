using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

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
	public async Task<PhienKhamBenh?> GetByIdAsync(int id)
	{
		const string sql = @"
			SELECT PhienKham_BenhID, PhienKhamID, LoaiBenhID, LoaiChanDoan, GhiChu
			FROM PhienKham_Benh
			WHERE PhienKham_BenhID = @Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Id", id);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
		{
			return new PhienKhamBenh(
				phienKham_BenhID: reader.GetInt32(0),
				phienKhamID: reader.GetInt32(1),
				loaiBenhID: reader.GetInt32(2),
				loaiChanDoan: LoaiChanDoanEnumExtensions.ToEnum(reader.GetString(3)),
				ghiChu: reader.IsDBNull(4) ? null : reader.GetString(4)
			);
		}
		return null;
	}
	public async Task<List<PhienKhamBenhReadModel>> GetByPhienKhamAsync(int phienKhamID)
	{
		const string sql = @"
		SELECT pk.PhienKham_BenhID, pk.PhienKhamID, lb.TenBenh, pk.LoaiChanDoan, pk.GhiChu
		FROM PhienKham_Benh pk
		JOIN LoaiBenh lb ON pk.LoaiBenhID = lb.LoaiBenhID
		WHERE PhienKhamID = @PhienKhamID";

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
				PhienKhamID = reader.GetInt32(1),
				LoaiBenh = reader.GetString(2),
				LoaiChanDoan = reader.GetString(3),
				GhiChu = reader.IsDBNull(4) ? null : reader.GetString(4)
			});
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
