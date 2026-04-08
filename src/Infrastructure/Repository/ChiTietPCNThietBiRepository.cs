using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class ChiTietPCNThietBiRepository : IChiTietPCNThietBiRepository
{
	private readonly string _connectionString;

	public ChiTietPCNThietBiRepository(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection")!;
	}

	#region Queries

	private const string BaseSelectList = @"
        SELECT ct.ChiTietID, ct.MaTaiSan, ct.NgayNhap, ct.TinhTrang
		FROM ChiTiet_PCNTB ct";

	private const string BaseSelectDetail = @"
        SELECT ct.ChiTietID, ct.PCN_TB_ID, ct.MaTaiSan, ct.NgayNhap,
              ct.TinhTrang, ct.GhiChu, pcn.TenPhong, tb.TenTB
		FROM ChiTiet_PCNTB ct
        JOIN PhongChucNang_ThietBi ptb ON ct.PCN_TB_ID = ptb.PCN_TB_ID
        JOIN PhongChucNang pcn ON ptb.PhongChucNangID = pcn.PhongChucNangID
        JOIN ThietBi tb ON ptb.ThietBiID = tb.ThietBiID";

	#endregion
	public async Task<bool> ExistsMaTaiSanAsync(string maTaiSan)
	{
		if (string.IsNullOrWhiteSpace(maTaiSan))
			return false;

		await using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"SELECT 1 
                FROM ChiTiet_PCNTB 
                WHERE MaTaiSan = @MaTaiSan";

		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@MaTaiSan", SqlDbType.NVarChar, 100).Value = maTaiSan.Trim();

		var result = await cmd.ExecuteScalarAsync();

		return result != null;
	}
	public async Task<ChiTietPCNThietBi?> GetByIdAsync(int id)
	{
		await using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"SELECT ChiTietID, PCN_TB_ID, MaTaiSan, NgayNhap, TinhTrang, GhiChu
                    FROM ChiTiet_PCNTB
                    WHERE ChiTietID=@Id";

		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		await using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}


	public async Task<ChiTietPCNThietBiReadModel?> GetDetailAsync(int id)
	{
		await using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = $@"
            {BaseSelectDetail}
            WHERE ct.ChiTietID=@Id";

		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		await using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToDetailDTO(reader);

		return null;
	}


	public async Task<List<ChiTietPCNThietBiListReadModel>> GetListAsync(int pcnTbId)
	{
		var list = new List<ChiTietPCNThietBiListReadModel>();

		await using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = $@"
        {BaseSelectList}
        WHERE ct.PCN_TB_ID = @PCN_TB_ID
        ORDER BY ct.NgayNhap DESC";

		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@PCN_TB_ID", SqlDbType.Int).Value = pcnTbId;

		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(MapToListDTO(reader));
		}
		return list;
	}
	public async Task<List<NameResponseDTO>> GetComboboxAsync(int pcnId, int tbId)
	{
		var list = new List<NameResponseDTO>();

		await using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"
		SELECT ptb.PCN_TB_ID,
               tb.TenTB + N' - ' + ct.MaTaiSan AS TenHienThi
        FROM ChiTiet_PCNTB ct
        JOIN PhongChucNang_ThietBi ptb ON ct.PCN_TB_ID = ptb.PCN_TB_ID
        JOIN ThietBi tb ON ptb.ThietBiID = tb.ThietBiID
        WHERE (@PCNID = 0 OR ptb.PhongChucNangID = @PCNID)
          AND (@TBID = 0 OR ptb.ThietBiID = @TBID)
        ORDER BY ct.MaTaiSan, tb.TenTB";

		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@PCNID", SqlDbType.Int).Value = pcnId;
		cmd.Parameters.Add("@TBID", SqlDbType.Int).Value = tbId;

		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add(new NameResponseDTO
			{
				Id = reader.GetInt32(reader.GetOrdinal("PCN_TB_ID")),
				Name = reader.GetString(reader.GetOrdinal("TenHienThi"))
			});
		}

		return list;
	}
	public async Task BulkInsertAsync(List<ChiTietPCNThietBi> list)
	{
		await using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();
		var table = new DataTable();
		table.Columns.Add("PCN_TB_ID", typeof(int));
		table.Columns.Add("MaTaiSan", typeof(string));
		foreach (var item in list)
		{
			table.Rows.Add(
				item.PCN_TB_ID,
				item.MaTaiSan
			);
		}
		using var bulk = new SqlBulkCopy(conn);
		bulk.DestinationTableName = "ChiTiet_PCNTB";
		bulk.ColumnMappings.Add("PCN_TB_ID", "PCN_TB_ID");
		bulk.ColumnMappings.Add("MaTaiSan", "MaTaiSan");
		await bulk.WriteToServerAsync(table);
	}

	public async Task<int> AddAsync(ChiTietPCNThietBi entity)
	{
		await using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"INSERT INTO ChiTiet_PCNTB
                    (PCN_TB_ID, MaTaiSan, TinhTrang, GhiChu)
                    OUTPUT INSERTED.ChiTietID
                    VALUES (@PCN_TB_ID,@MaTaiSan,@TinhTrang,@GhiChu)";

		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@PCN_TB_ID", SqlDbType.Int).Value = entity.PCN_TB_ID;
		cmd.Parameters.Add("@MaTaiSan", SqlDbType.NVarChar, 100).Value = entity.MaTaiSan;
		cmd.Parameters.Add("@TinhTrang", SqlDbType.NVarChar, 50).Value = entity.TinhTrang.ToDbValue();
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, -1).Value =
			(object?)entity.GhiChu ?? DBNull.Value;

		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}


	public async Task<int> UpdateAsync(ChiTietPCNThietBi entity)
	{
		await using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"UPDATE ChiTiet_PCNTB
                    SET MaTaiSan=@MaTaiSan,
						TinhTrang=@TinhTrang,
                        GhiChu=@GhiChu
                    WHERE ChiTietID=@Id";

		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@MaTaiSan", SqlDbType.NVarChar, 100).Value=entity.MaTaiSan;
		cmd.Parameters.Add("@TinhTrang", SqlDbType.NVarChar, 50) .Value = entity.TinhTrang.ToDbValue();
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, -1)
			.Value = (object?)entity.GhiChu ?? DBNull.Value;
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = entity.ChiTietID;

		return await cmd.ExecuteNonQueryAsync();
	}


	public async Task<int> DeleteAsync(int id)
	{
		await using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"DELETE FROM ChiTiet_PCNTB WHERE ChiTietID=@Id";

		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		return await cmd.ExecuteNonQueryAsync();
	}


	#region Mapping

	private ChiTietPCNThietBi MapToEntity(SqlDataReader r)
	{
		return new ChiTietPCNThietBi(
			r.GetInt32(r.GetOrdinal("ChiTietID")),
			r.GetInt32(r.GetOrdinal("PCN_TB_ID")),
			r.GetString(r.GetOrdinal("MaTaiSan")),
			r.GetDateTime(r.GetOrdinal("NgayNhap")),
			r.GetString(r.GetOrdinal("TinhTrang")),
			r.IsDBNull(r.GetOrdinal("GhiChu")) ? null : r.GetString(r.GetOrdinal("GhiChu"))
		);
	}


	private ChiTietPCNThietBiListReadModel MapToListDTO(SqlDataReader r)
	{
		return new ChiTietPCNThietBiListReadModel
		{
			ChiTietID = r.GetInt32(r.GetOrdinal("ChiTietID")),
			MaTaiSan = r.GetString(r.GetOrdinal("MaTaiSan")),
			NgayNhap = r.GetDateTime(r.GetOrdinal("NgayNhap")),
			TinhTrang = r.GetString(r.GetOrdinal("TinhTrang"))
		};
	}


	private ChiTietPCNThietBiReadModel MapToDetailDTO(SqlDataReader r)
	{
		return new ChiTietPCNThietBiReadModel
		{
			ChiTietID = r.GetInt32(r.GetOrdinal("ChiTietID")),
			MaTaiSan = r.GetString(r.GetOrdinal("MaTaiSan")),
			NgayNhap = r.GetDateTime(r.GetOrdinal("NgayNhap")),
			TinhTrang = r.GetString(r.GetOrdinal("TinhTrang")),
			GhiChu = r.IsDBNull(r.GetOrdinal("GhiChu")) ? null : r.GetString(r.GetOrdinal("GhiChu")),
			PhongChucNang = r.GetString(r.GetOrdinal("TenPhong")),
			ThietBi = r.GetString(r.GetOrdinal("TenTB"))
		};
	}

	#endregion
}