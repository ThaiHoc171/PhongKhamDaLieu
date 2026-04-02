using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class PCNThietBiRepository : IPCNThietBiRepository
{
	private readonly string _connectionString;

	public PCNThietBiRepository(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection")!;
	}

	#region Queries

	private const string BaseJoin = @"
        FROM PhongChucNang_ThietBi pcn_tb
        JOIN ThietBi tb ON pcn_tb.ThietBiID = tb.ThietBiID
        JOIN PhongChucNang pcn ON pcn_tb.PhongChucNangID = pcn.PhongChucNangID";

	private const string BaseSelect = @"
        SELECT pcn_tb.PCN_TB_ID,
               pcn.TenPhong,
               tb.TenTB,
               pcn_tb.TongSoLuong";

	#endregion

	public async Task<(List<PCNThietBiReadModel>, int)> GetPagedAsync(int page, int size, int? phongChucNangID)
	{
		var list = new List<PCNThietBiReadModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
        {BaseSelect}
        {BaseJoin}
        WHERE (@PhongID IS NULL OR pcn_tb.PhongChucNangID = @PhongID)
        ORDER BY pcn_tb.PCN_TB_ID DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

        SELECT COUNT(*)
        FROM PhongChucNang_ThietBi
        WHERE (@PhongID IS NULL OR PhongChucNangID = @PhongID)";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@PhongID", SqlDbType.Int).Value = (object?)phongChucNangID ?? DBNull.Value;
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@Size", SqlDbType.Int).Value = size;

		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToListDTO(reader));

		await reader.NextResultAsync();

		if (await reader.ReadAsync())
			total = reader.GetInt32(0);

		return (list, total);
	}

	public async Task<(List<PCNThietBiReadModel>, int)> SearchPagedAsync(string keyword, int page, int size, int? phongChucNangID)
	{
		var list = new List<PCNThietBiReadModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
        {BaseSelect}
        {BaseJoin}
        WHERE (@PhongID IS NULL OR pcn_tb.PhongChucNangID = @PhongID)
        AND (@Keyword IS NULL OR tb.TenTB LIKE @Keyword)
        ORDER BY pcn_tb.PCN_TB_ID DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

        SELECT COUNT(*)
        FROM PhongChucNang_ThietBi pcn_tb
        JOIN ThietBi tb ON pcn_tb.ThietBiID = tb.ThietBiID
        WHERE (@PhongID IS NULL OR pcn_tb.PhongChucNangID = @PhongID)
        AND (@Keyword IS NULL OR tb.TenTB LIKE @Keyword)";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@PhongID", SqlDbType.Int).Value = (object?)phongChucNangID ?? DBNull.Value;
		cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 100).Value =
			string.IsNullOrWhiteSpace(keyword) ? DBNull.Value : $"%{keyword}%";
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@Size", SqlDbType.Int).Value = size;

		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToListDTO(reader));

		await reader.NextResultAsync();

		if (await reader.ReadAsync())
			total = reader.GetInt32(0);

		return (list, total);
	}

	public async Task<PCNThietBi?> GetByIdAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"SELECT PCN_TB_ID,PhongChucNangID,ThietBiID,TongSoLuong
                    FROM PhongChucNang_ThietBi
                    WHERE PCN_TB_ID=@Id";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}

	public async Task<PCNThietBi?> GetByPhongAndThietBiAsync(int phongId, int thietBiId)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"SELECT PCN_TB_ID,PhongChucNangID,ThietBiID,TongSoLuong
                    FROM PhongChucNang_ThietBi
                    WHERE PhongChucNangID=@PhongID AND ThietBiID=@ThietBiID";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@PhongID", SqlDbType.Int).Value = phongId;
		cmd.Parameters.Add("@ThietBiID", SqlDbType.Int).Value = thietBiId;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}
	public async Task<int> AddAsync(PCNThietBi entity)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"INSERT INTO PhongChucNang_ThietBi(PhongChucNangID,ThietBiID)
					OUTPUT INSERTED.PCN_TB_ID
                    VALUES(@PhongID,@ThietBiID)";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@PhongID", SqlDbType.Int).Value = entity.PhongChucNangID;
		cmd.Parameters.Add("@ThietBiID", SqlDbType.Int).Value = entity.ThietBiID;

		int id = Convert.ToInt32(await cmd.ExecuteScalarAsync());

		return id;
	}

	public async Task<int> UpdateAsync(PCNThietBi entity)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"UPDATE PhongChucNang_ThietBi
                    SET TongSoLuong=@TongSoLuong
                    WHERE PCN_TB_ID=@Id";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = entity.PCN_TB_ID;
		cmd.Parameters.Add("@TongSoLuong", SqlDbType.Int).Value = entity.TongSoLuong;

		int row = await cmd.ExecuteNonQueryAsync();

		return row;
	}

	public async Task<int> DeleteAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"DELETE FROM PhongChucNang_ThietBi
                    WHERE PCN_TB_ID=@Id";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		int row = await cmd.ExecuteNonQueryAsync();

		return row;
	}

	#region Mapping

	private PCNThietBi MapToEntity(SqlDataReader r)
	{
		return new PCNThietBi(
			r.GetInt32(r.GetOrdinal("PCN_TB_ID")),
			r.GetInt32(r.GetOrdinal("PhongChucNangID")),
			r.GetInt32(r.GetOrdinal("ThietBiID")),
			r.GetInt32(r.GetOrdinal("TongSoLuong"))
		);
	}

	private PCNThietBiReadModel MapToListDTO(SqlDataReader r)
	{
		return new PCNThietBiReadModel
		{
			PCN_TB_ID = r.GetInt32(r.GetOrdinal("PCN_TB_ID")),
			PhongChucNang = r.GetString(r.GetOrdinal("TenPhong")),
			ThietBi = r.GetString(r.GetOrdinal("TenTB")),
			TongSoLuong = r.GetInt32(r.GetOrdinal("TongSoLuong"))
		};
	}

	#endregion
}