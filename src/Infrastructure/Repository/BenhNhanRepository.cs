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
			SELECT BenhNhanID, ThongTinID, LoaiDa, TrangThaiTheoDoi, GhiChu 
			FROM BenhNhan WHERE BenhNhanID = @Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Id", id);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}
	public async Task<List<BenhNhan>> GetNhanViens(string keyword)
	{
		const string sql = @"
				SELECT b.BenhNhanID, b.ThongTinID, b.LoaiDa, b.TrangThaiTheoDoi, b.GhiChu
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
		const string sql = @"SELECT BenhNhanID, ThongTinID, LoaiDa, TrangThaiTheoDoi, GhiChu FROM BenhNhan";
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
			INSERT INTO BenhNhan (ThongTinID, LoaiDa, TrangThaiTheoDoi, GhiChu) 
			OUTPUT INSERTED.BenhNhanID
			VALUES (@ThongTinID, @LoaiDa, @TrangThaiTheoDoi, @GhiChu)";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@ThongTinID", benhNhan.ThongTinID);
		cmd.Parameters.AddWithValue("@LoaiDa", benhNhan.LoaiDa);
		cmd.Parameters.AddWithValue("@TrangThaiTheoDoi", benhNhan.TrangThaiTheoDoi);
		cmd.Parameters.AddWithValue("@GhiChu", benhNhan.GhiChu ?? "");
		await conn.OpenAsync();
		return (int)await cmd.ExecuteScalarAsync();
	}

	public async Task UpdateAsync(BenhNhan benhNhan)
	{
		const string sql = @"
			UPDATE BenhNhan 
			SET LoaiDa = @LoaiDa, TrangThaiTheoDoi = @TrangThaiTheoDoi, GhiChu = @GhiChu 
            WHERE BenhNhanID = @Id";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@LoaiDa", benhNhan.LoaiDa);
		cmd.Parameters.AddWithValue("@TrangThaiTheoDoi", benhNhan.TrangThaiTheoDoi);
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
			loaiDa: reader.GetString(2),
			trangThaiTheoDoi: reader.GetString(3),
			ghiChu: reader.IsDBNull(4) ? "" : reader.GetString(4)
		);
	}
}

