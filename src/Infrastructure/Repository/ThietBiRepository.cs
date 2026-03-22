using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace Infrastructure.Repositories;
public class ThietBiRepository : IThietBiRepository
{
    private readonly string _connectionString;
    public ThietBiRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }
    private const string BaseSelectList = @"
        SELECT ThietBiID, TenTB, LoaiTB
        FROM ThietBi";
    private const string BaseSelectDetail = @"
        SELECT ThietBiID, TenTB, LoaiTB
        FROM ThietBi";
    public async Task<(List<ThietBiListReadModel>, int)> GetPagedAsync(int page, int size)
    {
        var list = new List<ThietBiListReadModel>();
        int total = 0;
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        int offset = (page - 1) * size;
        var sql = $@"
        {BaseSelectList}
        ORDER BY ThietBiID DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;
        SELECT COUNT(*) FROM ThietBi";
        using var cmd = new SqlCommand(sql, conn);
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
    public async Task<(List<ThietBiListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size)
    {
        var list = new List<ThietBiListReadModel>();
        int total = 0;
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        int offset = (page - 1) * size;
        var sql = $@"
        {BaseSelectList}
        WHERE TenTB LIKE @Keyword
        ORDER BY ThietBiID DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;
        SELECT COUNT(*)
        FROM ThietBi
        WHERE TenTB LIKE @Keyword";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar).Value = $"%{keyword}%";
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
    public async Task<ThietBiReadModel?> GetDetailAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = BaseSelectDetail + " WHERE ThietBiID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToDetailDTO(reader);
        return null;
    }
    public async Task<ThietBi?> GetByIdAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = BaseSelectDetail + " WHERE ThietBiID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToEntity(reader);
        return null;
    }
    public async Task<int> AddAsync(ThietBi entity)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"
        INSERT INTO ThietBi (TenTB, LoaiTB)
        OUTPUT INSERTED.ThietBiID
        VALUES (@TenTB, @LoaiTB)";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@TenTB", SqlDbType.NVarChar).Value = entity.TenTB;
        cmd.Parameters.Add("@LoaiTB", SqlDbType.NVarChar)
            .Value = (object?)entity.LoaiTB ?? DBNull.Value;
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    public async Task UpdateAsync(ThietBi entity)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"
        UPDATE ThietBi
        SET TenTB=@TenTB,
            LoaiTB=@LoaiTB
        WHERE ThietBiID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = entity.ThietBiID;
        cmd.Parameters.Add("@TenTB", SqlDbType.NVarChar).Value = entity.TenTB;
        cmd.Parameters.Add("@LoaiTB", SqlDbType.NVarChar)
            .Value = (object?)entity.LoaiTB ?? DBNull.Value;
        await cmd.ExecuteNonQueryAsync();
    }
    public async Task DeleteAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"DELETE FROM ThietBi WHERE ThietBiID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        await cmd.ExecuteNonQueryAsync();
    }
    private ThietBi MapToEntity(SqlDataReader r)
    {
        return new ThietBi(
            (int)r["ThietBiID"],
            (string)r["TenTB"],
            r["LoaiTB"] as string
        );
    }
    private ThietBiListReadModel MapToListDTO(SqlDataReader r)
    {
        return new ThietBiListReadModel
        {
            ThietBiID = (int)r["ThietBiID"],
            TenTB = (string)r["TenTB"],
            LoaiTB = r["LoaiTB"] as string
        };
    }
    private ThietBiReadModel MapToDetailDTO(SqlDataReader r)
    {
        return new ThietBiReadModel
        {
            ThietBiID = (int)r["ThietBiID"],
            TenTB = (string)r["TenTB"],
            LoaiTB = r["LoaiTB"] as string
        };
    }
}