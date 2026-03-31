using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class ThongTinCaNhanRepository : IThongTinCaNhanRepository
{
	private readonly string _connectionString;

	public ThongTinCaNhanRepository(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection")!;
	}

	#region Queries

	private const string BaseSelectList = @"
        SELECT ThongTinID, TaiKhoanID, HoTen, NgaySinh, GioiTinh, SDT, EmailLienHe
        FROM ThongTinCaNhan";

	private const string BaseSelectDetail = @"
        SELECT ThongTinID,TaiKhoanID,HoTen,NgaySinh,GioiTinh,SDT,
               EmailLienHe,DiaChi,Avatar,Loai,NgayTao,NgayCapNhat
        FROM ThongTinCaNhan";

	#endregion

	public async Task<ThongTinCaNhan?> GetByIdAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectDetail + " WHERE ThongTinID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}

	public async Task<ThongTinReadModel?> GetDetailAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectDetail + " WHERE ThongTinID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToReadModel(reader);

		return null;
	}

	public async Task<(List<ThongTinReadListModel>, int)> GetPagedAsync(int page, int size)
	{
		var list = new List<ThongTinReadListModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
			{BaseSelectList}
			WHERE Loai=N'Khách'
			ORDER BY HoTen
			OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

			SELECT COUNT(*)
			FROM ThongTinCaNhan
			WHERE Loai=N'Khách'";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@Size", SqlDbType.Int).Value = size;

		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToReadListModel(reader));

		await reader.NextResultAsync();

		if (await reader.ReadAsync())
			total = reader.GetInt32(0);

		return (list, total);
	}

	public async Task<ThongTinCaNhan?> GetByEmailOrSDTAsync(string? email, string? sdt)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectDetail + @"
            WHERE 
                (@Email IS NOT NULL AND EmailLienHe=@Email)
                OR
                (@SDT IS NOT NULL AND SDT=@SDT)";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Email", SqlDbType.NVarChar)
			.Value = (object?)email ?? DBNull.Value;

		cmd.Parameters.Add("@SDT", SqlDbType.NVarChar)
			.Value = (object?)sdt ?? DBNull.Value;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}

	public async Task<int> GetIdByTaiKhoanId(int taiKhoanId)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"SELECT ThongTinID FROM ThongTinCaNhan WHERE TaiKhoanID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = taiKhoanId;

		var result = await cmd.ExecuteScalarAsync();

		return result == null ? 0 : Convert.ToInt32(result);
	}

	public async Task<bool> ExistsByEmailAsync(string? email, string sdt)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"SELECT COUNT(1)
                    FROM ThongTinCaNhan
                    WHERE EmailLienHe=@Email OR SDT=@SDT";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
		cmd.Parameters.Add("@SDT", SqlDbType.NVarChar).Value = sdt;

		return Convert.ToInt32(await cmd.ExecuteScalarAsync()) > 0;
	}

	public async Task<int> AddAsync(ThongTinCaNhan tt)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"
        INSERT INTO ThongTinCaNhan
        (HoTen,NgaySinh,GioiTinh,SDT,EmailLienHe,DiaChi,Avatar,Loai,TaiKhoanID)
        OUTPUT INSERTED.ThongTinID
        VALUES
        (@HoTen,@NgaySinh,@GioiTinh,@SDT,@Email,@DiaChi,@Avatar,@Loai,@TaiKhoanID)";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar).Value = tt.HoTen;
		cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = tt.NgaySinh;
		cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar).Value = tt.GioiTinh.ToDbValue();
		cmd.Parameters.Add("@SDT", SqlDbType.NVarChar).Value = tt.SDT;
		cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = (object?)tt.EmailLienHe ?? DBNull.Value;
		cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = tt.DiaChi;
		cmd.Parameters.Add("@Avatar", SqlDbType.NVarChar).Value = (object?)tt.Avatar ?? DBNull.Value;
		cmd.Parameters.Add("@Loai", SqlDbType.NVarChar).Value = tt.Loai.ToDbValue();
		cmd.Parameters.Add("@TaiKhoanID", SqlDbType.Int).Value = (object?)tt.TaiKhoanID ?? DBNull.Value;

		var result = await cmd.ExecuteScalarAsync();

		return Convert.ToInt32(result);
	}

	public async Task<int> UpdateAsync(ThongTinCaNhan tt)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"
        UPDATE ThongTinCaNhan
        SET TaiKhoanID=@TaiKhoanID,
			HoTen=@HoTen,
            NgaySinh=@NgaySinh,
            GioiTinh=@GioiTinh,
            SDT=@SDT,
            EmailLienHe=@Email,
            DiaChi=@DiaChi,
            Avatar=@Avatar,
            Loai=@Loai,
            NgayCapNhat=GETDATE()
        WHERE ThongTinID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TaiKhoanID", SqlDbType.Int).Value = (object?)tt.TaiKhoanID ?? DBNull.Value;
		cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar).Value = tt.HoTen;
		cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = tt.NgaySinh;
		cmd.Parameters.Add("@GioiTinh", SqlDbType.NVarChar).Value = tt.GioiTinh.ToDbValue();
		cmd.Parameters.Add("@SDT", SqlDbType.NVarChar).Value = tt.SDT;
		cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = (object?)tt.EmailLienHe ?? DBNull.Value;
		cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = tt.DiaChi;
		cmd.Parameters.Add("@Avatar", SqlDbType.NVarChar).Value = (object?)tt.Avatar ?? DBNull.Value;
		cmd.Parameters.Add("@Loai", SqlDbType.NVarChar) .Value = tt.Loai.ToDbValue();
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = tt.ThongTinID;
		return await cmd.ExecuteNonQueryAsync();
	}


	#region Mapping

	private ThongTinCaNhan MapToEntity(SqlDataReader r)
	{
		return new ThongTinCaNhan(
			r.GetInt32(r.GetOrdinal("ThongTinID")),
			r.IsDBNull(r.GetOrdinal("TaiKhoanID")) ? null : r.GetInt32(r.GetOrdinal("TaiKhoanID")),
			r.GetString(r.GetOrdinal("HoTen")),
			r.GetDateTime(r.GetOrdinal("NgaySinh")),
			GioiTinhExtensions.FromDbValue(r.GetString(r.GetOrdinal("GioiTinh"))),
			r.GetString(r.GetOrdinal("SDT")),
			r.IsDBNull(r.GetOrdinal("EmailLienHe")) ? null : r.GetString(r.GetOrdinal("EmailLienHe")),
			r.GetString(r.GetOrdinal("DiaChi")),
			r.IsDBNull(r.GetOrdinal("Avatar")) ? null : r.GetString(r.GetOrdinal("Avatar")),
			LoaiThongTinExtensions.FromDbValue(r.GetString(r.GetOrdinal("Loai"))),
			r.GetDateTime(r.GetOrdinal("NgayTao")),
			r.IsDBNull(r.GetOrdinal("NgayCapNhat")) ? null : r.GetDateTime(r.GetOrdinal("NgayCapNhat"))
		);
	}

	private ThongTinReadListModel MapToReadListModel(SqlDataReader r)
	{
		return new ThongTinReadListModel
		{
			ThongTinID = r.GetInt32(r.GetOrdinal("ThongTinID")),
			TaiKhoanID = r.IsDBNull(r.GetOrdinal("TaiKhoanID")) ? null : r.GetInt32(r.GetOrdinal("TaiKhoanID")),
			HoTen = r.GetString(r.GetOrdinal("HoTen")),
			NgaySinh = r.GetDateTime(r.GetOrdinal("NgaySinh")),
			GioiTinh = r.GetString(r.GetOrdinal("GioiTinh")),
			SDT = r.GetString(r.GetOrdinal("SDT")),
			EmailLienHe = r.IsDBNull(r.GetOrdinal("EmailLienHe")) ? null : r.GetString(r.GetOrdinal("EmailLienHe"))
		};
	}

	private ThongTinReadModel MapToReadModel(SqlDataReader r)
	{
		return new ThongTinReadModel
		{
			ThongTinID = r.GetInt32(r.GetOrdinal("ThongTinID")),
			TaiKhoanID = r.IsDBNull(r.GetOrdinal("TaiKhoanID")) ? null : r.GetInt32(r.GetOrdinal("TaiKhoanID")),
			HoTen = r.GetString(r.GetOrdinal("HoTen")),
			NgaySinh = r.GetDateTime(r.GetOrdinal("NgaySinh")),
			GioiTinh = r.GetString(r.GetOrdinal("GioiTinh")),
			SDT = r.GetString(r.GetOrdinal("SDT")),
			EmailLienHe = r.IsDBNull(r.GetOrdinal("EmailLienHe")) ? null : r.GetString(r.GetOrdinal("EmailLienHe")),
			DiaChi = r.GetString(r.GetOrdinal("DiaChi")),
			Avatar = r.IsDBNull(r.GetOrdinal("Avatar")) ? null : r.GetString(r.GetOrdinal("Avatar")),
			Loai = r.GetString(r.GetOrdinal("Loai")),
			NgayTao = r.GetDateTime(r.GetOrdinal("NgayTao")),
			NgayCapNhat = r.IsDBNull(r.GetOrdinal("NgayCapNhat")) ? null : r.GetDateTime(r.GetOrdinal("NgayCapNhat"))
		};
	}
	#endregion
}