using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;
public class NhanVienRepository : INhanVienRepository
{
	private readonly string _connectionString;

	public NhanVienRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}

	public async Task AddAsync(NhanVien nv)
	{
		const string sql = @"
		INSERT INTO NhanVien
		(ThongTinID, ChucVuID, PhongChucNangID, NgayVaoLam, BangCap, KinhNghiem)
		VALUES
		(@ThongTinID, @ChucVuID, @PhongChucNangID, @NgayVaoLam, @BangCap, @KinhNghiem)
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@ThongTinID", nv.ThongTinID);
		cmd.Parameters.AddWithValue("@ChucVuID", nv.ChucVuID);
		cmd.Parameters.AddWithValue("@PhongChucNangID", nv.PhongChucNangID);
		cmd.Parameters.AddWithValue("@NgayVaoLam", (object?)nv.NgayVaoLam ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@BangCap", nv.BangCap);
		cmd.Parameters.AddWithValue("@KinhNghiem", nv.KinhNghiem);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task UpdateAsync(NhanVien nv)
	{
		const string sql = @"
		UPDATE NhanVien
		SET 
			ChucVuID = @ChucVuID,PhongChucNangID = @PhongChucNangID,
			NgayVaoLam = @NgayVaoLam,BangCap = @BangCap,KinhNghiem = @KinhNghiem,
			TrangThai = @TrangThai,NgayCapNhat = GETDATE()
		WHERE NhanVienID = @NhanVienID
	";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@NhanVienID", nv.NhanVienID);
		cmd.Parameters.AddWithValue("@ChucVuID", nv.ChucVuID);
		cmd.Parameters.AddWithValue("@PhongChucNangID", nv.PhongChucNangID);
		cmd.Parameters.AddWithValue("@NgayVaoLam", (object?)nv.NgayVaoLam ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@BangCap", nv.BangCap);
		cmd.Parameters.AddWithValue("@KinhNghiem", nv.KinhNghiem);
		cmd.Parameters.AddWithValue("@TrangThai", nv.TrangThai);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task<NhanVien?> GetByIdAsync(int nhanVienID)
	{
		const string sql = @"
			SELECT 
				nv.NhanVienID, nv.ThongTinID, nv.ChucVuID,nv.PhongChucNangID,
				nv.NgayVaoLam, nv.BangCap, nv.KinhNghiem, nv.TrangThai,
				nv.NgayTao,	nv.NgayCapNhat,
				cv.TenChucVu,
				tt.HoTen, tt.SDT, tt.EmailLienHe
			FROM NhanVien nv
			JOIN ChucVu cv ON nv.ChucVuID = cv.ChucVuID
			JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
			WHERE nv.NhanVienID = @NhanVienID
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@NhanVienID", nhanVienID);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}



	public async Task<List<NhanVien>> GetAllAsync()
	{
		const string sql = @"
			SELECT
				nv.NhanVienID, nv.ChucVuID, nv.PhongChucNangID,nv.NgayVaoLam, nv.TrangThai,
				cv.TenChucVu,
				tt.ThongTinID, tt.HoTen, tt.SDT, tt.EmailLienHe,
				nv.BangCap, nv.KinhNghiem
			FROM NhanVien nv
			JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
			JOIN ChucVu cv ON nv.ChucVuID = cv.ChucVuID
			ORDER BY nv.NhanVienID
		";

		var list = new List<NhanVien>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add(MapToListEntity(reader));
		}
		return list;
	}
	public async Task<int?> GetPhongChucNangIdByNhanVienIdAsync(int nhanVienId)
	{
		const string sql = @"
		SELECT PhongChucNangID
		FROM NhanVien
		WHERE NhanVienID = @NhanVienID
	";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@NhanVienID", nhanVienId);

		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();

		return result == null ? null : (int)result;
	}

	public async Task<List<NhanVien>> SearchAsync(string keyword)
	{
		const string sql = @"
				SELECT
					nv.NhanVienID, nv.ChucVuID, nv.PhongChucNangID, nv.NgayVaoLam, nv.TrangThai,
					cv.TenChucVu,
					tt.ThongTinID, tt.HoTen, tt.SDT, tt.EmailLienHe,
					nv.BangCap, nv.KinhNghiem
				FROM NhanVien nv
				JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
				JOIN ChucVu cv ON nv.ChucVuID = cv.ChucVuID
				WHERE tt.HoTen LIKE @Keyword OR tt.EmailLienHe LIKE @Keyword
			";

		var list = new List<NhanVien>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add(MapToListEntity(reader));
		}

		return list;
	}

	private static NhanVien MapToEntity(SqlDataReader r)
	{
		var thongTin = new ThongTinCaNhan(
			thongTinID: (int)r["ThongTinID"],
			taiKhoanID: null,
			hoTen: r["HoTen"].ToString()!,
			ngaySinh: null,
			gioiTinh: null,
			sdt: r["SDT"].ToString()!,
			emailLienHe: r["EmailLienHe"].ToString()!,
			diaChi: null,
			avatar: null,
			loai: "Nhân viên",
			ngayTao: DateTime.Now,
			ngayCapNhat: null
		);

		return new NhanVien(
			nhanVienID: (int)r["NhanVienID"],
			thongTinID: thongTin.ThongTinID,
			chucVuID: (int)r["ChucVuID"],
			phongChucNangID: (int)r["PhongChucNangID"],
			ngayVaoLam: r["NgayVaoLam"] as DateTime?,
			bangCap: r["BangCap"].ToString()!,
			kinhNghiem: r["KinhNghiem"].ToString()!,
			trangThai: r["TrangThai"].ToString()!,
			ngayTao: (DateTime)r["NgayTao"],
			ngayCapNhat: r["NgayCapNhat"] as DateTime?,
			tenChucVu: r["TenChucVu"].ToString(),
			thongTinCaNhan: thongTin
		);
	}



	private static NhanVien MapToListEntity(SqlDataReader r)
	{
		var thongTin = new ThongTinCaNhan(
			thongTinID: (int)r["ThongTinID"],
			taiKhoanID: null,
			hoTen: r["HoTen"]!.ToString()!,
			ngaySinh: null,
			gioiTinh: null,
			sdt: r["SDT"]!.ToString()!,
			emailLienHe: r["EmailLienHe"]!.ToString()!,
			diaChi: null,
			avatar: null,
			loai: "Nhân viên",
			ngayTao:DateTime.Now,
			ngayCapNhat: null
		);

		return new NhanVien(
			nhanVienID: (int)r["NhanVienID"],
			thongTinID: thongTin.ThongTinID,
			chucVuID: (int)r["ChucVuID"],
			phongChucNangID: (int)r["PhongChucNangID"],
			ngayVaoLam: r["NgayVaoLam"] as DateTime?,
			bangCap: r["BangCap"]!.ToString()!,
			kinhNghiem: r["KinhNghiem"]!.ToString()!,
			trangThai: r["TrangThai"]!.ToString()!,
			tenChucVu: r["TenChucVu"]!.ToString(),
			thongTinCaNhan: thongTin
		);
	}
}

