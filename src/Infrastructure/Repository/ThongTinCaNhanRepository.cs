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
	public ThongTinCaNhanRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}
	public async Task<ThongTinCaNhan?> GetByIdAsync(int id)
	{
		const string sql = @"
			SELECT ThongTinID,TaiKhoanID,HoTen,NgaySinh,GioiTinh,SDT,
				EmailLienHe,DiaChi,Avatar,Loai,NgayTao,NgayCapNhat
			FROM ThongTinCaNhan
			 WHERE ThongTinID=@Id";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}
    public async Task<int> GetIdByTaiKhoanId(int taiKhoanId)
    {
        const string sql =
        @"SELECT ThongTinID FROM ThongTinCaNhan WHERE TaiKhoanID=@Id";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = taiKhoanId });
        await conn.OpenAsync();
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    public async Task<bool> ExistsByEmailAsync(string email, string sdt)
    {
        const string sql = "SELECT COUNT(1) FROM ThongTinCaNhan WHERE EmailLienHe = @Email OR SDT = @SDT";
        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
        cmd.Parameters.Add("@SDT", SqlDbType.NVarChar).Value = sdt;
        await conn.OpenAsync();
        return Convert.ToInt32(await cmd.ExecuteScalarAsync()) > 0;
    }
    public async Task<ThongTinFullReadModel?> GetDetailAsync(int id)
	{
		const string sql = @"
			SELECT ThongTinID,TaiKhoanID,HoTen,NgaySinh,GioiTinh,SDT,
				EmailLienHe,DiaChi,Avatar,Loai,NgayTao,NgayCapNhat
			FROM ThongTinCaNhan
			WHERE ThongTinID=@Id";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (!await reader.ReadAsync())
			return null;
		return new ThongTinFullReadModel
		{
			ThongTinID = reader.GetInt32(0),
			TaiKhoanID = reader.IsDBNull(1) ? null : reader.GetInt32(1),
			HoTen = reader.GetString(2),
			NgaySinh = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
			GioiTinh = reader.IsDBNull(4) ? null : reader.GetString(4),
			SDT = reader.GetString(5),
			EmailLienHe = reader.GetString(6),
			DiaChi = reader.IsDBNull(7) ? null : reader.GetString(7),
			Avatar = reader.IsDBNull(8) ? null : reader.GetString(8),
			Loai = reader.GetString(9),
			NgayTao = reader.GetDateTime(10),
			NgayCapNhat = reader.IsDBNull(11) ? null : reader.GetDateTime(11)
		};
	}
	public async Task<List<ThongTinLiteReadModel>> GetAllByLoaiAsync(LoaiThongTinEnum loai)
	{
		const string sql = @"
			SELECT ThongTinID,TaiKhoanID,HoTen,SDT,EmailLienHe,Loai,NgayTao,NgayCapNhat
			FROM ThongTinCaNhan
			WHERE Loai=@Loai";
		var list = new List<ThongTinLiteReadModel>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add(new SqlParameter("@Loai", SqlDbType.NVarChar, 50) { Value = loai.ToDbValue() });
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(new ThongTinLiteReadModel
			{
				ThongTinID = reader.GetInt32(0),
				TaiKhoanID = reader.IsDBNull(1) ? null : reader.GetInt32(1),
				HoTen = reader.GetString(2),
				SDT = reader.GetString(3),
				EmailLienHe = reader.GetString(4),
				Loai = reader.GetString(5),
				NgayTao = reader.GetDateTime(6),
				NgayCapNhat = reader.IsDBNull(7) ? null : reader.GetDateTime(7)
			});
		}
		return list;
	}
	public async Task<int> AddAsync(ThongTinCaNhan tt)
	{
		const string sql = @"
			INSERT INTO ThongTinCaNhan(HoTen,NgaySinh,GioiTinh,SDT,EmailLienHe,DiaChi,Avatar,Loai,TaiKhoanID)
			OUTPUT INSERTED.ThongTinID
			VALUES(@HoTen,@NgaySinh,@GioiTinh,@SDT,@Email,@DiaChi,@Avatar,@Loai,@TaiKhoanID)";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add(new SqlParameter("@HoTen", SqlDbType.NVarChar) { Value = tt.HoTen });
		cmd.Parameters.Add(new SqlParameter("@NgaySinh", SqlDbType.DateTime) { Value = (object?)tt.NgaySinh ?? DBNull.Value });
		cmd.Parameters.Add(new SqlParameter("@GioiTinh", SqlDbType.NVarChar) { Value = (object?)tt.GioiTinh ?? DBNull.Value });
		cmd.Parameters.Add(new SqlParameter("@SDT", SqlDbType.NVarChar) { Value = tt.SDT });
		cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar) { Value = tt.EmailLienHe });
		cmd.Parameters.Add(new SqlParameter("@DiaChi", SqlDbType.NVarChar) { Value = (object?)tt.DiaChi ?? DBNull.Value });
		cmd.Parameters.Add(new SqlParameter("@Avatar", SqlDbType.NVarChar) { Value = (object?)tt.Avatar ?? DBNull.Value });
		cmd.Parameters.Add(new SqlParameter("@Loai", SqlDbType.NVarChar) { Value = tt.Loai });
		cmd.Parameters.Add(new SqlParameter("@TaiKhoanID", SqlDbType.Int) { Value = (object?)tt.TaiKhoanID ?? DBNull.Value });
		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		if (result == null || result == DBNull.Value)
			throw new InvalidOperationException("Không lấy được ID sau khi insert");
		return Convert.ToInt32(result);
	}
	public async Task UpdateAsync(ThongTinCaNhan tt)
	{
		const string sql = @"
			UPDATE ThongTinCaNhan
			SET HoTen=@HoTen,NgaySinh=@NgaySinh,GioiTinh=@GioiTinh,SDT=@SDT,
				EmailLienHe=@Email,DiaChi=@DiaChi,Avatar=@Avatar,NgayCapNhat=GETDATE()
			WHERE ThongTinID=@Id";	 
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add(new SqlParameter("@HoTen", SqlDbType.NVarChar) { Value = tt.HoTen });
		cmd.Parameters.Add(new SqlParameter("@NgaySinh", SqlDbType.DateTime) { Value = (object?)tt.NgaySinh ?? DBNull.Value });
		cmd.Parameters.Add(new SqlParameter("@GioiTinh", SqlDbType.NVarChar) { Value = (object?)tt.GioiTinh ?? DBNull.Value });
		cmd.Parameters.Add(new SqlParameter("@SDT", SqlDbType.NVarChar) { Value = tt.SDT });
		cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar) { Value = tt.EmailLienHe });
		cmd.Parameters.Add(new SqlParameter("@DiaChi", SqlDbType.NVarChar) { Value = (object?)tt.DiaChi ?? DBNull.Value });
		cmd.Parameters.Add(new SqlParameter("@Avatar", SqlDbType.NVarChar) { Value = (object?)tt.Avatar ?? DBNull.Value });
		cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = tt.ThongTinID });
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task<List<NameResponseDTO>> GetComboboxAsync()
	{
		const string sql = @"
			SELECT ThongTinID,HoTen
			FROM ThongTinCaNhan
			WHERE Loai=N'Bệnh nhân'
			ORDER BY HoTen";
		var list = new List<NameResponseDTO>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(new NameResponseDTO
			{
				Id = reader.GetInt32(0),
				Name = reader.GetString(1)
			});
		}
		return list;
	}
	private static ThongTinCaNhan MapToEntity(SqlDataReader reader)
	{
		return new ThongTinCaNhan(
			reader.GetInt32(0),
			reader.IsDBNull(1) ? null : reader.GetInt32(1),
			reader.GetString(2),
			reader.IsDBNull(3) ? null : reader.GetDateTime(3),
			reader.IsDBNull(4) ? null : reader.GetString(4),
			reader.GetString(5),
			reader.GetString(6),
			reader.IsDBNull(7) ? null : reader.GetString(7),
			reader.IsDBNull(8) ? null : reader.GetString(8),
			reader.GetString(9),
			reader.GetDateTime(10),
			reader.IsDBNull(11) ? null : reader.GetDateTime(11)
		);
	}
}