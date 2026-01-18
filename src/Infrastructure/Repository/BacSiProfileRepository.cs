using Application.Repository;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class BacSiProfileRepository : IBacSiProfileRepository
{
	private readonly string _connectionString;

	public BacSiProfileRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}

	public async Task<BacSiProfile?> GetByNhanVienIdAsync(int nhanVienID)
	{
		const string sql = @"SELECT * FROM BacSiProfile WHERE NhanVienID = @NhanVienID";

		using var conn = new SqlConnection(_connectionString);
		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@NhanVienID", nhanVienID);

		await conn.OpenAsync();
		using var reader = await cmd.ExecuteReaderAsync();

		if (!await reader.ReadAsync())
			return null;

		return new BacSiProfile(
			reader.GetInt32("BacSiProfileID"),
			reader.GetInt32("NhanVienID"),
			reader["GioiThieu"] as string,
			reader["ChuyenMon"] as string,
			reader["ThanhTuu"] as string,
			reader["HinhAnh"] as string,
			reader["KinhNghiem"] as string,
			reader.GetDateTime("NgayCapNhat")
		);
	}

	public async Task AddAsync(BacSiProfile profile)
	{
		const string sql = @"
        INSERT INTO BacSiProfile
        (NhanVienID, GioiThieu, ChuyenMon, ThanhTuu, HinhAnh, KinhNghiem)
        VALUES
        (@NhanVienID, @GioiThieu, @ChuyenMon, @ThanhTuu, @HinhAnh, @KinhNghiem)";

		using var conn = new SqlConnection(_connectionString);
		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@NhanVienID", profile.NhanVienID);
		cmd.Parameters.AddWithValue("@GioiThieu", (object?)profile.GioiThieu ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@ChuyenMon", (object?)profile.ChuyenMon ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@ThanhTuu", (object?)profile.ThanhTuu ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@HinhAnh", (object?)profile.HinhAnh ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@KinhNghiem", (object?)profile.KinhNghiem ?? DBNull.Value);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task UpdateAsync(BacSiProfile profile)
	{
		const string sql = @"
        UPDATE BacSiProfile
        SET GioiThieu = @GioiThieu,
            ChuyenMon = @ChuyenMon,
            ThanhTuu = @ThanhTuu,
            HinhAnh = @HinhAnh,
            KinhNghiem = @KinhNghiem,
            NgayCapNhat = GETDATE()
        WHERE NhanVienID = @NhanVienID";

		using var conn = new SqlConnection(_connectionString);
		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@NhanVienID", profile.NhanVienID);
		cmd.Parameters.AddWithValue("@GioiThieu", (object?)profile.GioiThieu ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@ChuyenMon", (object?)profile.ChuyenMon ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@ThanhTuu", (object?)profile.ThanhTuu ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@HinhAnh", (object?)profile.HinhAnh ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@KinhNghiem", (object?)profile.KinhNghiem ?? DBNull.Value);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
}
