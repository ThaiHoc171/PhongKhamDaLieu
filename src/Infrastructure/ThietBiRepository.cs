using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class ThietBiRepository : IThietBiRepository
{
	private readonly string _connectionString;

	public ThietBiRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException();
	}

	public async Task<List<ThietBi>> GetAllAsync()
	{
		const string sql = @"
			SELECT 
				ThietBiID,
				TenTB,
				LoaiTB,
				TinhTrang,
				NgayNhap
			FROM ThietBi";

		var list = new List<ThietBi>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToEntity(reader));

		return list;
	}

	public async Task<ThietBi?> GetByIdAsync(int id)
	{
		const string sql = @"
			SELECT 
				ThietBiID,
				TenTB,
				LoaiTB,
				TinhTrang,
				NgayNhap
			FROM ThietBi
			WHERE ThietBiID = @Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Id", id);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}

	public async Task<List<ThietBi>> SearchByTenAsync(string tenTB)
	{
		const string sql = @"
			SELECT 
				ThietBiID,
				TenTB,
				LoaiTB,
				TinhTrang,
				NgayNhap
			FROM ThietBi
			WHERE TenTB LIKE @TenTB";

		var list = new List<ThietBi>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@TenTB", $"%{tenTB}%");

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToEntity(reader));

		return list;
	}

	public async Task AddAsync(ThietBi tb)
	{
		const string sql = @"
			INSERT INTO ThietBi (TenTB, LoaiTB, TinhTrang, NgayNhap)
			VALUES (@TenTB, @LoaiTB, @TinhTrang, @NgayNhap)";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@TenTB", tb.TenTB);
		cmd.Parameters.AddWithValue("@LoaiTB", tb.LoaiTB ?? (object)DBNull.Value);
		cmd.Parameters.AddWithValue("@TinhTrang", tb.TinhTrang);
		cmd.Parameters.AddWithValue("@NgayNhap", tb.NgayNhap);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task UpdateAsync(ThietBi tb)
	{
		const string sql = @"
			UPDATE ThietBi
			SET TenTB = @TenTB,
			    LoaiTB = @LoaiTB,
			    TinhTrang = @TinhTrang
			WHERE ThietBiID = @Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@Id", tb.Id);
		cmd.Parameters.AddWithValue("@TenTB", tb.TenTB);
		cmd.Parameters.AddWithValue("@LoaiTB", tb.LoaiTB ?? (object)DBNull.Value);
		cmd.Parameters.AddWithValue("@TinhTrang", tb.TinhTrang);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	private static ThietBi MapToEntity(SqlDataReader r)
		=> new(
			id: r.GetInt32(r.GetOrdinal("ThietBiID")),
			tenTB: r.GetString(r.GetOrdinal("TenTB")),
			loaiTB: r["LoaiTB"] as string,
			tinhTrang: r.GetString(r.GetOrdinal("TinhTrang")),
			ngayNhap: r.GetDateTime(r.GetOrdinal("NgayNhap"))
		);
}
