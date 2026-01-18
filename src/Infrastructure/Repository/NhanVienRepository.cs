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
				(ThongTinID, ChucVuID, NgayVaoLam, BangCap, KinhNghiem, TrangThai)
				VALUES
				(@ThongTinID, @ChucVuID, @NgayVaoLam, @BangCap, @KinhNghiem, @TrangThai)
			";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@ThongTinID", nv.ThongTinID);
		cmd.Parameters.AddWithValue("@ChucVuID", nv.ChucVuID);
		cmd.Parameters.AddWithValue("@NgayVaoLam", (object?)nv.NgayVaoLam ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@BangCap", nv.BangCap);
		cmd.Parameters.AddWithValue("@KinhNghiem", nv.KinhNghiem);
		cmd.Parameters.AddWithValue("@TrangThai", nv.TrangThai);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task UpdateAsync(NhanVien nv)
	{
		const string sql = @"
				UPDATE NhanVien
				SET
					ChucVuID   = @ChucVuID,
					NgayVaoLam = @NgayVaoLam,
					BangCap   = @BangCap,
					KinhNghiem= @KinhNghiem,
					TrangThai = @TrangThai
				WHERE NhanVienID = @NhanVienID
			";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@NhanVienID", nv.NhanVienID);
		cmd.Parameters.AddWithValue("@ChucVuID", nv.ChucVuID);
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
				nv.NhanVienID, nv.ThongTinID, nv.ChucVuID, nv.NgayVaoLam, nv.BangCap, nv.KinhNghiem, nv.TrangThai,
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
				nv.NhanVienID, nv.ChucVuID, nv.NgayVaoLam, nv.TrangThai,
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
			list.Add(MapToEntityList(reader));
		}
		return list;
	}

	public async Task<List<NhanVien>> SearchAsync(string keyword)
	{
		const string sql = @"
				SELECT
					nv.NhanVienID, nv.ChucVuID, nv.NgayVaoLam, nv.TrangThai,
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
			list.Add(MapToEntityList(reader));
		}

		return list;
	}

	private static NhanVien MapToEntity(SqlDataReader r)
	{
		var thongTin = new ThongTinCaNhan(
			thongTinID: (int)r["ThongTinID"],
			taiKhoanID: null,
			hoTen: r["HoTen"]?.ToString() ?? string.Empty,
			ngaySinh: null,
			gioiTinh: null,
			sdt: r["SDT"]?.ToString() ?? string.Empty,
			emailLienHe: r["EmailLienHe"]?.ToString() ?? string.Empty,
			diaChi: null,
			avatar: null,
			loai: "Nhân viên",
			ngayTao: DateTime.MinValue,
			ngayCapNhat: DateTime.MinValue
		);

		return new NhanVien(
			nhanVienID: (int)r["NhanVienID"],
			thongTinID: thongTin.ThongTinID,
			chucVuID: (int)r["ChucVuID"],
			ngayVaoLam: r["NgayVaoLam"] as DateTime?,
			bangCap: r["BangCap"]?.ToString() ?? string.Empty,
			kinhNghiem: r["KinhNghiem"]?.ToString() ?? string.Empty,
			trangThai: r["TrangThai"]?.ToString() ?? string.Empty,
			tenChucVu: r["TenChucVu"]?.ToString() ?? string.Empty,
			thongTinCaNhan: thongTin
		);
	}


	private static NhanVien MapToEntityList(SqlDataReader r)
	{
		var thongTin = new ThongTinCaNhan(
			thongTinID: (int)r["ThongTinID"],
			taiKhoanID: null,
			hoTen: r["HoTen"]?.ToString() ?? string.Empty,
			ngaySinh: null,
			gioiTinh: null,
			sdt: r["SDT"]?.ToString() ?? string.Empty,
			emailLienHe: r["EmailLienHe"]?.ToString()??string.Empty,
			diaChi: null,
			avatar: null,
			loai: "Nhân viên",
			ngayTao: DateTime.MinValue,
			ngayCapNhat: DateTime.MinValue
		);

		return new NhanVien(
			nhanVienID: (int)r["NhanVienID"],
			thongTinID: thongTin.ThongTinID,
			chucVuID: r["ChucVuID"] != DBNull.Value ? (int)r["ChucVuID"] : 0,
			ngayVaoLam: r["NgayVaoLam"] as DateTime?,
			bangCap: r["BangCap"]?.ToString() ?? string.Empty,
			kinhNghiem: r["KinhNghiem"]?.ToString() ?? string.Empty,
			trangThai: r["TrangThai"]?.ToString() ?? string.Empty,
			tenChucVu: r["TenChucVu"]?.ToString() ?? string.Empty,
			thongTinCaNhan: thongTin
		);

	}
}

