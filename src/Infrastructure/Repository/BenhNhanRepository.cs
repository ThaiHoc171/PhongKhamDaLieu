using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class BenhNhanRepository : IBenhNhanRepository
{
	private readonly string _connectionString;

	public BenhNhanRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}

	public async Task<BenhNhan?> GetByIdAsync(int id)
	{
		const string sql = @"
			SELECT BenhNhanID, ThongTinID, GhiChu, NgayTao, NgayCapNhat
			FROM BenhNhan WHERE BenhNhanID = @Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Id", id);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}
	public async Task<List<BenhNhan>> GetBenhNhans(string keyword)
	{
		const string sql = @"
				SELECT b.BenhNhanID, b.ThongTinID, b.GhiChu
				FROM BenhNhan b
				INNER JOIN ThongTinCaNhan t ON b.ThongTinID = t.ThongTinID
				WHERE t.HoTen LIKE @Keyword
				";
		var list = new List<BenhNhan>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(MapToEntity(reader));
		}
		return list;
	}
	public async Task<List<BenhNhan>> GetAllAsync()
	{
		const string sql = @"SELECT BenhNhanID, ThongTinID, GhiChu, NgayTao, NgayCapNhat
						FROM BenhNhan";
		var list = new List<BenhNhan>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(MapToEntity(reader));
		}
		return list;
	}

	public async Task<int> AddAsync(BenhNhan benhNhan)
	{
		const string sql = @"
			INSERT INTO BenhNhan (ThongTinID, GhiChu) 
			OUTPUT INSERTED.BenhNhanID
			VALUES (@ThongTinID, @GhiChu)";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@ThongTinID", benhNhan.ThongTinID);
		cmd.Parameters.AddWithValue("@GhiChu", benhNhan.GhiChu ?? "");
		await conn.OpenAsync();
		return (int)await cmd.ExecuteScalarAsync();
	}

	public async Task UpdateAsync(BenhNhan benhNhan)
	{
		const string sql = @"
			UPDATE BenhNhan 
			SET GhiChu = @GhiChu, NgayCapNhat = GETDATE()
            WHERE BenhNhanID = @Id";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@GhiChu", benhNhan.GhiChu ?? "");
		cmd.Parameters.AddWithValue("@Id", benhNhan.BenhNhanID);
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	private static BenhNhan MapToEntity(SqlDataReader reader)
	{
		return new BenhNhan(
			benhNhanID: reader.GetInt32(0),
			thongTinID: reader.GetInt32(1),
			ghiChu: reader.IsDBNull(2) ? "" : reader.GetString(2),
			ngayTao: reader.GetDateTime(3),
			ngayCapNhat: reader.IsDBNull(4) ? reader.GetDateTime(3) : reader.GetDateTime(4)
		);
	}
}

