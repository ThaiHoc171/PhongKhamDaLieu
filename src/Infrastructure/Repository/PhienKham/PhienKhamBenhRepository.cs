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
	public async Task<int> CountAsync(int phienKhamID)
	{
		const string sql = @"
			SELECT COUNT(*)
			FROM PhienKham_Benh
			WHERE PhienKhamID = @PhienKhamID
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@PhienKhamID", System.Data.SqlDbType.Int).Value = phienKhamID;
		await conn.OpenAsync();
		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}
	public async Task<bool> PrimaryExistsAsync(int phienKhamID)
	{
		const string sql = @"
			SELECT COUNT(*)
			FROM PhienKham_Benh
			WHERE PhienKhamID = @PhienKhamID
			AND LoaiChanDoan = N'Chẩn đoán chính'
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@PhienKhamID", System.Data.SqlDbType.Int).Value = phienKhamID;
		await conn.OpenAsync();
		var count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
		return count > 0;
	}
	public async Task<PhienKhamBenh?> GetByIdAsync(int id)
	{
		const string sql = @"
			SELECT PhienKham_BenhID, PhienKhamID, LoaiBenhID, LoaiChanDoan, GhiChu
			FROM PhienKham_Benh
			WHERE PhienKham_BenhID = @Id
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (!await reader.ReadAsync())
			return null;
		return new PhienKhamBenh(
			reader.GetInt32(0),
			reader.GetInt32(1),
			reader.GetInt32(2),
			LoaiChanDoanEnumExtensions.ToEnum(reader.GetString(3)),
			reader.IsDBNull(4) ? null : reader.GetString(4)
		);
	}
	public async Task<List<PhienKhamBenhReadModel>> GetByPhienKhamIdAsync(int phienKhamID)
	{
		const string sql = @"
			SELECT pk.PhienKham_BenhID, pk.PhienKhamID,lb.LoaiBenhID, lb.TenBenh, pk.LoaiChanDoan, pk.GhiChu
			FROM PhienKham_Benh pk
			JOIN LoaiBenh lb ON pk.LoaiBenhID = lb.LoaiBenhID
			WHERE pk.PhienKhamID = @PhienKhamID
		";
		var results = new List<PhienKhamBenhReadModel>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@PhienKhamID", System.Data.SqlDbType.Int).Value = phienKhamID;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			results.Add(new PhienKhamBenhReadModel
			{
				Id = reader.GetInt32(0),
				PhienKhamID = reader.GetInt32(1),
				LoaiBenh = new NameResponseDTO
				{
					Id = reader.GetInt32(2),
					Name = reader.GetString(3)
				},
				LoaiChanDoan = reader.GetString(4),
				GhiChu = reader.IsDBNull(5) ? null : reader.GetString(4)
			});
		}
		return results;
	}
	public async Task AddAsync(PhienKhamBenh entity)
	{
		const string sql = @"
			INSERT INTO PhienKham_Benh (PhienKhamID, LoaiBenhID, LoaiChanDoan, GhiChu)
			VALUES (@PhienKhamID, @LoaiBenhID, @LoaiChanDoan, @GhiChu)
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@PhienKhamID", System.Data.SqlDbType.Int).Value = entity.PhienKhamID;
		cmd.Parameters.Add("@LoaiBenhID", System.Data.SqlDbType.Int).Value = entity.LoaiBenhID;
		cmd.Parameters.Add("@LoaiChanDoan", System.Data.SqlDbType.NVarChar).Value = entity.LoaiChanDoan.ToDbValue();
		cmd.Parameters.Add("@GhiChu", System.Data.SqlDbType.NVarChar).Value =
			entity.GhiChu ?? (object)DBNull.Value;
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task UpdateAsync(PhienKhamBenh entity)
	{
		const string sql = @"
			UPDATE PhienKham_Benh
			SET LoaiBenhID = @LoaiBenhID,
				LoaiChanDoan = @LoaiChanDoan,
				GhiChu = @GhiChu
			WHERE PhienKham_BenhID = @Id
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@LoaiBenhID", System.Data.SqlDbType.Int).Value = entity.LoaiBenhID;
		cmd.Parameters.Add("@LoaiChanDoan", System.Data.SqlDbType.NVarChar).Value = entity.LoaiChanDoan.ToDbValue();
		cmd.Parameters.Add("@GhiChu", System.Data.SqlDbType.NVarChar).Value =
			entity.GhiChu ?? (object)DBNull.Value;
		cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = entity.PhienKham_BenhID;
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
}