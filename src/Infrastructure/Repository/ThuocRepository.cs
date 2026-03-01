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
	public async Task<List<Thuoc>> GetAllAsync() 
	{ 
		const string sql = @"
			SELECT ThuocID, TenThuoc, HoatChat 
			FROM Thuoc"; 
		var list = new List<Thuoc>(); 
		await using var conn = new SqlConnection(_connectionString); 
		await using var cmd = new SqlCommand(sql, conn); await conn.OpenAsync(); 
		await using var reader = await cmd.ExecuteReaderAsync(); 
		while (await reader.ReadAsync()) 
		{ 
			list.Add(MapToEntity(reader)); 
		} 
		return list; 
	}
	public async Task<(List<Thuoc> Data, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
	{
		const string sql = @"
			SELECT 
				ThuocID, TenThuoc, HoatChat
			FROM Thuoc
			ORDER BY ThuocID
			OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

			SELECT COUNT(*) FROM Thuoc;
		";

		var list = new List<Thuoc>();
		int totalCount = 0;

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		int offset = (pageNumber - 1) * pageSize;

		cmd.Parameters.AddWithValue("@Offset", offset);
		cmd.Parameters.AddWithValue("@PageSize", pageSize);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		// Result 1: Data
		while (await reader.ReadAsync())
		{
			list.Add(MapToEntity(reader));
		}

		// Result 2: TotalCount
		if (await reader.NextResultAsync())
		{
			if (await reader.ReadAsync())
			{
				totalCount = reader.GetInt32(0);
			}
		}

		return (list, totalCount);
	}

	public async Task<List<Thuoc>> SearchAsync(string keyword)
	{
		const string sql = @"
			SELECT ThuocID, TenThuoc, HoatChat
			FROM Thuoc
			WHERE TenThuoc LIKE @kw OR HoatChat LIKE @kw";

		var list = new List<Thuoc>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add(MapToEntity(reader));
		}

		return list;
	}

    public async Task<List<(int Id, string Ten)>> GetIdAndNameAsync()
    {
        const string sql = @"
			SELECT ThuocID, TenThuoc
			FROM Thuoc
			ORDER BY TenThuoc";

        var list = new List<(int, string)>();

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add((
                reader.GetInt32(reader.GetOrdinal("ThuocID")),
                reader.GetString(reader.GetOrdinal("TenThuoc"))
            ));
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

		return await reader.ReadAsync() ? MapToEntity(reader) : null;
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

	private static Thuoc MapToEntity(SqlDataReader r)
	{
		return new Thuoc(
			id: r.GetInt32(0),
			tenThuoc: r.GetString(1),
			hoatChat: r.IsDBNull(2) ? null : r.GetString(2)
		);
	}
}
