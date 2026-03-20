using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository;

public class ChiTietPCNThietBiRepository : IChiTietPCNThietBiRepository
{
    private readonly string _connectionString;
    public ChiTietPCNThietBiRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string not found.");
    }
    private SqlConnection CreateConnection() => new(_connectionString);
    private const string BaseJoin =
        @"FROM ChiTiet_PCNTB ct
          JOIN PhongChucNang_ThietBi ptb ON ct.PCN_TB_ID = ptb.PCN_TB_ID
          JOIN ThietBi tb ON ptb.ThietBiID = tb.ThietBiID";

    private const string BaseSelectLite =
        @"SELECT ct.ChiTietID, ct.MaTaiSan, ct.NgayNhap, ct.TinhTrang";

    private const string BaseSelectDetail =
        @"SELECT ct.ChiTietID, ct.MaTaiSan, ct.NgayNhap, ct.TinhTrang, ct.GhiChu,
                 ptb.PhongChucNangID,
                 tb.ThietBiID, tb.TenTB";
    public async Task<ChiTietPCNThietBi?> GetByIdAsync(int id)
    {
        const string sql = @"
            SELECT ChiTietID, PCN_TB_ID, MaTaiSan, NgayNhap, TinhTrang, GhiChu
            FROM ChiTiet_PCNTB
            WHERE ChiTietID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }
    public async Task<ChiTietPCNThietBiReadModel?> GetDetailAsync(int id)
    {
        var sql = $@"
            {BaseSelectDetail}
            {BaseJoin}
            WHERE ct.ChiTietID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToDetailDTO(reader) : null;
    }
    public async Task<(List<ChiTietPCNThietBiListReadModel>, int)> GetPagedAsync(int pcnTbId, int page, int size)
    {
        var sql = $@"
            {BaseSelectLite}
            {BaseJoin}
            WHERE ct.PCN_TB_ID=@PCN_TB_ID
            ORDER BY ct.NgayNhap DESC
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

            SELECT COUNT(*)
            FROM ChiTiet_PCNTB
            WHERE PCN_TB_ID=@PCN_TB_ID";
        var list = new List<ChiTietPCNThietBiListReadModel>();
        int total = 0;
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@PCN_TB_ID", pcnTbId);
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
    public async Task<(List<ChiTietPCNThietBiListReadModel>, int)> SearchPagedAsync(int pcnTbId, string keyword, int page, int size)
    {
        var sql = $@"
            {BaseSelectLite}
            {BaseJoin}
            WHERE ct.PCN_TB_ID=@PCN_TB_ID
              AND (@Keyword IS NULL OR ct.MaTaiSan LIKE @Keyword OR tb.TenTB LIKE @Keyword)
            ORDER BY ct.NgayNhap DESC
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

            SELECT COUNT(*)
            FROM ChiTiet_PCNTB ct
            JOIN PhongChucNang_ThietBi ptb ON ct.PCN_TB_ID = ptb.PCN_TB_ID
            JOIN ThietBi tb ON ptb.ThietBiID = tb.ThietBiID
            WHERE ct.PCN_TB_ID=@PCN_TB_ID
              AND (@Keyword IS NULL OR ct.MaTaiSan LIKE @Keyword OR tb.TenTB LIKE @Keyword)";
        var list = new List<ChiTietPCNThietBiListReadModel>();
        int total = 0;
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@PCN_TB_ID", pcnTbId);
        cmd.Parameters.AddWithValue("@Keyword",
            string.IsNullOrWhiteSpace(keyword) ? DBNull.Value : $"%{keyword}%");
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
    public async Task<List<(int Id, string Ten)>> GetComboboxAsync(int pcnTbId)
    {
        const string sql = @"
            SELECT ct.ChiTietID, tb.TenTB
            FROM ChiTiet_PCNTB ct
            JOIN PhongChucNang_ThietBi ptb ON ct.PCN_TB_ID = ptb.PCN_TB_ID
            JOIN ThietBi tb ON ptb.ThietBiID = tb.ThietBiID
            WHERE ct.PCN_TB_ID=@PCN_TB_ID";
        var list = new List<(int, string)>();
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@PCN_TB_ID", pcnTbId);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add((reader.GetInt32(0), reader.GetString(1)));
        return list;
    }
    public async Task<int> AddAsync(ChiTietPCNThietBi entity)
    {
        const string sql = @"
            INSERT INTO ChiTiet_PCNTB (PCN_TB_ID, MaTaiSan, TinhTrang, GhiChu)
            OUTPUT INSERTED.ChiTietID
            VALUES (@PCN_TB_ID, @MaTaiSan, @TinhTrang, @GhiChu)";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@PCN_TB_ID", entity.PCN_TB_ID);
        cmd.Parameters.AddWithValue("@MaTaiSan", entity.MaTaiSan);
        cmd.Parameters.AddWithValue("@TinhTrang", entity.TinhTrang.ToDbValue());
        cmd.Parameters.AddWithValue("@GhiChu", (object?)entity.GhiChu ?? DBNull.Value);
        await conn.OpenAsync();
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    public async Task UpdateAsync(ChiTietPCNThietBi entity)
    {
        const string sql = @"
            UPDATE ChiTiet_PCNTB
            SET TinhTrang=@TinhTrang,
                GhiChu=@GhiChu
            WHERE ChiTietID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@TinhTrang", entity.TinhTrang.ToDbValue());
        cmd.Parameters.AddWithValue("@GhiChu", (object?)entity.GhiChu ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Id", entity.ChiTietID);
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
    public async Task DeleteAsync(int id)
    {
        const string sql = @"DELETE FROM ChiTiet_PCNTB WHERE ChiTietID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
    private static ChiTietPCNThietBi MapToEntity(SqlDataReader r)
    {
        return new ChiTietPCNThietBi(
            r.GetInt32(0),
            r.GetInt32(1),
            r.GetString(2),
            r.GetDateTime(3),
            r.GetString(4),
            r.IsDBNull(5) ? null : r.GetString(5)
        );
    }
    private static ChiTietPCNThietBiListReadModel MapToLiteDTO(SqlDataReader r)
    {
        return new ChiTietPCNThietBiListReadModel
        {
            ChiTietID = r.GetInt32(0),
            MaTaiSan = r.GetString(1),
            NgayNhap = r.GetDateTime(2),
            TinhTrang = r.GetString(3)
        };
    }
    private static ChiTietPCNThietBiReadModel MapToDetailDTO(SqlDataReader r)
    {
        return new ChiTietPCNThietBiReadModel
        {
            ChiTietID = r.GetInt32(0),
            MaTaiSan = r.GetString(1),
            NgayNhap = r.GetDateTime(2),
            TinhTrang = r.GetString(3),
            GhiChu = r.IsDBNull(4) ? null : r.GetString(4),
            PhongChucNang = new NameResponseDTO
            {
                Id = r.GetInt32(5),
                Name = "PCN"
            },
            ThietBi = new NameResponseDTO
            {
                Id = r.GetInt32(6),
                Name = r.GetString(7)
            }
        };
    }
}