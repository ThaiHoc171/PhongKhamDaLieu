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
    public PCNThietBiRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string not found.");
    }
    private const string BaseJoin = @"
        FROM PhongChucNang_ThietBi pcn_tb
        JOIN ThietBi tb ON pcn_tb.ThietBiID = tb.ThietBiID
        JOIN PhongChucNang pcn ON pcn_tb.PhongChucNangID = pcn.PhongChucNangID";
    private const string BaseSelectLite = @"
        SELECT pcn_tb.PCN_TB_ID,
               pcn.TenPhong,
               tb.TenTB,
               pcn_tb.TongSoLuong";
    private const string BaseSelectDetail = @"
        SELECT pcn_tb.PCN_TB_ID,
               pcn_tb.PhongChucNangID,
               pcn_tb.TongSoLuong,
               tb.ThietBiID,
               pcn.TenPhong,
               tb.TenTB";
    public async Task<PCNThietBi?> GetByIdAsync(int id)
    {
        const string sql = @"
            SELECT PCN_TB_ID, PhongChucNangID, ThietBiID, TongSoLuong
            FROM PhongChucNang_ThietBi
            WHERE PCN_TB_ID = @Id";
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }
    public async Task<PCNThietBi?> GetByPhongAndThietBiAsync(int phongId, int thietBiId)
    {
        const string sql = @"
            SELECT PCN_TB_ID, PhongChucNangID, ThietBiID, TongSoLuong
            FROM PhongChucNang_ThietBi
            WHERE PhongChucNangID = @PhongID AND ThietBiID = @ThietBiID";
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@PhongID", SqlDbType.Int).Value = phongId;
        cmd.Parameters.Add("@ThietBiID", SqlDbType.Int).Value = thietBiId;
        using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }
    public async Task<List<PCNThietBiReadModel>> GetByPhongAsync(int phongId)
    {
        var sql = $@"
            {BaseSelectDetail}
            {BaseJoin}
            WHERE pcn_tb.PhongChucNangID = @PhongID";
        var list = new List<PCNThietBiReadModel>();
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@PhongID", SqlDbType.Int).Value = phongId;
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToDetailDTO(reader));
        return list;
    }
    public async Task<PCNThietBiReadModel?> GetDetailAsync(int id)
    {
        var sql = $@"
            {BaseSelectDetail}
            {BaseJoin}
            WHERE pcn_tb.PCN_TB_ID = @Id";
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToDetailDTO(reader) : null;
    }
    public async Task<(List<PCNThietBiListReadModel>, int)> GetPagedAsync(int page, int size, int? phongChucNangID)
    {
        var sql = $@"
            {BaseSelectLite}
            {BaseJoin}
            WHERE (@PhongChucNangID IS NULL OR pcn_tb.PhongChucNangID = @PhongChucNangID)
            ORDER BY pcn_tb.PCN_TB_ID DESC
            OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;
            SELECT COUNT(*)
            FROM PhongChucNang_ThietBi
            WHERE (@PhongChucNangID IS NULL OR PhongChucNangID = @PhongChucNangID)";
        var list = new List<PCNThietBiListReadModel>();
        int total = 0;
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@PhongChucNangID", SqlDbType.Int).Value = (object?)phongChucNangID ?? DBNull.Value;
        cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = (page - 1) * size;
        cmd.Parameters.Add("@Size", SqlDbType.Int).Value = size;
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToLiteDTO(reader));
        await reader.NextResultAsync();
        if (await reader.ReadAsync())
            total = reader.GetInt32(0);
        return (list, total);
    }
    public async Task<(List<PCNThietBiListReadModel>, int)> SearchPagedAsync(
    string keyword, int page, int size, int? phongChucNangID)
    {
        var sql = $@"
        {BaseSelectLite}
        {BaseJoin}
        WHERE (@PhongChucNangID IS NULL OR pcn_tb.PhongChucNangID = @PhongChucNangID)
          AND (@Keyword IS NULL OR tb.TenTB LIKE @Keyword)
        ORDER BY pcn_tb.PCN_TB_ID DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

        SELECT COUNT(*)
        FROM PhongChucNang_ThietBi pcn_tb
        JOIN ThietBi tb ON pcn_tb.ThietBiID = tb.ThietBiID
        WHERE (@PhongChucNangID IS NULL OR pcn_tb.PhongChucNangID = @PhongChucNangID)
          AND (@Keyword IS NULL OR tb.TenTB LIKE @Keyword)";
        var list = new List<PCNThietBiListReadModel>();
        int total = 0;
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@PhongChucNangID", SqlDbType.Int)
            .Value = (object?)phongChucNangID ?? DBNull.Value;
        cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar)
            .Value = string.IsNullOrWhiteSpace(keyword)
                ? DBNull.Value
                : $"%{keyword}%";
        cmd.Parameters.Add("@Offset", SqlDbType.Int)
            .Value = (page - 1) * size;
        cmd.Parameters.Add("@Size", SqlDbType.Int)
            .Value = size;
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToLiteDTO(reader));
        await reader.NextResultAsync();
        if (await reader.ReadAsync())
            total = reader.GetInt32(0);
        return (list, total);
    }
    public async Task<int> AddAsync(PCNThietBi entity)
    {
        const string sql = @"
            INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID)
            OUTPUT INSERTED.PCN_TB_ID
            VALUES (@PhongID, @ThietBiID)";
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@PhongID", SqlDbType.Int).Value = entity.PhongChucNangID;
        cmd.Parameters.Add("@ThietBiID", SqlDbType.Int).Value = entity.ThietBiID;
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    public async Task UpdateAsync(PCNThietBi entity)
    {
        const string sql = @"
            UPDATE PhongChucNang_ThietBi
            SET TongSoLuong = @TongSoLuong
            WHERE PCN_TB_ID = @Id";
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@TongSoLuong", SqlDbType.Int).Value = entity.TongSoLuong;
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = entity.PCN_TB_ID;
        await cmd.ExecuteNonQueryAsync();
    }
    public async Task DeleteAsync(int id)
    {
        const string sql = @"DELETE FROM PhongChucNang_ThietBi WHERE PCN_TB_ID = @Id";
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        await cmd.ExecuteNonQueryAsync();
    }
    private static PCNThietBi MapToEntity(SqlDataReader r)
    {
        return new PCNThietBi(
            (int)r["PCN_TB_ID"],
            (int)r["PhongChucNangID"],
            (int)r["ThietBiID"],
            (int)r["TongSoLuong"]
        );
    }
    private static PCNThietBiListReadModel MapToLiteDTO(SqlDataReader r)
    {
        return new PCNThietBiListReadModel
        {
            PCN_TB_ID = (int)r["PCN_TB_ID"],
            PhongChucNang = (string)r["TenPhong"],
            ThietBi = (string)r["TenTB"],
            TongSoLuong = (int)r["TongSoLuong"]
        };
    }
    private static PCNThietBiReadModel MapToDetailDTO(SqlDataReader r)
    {
        return new PCNThietBiReadModel
        {
            PCN_TB_ID = (int)r["PCN_TB_ID"],
            PhongChucNang = new NameResponseDTO
            {
                Id = (int)r["PhongChucNangID"],
                Name = (string)r["TenPhong"]
            },            
            TongSoLuong = (int)r["TongSoLuong"],
            ThietBi = new NameResponseDTO
            {
                Id = (int)r["ThietBiID"],
                Name = (string)r["TenTB"]
            }
        };
    }
}