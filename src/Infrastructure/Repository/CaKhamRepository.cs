using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using static Amazon.S3.Util.S3EventNotification;
namespace Infrastructure.Repositories;
public class CaKhamRepository : ICaKhamRepository
{
	private readonly string _connectionString;
	public CaKhamRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}
	public async Task<CaKham?> GetByIdAsync(int caKhamID)
	{
		const string sql = @"
			SELECT CaKhamID, LoaiCaKham, LichLamViecID, KhungGioID, PhongChucNangID, 
				ThongTinID, LyDoKham, TrangThai, NgayDat, NgayKham, GhiChu 
			FROM CaKham 
			WHERE CaKhamID = @Id
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = caKhamID;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (!await reader.ReadAsync())
			return null;
		return MapEntity(reader);
	}
	public async Task<CaKhamReadModel?> GetDetailAsync(int caKhamID)
	{
		const string sql = @"
			SELECT ck.CaKhamID, ck.LoaiCaKham, ck.LichLamViecID, kg.TenKhung, pc.TenPhong, 
				tt.HoTen, ck.LyDoKham, ck.TrangThai, ck.NgayDat, ck.NgayKham, ck.GhiChu 
			FROM CaKham ck LEFT JOIN KhungGioKham kg ON ck.KhungGioID = kg.KhungGioID 
			LEFT JOIN PhongChucNang pc ON ck.PhongChucNangID = pc.PhongChucNangID 
			LEFT JOIN ThongTinCaNhan tt ON ck.ThongTinID = tt.ThongTinID 
			WHERE ck.CaKhamID = @Id
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = caKhamID;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (!await reader.ReadAsync())
			return null;
		return new CaKhamReadModel
		{
			CaKhamID = reader.GetInt32(0),
			LoaiCaKham = reader.GetString(1),
			LichLamViecID = reader.IsDBNull(2) ? null : reader.GetInt32(2),
			TenKhungGio = reader.GetString(3),
			TenPhong = reader.IsDBNull(4) ? null : reader.GetString(4),
			HoTen = reader.IsDBNull(5) ? null : reader.GetString(5),
			LyDoKham = reader.IsDBNull(6) ? null : reader.GetString(6),
			TrangThai = reader.GetString(7),
			NgayDat = reader.IsDBNull(8) ? null : reader.GetDateTime(8),
			NgayKham = reader.GetDateTime(9),
			GhiChu = reader.IsDBNull(10) ? null : reader.GetString(10)
		};
	}
	public async Task<(List<CaKhamListReadModel>, int)> 
		GetPagedAsync(DateTime ngayKham, string trangThai, string loaiCaKham, int pageNumber, int pageSize)
	{
		const string sql = @"
			SELECT ck.CaKhamID, ck.NgayKham, kg.TenKhung, pc.TenPhong, tt.HoTen, ck.LyDoKham, ck.TrangThai 
			FROM CaKham ck 
			LEFT JOIN KhungGioKham kg ON ck.KhungGioID = kg.KhungGioID 
			LEFT JOIN PhongChucNang pc ON ck.PhongChucNangID = pc.PhongChucNangID 
			LEFT JOIN ThongTinCaNhan tt ON ck.ThongTinID = tt.ThongTinID 
			WHERE ck.NgayKham = @NgayKham AND ck.TrangThai = @TrangThai AND ck.LoaiCaKham = @LoaiCaKham 
			ORDER BY ck.CaKhamID 
			OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY; 
			SELECT COUNT(*) 
			FROM CaKham 
			WHERE NgayKham = @NgayKham AND TrangThai = @TrangThai AND LoaiCaKham = @LoaiCaKham
		";
		var list = new List<CaKhamListReadModel>();
		int total = 0;
		int offset = (pageNumber - 1) * pageSize;
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@NgayKham", SqlDbType.Date).Value = ngayKham.Date;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = trangThai;
		cmd.Parameters.Add("@LoaiCaKham", SqlDbType.NVarChar, 50).Value = loaiCaKham;
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(new CaKhamListReadModel
			{
				CaKhamID = reader.GetInt32(0),
				NgayKham = reader.GetDateTime(1),
				TenKhungGio = reader.GetString(2),
				TenPhong = reader.IsDBNull(3) ? null : reader.GetString(3),
				HoTen = reader.IsDBNull(4) ? null : reader.GetString(4),
				LyDoKham = reader.IsDBNull(5) ? null : reader.GetString(5),
				TrangThai = reader.GetString(6)
			});
		}
		if (await reader.NextResultAsync())
		{
			if (await reader.ReadAsync())
				total = reader.GetInt32(0);
		}
		return (list, total);
	}
	public async Task<(List<CaKhamListReadModel>, int)> GetByThongTinAsync(	int thongTinID,	int pageNumber,	int pageSize)
	{
		const string sql = @"
			SELECT ck.CaKhamID, ck.NgayKham, kg.TenKhung, pc.TenPhong, tt.HoTen, ck.LyDoKham, ck.TrangThai 
			FROM CaKham ck 
			LEFT JOIN KhungGioKham kg ON ck.KhungGioID = kg.KhungGioID 
			LEFT JOIN PhongChucNang pc ON ck.PhongChucNangID = pc.PhongChucNangID 
			LEFT JOIN ThongTinCaNhan tt ON ck.ThongTinID = tt.ThongTinID 
			WHERE ck.ThongTinID = @ThongTinID ORDER BY ck.CaKhamID 
			DESC OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY; 
			SELECT COUNT(*) 
			FROM CaKham 
			WHERE ThongTinID = @ThongTinID
		";
		var list = new List<CaKhamListReadModel>();
		int total = 0;
		int offset = (pageNumber - 1) * pageSize;
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@ThongTinID", SqlDbType.Int).Value = thongTinID;
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(new CaKhamListReadModel
			{
				CaKhamID = reader.GetInt32(0),
				NgayKham = reader.GetDateTime(1),
				TenKhungGio = reader.GetString(2),
				TenPhong = reader.IsDBNull(3) ? null : reader.GetString(3),
				HoTen = reader.IsDBNull(4) ? null : reader.GetString(4),
				LyDoKham = reader.IsDBNull(5) ? null : reader.GetString(5),
				TrangThai = reader.GetString(6)
			});
		}
		if (await reader.NextResultAsync())
		{
			if (await reader.ReadAsync())
				total = reader.GetInt32(0);
		}
		return (list, total);
	}
	public async Task<int> AddAsync(CaKham entity)
	{
		const string sql = @"
			INSERT INTO CaKham (LoaiCaKham, LichLamViecID, KhungGioID, PhongChucNangID, NgayKham, TrangThai) 
			VALUES (@LoaiCaKham, @LichLamViecID, @KhungGioID, @PhongChucNangID, @NgayKham, @TrangThai); 
			SELECT SCOPE_IDENTITY();
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@LoaiCaKham", SqlDbType.NVarChar, 50).Value = entity.LoaiCaKham;
		cmd.Parameters.Add("@LichLamViecID", SqlDbType.Int).Value = (object?)entity.LichLamViecID ?? DBNull.Value;
		cmd.Parameters.Add("@KhungGioID", SqlDbType.Int).Value = entity.KhungGioID;
		cmd.Parameters.Add("@PhongChucNangID", SqlDbType.Int).Value = (object?)entity.PhongChucNangID ?? DBNull.Value;
		cmd.Parameters.Add("@NgayKham", SqlDbType.Date).Value = entity.NgayKham;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = entity.TrangThai;
		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		return Convert.ToInt32(result);
	}
	public async Task<List<int>> GetKhungGioConTrongAsync(DateTime ngayKham, string loaiCaKham)
	{
		const string sql = @"
			SELECT KhungGioID 
			FROM CaKham 
			WHERE CAST(NgayKham AS DATE)=@NgayKham AND LoaiCaKham=@LoaiCaKham AND TrangThai!=N'Đã hủy' 
			GROUP BY KhungGioID 
			HAVING ((@LoaiCaKham=N'Khám' AND COUNT(CASE WHEN TrangThai!=N'Trống' THEN 1 END)<5) 
				OR (@LoaiCaKham=N'Điều trị' AND COUNT(CASE WHEN TrangThai!=N'Trống' THEN 1 END)<1))
		";
		var list = new List<int>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@NgayKham", SqlDbType.Date).Value = ngayKham.Date;
		cmd.Parameters.Add("@LoaiCaKham", SqlDbType.NVarChar, 50).Value = loaiCaKham;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(reader.GetInt32(0));
		}
		return list;
	}
	public async Task<int> GetCaKhamAsync(DateTime ngayKham, int khungGioId, string loaiCaKham)
	{
		const string sql = @"
			SELECT TOP 1 CaKhamID 
			FROM CaKham 
			WHERE CAST(NgayKham AS DATE)=@NgayKham AND KhungGioID=@KhungGioID 
			AND LoaiCaKham=@LoaiCaKham AND TrangThai=N'Trống' 
			ORDER BY CaKhamID ASC
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@NgayKham", SqlDbType.Date).Value = ngayKham.Date;
		cmd.Parameters.Add("@KhungGioID", SqlDbType.Int).Value = khungGioId;
		cmd.Parameters.Add("@LoaiCaKham", SqlDbType.NVarChar, 50).Value = loaiCaKham;
		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		return result == null ? 0 : Convert.ToInt32(result);
	}
	public async Task<int> CountByNgayAndKhungGioAsync(DateTime ngay, int khungGioId, string loaiCaKham)
	{
		const string sql = @"
			SELECT COUNT(CaKhamID) 
			FROM CaKham 
			WHERE NgayKham=@NgayKham AND KhungGioID=@KhungGioID AND LoaiCaKham=@LoaiCaKham
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@NgayKham", SqlDbType.Date).Value = ngay.Date;
		cmd.Parameters.Add("@KhungGioID", SqlDbType.Int).Value = khungGioId;
		cmd.Parameters.Add("@LoaiCaKham", SqlDbType.NVarChar, 50).Value = loaiCaKham;
		await conn.OpenAsync();
		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}
	public async Task<bool> ExistsAsync(DateTime ngay, int khungGioId, string loaiCaKham)
	{
		const string sql = @"
			SELECT 1 
			FROM CaKham 
			WHERE NgayKham=@NgayKham AND KhungGioID=@KhungGioID AND LoaiCaKham=@LoaiCaKham
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@NgayKham", SqlDbType.Date).Value = ngay.Date;
		cmd.Parameters.Add("@KhungGioID", SqlDbType.Int).Value = khungGioId;
		cmd.Parameters.Add("@LoaiCaKham", SqlDbType.NVarChar, 50).Value = loaiCaKham;
		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		return result != null;
	}
	public async Task<bool> CheckThongTinDaDangKyAsync(DateTime ngay, int khungGioId, string loaiCaKham, int thongTinId)
	{
		const string sql = @"
			SELECT 1 
			FROM CaKham 
			WHERE NgayKham=@NgayKham AND KhungGioID=@KhungGioID 
			AND LoaiCaKham=@LoaiCaKham AND ThongTinID=@ThongTinID 
			AND TrangThai IN (N'Đã đặt',N'Đã xác nhận',N'Hoàn thành')
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@NgayKham", SqlDbType.Date).Value = ngay.Date;
		cmd.Parameters.Add("@KhungGioID", SqlDbType.Int).Value = khungGioId;
		cmd.Parameters.Add("@LoaiCaKham", SqlDbType.NVarChar, 50).Value = loaiCaKham;
		cmd.Parameters.Add("@ThongTinID", SqlDbType.Int).Value = thongTinId;
		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		return result != null;
	}
    public async Task<string?> GetFcmTokenByCaKhamIdAsync(int caKhamId)
    {
        const string sql = @"
        SELECT tk.FcmToken
        FROM CaKham ck
        JOIN ThongTinBenhNhan tt ON ck.ThongTinID = tt.ThongTinID
        JOIN TaiKhoan tk ON tt.TaiKhoanID = tk.TaiKhoanID
        WHERE ck.CaKhamID = @Id";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = caKhamId;
        await conn.OpenAsync();
        var result = await cmd.ExecuteScalarAsync();
        return result?.ToString();
    }
    public async Task UpdateAsync(CaKham entity)
	{
		const string sql = @"
			UPDATE CaKham 
			SET ThongTinID=@ThongTinID, LyDoKham=@LyDoKham, TrangThai=@TrangThai, NgayDat=@NgayDat, GhiChu=@GhiChu 
			WHERE CaKhamID=@Id
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@ThongTinID", SqlDbType.Int).Value = (object?)entity.ThongTinID ?? DBNull.Value;
		cmd.Parameters.Add("@LyDoKham", SqlDbType.NVarChar, 500).Value = (object?)entity.LyDoKham ?? DBNull.Value;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = entity.TrangThai;
		cmd.Parameters.Add("@NgayDat", SqlDbType.DateTime).Value = (object?)entity.NgayDat ?? DBNull.Value;
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 500).Value = (object?)entity.GhiChu ?? DBNull.Value;
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = entity.CaKhamID;
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task UpdateTrangThaiAsync(int caKhamID, string trangThai, string ghiChu)
	{
		const string sql = @"UPDATE CaKham SET TrangThai=@TrangThai, GhiChu=@GhiChu WHERE CaKhamID=@Id";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = trangThai;
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, 500).Value = ghiChu;
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = caKhamID;
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task<int> AssignAsync(DateTime tuNgay, DateTime denNgay)
	{
		const string sql = @"
			UPDATE ck
			SET 
				ck.LichLamViecID = llv.LichLamViecID,
				ck.PhongChucNangID = nv.PhongChucNangID
			FROM CaKham ck
			JOIN KhungGioKham kg ON ck.KhungGioID = kg.KhungGioID
			JOIN LichLamViecNhanVien llv 
				ON llv.CaLamViec = kg.CaLamViec
				AND llv.Ngay = ck.NgayKham
			JOIN NhanVien nv ON nv.NhanVienID = llv.NhanVienID
			WHERE ck.NgayKham BETWEEN @TuNgay AND @DenNgay
				AND ck.LichLamViecID IS NULL
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = tuNgay.Date;
		cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = denNgay.Date;
		await conn.OpenAsync();
		return await cmd.ExecuteNonQueryAsync();
	}
	public async Task<int> CountNotAssignedAsync(DateTime tuNgay, DateTime denNgay)
	{
		const string sql = @"
			SELECT COUNT(*)
			FROM CaKham
			WHERE NgayKham BETWEEN @TuNgay AND @DenNgay
			AND LichLamViecID IS NULL
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = tuNgay.Date;
		cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = denNgay.Date;
		await conn.OpenAsync();
		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}
	private static CaKham MapEntity(SqlDataReader reader)
	{
		return new CaKham(
			reader.GetInt32(0),
			reader.GetString(1),
			reader.IsDBNull(2) ? null : reader.GetInt32(2),
			reader.GetInt32(3),
			reader.IsDBNull(4) ? null : reader.GetInt32(4),
			reader.IsDBNull(5) ? null : reader.GetInt32(5),
			reader.IsDBNull(6) ? null : reader.GetString(6),
			reader.GetString(7),
			reader.IsDBNull(8) ? null : reader.GetDateTime(8),
			reader.GetDateTime(9),
			reader.IsDBNull(10) ? null : reader.GetString(10)
		);
	}
}