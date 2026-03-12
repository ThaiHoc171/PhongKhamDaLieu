using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace Infrastructure.Repositories;
public class NhanVienRepository : INhanVienRepository
{
	private readonly string _connectionString;
	public NhanVienRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}
	private const string SELECT_LIST = @"
		SELECT nv.NhanVienID,tt.HoTen,tt.EmailLienHe,cv.TenChucVu,nv.TrangThai
		FROM NhanVien nv
		JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
		JOIN ChucVu cv ON nv.ChucVuID = cv.ChucVuID
	";
	private const string SELECT_DETAIL = @"
		SELECT nv.NhanVienID, nv.ThongTinID, nv.NgayVaoLam, nv.BangCap,
			nv.KinhNghiem, nv.TrangThai, nv.NgayTao, nv.NgayCapNhat, 
			
			tt.HoTen, tt.NgaySinh, tt.GioiTinh,	tt.SDT, tt.EmailLienHe,
			tt.DiaChi, tt.Avatar,

			cv.ChucVuID, cv.TenChucVu,

			pcn.PhongChucNangID, pcn.TenPhong
		FROM NhanVien nv
		JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
		JOIN ChucVu cv ON nv.ChucVuID = cv.ChucVuID
		JOIN PhongChucNang pcn ON nv.PhongChucNangID = pcn.PhongChucNangID
	";
	public async Task AddAsync(NhanVien nv)
	{
		const string sql = @"
			INSERT INTO NhanVien (ThongTinID,ChucVuID,PhongChucNangID,NgayVaoLam,BangCap,KinhNghiem)
			VALUES (@ThongTinID,@ChucVuID,@PhongChucNangID,@NgayVaoLam,@BangCap,@KinhNghiem)
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add(new SqlParameter("@ThongTinID", SqlDbType.Int) { Value = nv.ThongTinID });
		cmd.Parameters.Add(new SqlParameter("@ChucVuID", SqlDbType.Int) { Value = nv.ChucVuID });
		cmd.Parameters.Add(new SqlParameter("@PhongChucNangID", SqlDbType.Int) { Value = nv.PhongChucNangID });
		cmd.Parameters.Add(new SqlParameter("@NgayVaoLam", SqlDbType.DateTime) { Value = (object?)nv.NgayVaoLam ?? DBNull.Value });
		cmd.Parameters.Add(new SqlParameter("@BangCap", SqlDbType.NVarChar) { Value = nv.BangCap });
		cmd.Parameters.Add(new SqlParameter("@KinhNghiem", SqlDbType.NVarChar) { Value = nv.KinhNghiem });
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task UpdateAsync(NhanVien nv)
	{
		const string sql = @"
			UPDATE NhanVien
			SET ChucVuID=@ChucVuID,
				PhongChucNangID=@PhongChucNangID,
				NgayVaoLam=@NgayVaoLam,
				BangCap=@BangCap,
				KinhNghiem=@KinhNghiem,
				TrangThai=@TrangThai,
				NgayCapNhat=GETDATE()
			WHERE NhanVienID=@NhanVienID
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add(new SqlParameter("@NhanVienID", SqlDbType.Int) { Value = nv.NhanVienID });
		cmd.Parameters.Add(new SqlParameter("@ChucVuID", SqlDbType.Int) { Value = nv.ChucVuID });
		cmd.Parameters.Add(new SqlParameter("@PhongChucNangID", SqlDbType.Int) { Value = nv.PhongChucNangID });
		cmd.Parameters.Add(new SqlParameter("@NgayVaoLam", SqlDbType.DateTime) { Value = (object?)nv.NgayVaoLam ?? DBNull.Value });
		cmd.Parameters.Add(new SqlParameter("@BangCap", SqlDbType.NVarChar) { Value = nv.BangCap });
		cmd.Parameters.Add(new SqlParameter("@KinhNghiem", SqlDbType.NVarChar) { Value = nv.KinhNghiem });
		cmd.Parameters.Add(new SqlParameter("@TrangThai", SqlDbType.NVarChar) { Value = nv.TrangThai });
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task<NhanVien?> GetByIdAsync(int nhanVienId)
	{
		const string sql = @"
			SELECT 
				NhanVienID, ThongTinID, ChucVuID, PhongChucNangID, NgayVaoLam, BangCap,
				KinhNghiem, TrangThai, NgayTao, NgayCapNhat
			FROM NhanVien
			WHERE nNhanVienID = @NhanVienID
		";
		using var conn = new SqlConnection(_connectionString);
		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@NhanVienID", nhanVienId);
		await conn.OpenAsync();
		using var reader = await cmd.ExecuteReaderAsync();
		if (!reader.Read())
			return null;
		return new NhanVien(
			reader.GetInt32(0),
			reader.GetInt32(1),
			reader.GetInt32(2),
			reader.GetInt32(3),
			reader.GetDateTime(4),
			reader.GetString(5),
			reader.GetString(6),
			reader.GetString(7),
			reader.GetDateTime(8),
			reader.GetDateTime(9)
		);
	}
	public async Task<(List<NhanVienListReadModel>, int)> GetPageAsync(int pageNumber, int pageSize)
	{
		var sql = $@"
			{SELECT_LIST}
			ORDER BY nv.NhanVienID
			OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
			SELECT COUNT(*) FROM NhanVien;
		";
		var list = new List<NhanVienListReadModel>();
		int total = 0;
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		int offset = (pageNumber - 1) * pageSize;
		cmd.Parameters.Add(new SqlParameter("@Offset", SqlDbType.Int) { Value = offset });
		cmd.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int) { Value = pageSize });
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
			list.Add(MapList(reader));
		if (await reader.NextResultAsync())
			if (await reader.ReadAsync())
				total = reader.GetInt32(0);
		return (list, total);
	}
	public async Task<(List<NhanVienListReadModel>, int)> SearchAsync(string keyword, int pageNumber, int pageSize)
	{
		var sql = $@"
			{SELECT_LIST}
			WHERE tt.HoTen LIKE @Keyword OR tt.EmailLienHe LIKE @Keyword
			ORDER BY nv.NhanVienID
			OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
			SELECT COUNT(*)
			FROM NhanVien nv
			JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
			WHERE tt.HoTen LIKE @Keyword OR tt.EmailLienHe LIKE @Keyword;
		";
		var list = new List<NhanVienListReadModel>();
		int total = 0;
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		int offset = (pageNumber - 1) * pageSize;
		cmd.Parameters.Add(new SqlParameter("@Keyword", SqlDbType.NVarChar) { Value = $"%{keyword}%" });
		cmd.Parameters.Add(new SqlParameter("@Offset", SqlDbType.Int) { Value = offset });
		cmd.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int) { Value = pageSize });
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
			list.Add(MapList(reader));
		if (await reader.NextResultAsync())
			if (await reader.ReadAsync())
				total = reader.GetInt32(0);
		return (list, total);
	}
	public async Task<NhanVienDetailReadModel?> GetDetailAsync(int id)
	{
		var sql = $@"
			{SELECT_DETAIL}
			WHERE nv.NhanVienID=@Id
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (!await reader.ReadAsync())
			return null;
		return MapDetail(reader);
	}
	public async Task<List<NameResponseDTO>> GetComboboxAsync(int chucVuId)
	{
		const string sql = @"
			SELECT nv.NhanVienID,tt.HoTen
			FROM NhanVien nv
			JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
			WHERE nv.ChucVuID=@ChucVuID AND nv.TrangThai=N'Đang làm việc'
			ORDER BY tt.HoTen
		";
		var list = new List<NameResponseDTO>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add(new SqlParameter("@ChucVuID", SqlDbType.Int) { Value = chucVuId });
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
			list.Add( new NameResponseDTO
			{
				Id = (int)reader["NhanVienID"],
				Name = reader["HoTen"].ToString()!
			});
		return list;
	}
	public async Task<int> GetIdAsync(int taiKhoanId)
	{
		const string sql = @"
			SELECT nv.NhanVienID
			FROM NhanVien nv
			JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
			JOIN TaiKhoan tk ON tt.TaiKhoanID = tk.TaiKhoanID
			WHERE tt.TaiKhoanID = @TaiKhoanID
		";
		using var conn = new SqlConnection(_connectionString);
		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@TaiKhoanID", taiKhoanId);
		await conn.OpenAsync();
		using var reader = await cmd.ExecuteReaderAsync();
		return reader.GetInt32(0);
	}
	private static NhanVienListReadModel MapList(SqlDataReader r)
	{
		return new NhanVienListReadModel
		{
			NhanVienID = (int)r["NhanVienID"],
			HoTen = r["HoTen"].ToString()!,
			Email = r["EmailLienHe"].ToString()!,
			TenChucVu = r["TenChucVu"].ToString()!,
			TrangThai = r["TrangThai"].ToString()!
		};
	}
	private static NhanVienDetailReadModel MapDetail(SqlDataReader r)
	{
		return new NhanVienDetailReadModel
		{
			NhanVienID = (int)r["NhanVienID"],
			ThongTinID = (int)r["ThongTinID"],
			ChucVu = new NameResponseDTO
			{
				Id = (int)r["ChucVuID"],
				Name = r["TenChucVu"].ToString()!
			},
			PhongChucNang = new NameResponseDTO
			{
				Id = (int)r["PhongChucNangID"],
				Name = r["TenPhong"].ToString()!
			},
			HoTen = r["HoTen"].ToString()!,
			NgaySinh = r["NgaySinh"] as DateTime?,
			GioiTinh = r["GioiTinh"].ToString(),
			SDT = r["SDT"].ToString(),
			EmailLienHe = r["EmailLienHe"].ToString()!,
			DiaChi = r["DiaChi"].ToString(),
			Avatar = r["Avatar"].ToString(),
			NgayVaoLam = r["NgayVaoLam"] as DateTime?,
			BangCap = r["BangCap"].ToString(),
			KinhNghiem = r["KinhNghiem"].ToString(),
			TrangThai = r["TrangThai"].ToString()!,
			NgayTao = (DateTime)r["NgayTao"],
			NgayCapNhat = r["NgayCapNhat"] as DateTime?
		};
	}
}