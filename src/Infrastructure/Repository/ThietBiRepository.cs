using Application.DTOs;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository;

public class ThietBiRepository : IThietBiRepository
{
    private readonly string _connectionString;

    public ThietBiRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string not found.");
    }

    private SqlConnection CreateConnection() => new(_connectionString);

    private const string BaseSelect =
        @"SELECT ThietBiID, TenTB, LoaiTB FROM ThietBi";

    public async Task<ThietBi?> GetByIdAsync(int id)
    {
        const string sql = @"SELECT * FROM ThietBi WHERE ThietBiID=@Id";

        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Id", id);

        await conn.OpenAsync();

        await using var reader = await cmd.ExecuteReaderAsync();

        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }

    public async Task<(List<ThietBiListReadModel>, int)> GetPagedAsync(int page, int size)
    {
        var sql =
        $@"{BaseSelect}
           ORDER BY ThietBiID DESC
           OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

           SELECT COUNT(*) FROM ThietBi";

        var list = new List<ThietBiListReadModel>();
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

    public async Task<(List<ThietBiListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size)
    {
        var sql =
        $@"{BaseSelect}
           WHERE TenTB LIKE @Keyword
           ORDER BY ThietBiID DESC
           OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

           SELECT COUNT(*) FROM ThietBi
           WHERE TenTB LIKE @Keyword";

        var list = new List<ThietBiListReadModel>();
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

    public async Task<ThietBiReadModel?> GetDetailAsync(int id)
    {
        const string sql =
        @"SELECT ThietBiID, TenTB, LoaiTB
          FROM ThietBi
          WHERE ThietBiID=@Id";

        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Id", id);

        await conn.OpenAsync();

        await using var reader = await cmd.ExecuteReaderAsync();

        return await reader.ReadAsync() ? MapToDetailDTO(reader) : null;
    }

    public async Task<int> AddAsync(ThietBi entity)
    {
        const string sql =
        @"INSERT INTO ThietBi (TenTB,LoaiTB)
          OUTPUT INSERTED.ThietBiID
          VALUES (@TenTB,@LoaiTB)";

        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@TenTB", entity.TenTB);
        cmd.Parameters.AddWithValue("@LoaiTB", (object?)entity.LoaiTB ?? DBNull.Value);

        await conn.OpenAsync();

        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }

    public async Task UpdateAsync(ThietBi entity)
    {
        const string sql =
        @"UPDATE ThietBi
          SET TenTB=@TenTB,
              LoaiTB=@LoaiTB
          WHERE ThietBiID=@Id";

        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@TenTB", entity.TenTB);
        cmd.Parameters.AddWithValue("@LoaiTB", (object?)entity.LoaiTB ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Id", entity.ThietBiID);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(int id)
    {
        const string sql = @"DELETE FROM ThietBi WHERE ThietBiID=@Id";

        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Id", id);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    private static ThietBi MapToEntity(SqlDataReader r)
    {
        return new ThietBi(
            r.GetInt32(r.GetOrdinal("ThietBiID")),
            r.GetString(r.GetOrdinal("TenTB")),
            r.IsDBNull(r.GetOrdinal("LoaiTB")) ? null : r.GetString(r.GetOrdinal("LoaiTB"))
        );
    }

    private static ThietBiListReadModel MapToLiteDTO(SqlDataReader r)
    {
        return new ThietBiListReadModel
        {
            ThietBiID = r.GetInt32(r.GetOrdinal("ThietBiID")),
            TenTB = r.GetString(r.GetOrdinal("TenTB")),
            LoaiTB = r.IsDBNull(r.GetOrdinal("LoaiTB")) ? null : r.GetString(r.GetOrdinal("LoaiTB"))
        };
    }

    private static ThietBiReadModel MapToDetailDTO(SqlDataReader r)
    {
        return new ThietBiReadModel
        {
            ThietBiID = r.GetInt32(r.GetOrdinal("ThietBiID")),
            TenTB = r.GetString(r.GetOrdinal("TenTB")),
            LoaiTB = r.IsDBNull(r.GetOrdinal("LoaiTB")) ? null : r.GetString(r.GetOrdinal("LoaiTB"))
        };
    }
}