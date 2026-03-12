using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class ThuocRepository : IThuocRepository
{
    private readonly string _connectionString;

    public ThuocRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")!;
    }

    public async Task<List<ThuocListReadModel>> GetPagedAsync(int pageNumber, int pageSize)
    {
        const string sql = @"
            SELECT ThuocID, TenThuoc, HoatChat
            FROM Thuoc
            ORDER BY ThuocID
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

        var list = new List<ThuocListReadModel>();

        int offset = (pageNumber - 1) * pageSize;

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Offset", offset);
        cmd.Parameters.AddWithValue("@PageSize", pageSize);

        await conn.OpenAsync();

        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(new ThuocListReadModel
            {
                ThuocID = reader.GetInt32(0),
                TenThuoc = reader.GetString(1),
                HoatChat = reader.IsDBNull(2) ? null : reader.GetString(2)
            });
        }

        return list;
    }

    public async Task<int> CountAsync()
    {
        const string sql = "SELECT COUNT(*) FROM Thuoc";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        await conn.OpenAsync();

        return (int)await cmd.ExecuteScalarAsync();
    }

    public async Task<List<ThuocListReadModel>> SearchAsync(string keyword)
    {
        const string sql = @"
            SELECT ThuocID, TenThuoc, HoatChat
            FROM Thuoc
            WHERE TenThuoc LIKE @kw OR HoatChat LIKE @kw";

        var list = new List<ThuocListReadModel>();

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

        await conn.OpenAsync();

        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(new ThuocListReadModel
            {
                ThuocID = reader.GetInt32(0),
                TenThuoc = reader.GetString(1),
                HoatChat = reader.IsDBNull(2) ? null : reader.GetString(2)
            });
        }

        return list;
    }

    public async Task<List<ThuocComboboxReadModel>> GetComboboxAsync()
    {
        const string sql = @"
            SELECT ThuocID, TenThuoc
            FROM Thuoc
            ORDER BY TenThuoc";

        var list = new List<ThuocComboboxReadModel>();

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        await conn.OpenAsync();

        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(new ThuocComboboxReadModel
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            });
        }

        return list;
    }

    public async Task<Thuoc?> GetByIdAsync(int id)
    {
        const string sql = @"
            SELECT ThuocID, TenThuoc, HoatChat
            FROM Thuoc
            WHERE ThuocID = @id";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@id", id);

        await conn.OpenAsync();

        await using var reader = await cmd.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
            return null;

        return new Thuoc(
            reader.GetInt32(0),
            reader.GetString(1),
            reader.IsDBNull(2) ? null : reader.GetString(2)
        );
    }

    public async Task<List<Thuoc>> GetAllAsync()
    {
        const string sql = @"
            SELECT ThuocID, TenThuoc, HoatChat
            FROM Thuoc";

        var list = new List<Thuoc>();

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        await conn.OpenAsync();

        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(new Thuoc(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.IsDBNull(2) ? null : reader.GetString(2)
            ));
        }

        return list;
    }

    public async Task AddAsync(Thuoc thuoc)
    {
        const string sql = @"
            INSERT INTO Thuoc (TenThuoc, HoatChat)
            VALUES (@TenThuoc, @HoatChat)";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@TenThuoc", thuoc.TenThuoc);
        cmd.Parameters.AddWithValue("@HoatChat", (object?)thuoc.HoatChat ?? DBNull.Value);

        await conn.OpenAsync();

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task UpdateAsync(Thuoc thuoc)
    {
        const string sql = @"
            UPDATE Thuoc
            SET TenThuoc = @TenThuoc,
                HoatChat = @HoatChat
            WHERE ThuocID = @Id";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Id", thuoc.ThuocID);
        cmd.Parameters.AddWithValue("@TenThuoc", thuoc.TenThuoc);
        cmd.Parameters.AddWithValue("@HoatChat", (object?)thuoc.HoatChat ?? DBNull.Value);

        await conn.OpenAsync();

        await cmd.ExecuteNonQueryAsync();
    }
}