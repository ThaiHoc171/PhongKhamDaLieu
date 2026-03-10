using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class ChiTietToaThuocRepository : IChiTietToaThuocRepository
{
	private readonly string _connectionString;

	public ChiTietToaThuocRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
		?? throw new ArgumentNullException("Connection string not found");
	}
	public async Task<List<int>> GetThuocIdsAsync(int toaThuocID)
	{
		const string sql = @"
		SELECT ThuocID
		FROM ChiTietToaThuoc
		WHERE ToaThuocID = @ToaThuocID";

		var list = new List<int>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@ToaThuocID", toaThuocID);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(reader.GetInt32(0));

		return list;
	}
	public async Task AddAsync(int toaThuocID, List<ChiTietToaThuoc> chiTiet)
	{
		const string sql = @"
        INSERT INTO ChiTietToaThuoc
        (ToaThuocID, ThuocID, LieuDung, SoLuong)
        VALUES (@ToaThuocID, @ThuocID, @LieuDung, @SoLuong)";

		await using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		foreach (var ct in chiTiet)
		{
			await using var cmd = new SqlCommand(sql, conn);
			cmd.Parameters.AddWithValue("@ToaThuocID", toaThuocID);
			cmd.Parameters.AddWithValue("@ThuocID", ct.ThuocID);
			cmd.Parameters.AddWithValue("@LieuDung", (object?)ct.LieuDung ?? DBNull.Value);
			cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);

			await cmd.ExecuteNonQueryAsync();
		}
	}

	public async Task<List<ChiTietToaThuocReadModel>> GetByToaThuocAsync(int toaThuocID)
	{
		const string sql = @"
        SELECT t.TenThuoc, ct.LieuDung, ct.SoLuong
        FROM ChiTietToaThuoc ct
        JOIN Thuoc t ON ct.ThuocID = t.ThuocID
        WHERE ct.ToaThuocID = @ToaThuocID";

		var list = new List<ChiTietToaThuocReadModel>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@ToaThuocID", toaThuocID);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add(new ChiTietToaThuocReadModel
			{
				TenThuoc = reader.GetString(0),
				LieuDung = reader.GetString(1),
				SoLuong = reader.GetInt32(2)
			});
		}

		return list;
	}
	public async Task UpdateAsync(int toaThuocID, List<ChiTietToaThuoc> chiTiet)
	{
		const string sql = @"
		UPDATE ChiTietToaThuoc
		SET LieuDung = @LieuDung,
			SoLuong = @SoLuong
		WHERE ToaThuocID = @ToaThuocID
		AND ThuocID = @ThuocID";

		await using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		foreach (var ct in chiTiet)
		{
			await using var cmd = new SqlCommand(sql, conn);

			cmd.Parameters.AddWithValue("@ToaThuocID", toaThuocID);
			cmd.Parameters.AddWithValue("@ThuocID", ct.ThuocID);
			cmd.Parameters.AddWithValue("@LieuDung", (object?)ct.LieuDung ?? DBNull.Value);
			cmd.Parameters.AddWithValue("@SoLuong", ct.SoLuong);

			await cmd.ExecuteNonQueryAsync();
		}
	}
	public async Task DeleteAsync(int toaThuocID, int thuocID)
	{
		const string sql = @"
		DELETE FROM ChiTietToaThuoc
		WHERE ToaThuocID = @ToaThuocID
		AND ThuocID = @ThuocID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@ToaThuocID", toaThuocID);
		cmd.Parameters.AddWithValue("@ThuocID", thuocID);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task<int> CountAsync(int toaThuocID)
	{
		const string sql = @"
		SELECT COUNT(*) 
		FROM ChiTietToaThuoc
		WHERE ToaThuocID = @ToaThuocID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@ToaThuocID", toaThuocID);

		await conn.OpenAsync();

		return (int)await cmd.ExecuteScalarAsync();
	}
}
