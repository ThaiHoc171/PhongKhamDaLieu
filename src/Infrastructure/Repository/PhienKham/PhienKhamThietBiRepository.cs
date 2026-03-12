using Application.DTOs;
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
			VALUES (@PhienKhamID, @ChiTietID, @GhiChu)
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@PhienKhamID", System.Data.SqlDbType.Int).Value = pk.PhienKhamID;
		cmd.Parameters.Add("@ChiTietID", System.Data.SqlDbType.Int).Value = pk.ChiTietID;
		cmd.Parameters.Add("@GhiChu", System.Data.SqlDbType.NVarChar).Value =
			(object?)pk.GhiChu ?? DBNull.Value;

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task<List<PhienKhamThietBiReadModel>> GetByPhienKhamAsync(int phienKhamID)
	{
		const string sql = @"
			SELECT pktb.PhienKham_ThietBiID,
				   tb.TenTB,
				   pcn.TenPhong,
				   pktb.GhiChu
			FROM PhienKham_ThietBi pktb
			JOIN ChiTiet_PCNTB ct ON pktb.ChiTietID = ct.ChiTietID
			JOIN PhongChucNang_ThietBi pcntb ON ct.PCN_TB_ID = pcntb.PCN_TB_ID
			JOIN ThietBi tb ON pcntb.ThietBiID = tb.ThietBiID
			JOIN PhongChucNang pcn ON pcntb.PhongChucNangID = pcn.PhongChucNangID
			WHERE pktb.PhienKhamID = @PhienKhamID
			ORDER BY tb.TenTB
		";

		var list = new List<PhienKhamThietBiReadModel>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@PhienKhamID", System.Data.SqlDbType.Int).Value = phienKhamID;

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add(new PhienKhamThietBiReadModel
			{
				PhienKhamThietBiID = reader.GetInt32(0),
				TenThietBi = reader.GetString(1),
				TenPhong = reader.IsDBNull(2) ? null : reader.GetString(2),
				GhiChu = reader.IsDBNull(3) ? null : reader.GetString(3)
			});
		}

		return list;
	}

	public async Task<PhienKhamThietBi?> GetByPhienKhamAndChiTietAsync(int phienKhamID, int chiTietID)
	{
		const string sql = @"
			SELECT PhienKham_ThietBiID,
				   PhienKhamID,
				   ChiTietID,
				   GhiChu
			FROM PhienKham_ThietBi
			WHERE PhienKhamID = @PhienKhamID
			  AND ChiTietID = @ChiTietID
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@PhienKhamID", System.Data.SqlDbType.Int).Value = phienKhamID;
		cmd.Parameters.Add("@ChiTietID", System.Data.SqlDbType.Int).Value = chiTietID;

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		if (!await reader.ReadAsync())
			return null;

		return MapToEntity(reader);
	}

	public async Task<PhienKhamThietBi?> GetByIdAsync(int id)
	{
		const string sql = @"
			SELECT PhienKham_ThietBiID,
				   PhienKhamID,
				   ChiTietID,
				   GhiChu
			FROM PhienKham_ThietBi
			WHERE PhienKham_ThietBiID = @ID
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = id;

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		if (!await reader.ReadAsync())
			return null;

		return MapToEntity(reader);
	}

	public async Task UpdateAsync(PhienKhamThietBi pk)
	{
		const string sql = @"
			UPDATE PhienKham_ThietBi
			SET GhiChu = @GhiChu
			WHERE PhienKham_ThietBiID = @ID
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@GhiChu", System.Data.SqlDbType.NVarChar).Value =
			(object?)pk.GhiChu ?? DBNull.Value;

		cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int).Value = pk.PhienKhamThietBiID;

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	private static PhienKhamThietBi MapToEntity(SqlDataReader reader)
	{
		return new PhienKhamThietBi(
			reader.GetInt32(0),
			reader.GetInt32(1),
			reader.GetInt32(2),
			reader.IsDBNull(3) ? null : reader.GetString(3)
		);
	}
}