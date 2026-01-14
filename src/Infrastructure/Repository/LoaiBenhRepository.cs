using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;
public class LoaiBenhRepository : ILoaiBenhRepository
{
	private readonly string _connectionString;

	public LoaiBenhRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")!;
	}

	public async Task<List<LoaiBenh>> GetAllAsync()
	{
		const string sql = @"
			SELECT LoaiBenhID, TenBenh, TenKhoaHoc, NhomBenh,
			       MoTa, DoPhoBien, MucDoNghiemTrong, NgayTao
			FROM LoaiBenh";

		var list = new List<LoaiBenh>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		await conn.OpenAsync();
		await using var r = await cmd.ExecuteReaderAsync();

		while (await r.ReadAsync())
			list.Add(MapToEntity(r));

		return list;
	}

	public async Task<LoaiBenh?> GetByIdAsync(int id)
	{
		const string sql = @"
			SELECT LoaiBenhID, TenBenh, TenKhoaHoc, NhomBenh,
			       MoTa, DoPhoBien, MucDoNghiemTrong, NgayTao
			FROM LoaiBenh
			WHERE LoaiBenhID = @id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@id", id);

		await conn.OpenAsync();
		await using var r = await cmd.ExecuteReaderAsync();

		return await r.ReadAsync() ? MapToEntity(r) : null;
	}

	public async Task<List<LoaiBenh>> SearchByTenAsync(string keyword)
	{
		const string sql = @"
			SELECT LoaiBenhID, TenBenh, TenKhoaHoc, NhomBenh,
			       MoTa, DoPhoBien, MucDoNghiemTrong, NgayTao
			FROM LoaiBenh
			WHERE TenBenh LIKE @kw OR TenKhoaHoc LIKE @kw";

		var list = new List<LoaiBenh>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

		await conn.OpenAsync();
		await using var r = await cmd.ExecuteReaderAsync();

		while (await r.ReadAsync())
			list.Add(MapToEntity(r));

		return list;
	}

	public async Task AddAsync(LoaiBenh lb)
	{
		const string sql = @"
			INSERT INTO LoaiBenh
			(TenBenh, TenKhoaHoc, NhomBenh, MoTa, DoPhoBien, MucDoNghiemTrong)
			VALUES
			(@TenBenh, @TenKhoaHoc, @NhomBenh, @MoTa, @DoPhoBien, @MucDoNghiemTrong)";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@TenBenh", lb.TenBenh);
		cmd.Parameters.AddWithValue("@TenKhoaHoc", (object?)lb.TenKhoaHoc ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@NhomBenh", (object?)lb.NhomBenh ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@MoTa", (object?)lb.MoTa ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@DoPhoBien", (object?)lb.DoPhoBien ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@MucDoNghiemTrong", (object?)lb.MucDoNghiemTrong ?? DBNull.Value);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task UpdateAsync(LoaiBenh lb)
	{
		const string sql = @"
			UPDATE LoaiBenh
			SET TenBenh = @TenBenh,
				TenKhoaHoc = @TenKhoaHoc,
				NhomBenh = @NhomBenh,
				MoTa = @MoTa,
				DoPhoBien = @DoPhoBien,
				MucDoNghiemTrong = @MucDoNghiemTrong
			WHERE LoaiBenhID = @Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@Id", lb.LoaiBenhID);
		cmd.Parameters.AddWithValue("@TenBenh", lb.TenBenh);
		cmd.Parameters.AddWithValue("@TenKhoaHoc", (object?)lb.TenKhoaHoc ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@NhomBenh", (object?)lb.NhomBenh ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@MoTa", (object?)lb.MoTa ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@DoPhoBien", (object?)lb.DoPhoBien ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@MucDoNghiemTrong", (object?)lb.MucDoNghiemTrong ?? DBNull.Value);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	private static LoaiBenh MapToEntity(SqlDataReader r)
	{
		return new LoaiBenh(
			r.GetInt32(0),
			r.GetString(1),
			r.IsDBNull(2) ? null : r.GetString(2),
			r.IsDBNull(3) ? null : r.GetString(3),
			r.IsDBNull(4) ? null : r.GetString(4),
			r.IsDBNull(5) ? null : r.GetString(5),
			r.IsDBNull(6) ? null : r.GetString(6),
			r.GetDateTime(7)
		);
	}
}
