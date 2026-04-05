using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class ChiTietToaThuocRepository : IChiTietToaThuocRepository
{
	private readonly string _connectionString;

	public ChiTietToaThuocRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new InvalidOperationException("Connection string not found.");
	}

	private const string BaseSelectList = @"
        SELECT ct.ChiTietToaThuocID, ct.ToaThuocID, ct.ThuocID, t.TenThuoc, ct.LieuDung, ct.SoLuong
        FROM ChiTietToaThuoc ct
        INNER JOIN Thuoc t ON ct.ThuocID = t.ThuocID";

	private SqlConnection CreateConnection() => new(_connectionString);

	public async Task<List<int>> GetThuocIdsAsync(int toaThuocID)
	{
		const string sql = @"SELECT ThuocID FROM ChiTietToaThuoc WHERE ToaThuocID=@ToaThuocID";
		var list = new List<int>();
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@ToaThuocID", SqlDbType.Int).Value = toaThuocID;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
			list.Add(reader.GetInt32(0));
		return list;
	}

	public async Task AddAsync(int toaThuocID, List<ChiTietToaThuoc> chiTiet)
	{
		const string sql = @"INSERT INTO ChiTietToaThuoc (ToaThuocID, ThuocID, LieuDung, SoLuong)
                             VALUES (@ToaThuocID, @ThuocID, @LieuDung, @SoLuong)";
		await using var conn = CreateConnection();
		await conn.OpenAsync();

		foreach (var ct in chiTiet)
		{
			await using var cmd = new SqlCommand(sql, conn);
			cmd.Parameters.Add("@ToaThuocID", SqlDbType.Int).Value = toaThuocID;
			cmd.Parameters.Add("@ThuocID", SqlDbType.Int).Value = ct.ThuocID;
			cmd.Parameters.Add("@LieuDung", SqlDbType.NVarChar).Value = (object?)ct.LieuDung ?? DBNull.Value;
			cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = ct.SoLuong;
			await cmd.ExecuteNonQueryAsync();
		}
	}

	public async Task UpdateAsync(int toaThuocID, List<ChiTietToaThuoc> chiTiet)
	{
		const string sql = @"UPDATE ChiTietToaThuoc
                             SET LieuDung=@LieuDung, SoLuong=@SoLuong
                             WHERE ToaThuocID=@ToaThuocID AND ThuocID=@ThuocID";
		await using var conn = CreateConnection();
		await conn.OpenAsync();

		foreach (var ct in chiTiet)
		{
			await using var cmd = new SqlCommand(sql, conn);
			cmd.Parameters.Add("@ToaThuocID", SqlDbType.Int).Value = toaThuocID;
			cmd.Parameters.Add("@ThuocID", SqlDbType.Int).Value = ct.ThuocID;
			cmd.Parameters.Add("@LieuDung", SqlDbType.NVarChar).Value = (object?)ct.LieuDung ?? DBNull.Value;
			cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = ct.SoLuong;
			await cmd.ExecuteNonQueryAsync();
		}
	}

	public async Task DeleteAsync(int toaThuocID, int thuocID)
	{
		const string sql = @"DELETE FROM ChiTietToaThuoc
                             WHERE ToaThuocID=@ToaThuocID AND ThuocID=@ThuocID";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@ToaThuocID", SqlDbType.Int).Value = toaThuocID;
		cmd.Parameters.Add("@ThuocID", SqlDbType.Int).Value = thuocID;
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task<int> CountAsync(int toaThuocID)
	{
		const string sql = @"SELECT COUNT(*) FROM ChiTietToaThuoc WHERE ToaThuocID=@ToaThuocID";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@ToaThuocID", SqlDbType.Int).Value = toaThuocID;
		await conn.OpenAsync();
		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}
	public async Task<List<ChiTietToaThuocReadModel>> GetByToaThuocAsync(int toaThuocID)
	{
		const string sql = @$"
            {BaseSelectList}
            WHERE ct.ToaThuocID=@ToaThuocID";

		var list = new List<ChiTietToaThuocReadModel>();
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@ToaThuocID", SqlDbType.Int).Value = toaThuocID;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		var tenThuoc = reader.GetOrdinal("TenThuoc");
		var lieuDung = reader.GetOrdinal("LieuDung");
		var soLuong = reader.GetOrdinal("SoLuong");

		while (await reader.ReadAsync())
			list.Add(MapToReadModel(reader));
		return list;
	}
	#region Mapping
	private ChiTietToaThuocReadModel MapToReadModel(SqlDataReader r)
	{
		return new ChiTietToaThuocReadModel
		{
			TenThuoc = r.GetString(r.GetOrdinal("TenThuoc")),
			LieuDung = r.IsDBNull(r.GetOrdinal("LieuDung")) ? null : r.GetString(r.GetOrdinal("LieuDung")),
			SoLuong = r.GetInt32(r.GetOrdinal("SoLuong"))
		};
	}
	#endregion
}