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
	public async Task<(List<NhanVien> Data, int TotalCount)> GetPageAsync(int pageNumber, int pageSize)
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
			ORDER BY nv.NhanVienID
			OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

			SELECT COUNT(*) FROM NhanVien;
		";

		var list = new List<NhanVien>();
		int totalCount = 0;

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		int offset = (pageNumber - 1) * pageSize;

		cmd.Parameters.AddWithValue("@Offset", offset);
		cmd.Parameters.AddWithValue("@PageSize", pageSize);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		// Result 1: Data
		while (await reader.ReadAsync())
		{
			list.Add(MapToListEntity(reader));
		}

		// Result 2: TotalCount
		if (await reader.NextResultAsync())
		{
			if (await reader.ReadAsync())
			{
				totalCount = reader.GetInt32(0);
			}
		}

		return (list, totalCount);
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
				tt.HoTen,tt.NgaySinh, tt.GioiTinh, tt.SDT, tt.EmailLienHe, tt.DiaChi, tt.Avatar
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

	public async Task<(List<NhanVien> Data, int TotalCount)>
		SearchAsync(string keyword, int pageNumber, int pageSize)
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
        WHERE tt.HoTen LIKE @Keyword 
              OR tt.EmailLienHe LIKE @Keyword
        ORDER BY nv.NhanVienID
        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

        SELECT COUNT(*)
        FROM NhanVien nv
        JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
        WHERE tt.HoTen LIKE @Keyword 
              OR tt.EmailLienHe LIKE @Keyword;
    ";

		var list = new List<NhanVien>();
		int totalCount = 0;

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		int offset = (pageNumber - 1) * pageSize;

		cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");
		cmd.Parameters.AddWithValue("@Offset", offset);
		cmd.Parameters.AddWithValue("@PageSize", pageSize);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		// Result 1: Data
		while (await reader.ReadAsync())
		{
			list.Add(MapToListEntity(reader));
		}

		// Result 2: TotalCount
		if (await reader.NextResultAsync())
		{
			if (await reader.ReadAsync())
			{
				totalCount = reader.GetInt32(0);
			}
		}

		return (list, totalCount);
	}
	public async Task<string?> GetNameByIdAsync	(int id)
	{
		const string sql = @"
			SELECT tt.HoTen as TenNhanVien
			FROM NhanVien nv
			INNER JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
			WHERE nv.NhanVienID = @Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Id", id);

		await conn.OpenAsync();
		return await cmd.ExecuteScalarAsync() as string;
	}
	public async Task<NhanVien?> GetForAuthAsync(int TaiKhoanId)
	{
		const string sql = @"
			SELECT 
				nv.NhanVienID, nv.ChucVuID, nv.ThongTinID
			FROM NhanVien nv
			JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
			WHERE tt.TaiKhoanID = @TaiKhoanID
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@TaiKhoanID", TaiKhoanId);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (!await reader.ReadAsync())
			return null;
		return new NhanVien(
			nhanVienID: (int)reader["NhanVienID"],
			chucVuID: (int)reader["ChucVuID"],
			thongTinID: (int)reader["ThongTinID"]
			);
	}
	public async Task<List<(int Id, string Name)>>	GetDropdownAsync(int chucVuId)
	{
		const string sql = @"
			SELECT nv.NhanVienID, tt.HoTen
			FROM NhanVien nv
			JOIN ThongTinCaNhan tt 
				ON nv.ThongTinID = tt.ThongTinID
			WHERE nv.ChucVuID = @ChucVuID
				  AND nv.TrangThai = N'Đang làm việc'
			ORDER BY tt.HoTen
		";

		var list = new List<(int, string)>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@ChucVuID", chucVuId);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add((
				(int)reader["NhanVienID"],
				reader["HoTen"].ToString()!
			));
		}

		return list;
	}


	private static NhanVien MapToEntity(SqlDataReader r)
	{
		var thongTin = new ThongTinCaNhan(
			thongTinID: (int)r["ThongTinID"],
			taiKhoanID:  null,
			hoTen: r["HoTen"].ToString()!,
			ngaySinh: r["NgaySinh"] as DateTime?,
			gioiTinh: r["GioiTinh"].ToString(),
			sdt: r["SDT"].ToString()!,
			emailLienHe: r["EmailLienHe"].ToString()!,
			diaChi: r["DiaChi"].ToString(),
			avatar: r["Avatar"].ToString(),
			loai: "Nhân viên",
			ngayTao: (DateTime)r["NgayTao"],
			ngayCapNhat: r["NgayCapNhat"] as DateTime?
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

