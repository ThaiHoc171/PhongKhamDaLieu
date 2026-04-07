using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class PhienKhamThietBiRepository : IPhienKhamThietBiRepository
{
	private readonly string _connectionString;

	public PhienKhamThietBiRepository(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection")!;
	}

	#region Queries

	private const string BaseSelect = @"
        SELECT PhienKham_ThietBiID,
               PhienKhamID,
               ChiTietID,
               GhiChu
        FROM PhienKham_ThietBi";

	private const string BaseSelectJoin = @"
        SELECT pktb.PhienKham_ThietBiID,
               tb.TenTB,
               pcn.TenPhong,
               pktb.GhiChu
        FROM PhienKham_ThietBi pktb
        JOIN ChiTiet_PCNTB ct ON pktb.ChiTietID = ct.ChiTietID
        JOIN PhongChucNang_ThietBi pcntb ON ct.PCN_TB_ID = pcntb.PCN_TB_ID
        JOIN ThietBi tb ON pcntb.ThietBiID = tb.ThietBiID
        JOIN PhongChucNang pcn ON pcntb.PhongChucNangID = pcn.PhongChucNangID";

	#endregion

	public async Task<int> AddAsync(PhienKhamThietBi pk)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"
        INSERT INTO PhienKham_ThietBi (PhienKhamID, ChiTietID, GhiChu)
        VALUES (@PhienKhamID, @ChiTietID, @GhiChu)";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = pk.PhienKhamID;
		cmd.Parameters.Add("@ChiTietID", SqlDbType.Int).Value = pk.ChiTietID;
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value =
			(object?)pk.GhiChu ?? DBNull.Value;

		int row = await cmd.ExecuteNonQueryAsync();
		return row;
	}

	public async Task<List<PhienKhamThietBiReadModel>> GetByPhienKhamAsync(int phienKhamID)
	{
		var list = new List<PhienKhamThietBiReadModel>();

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectJoin + @"
        WHERE pktb.PhienKhamID = @PhienKhamID
        ORDER BY tb.TenTB";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = phienKhamID;

		using var reader = await cmd.ExecuteReaderAsync();

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
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelect + @"
        WHERE PhienKhamID = @PhienKhamID
          AND ChiTietID = @ChiTietID";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = phienKhamID;
		cmd.Parameters.Add("@ChiTietID", SqlDbType.Int).Value = chiTietID;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}

	public async Task<PhienKhamThietBi?> GetByIdAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelect + " WHERE PhienKham_ThietBiID=@ID";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}

	public async Task<int> UpdateAsync(PhienKhamThietBi pk)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"
        UPDATE PhienKham_ThietBi
        SET GhiChu = @GhiChu
        WHERE PhienKham_ThietBiID = @ID";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value =
			(object?)pk.GhiChu ?? DBNull.Value;

		cmd.Parameters.Add("@ID", SqlDbType.Int).Value = pk.PhienKhamThietBiID;

		int row = await cmd.ExecuteNonQueryAsync();
		return row;
	}

	#region Mapping

	private PhienKhamThietBi MapToEntity(SqlDataReader r)
	{
		return new PhienKhamThietBi(
			r.GetInt32(r.GetOrdinal("PhienKham_ThietBiID")),
			r.GetInt32(r.GetOrdinal("PhienKhamID")),
			r.GetInt32(r.GetOrdinal("ChiTietID")),
			r.IsDBNull(r.GetOrdinal("GhiChu")) ? null : r.GetString(r.GetOrdinal("GhiChu"))
		);
	}

	#endregion
}