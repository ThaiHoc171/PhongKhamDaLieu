using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class TaiKhoanRepository : ITaiKhoanRepository
{
	private readonly string _connectionString;

	public TaiKhoanRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}

	public async Task<TaiKhoan?> GetByEmailAsync(string email)
	{
		const string sql = @"SELECT TaiKhoanID, Email, MatKhau, VaiTro, TrangThai, NgayTao FROM TaiKhoan WHERE Email = @Email AND TrangThai = N'Hoạt động'";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Email", email);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}

	public async Task<TaiKhoan?> GetByIdAsync(int id)
	{
		const string sql = @"SELECT TaiKhoanID, Email, MatKhau, VaiTro, TrangThai, NgayTao FROM TaiKhoan WHERE TaiKhoanID = @Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Id", id);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}

	public async Task<List<TaiKhoan>> GetAllAsync()
	{
		const string sql = @"SELECT TaiKhoanID, Email, MatKhau, VaiTro, TrangThai, NgayTao FROM TaiKhoan ";

		var list = new List<TaiKhoan>();

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

	public async Task AddAsync(TaiKhoan taiKhoan)
	{
		const string sql = @"INSERT INTO TaiKhoan (Email, MatKhau, VaiTro, TrangThai, NgayTao) 
							VALUES (@Email, @MatKhau, @VaiTro, @TrangThai, @NgayTao)";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@Email", taiKhoan.Email);
		cmd.Parameters.AddWithValue("@MatKhau", taiKhoan.MatKhau);
		cmd.Parameters.AddWithValue("@VaiTro", taiKhoan.VaiTro);
		cmd.Parameters.AddWithValue("@TrangThai", taiKhoan.TrangThai);
		cmd.Parameters.AddWithValue("@NgayTao", taiKhoan.NgayTao);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task UpdateAsync(TaiKhoan taiKhoan)
	{
		const string sql = @"UPDATE TaiKhoan SET MatKhau = @MatKhau, TrangThai = @TrangThai, NgayCapNhat = GETDATE() WHERE TaiKhoanID = @Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@MatKhau", taiKhoan.MatKhau);
		cmd.Parameters.AddWithValue("@TrangThai", taiKhoan.TrangThai);
		cmd.Parameters.AddWithValue("@Id", taiKhoan.Id);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	private static TaiKhoan MapToEntity(SqlDataReader reader)
	{
		return new TaiKhoan(
			id: reader.GetInt32(0),
			email: reader.GetString(1),
			matKhau: reader.GetString(2),
			vaiTro: reader.GetString(3),
			trangThai: reader.GetString(4),
			ngayTao: reader.GetDateTime(5)
		);
	}
}
