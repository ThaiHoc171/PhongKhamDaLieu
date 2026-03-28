using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace Infrastructure.Repositories;
public class TaiKhoanRepository : ITaiKhoanRepository
{
	private readonly string _connectionString;
	public TaiKhoanRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new InvalidOperationException("Connection string not found.");
	}
	private SqlConnection CreateConnection() => new(_connectionString);
	private const string BaseSelect =
	@"SELECT TaiKhoanID, Email, MatKhau, VaiTro, TrangThai, NgayTao";
	private const string BaseSelectLite =
	@"SELECT TaiKhoanID, Email, VaiTro, TrangThai";
	public async Task<TaiKhoan?> GetByEmailAsync(string email)
	{
		const string sql =
		@"SELECT TaiKhoanID, Email, MatKhau, VaiTro, TrangThai, NgayTao FROM TaiKhoan WHERE Email=@Email";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = email;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        const string sql = "SELECT COUNT(1) FROM TaiKhoan WHERE Email = @Email";
        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
        await conn.OpenAsync();
        return Convert.ToInt32(await cmd.ExecuteScalarAsync()) > 0;
    }
    public async Task<TaiKhoan?> GetByIdAsync(int id)
	{
		const string sql =
		@"SELECT TaiKhoanID, Email, MatKhau, VaiTro, TrangThai, NgayTao FROM TaiKhoan WHERE TaiKhoanID=@Id";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}
	public async Task<(List<TaiKhoanListReadModel>, int)>
		GetPagedAsync(int page, int size, string? vaiTro, string? trangThai)
	{
		var sql =$@"
			{BaseSelectLite}
			FROM TaiKhoan
			WHERE (@VaiTro IS NULL OR VaiTro=@VaiTro)
			AND (@TrangThai IS NULL OR TrangThai=@TrangThai)
			ORDER BY NgayTao DESC
			OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
			SELECT COUNT(*) FROM TaiKhoan
			WHERE (@VaiTro IS NULL OR VaiTro=@VaiTro)
			AND (@TrangThai IS NULL OR TrangThai=@TrangThai)
		";
		var list = new List<TaiKhoanListReadModel>();
		int total = 0;
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@VaiTro", SqlDbType.NVarChar, 20).Value = (object?)vaiTro ?? DBNull.Value;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = (object?)trangThai ?? DBNull.Value;
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = (page - 1) * size;
		cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = size;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
			list.Add(MapToListDTO(reader));
		if (await reader.NextResultAsync() && await reader.ReadAsync())
			total = reader.GetInt32(0);
		return (list, total);
	}
	public async Task<TaiKhoanReadModel?> GetDetailAsync(int id)
	{
		var sql =
		$@"{BaseSelect}
		FROM TaiKhoan
		WHERE TaiKhoanID=@Id";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToDetailDTO(reader) : null;
	}
	public async Task<int> AddAsync(TaiKhoan taiKhoan)
	{
		const string sql =
		@"INSERT INTO TaiKhoan (Email, MatKhau, VaiTro, TrangThai)
		  OUTPUT INSERTED.TaiKhoanID
		  VALUES (@Email,@MatKhau,@VaiTro,@TrangThai)";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = taiKhoan.Email;
		cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar).Value = taiKhoan.MatKhau;
		cmd.Parameters.Add("@VaiTro", SqlDbType.NVarChar).Value = taiKhoan.VaiTro;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar).Value = taiKhoan.TrangThai;
		await conn.OpenAsync();
		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}
	public async Task UpdateAsync(TaiKhoan taiKhoan)
	{
		const string sql =
		@"UPDATE TaiKhoan SET MatKhau=@MatKhau, TrangThai=@TrangThai, NgayCapNhat=GETDATE() WHERE TaiKhoanID=@Id";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 255).Value = taiKhoan.MatKhau;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = taiKhoan.TrangThai;
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = taiKhoan.TaiKhoanID;
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
    public async Task UpdateFcmTokenAsync(int taiKhoanId, string? fcmToken)
    {
        const string sql =
        @"UPDATE TaiKhoan SET FCMToken=@FCMToken, NgayCapNhat=GETDATE() WHERE TaiKhoanID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@FCMToken", SqlDbType.NVarChar, 500).Value =
            (object?)fcmToken ?? DBNull.Value;
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = taiKhoanId;
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
    private static TaiKhoan MapToEntity(SqlDataReader r)
	{
		var id = r.GetOrdinal("TaiKhoanID");
		var email = r.GetOrdinal("Email");
		var matKhau = r.GetOrdinal("MatKhau");
		var vaiTro = r.GetOrdinal("VaiTro");
		var trangThai = r.GetOrdinal("TrangThai");
		var ngayTao = r.GetOrdinal("NgayTao");
		return new TaiKhoan(
			r.GetInt32(id),
			r.GetString(email),
			r.GetString(matKhau),
			r.GetString(vaiTro),
			r.GetString(trangThai),
			r.GetDateTime(ngayTao)
		);
	}
	private static TaiKhoanListReadModel MapToListDTO(SqlDataReader r)
	{
		var id = r.GetOrdinal("TaiKhoanID");
		var email = r.GetOrdinal("Email");
		var vaiTro = r.GetOrdinal("VaiTro");
		var trangThai = r.GetOrdinal("TrangThai");
		return new TaiKhoanListReadModel
		{
			Id = r.GetInt32(id),
			Email = r.GetString(email),
			VaiTro = r.GetString(vaiTro),
			TrangThai = r.GetString(trangThai)
		};
	}
	private static TaiKhoanReadModel MapToDetailDTO(SqlDataReader r)
	{
		var id = r.GetOrdinal("TaiKhoanID");
		var email = r.GetOrdinal("Email");
		var vaiTro = r.GetOrdinal("VaiTro");
		var trangThai = r.GetOrdinal("TrangThai");
		var ngayTao = r.GetOrdinal("NgayTao");
		return new TaiKhoanReadModel
		{
			TaiKhoanID = r.GetInt32(id),
			Email = r.GetString(email),
			VaiTro = r.GetString(vaiTro),
			TrangThai = r.GetString(trangThai),
			NgayTao = r.GetDateTime(ngayTao)
		};
	}
}