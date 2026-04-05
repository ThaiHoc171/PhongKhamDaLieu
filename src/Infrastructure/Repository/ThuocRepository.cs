using Application.DTOs;
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
	private const string BaseSelect = @"
        SELECT ThuocID,TenThuoc,HoatChat
        FROM Thuoc";
	public async Task<(List<ThuocReadModel>, int)> GetPagedAsync(int page, int size)
	{
		var list = new List<ThuocReadModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
        {BaseSelect}
        ORDER BY TenThuoc
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

        SELECT COUNT(*) FROM Thuoc";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@Size", SqlDbType.Int).Value = size;

		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToDTO(reader));

		await reader.NextResultAsync();

		if (await reader.ReadAsync())
			total = reader.GetInt32(0);

		return (list, total);
	}

	public async Task<(List<ThuocReadModel>, int)> SearchPagedAsync(string keyword, int page, int size)
	{
		var list = new List<ThuocReadModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
        {BaseSelect}
        WHERE TenThuoc LIKE @Keyword
        ORDER BY TenThuoc
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

        SELECT COUNT(*)
        FROM Thuoc
        WHERE TenThuoc LIKE @Keyword";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 200).Value = $"%{keyword}%";
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@Size", SqlDbType.Int).Value = size;

		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToDTO(reader));

		await reader.NextResultAsync();

		if (await reader.ReadAsync())
			total = reader.GetInt32(0);

		return (list, total);
	}

	public async Task<ThuocReadModel?> GetDetailAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelect + " WHERE ThuocID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToDTO(reader);

		return null;
	}

	public async Task<Thuoc?> GetByIdAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelect + " WHERE ThuocID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}

	public async Task<int> AddAsync(Thuoc thuoc)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"INSERT INTO Thuoc
					(TenThuoc,HoatChat)
					VALUES(@TenThuoc,@HoatChat)";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@TenThuoc", SqlDbType.NVarChar, 200).Value = thuoc.TenThuoc;
		cmd.Parameters.Add("@HoatChat", SqlDbType.NVarChar, 200).Value = thuoc.HoatChat;

		int row = await cmd.ExecuteNonQueryAsync();

		return row;
	}

	public async Task<int> UpdateAsync(Thuoc thuoc)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"UPDATE Thuoc
					SET TenThuoc=@TenThuoc,
						HoatChat=@HoatChat
					WHERE ThuocID=@Id";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = thuoc.ThuocID;
		cmd.Parameters.Add("@TenThuoc", SqlDbType.NVarChar, 200).Value = thuoc.TenThuoc;
		cmd.Parameters.Add("@HoatChat", SqlDbType.NVarChar, 200).Value = thuoc.HoatChat;

		int row = await cmd.ExecuteNonQueryAsync();

		return row;
	}

	public async Task BulkInsertAsync(List<Thuoc> list)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var table = new DataTable();

		table.Columns.Add("TenThuoc");
		table.Columns.Add("HoatChat");

		foreach (var item in list)
		{
			table.Rows.Add(
				item.TenThuoc,
				item.HoatChat
			);
		}

		using var bulk = new SqlBulkCopy(conn);

		bulk.DestinationTableName = "Thuoc";

		bulk.ColumnMappings.Add("TenThuoc", "TenThuoc");
		bulk.ColumnMappings.Add("HoatChat", "HoatChat");

		await bulk.WriteToServerAsync(table);
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
	public async Task<bool> ExistsTenThuocAsync(string tenThuoc)
	{
		const string sql = @"
		SELECT 1
		FROM Thuoc
		WHERE TenThuoc = @TenThuoc";

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TenThuoc", SqlDbType.NVarChar, 200).Value = tenThuoc;

		var result = await cmd.ExecuteScalarAsync();

		return result != null;
	}
	public async Task<List<NameResponseDTO>> GetComboboxAsync()
	{
		var list = new List<NameResponseDTO>();
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();
		var sql = @"SELECT ThuocID, TenThuoc
                    FROM Thuoc
                    ORDER BY TenThuoc ASC";
		using var cmd = new SqlCommand(sql, conn);
		using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(new NameResponseDTO
			{
				Id = reader.GetInt32(reader.GetOrdinal("ThuocID")),
				Name = reader.GetString(reader.GetOrdinal("TenThuoc"))
			});
		}
		return list;
	}
	#region Mapping

	private Thuoc MapToEntity(SqlDataReader r)
	{
		return new Thuoc(
			r.GetInt32(r.GetOrdinal("ThuocID")),
			r.GetString(r.GetOrdinal("TenThuoc")),
			r.GetString(r.GetOrdinal("HoatChat"))
		);
	}

	private ThuocReadModel MapToDTO(SqlDataReader r)
	{
		return new ThuocReadModel
		{
			ThuocID = r.GetInt32(r.GetOrdinal("ThuocID")),
			TenThuoc = r.GetString(r.GetOrdinal("TenThuoc")),
			HoatChat = r.GetString(r.GetOrdinal("HoatChat"))
		};
	}

	#endregion
}