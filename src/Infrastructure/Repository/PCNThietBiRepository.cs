using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository;

public class PCNThietBiRepository : IPCNThietBiRepository
{
    private readonly string _connectionString;
    public PCNThietBiRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string not found.");
    }
    private SqlConnection CreateConnection() => new(_connectionString);
    private const string BaseJoin =
        @"FROM PhongChucNang_ThietBi pcn_tb
          JOIN ThietBi tb ON pcn_tb.ThietBiID = tb.ThietBiID
          JOIN PhongChucNang pcn ON pcn_tb.PhongChucNangID = pcn.PhongChucNangID";

    private const string BaseSelectLite =
        @"SELECT pcn_tb.PCN_TB_ID,
                 pcn.TenPhongChucNang,
                 tb.TenThietBi,
                 pcn_tb.TongSoLuong";

    private const string BaseSelectDetail =
        @"SELECT pcn_tb.PCN_TB_ID,
                 pcn_tb.PhongChucNangID,
                 pcn_tb.TongSoLuong,
                 tb.ThietBiID,
                 tb.TenThietBi";
    public async Task<PCNThietBi?> GetByIdAsync(int id)
    {
        const string sql =
        @"SELECT PCN_TB_ID, PhongChucNangID, ThietBiID, TongSoLuong
          FROM PhongChucNang_ThietBi
          WHERE PCN_TB_ID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }
    public async Task<PCNThietBi?> GetByPhongAndThietBiAsync(int phongChucNangId, int thietBiId)
    {
        const string sql =
        @"SELECT PCN_TB_ID, PhongChucNangID, ThietBiID, TongSoLuong
          FROM PhongChucNang_ThietBi
          WHERE PhongChucNangID = @PhongID
        AND ThietBiID = @ThietBiID";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@PhongID", phongChucNangId);
        cmd.Parameters.AddWithValue("@ThietBiID", thietBiId);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }
    public async Task<List<PCNThietBiReadModel>> GetByPhongAsync(int phongChucNangID)
    {
        var sql =
        $@"{BaseSelectDetail}
           {BaseJoin}
           WHERE pcn_tb.PhongChucNangID=@PhongID";
        var list = new List<PCNThietBiReadModel>();
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@PhongID", phongChucNangID);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToDetailDTO(reader));
        return list;
    }
    public async Task<PCNThietBiReadModel?> GetDetailAsync(int id)
    {
        var sql =
        $@"{BaseSelectDetail}
           {BaseJoin}
           WHERE pcn_tb.PCN_TB_ID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToDetailDTO(reader) : null;
    }
    public async Task<(List<PCNThietBiListReadModel>, int)>GetPagedAsync(int page, int size, int? phongChucNangID)
    {
        var sql =
        $@"{BaseSelectLite}
           {BaseJoin}
           WHERE (@PhongID IS NULL OR pcn_tb.PhongChucNangID=@PhongID)
           ORDER BY pcn_tb.PCN_TB_ID DESC
           OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
           SELECT COUNT(*)
           FROM PhongChucNang_ThietBi
           WHERE (@PhongID IS NULL OR PhongChucNangID=@PhongID)";
        var list = new List<PCNThietBiListReadModel>();
        int total = 0;
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@PhongID", (object?)phongChucNangID ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Offset", (page - 1) * size);
        cmd.Parameters.AddWithValue("@PageSize", size);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToLiteDTO(reader));
        if (await reader.NextResultAsync() && await reader.ReadAsync())
            total = reader.GetInt32(0);
        return (list, total);
    }
    public async Task<(List<PCNThietBiListReadModel>, int)>SearchPagedAsync(string keyword, int page, int size, int? phongChucNangID)
    {
        var sql =
        $@"{BaseSelectLite}
           {BaseJoin}
           WHERE (@PhongID IS NULL OR pcn_tb.PhongChucNangID=@PhongID)
             AND (@Keyword IS NULL OR tb.TenThietBi LIKE @Keyword)
           ORDER BY pcn_tb.PCN_TB_ID DESC
           OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
           SELECT COUNT(*)
           FROM PhongChucNang_ThietBi pcn_tb
           JOIN ThietBi tb ON pcn_tb.ThietBiID = tb.ThietBiID
           WHERE (@PhongID IS NULL OR pcn_tb.PhongChucNangID=@PhongID)
             AND (@Keyword IS NULL OR tb.TenThietBi LIKE @Keyword)";
        var list = new List<PCNThietBiListReadModel>();
        int total = 0;
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@PhongID", (object?)phongChucNangID ?? DBNull.Value);
        cmd.Parameters.AddWithValue(
            "@Keyword",
            string.IsNullOrWhiteSpace(keyword)
                ? DBNull.Value
                : $"%{keyword}%"
        );
        cmd.Parameters.AddWithValue("@Offset", (page - 1) * size);
        cmd.Parameters.AddWithValue("@PageSize", size);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToLiteDTO(reader));
        if (await reader.NextResultAsync() && await reader.ReadAsync())
            total = reader.GetInt32(0);
        return (list, total);
    }
    public async Task<int> AddAsync(PCNThietBi entity)
    {
        const string sql =
        @"INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID)
          OUTPUT INSERTED.PCN_TB_ID
          VALUES (@PhongChucNangID,@ThietBiID)";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@PhongChucNangID", entity.PhongChucNangID);
        cmd.Parameters.AddWithValue("@ThietBiID", entity.ThietBiID);
        await conn.OpenAsync();
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    public async Task UpdateAsync(PCNThietBi entity)
    {
        const string sql =
        @"UPDATE PhongChucNang_ThietBi
          SET TongSoLuong=@TongSoLuong
          WHERE PCN_TB_ID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@TongSoLuong", entity.TongSoLuong);
        cmd.Parameters.AddWithValue("@Id", entity.PCN_TB_ID);
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
    public async Task DeleteAsync(int pcnTbId)
    {
        const string sql =
        @"DELETE FROM PhongChucNang_ThietBi
         WHERE PCN_TB_ID = @Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", pcnTbId);
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
    private static PCNThietBi MapToEntity(SqlDataReader r)
    {
        var pcnTbId = r.GetOrdinal("PCN_TB_ID");
        var phongId = r.GetOrdinal("PhongChucNangID");
        var thietBiId = r.GetOrdinal("ThietBiID");
        var tongSoLuong = r.GetOrdinal("TongSoLuong");

        return new PCNThietBi(
            r.GetInt32(pcnTbId),
            r.GetInt32(phongId),
            r.GetInt32(thietBiId),
            r.GetInt32(tongSoLuong)
        );
    }
    private static PCNThietBiListReadModel MapToLiteDTO(SqlDataReader r)
    {
        var pcnTbId = r.GetOrdinal("PCN_TB_ID");
        var phong = r.GetOrdinal("TenPhongChucNang");
        var thietBi = r.GetOrdinal("TenThietBi");
        var tongSoLuong = r.GetOrdinal("TongSoLuong");

        return new PCNThietBiListReadModel
        {
            PCN_TB_ID = r.GetInt32(pcnTbId),
            PhongChucNang = r.GetString(phong),
            ThietBi = r.GetString(thietBi),
            TongSoLuong = r.GetInt32(tongSoLuong)
        };
    }
    private static PCNThietBiReadModel MapToDetailDTO(SqlDataReader r)
    {
        var pcnTbId = r.GetOrdinal("PCN_TB_ID");
        var phongId = r.GetOrdinal("PhongChucNangID");
        var tongSoLuong = r.GetOrdinal("TongSoLuong");
        var thietBiId = r.GetOrdinal("ThietBiID");
        var tenThietBi = r.GetOrdinal("TenThietBi");

        return new PCNThietBiReadModel
        {
            PCN_TB_ID = r.GetInt32(pcnTbId),
            PhongChucNangID = r.GetInt32(phongId),
            TongSoLuong = r.GetInt32(tongSoLuong),
            ThietBi = new NameResponseDTO
            {
                Id = r.GetInt32(thietBiId),
                Name = r.GetString(tenThietBi)
            }
        };
    }
}