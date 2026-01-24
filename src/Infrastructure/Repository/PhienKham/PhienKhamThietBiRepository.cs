using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Application.ReadModels;

namespace Infrastructure.Repository;

public class PhienKhamThietBiRepository : IPhienKhamThietBiRepository
{
	private readonly string _connectionString;
	public PhienKhamThietBiRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}
	public async Task AddAsync(PhienKhamThietBi phienKhamThietBi)
	{
		const string sql = @"INSERT INTO PhienKham_ThietBi (PhienKhamID, ThietBiID, SoLuong, GhiChu)
							VALUES (@PhienKhamID, @ThietBiID, @SoLuong, @GhiChu)";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PhienKhamID", phienKhamThietBi.PhienKhamID);
		cmd.Parameters.AddWithValue("@ThietBiID", phienKhamThietBi.ThietBiID);
		cmd.Parameters.AddWithValue("@SoLuong", phienKhamThietBi.SoLuong);
		cmd.Parameters.AddWithValue("@GhiChu", (object?)phienKhamThietBi.GhiChu ?? DBNull.Value);
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task<List<PhienKhamThietBiReadModel>> GetByPhienKhamAsync(int phienKhamID)
	{
		const string sql = @"SELECT pktb.PhienKham_ThietBiID, pktb.PhienKhamID, pktb.ThietBiID, tb.TenTB, pktb.SoLuong, pktb.GhiChu
							FROM PhienKham_ThietBi pktb
							JOIN ThietBi tb ON pktb.ThietBiID = tb.ThietBiID
							WHERE pktb.PhienKhamID = @PhienKhamID";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PhienKhamID", phienKhamID);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		var results = new List<PhienKhamThietBiReadModel>();
		while (await reader.ReadAsync())
		{
			results.Add(new PhienKhamThietBiReadModel
			{
				Id = reader.GetInt32(0),
				PhienKhamID = reader.GetInt32(1),
				ThietBiID = reader.GetInt32(2),
				TenThietBi =  reader.GetString(3),
				SoLuong = reader.GetInt32(4),
				GhiChu = reader.IsDBNull(5) ? null : reader.GetString(5)
			});
		}
		return results;
	}
	public async Task<PhienKhamThietBi?> GetByPhienKhamAndThietBiAsync(int phienKhamId, int thietBiId)
	{
		const string sql = @"SELECT PhienKham_ThietBiID, PhienKhamID, ThietBiID, SoLuong, GhiChu
						 FROM PhienKham_ThietBi
						 WHERE PhienKhamID = @PhienKhamID AND ThietBiID = @ThietBiID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@PhienKhamID", phienKhamId);
		cmd.Parameters.AddWithValue("@ThietBiID", thietBiId);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		if (!await reader.ReadAsync()) return null;

		return new PhienKhamThietBi(
			reader.GetInt32(0),
			reader.GetInt32(1),
			reader.GetInt32(2),
			reader.GetInt32(3),
			reader.IsDBNull(4) ? null : reader.GetString(4)
		);
	}

	public async Task<PhienKhamThietBi?> GetByIdAsync(int id)
	{
		const string sql = @"SELECT PhienKham_ThietBiID, PhienKhamID, ThietBiID, SoLuong, GhiChu
							FROM PhienKham_ThietBi
							WHERE PhienKham_ThietBiID = @ID";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@ID", id);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (!await reader.ReadAsync()) return null;
		return new PhienKhamThietBi(
			reader.GetInt32(0),
			reader.GetInt32(1),
			reader.GetInt32(2),
			reader.GetInt32(3),
			reader.IsDBNull(4) ? null : reader.GetString(4)
		);
	}
	public async Task UpdateAsync(PhienKhamThietBi pk)
	{
		const string sql = @"UPDATE PhienKham_ThietBi
							SET SoLuong = @SoLuong,
								GhiChu = @GhiChu
							WHERE PhienKham_ThietBiID = @ID";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@SoLuong", pk.SoLuong);
		cmd.Parameters.AddWithValue("@GhiChu", (object?)pk.GhiChu ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@ID", pk.PhienKhamThietBiID);
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
}