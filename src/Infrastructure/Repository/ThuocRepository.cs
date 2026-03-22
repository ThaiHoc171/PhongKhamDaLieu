using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace Infrastructure.Repositories;
public class ThuocRepository : IThuocRepository
{
    private readonly string _connectionString;
    public ThuocRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }
    private const string BaseSelectList = @"
        SELECT ThuocID, TenThuoc, HoatChat
        FROM Thuoc";
    private const string BaseSelectDetail = @"
        SELECT ThuocID, TenThuoc, HoatChat
        FROM Thuoc";
    public async Task<(List<ThuocListReadModel>, int)> GetPagedAsync(int page, int size)
    {
        var list = new List<ThuocListReadModel>();
        int total = 0;
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        int offset = (page - 1) * size;
        var sql = $@"
        {BaseSelectList}
        ORDER BY ThuocID DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;
        SELECT COUNT(*) FROM Thuoc";
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
    public async Task<(List<ThuocListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size)
    {
        var list = new List<ThuocListReadModel>();
        int total = 0;
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        int offset = (page - 1) * size;
        var sql = $@"
        {BaseSelectList}
        WHERE TenThuoc LIKE @Keyword
        ORDER BY ThuocID DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;
        SELECT COUNT(*)
        FROM Thuoc
        WHERE TenThuoc LIKE @Keyword";
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
    public async Task<List<(int Id, string Ten)>> GetIdAndNameAsync()
    {
        var list = new List<(int Id, string Ten)>();
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"SELECT ThuocID, TenThuoc
                    FROM Thuoc
                    ORDER BY TenThuoc";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add((reader.GetInt32(0), reader.GetString(1)));
        }
        return list;
    }
    public async Task<ThuocReadModel?> GetDetailAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = BaseSelectDetail + " WHERE ThuocID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToDetailDTO(reader);
        return null;
    }
    public async Task<Thuoc?> GetByIdAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = BaseSelectDetail + " WHERE ThuocID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToEntity(reader);
        return null;
    }
    public async Task<int> AddAsync(Thuoc entity)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"
        INSERT INTO Thuoc (TenThuoc, HoatChat)
        OUTPUT INSERTED.ThuocID
        VALUES (@TenThuoc, @HoatChat)";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@TenThuoc", SqlDbType.NVarChar).Value = entity.TenThuoc;
        cmd.Parameters.Add("@HoatChat", SqlDbType.NVarChar)
            .Value = (object?)entity.HoatChat ?? DBNull.Value;
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    public async Task UpdateAsync(Thuoc entity)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"
        UPDATE Thuoc
        SET TenThuoc=@TenThuoc,
            HoatChat=@HoatChat
        WHERE ThuocID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = entity.ThuocID;
        cmd.Parameters.Add("@TenThuoc", SqlDbType.NVarChar).Value = entity.TenThuoc;
        cmd.Parameters.Add("@HoatChat", SqlDbType.NVarChar)
            .Value = (object?)entity.HoatChat ?? DBNull.Value;
        await cmd.ExecuteNonQueryAsync();
    }
    public async Task DeleteAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"DELETE FROM Thuoc WHERE ThuocID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        await cmd.ExecuteNonQueryAsync();
    }
    private Thuoc MapToEntity(SqlDataReader r)
    {
        return new Thuoc(
            (int)r["ThuocID"],
            (string)r["TenThuoc"],
            r["HoatChat"] as string
        );
    }
    private ThuocListReadModel MapToListDTO(SqlDataReader r)
    {
        return new ThuocListReadModel
        {
            ThuocID = (int)r["ThuocID"],
            TenThuoc = (string)r["TenThuoc"],
            HoatChat = r["HoatChat"] as string
        };
    }
    private ThuocReadModel MapToDetailDTO(SqlDataReader r)
    {
        return new ThuocReadModel
        {
            ThuocID = (int)r["ThuocID"],
            TenThuoc = (string)r["TenThuoc"],
            HoatChat = r["HoatChat"] as string
        };
    }
}