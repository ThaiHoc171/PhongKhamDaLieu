using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class TaiKhoanRepository : ITaiKhoanRepository
{
	private readonly string _connectionString;

	public TaiKhoanRepository(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection")!;
	}

	#region Queries

	private const string BaseSelectList = @"
		SELECT TaiKhoanID,Email,VaiTro,TrangThai
		FROM TaiKhoan";

	private const string BaseSelectDetail = @"
		SELECT TaiKhoanID,Email,MatKhau,VaiTro,TrangThai,NgayTao,NgayCapNhat,FCMToken
		FROM TaiKhoan";

	#endregion

	public async Task<TaiKhoan?> GetByEmailAsync(string email)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectDetail + " WHERE Email=@Email";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = email;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}
    public async Task<TaiKhoan?> GetBySDTAsync(string sdt)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        var sql = @"SELECT TaiKhoanID,Email,MatKhau,VaiTro,TrangThai,NgayTao,NgayCapNhat,FCMToken
					FROM TaiKhoan tk, ThongTinCaNhan tt
					WHERE tt.TaiKhoanID = tk.TaiKhoanID AND tt.SDT = @SDT";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@SDT", SqlDbType.NVarChar, 100).Value = sdt;

        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
            return MapToEntity(reader);

        return null;
    }
    public async Task<TaiKhoan?> GetByIdAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectDetail + " WHERE TaiKhoanID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}

	public async Task<bool> ExistsByEmailAsync(string email)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = "SELECT COUNT(1) FROM TaiKhoan WHERE Email=@Email";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = email;

		return Convert.ToInt32(await cmd.ExecuteScalarAsync()) > 0;
	}
    public async Task<int> GetIdByEmailAsync(string email)
    {
        const string sql = @"
			SELECT TaiKhoanID 
			FROM TaiKhoan 
			WHERE Email=@Email
		";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = email;
        await conn.OpenAsync();
        var result = await cmd.ExecuteScalarAsync();
        return result == null ? 0 : Convert.ToInt32(result);
    }
    public async Task<(List<TaiKhoanListReadModel>, int)> GetPagedAsync(
		int page, int size, string? vaiTro, string? trangThai)
	{
		var list = new List<TaiKhoanListReadModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
		{BaseSelectList}
		WHERE (@VaiTro IS NULL OR VaiTro=@VaiTro)
		AND (@TrangThai IS NULL OR TrangThai=@TrangThai)
		ORDER BY TaiKhoanID ASC
		OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

		SELECT COUNT(*)
		FROM TaiKhoan
		WHERE (@VaiTro IS NULL OR VaiTro=@VaiTro)
		AND (@TrangThai IS NULL OR TrangThai=@TrangThai)";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@VaiTro", SqlDbType.NVarChar, 20).Value =
			(object?)vaiTro ?? DBNull.Value;

		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value =
			(object?)trangThai ?? DBNull.Value;

		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@Size", SqlDbType.Int).Value = size;

		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToListDTO(reader));

		await reader.NextResultAsync();

		if (await reader.ReadAsync())
			total = reader.GetInt32(0);

		return (list, total);
	}
	public async Task<(List<TaiKhoanListReadModel>, int)> 
		SearchPagedAsync(int page, int size, string? keyword, string? vaiTro, string? trangThai)
	{
		var list = new List<TaiKhoanListReadModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
		{BaseSelectList}
		WHERE (@Keyword IS NULL OR Email LIKE '%' + @Keyword + '%')
		AND (@VaiTro IS NULL OR VaiTro = @VaiTro)
		AND (@TrangThai IS NULL OR TrangThai = @TrangThai)
		ORDER BY TaiKhoanID DESC
		OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

		SELECT COUNT(*)
		FROM TaiKhoan
		WHERE (@Keyword IS NULL OR Email LIKE '%' + @Keyword + '%')
		AND (@VaiTro IS NULL OR VaiTro = @VaiTro)
		AND (@TrangThai IS NULL OR TrangThai = @TrangThai)";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 100).Value =
			(object?)keyword ?? DBNull.Value;

		cmd.Parameters.Add("@VaiTro", SqlDbType.NVarChar, 20).Value =
			(object?)vaiTro ?? DBNull.Value;

		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value =
			(object?)trangThai ?? DBNull.Value;

		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@Size", SqlDbType.Int).Value = size;

		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToListDTO(reader));

		await reader.NextResultAsync();

		if (await reader.ReadAsync())
			total = reader.GetInt32(0);

		return (list, total);
	}
	public async Task<TaiKhoanReadModel?> GetDetailAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectDetail + " WHERE TaiKhoanID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToDetailDTO(reader);

		return null;
	}

	public async Task<int> AddAsync(TaiKhoan taiKhoan)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"INSERT INTO TaiKhoan(Email,MatKhau,VaiTro,TrangThai)
					OUTPUT INSERTED.TaiKhoanID
					VALUES(@Email,@MatKhau,@VaiTro,@TrangThai)";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = taiKhoan.Email;
		cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 255).Value = taiKhoan.MatKhau;
		cmd.Parameters.Add("@VaiTro", SqlDbType.NVarChar, 20) .Value = taiKhoan.VaiTro.ToDbValue();
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = taiKhoan.TrangThai;

		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}

	public async Task<int> UpdateAsync(TaiKhoan taiKhoan)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"UPDATE TaiKhoan
					SET MatKhau=@MatKhau,
						TrangThai=@TrangThai,
						NgayCapNhat=@NgayCapNhat
					WHERE TaiKhoanID=@Id";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = taiKhoan.TaiKhoanID;
		cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 255).Value = taiKhoan.MatKhau;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = taiKhoan.TrangThai;
		cmd.Parameters.Add("@NgayCapNhat", SqlDbType.DateTime).Value =
			(object?)taiKhoan.NgayCapNhat ?? DBNull.Value;

		return await cmd.ExecuteNonQueryAsync();
	}

	public async Task UpdateFcmTokenAsync(int taiKhoanId, string? token)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"UPDATE TaiKhoan
					SET FCMToken=@Token,
						NgayCapNhat=GETDATE()
					WHERE TaiKhoanID=@Id";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Token", SqlDbType.NVarChar, 500).Value =
			(object?)token ?? DBNull.Value;

		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = taiKhoanId;

		await cmd.ExecuteNonQueryAsync();
	}

	#region Mapping

	private TaiKhoan MapToEntity(SqlDataReader r)
	{
		return new TaiKhoan(
			r.GetInt32(r.GetOrdinal("TaiKhoanID")),
			r.GetString(r.GetOrdinal("Email")),
			r.GetString(r.GetOrdinal("MatKhau")),
			VaiTroExtensions.ToEnum(r.GetString(r.GetOrdinal("VaiTro"))),
			r.GetString(r.GetOrdinal("TrangThai")),
			r.GetDateTime(r.GetOrdinal("NgayTao")),
			r.IsDBNull(r.GetOrdinal("NgayCapNhat"))
				? null
				: r.GetDateTime(r.GetOrdinal("NgayCapNhat")),
			r.IsDBNull(r.GetOrdinal("FCMToken"))
				? null
				: r.GetString(r.GetOrdinal("FCMToken"))
		);
	}

	private TaiKhoanListReadModel MapToListDTO(SqlDataReader r)
	{
		return new TaiKhoanListReadModel
		{
			Id = r.GetInt32(r.GetOrdinal("TaiKhoanID")),
			Email = r.GetString(r.GetOrdinal("Email")),
			VaiTro = r.GetString(r.GetOrdinal("VaiTro")),
			TrangThai = r.GetString(r.GetOrdinal("TrangThai"))
		};
	}

	private TaiKhoanReadModel MapToDetailDTO(SqlDataReader r)
	{
		return new TaiKhoanReadModel
		{
			TaiKhoanID = r.GetInt32(r.GetOrdinal("TaiKhoanID")),
			Email = r.GetString(r.GetOrdinal("Email")),
			VaiTro = r.GetString(r.GetOrdinal("VaiTro")),
			TrangThai = r.GetString(r.GetOrdinal("TrangThai")),
			NgayTao = r.GetDateTime(r.GetOrdinal("NgayTao")),
			NgayCapNhat = r.IsDBNull(r.GetOrdinal("NgayCapNhat"))? null: r.GetDateTime(r.GetOrdinal("NgayCapNhat"))
		};
	}

	#endregion
}