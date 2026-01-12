using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class ThongTinCaNhanRepository : IThongTinCaNhanRepository
{
	private readonly string _connectionString;

	public ThongTinCaNhanRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}

	public async Task<ThongTinCaNhan?> GetByIdAsync(int thongTinId)
	{
		const string sql = @"
			SELECT ThongTinID, TaiKhoanID, HoTen, NgaySinh, GioiTinh,
			       SDT, EmailLienHe, DiaChi, Avatar, Loai,
			       NgayTao, NgayCapNhat
			FROM ThongTinCaNhan
			WHERE ThongTinID = @Id
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Id", thongTinId);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		return await reader.ReadAsync() ? MapToEntityFull(reader) : null;
	}

	public async Task<List<ThongTinCaNhan>> GetAllByLoaiAsync(LoaiThongTinEnum loai)
	{
		const string sql = @"
			SELECT ThongTinID, TaiKhoanID, HoTen, SDT,
			       EmailLienHe, Loai, NgayTao, NgayCapNhat
			FROM ThongTinCaNhan
			WHERE Loai = @Loai
		";

		var list = new List<ThongTinCaNhan>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Loai", loai.ToDbValue());

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add(MapToEntityLite(reader));
		}

		return list;
	}

	public async Task<int> AddAsync(ThongTinCaNhan tt)
	{
		const string sql = @"
			INSERT INTO ThongTinCaNhan
			(HoTen, NgaySinh, GioiTinh, SDT, EmailLienHe,
			 DiaChi, Avatar, Loai, TaiKhoanID, NgayTao, NgayCapNhat)
			OUTPUT INSERTED.ThongTinID
			VALUES
			(@HoTen, @NgaySinh, @GioiTinh, @SDT, @Email,
			 @DiaChi, @Avatar, @Loai, @TaiKhoanID, @NgayTao, @NgayCapNhat)
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@HoTen", tt.HoTen);
		cmd.Parameters.AddWithValue("@NgaySinh", (object?)tt.NgaySinh ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@GioiTinh", (object?)tt.GioiTinh ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@SDT", tt.SDT);
		cmd.Parameters.AddWithValue("@Email", tt.EmailLienHe);
		cmd.Parameters.AddWithValue("@DiaChi", (object?)tt.DiaChi ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@Avatar", (object?)tt.Avatar ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@Loai", tt.Loai);
		cmd.Parameters.AddWithValue("@TaiKhoanID", (object?)tt.TaiKhoanID ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@NgayTao", tt.NgayTao);
		cmd.Parameters.AddWithValue("@NgayCapNhat", tt.NgayCapNhat);

		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();

		if (result is null || result == DBNull.Value)
			throw new InvalidOperationException("Không lấy được ID sau khi insert.");

		return Convert.ToInt32(result);
	}

	public async Task UpdateAsync(ThongTinCaNhan tt)
	{
		const string sql = @"
			UPDATE ThongTinCaNhan
			SET HoTen = @HoTen,
			    NgaySinh = @NgaySinh,
			    GioiTinh = @GioiTinh,
			    SDT = @SDT,
			    EmailLienHe = @Email,
			    DiaChi = @DiaChi,
			    Avatar = @Avatar,
			    NgayCapNhat = GETDATE()
			WHERE ThongTinID = @Id
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@HoTen", tt.HoTen);
		cmd.Parameters.AddWithValue("@NgaySinh", (object?)tt.NgaySinh ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@GioiTinh", (object?)tt.GioiTinh ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@SDT", tt.SDT);
		cmd.Parameters.AddWithValue("@Email", tt.EmailLienHe);
		cmd.Parameters.AddWithValue("@DiaChi", (object?)tt.DiaChi ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@Avatar", (object?)tt.Avatar ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@Id", tt.ThongTinID);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}


	private static ThongTinCaNhan MapToEntityFull(SqlDataReader reader)
	{
		return new ThongTinCaNhan(
			thongTinID: reader.GetInt32(0),
			taiKhoanID: reader.IsDBNull(1) ? null : reader.GetInt32(1),
			hoTen: reader.GetString(2),
			ngaySinh: reader.IsDBNull(3) ? null : reader.GetDateTime(3),
			gioiTinh: reader.IsDBNull(4) ? null : reader.GetString(4),
			sdt: reader.GetString(5),
			emailLienHe: reader.GetString(6),
			diaChi: reader.IsDBNull(7) ? null : reader.GetString(7),
			avatar: reader.IsDBNull(8) ? null : reader.GetString(8),
			loai: reader.GetString(9),
			ngayTao: reader.GetDateTime(10),
			ngayCapNhat: reader.GetDateTime(11)
		);
	}

	private static ThongTinCaNhan MapToEntityLite(SqlDataReader reader)
	{
		return new ThongTinCaNhan(
			thongTinID: reader.GetInt32(0),
			taiKhoanID: reader.IsDBNull(1) ? null : reader.GetInt32(1),
			hoTen: reader.GetString(2),
			ngaySinh: null,
			gioiTinh: null,
			sdt: reader.GetString(3),
			emailLienHe: reader.GetString(4),
			diaChi: null,
			avatar: null,
			loai: reader.GetString(5),
			ngayTao: reader.GetDateTime(6),
			ngayCapNhat: reader.GetDateTime(7)
		);
	}
}
