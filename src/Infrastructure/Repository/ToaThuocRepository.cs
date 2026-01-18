using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class ToaThuocRepository : IToaThuocRepository
{
	private readonly string _connectionString;

	public ToaThuocRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
	?? throw new ArgumentNullException("Connection string not found");
	}

	public async Task<int> AddAsync(ToaThuoc toaThuoc)
	{
		const string sql = @"
        INSERT INTO ToaThuoc (PhienKhamID, NhanVienKeDonID, GhiChu)
        OUTPUT INSERTED.ToaThuocID
        VALUES (@PhienKhamID, @NhanVienKeDonID, @GhiChu)";

		using var conn = new SqlConnection(_connectionString);
		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@PhienKhamID", toaThuoc.PhienKhamID);
		cmd.Parameters.AddWithValue("@NhanVienKeDonID", toaThuoc.NhanVienKeDonID);
		cmd.Parameters.AddWithValue("@GhiChu", (object?)toaThuoc.GhiChu ?? DBNull.Value);

		await conn.OpenAsync();
		return (int)await cmd.ExecuteScalarAsync();
	}

	public async Task<ToaThuoc?> GetByPhienKhamIdAsync(int phienKhamID)
	{
		const string sql = @"
        SELECT ToaThuocID, PhienKhamID, NhanVienKeDonID, NgayLap, GhiChu
        FROM ToaThuoc
        WHERE PhienKhamID = @PhienKhamID";

		using var conn = new SqlConnection(_connectionString);
		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PhienKhamID", phienKhamID);

		await conn.OpenAsync();
		using var reader = await cmd.ExecuteReaderAsync();

		return await reader.ReadAsync()
			? new ToaThuoc(
				reader.GetInt32(0),
				reader.GetInt32(1),
				reader.GetInt32(2),
				reader.GetDateTime(3),
				reader["GhiChu"] as string)
			: null;
	}
}
