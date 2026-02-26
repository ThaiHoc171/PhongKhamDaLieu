using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class NgayNghiNhanVienRepository : INgayNghiNhanVienRepository
{
	private readonly string _connectionString;

	public NgayNghiNhanVienRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}

	public async Task AddAsync(NgayNghiNhanVien entity)
	{
		const string sql = @"
			INSERT INTO NgayNghiNhanVien (NhanVienID, Ngay, LyDo)
			VALUES (@NhanVienID, @Ngay, @LyDo)
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@NhanVienID", entity.NhanVienID);
		cmd.Parameters.AddWithValue("@Ngay", entity.Ngay);
		cmd.Parameters.AddWithValue("@LyDo", (object?)entity.LyDo ?? DBNull.Value);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task<NgayNghiNhanVien?> GetByIdAsync(int id)
	{
		const string sql = @"
			SELECT NgayNghiID, NhanVienID, Ngay, LyDo
			FROM NgayNghiNhanVien
			WHERE NgayNghiID = @Id
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Id", id);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		return await reader.ReadAsync()
			? Map(reader)
			: null;
	}

	public async Task<List<NgayNghiNhanVien>> GetByNhanVienIdAsync(int nhanVienID)
	{
		const string sql = @"
			SELECT NgayNghiID, NhanVienID, Ngay, LyDo
			FROM NgayNghiNhanVien
			WHERE NhanVienID = @NhanVienID
			ORDER BY Ngay
		";

		var list = new List<NgayNghiNhanVien>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@NhanVienID", nhanVienID);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(Map(reader));

		return list;
	}
	public async Task<List<NgayNghiNhanVien>> GetByMonthAsync(int thang, int nam)
	{
		// Tính ngày đầu tháng và đầu tháng sau
		var tuNgay = new DateTime(nam, thang, 1);
		var denNgay = tuNgay.AddMonths(1);

		const string sql = @"
			SELECT NgayNghiID, NhanVienID, Ngay, LyDo
			FROM NgayNghiNhanVien
			WHERE Ngay >= @TuNgay AND Ngay < @DenNgay
			ORDER BY Ngay
		";

		var list = new List<NgayNghiNhanVien>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@TuNgay", SqlDbType.DateTime).Value = tuNgay;
		cmd.Parameters.Add("@DenNgay", SqlDbType.DateTime).Value = denNgay;

		await conn.OpenAsync();

		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(Map(reader));
		}

		return list;
	}

	public async Task<bool> IsNgayNghiAsync(int nhanVienID, DateTime ngay)
	{
		const string sql = @"
			SELECT 1
			FROM NgayNghiNhanVien
			WHERE NhanVienID = @NhanVienID
			  AND Ngay = @Ngay
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@NhanVienID", nhanVienID);
		cmd.Parameters.AddWithValue("@Ngay", ngay.Date);

		await conn.OpenAsync();
		return await cmd.ExecuteScalarAsync() != null;
	}
	public async Task UpdateAsync(NgayNghiNhanVien entity)
	{
		const string sql = @"
		UPDATE NgayNghiNhanVien
		SET LyDo = @LyDo
		WHERE NgayNghiID = @NgayNghiID
	";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@NgayNghiID", entity.NgayNghiID);
		cmd.Parameters.AddWithValue("@LyDo", (object?)entity.LyDo ?? DBNull.Value);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	private static NgayNghiNhanVien Map(SqlDataReader reader)
	{
		return new NgayNghiNhanVien(
			reader.GetInt32(0),
			reader.GetInt32(1),
			reader.GetDateTime(2),
			reader.IsDBNull(3) ? null : reader.GetString(3)
		);
	}
}
