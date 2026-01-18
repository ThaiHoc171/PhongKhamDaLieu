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

	public async Task<List<ChiTietToaThuoc>> GetByToaThuocIdAsync(int toaThuocID)
	{
		const string sql = @"
        SELECT ct.ChiTietToaThuocID, ct.ToaThuocID,
               ct.ThuocID, t.TenThuoc,
               ct.LieuDung, ct.SoLuong
        FROM ChiTietToaThuoc ct
        JOIN Thuoc t ON ct.ThuocID = t.ThuocID
        WHERE ct.ToaThuocID = @ToaThuocID";

		var list = new List<ChiTietToaThuoc>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@ToaThuocID", toaThuocID);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add(new ChiTietToaThuoc(
				reader.GetInt32(0),
				reader.GetInt32(1),
				reader.GetInt32(2),
				reader.GetString(3),
				reader["LieuDung"] as string,
				reader.GetInt32(5)
			));
		}

		return list;
	}

}
