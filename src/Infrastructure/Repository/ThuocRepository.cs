using Application.DTOs;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace Infrastructure.Repository;
public class ThuocRepository : IThuocRepository
{
    private readonly string _connectionString;
    public ThuocRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string not found.");
    }
    private SqlConnection CreateConnection() => new(_connectionString);
    private const string BaseSelect =
        @"SELECT ThuocID, TenThuoc, HoatChat FROM Thuoc";
    public async Task<Thuoc?> GetByIdAsync(int id)
    {
        const string sql =
        @"SELECT ThuocID, TenThuoc, HoatChat
          FROM Thuoc
          WHERE ThuocID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }
    public async Task<(List<ThuocListReadModel>, int)> GetPagedAsync(int page, int size)
    {
        var sql =
        $@"{BaseSelect}
           ORDER BY ThuocID DESC
           OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
           SELECT COUNT(*) FROM Thuoc";
        var list = new List<ThuocListReadModel>();
        int total = 0;
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
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
    public async Task<(List<ThuocListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size)
    {
        var sql =
        $@"{BaseSelect}
           WHERE TenThuoc LIKE @Keyword
           ORDER BY ThuocID DESC
           OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
           SELECT COUNT(*) FROM Thuoc
           WHERE TenThuoc LIKE @Keyword";
        var list = new List<ThuocListReadModel>();
        int total = 0;
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");
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
    public async Task<ThuocReadModel?> GetDetailAsync(int id)
    {
        const string sql =
        @"SELECT ThuocID, TenThuoc, HoatChat
          FROM Thuoc
          WHERE ThuocID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToDetailDTO(reader) : null;
    }
    public async Task<int> AddAsync(Thuoc entity)
    {
        const string sql =
        @"INSERT INTO Thuoc (TenThuoc,HoatChat)
          OUTPUT INSERTED.ThuocID
          VALUES (@TenThuoc,@HoatChat)";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@TenThuoc", entity.TenThuoc);
        cmd.Parameters.AddWithValue("@HoatChat", (object?)entity.HoatChat ?? DBNull.Value);
        await conn.OpenAsync();
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    public async Task UpdateAsync(Thuoc entity)
    {
        const string sql =
        @"UPDATE Thuoc
          SET TenThuoc=@TenThuoc,
              HoatChat=@HoatChat
          WHERE ThuocID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@TenThuoc", entity.TenThuoc);
        cmd.Parameters.AddWithValue("@HoatChat", (object?)entity.HoatChat ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Id", entity.ThuocID);
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
    public async Task DeleteAsync(int id)
    {
        const string sql = @"DELETE FROM Thuoc WHERE ThuocID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
    private static Thuoc MapToEntity(SqlDataReader r)
    {
        return new Thuoc(
            r.GetInt32(r.GetOrdinal("ThuocID")),
            r.GetString(r.GetOrdinal("TenThuoc")),
            r.IsDBNull(r.GetOrdinal("HoatChat")) ? null : r.GetString(r.GetOrdinal("HoatChat"))
        );
    }
    private static ThuocListReadModel MapToLiteDTO(SqlDataReader r)
    {
        return new ThuocListReadModel
        {
            ThuocID = r.GetInt32(r.GetOrdinal("ThuocID")),
            TenThuoc = r.GetString(r.GetOrdinal("TenThuoc")),
            HoatChat = r.IsDBNull(r.GetOrdinal("HoatChat")) ? null : r.GetString(r.GetOrdinal("HoatChat"))
        };
    }
    private static ThuocReadModel MapToDetailDTO(SqlDataReader r)
    {
        return new ThuocReadModel
        {
            ThuocID = r.GetInt32(r.GetOrdinal("ThuocID")),
            TenThuoc = r.GetString(r.GetOrdinal("TenThuoc")),
            HoatChat = r.IsDBNull(r.GetOrdinal("HoatChat")) ? null : r.GetString(r.GetOrdinal("HoatChat"))
        };
    }
}