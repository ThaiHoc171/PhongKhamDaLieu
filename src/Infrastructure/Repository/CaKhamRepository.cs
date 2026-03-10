using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
		const string sql = @"SELECT CaKhamID, LoaiCaKham, LichLamViecID, KhungGioID, PhongChucNangID, 
                             ThongTinID, LyDoKham, TrangThai, NgayDat, NgayKham, GhiChu
                             FROM CaKham WHERE CaKhamID = @caKhamID";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@caKhamID", caKhamID);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}
	public async Task<(List<CaKhamListReadModel>, int)> GetCaKhamsAsync(
		DateTime ngayKham, string trangThai, string loaiCaKham, int pageNumber, int pageSize)
	{
		const string sql = @"
			SELECT ck.CaKhamID, kg.TenKhung, pc.TenPhong, tt.HoTen, ck.LyDoKham, ck.TrangThai
			FROM CaKham ck
			LEFT JOIN KhungGioKham kg ON ck.KhungGioID = kg.KhungGioID
			LEFT JOIN PhongChucNang pc ON ck.PhongChucNangID = pc.PhongChucNangID
			LEFT JOIN ThongTinCaNhan tt ON ck.ThongTinID = tt.ThongTinID
			WHERE ck.NgayKham = @NgayKham
			AND ck.TrangThai = @TrangThai
			AND ck.LoaiCaKham = @LoaiCaKham
			ORDER BY ck.CaKhamID
			OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
			SELECT COUNT(*)
			FROM CaKham
			WHERE NgayKham = @NgayKham
			AND TrangThai = @TrangThai
			AND LoaiCaKham = @LoaiCaKham;
			";
		var list = new List<CaKhamListReadModel>();
		int total = 0;
		int offset = (pageNumber - 1) * pageSize;
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@NgayKham", ngayKham);
		cmd.Parameters.AddWithValue("@TrangThai", trangThai);
		cmd.Parameters.AddWithValue("@LoaiCaKham", loaiCaKham);
		cmd.Parameters.AddWithValue("@Offset", offset);
		cmd.Parameters.AddWithValue("@PageSize", pageSize);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(new CaKhamListReadModel
			{
				CaKhamID = reader.GetInt32(0),
				TenKhungGio = reader.GetString(1),
				TenPhong = reader.GetString(2),
				HoTen = reader.IsDBNull(3) ? null : reader.GetString(3),
				LyDoKham = reader.IsDBNull(4) ? null : reader.GetString(4),
				TrangThai = reader.GetString(5)
			});
		}
		if (await reader.NextResultAsync())
		{
			if (await reader.ReadAsync())
				total = reader.GetInt32(0);
		}
		return (list, total);
	}
	public async Task<CaKhamReadModel?> GetCaKhamDetailAsync(int caKhamId)
	{
		const string sql = @"
		SELECT ck.CaKhamID, ck.LoaiCaKham, ck.LichLamViecID, kg.TenKhung, pc.TenPhong, tt.HoTen,
			   ck.LyDoKham, ck.TrangThai, ck.NgayDat, ck.NgayKham, ck.GhiChu
		FROM CaKham ck
		LEFT JOIN KhungGioKham kg ON ck.KhungGioID = kg.KhungGioID
		LEFT JOIN PhongChucNang pc ON ck.PhongChucNangID = pc.PhongChucNangID
		LEFT JOIN ThongTinCaNhan tt ON ck.ThongTinID = tt.ThongTinID	
		WHERE ck.CaKhamID = @CaKhamID";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@CaKhamID", caKhamId);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (await reader.ReadAsync())
		{
			return new CaKhamReadModel
			{
				CaKhamID = reader.GetInt32(0),
				LoaiCaKham = reader.GetString(1),
				LichLamViecID = reader.GetInt32(2),
				TenKhungGio = reader.GetString(3),
				TenPhong = reader.GetString(4),
				HoTen = reader.IsDBNull(5) ? null : reader.GetString(5),
				LyDoKham = reader.IsDBNull(6) ? null : reader.GetString(6),
				TrangThai = reader.GetString(7),
				NgayDat = reader.IsDBNull(8) ? null : reader.GetDateTime(8),
				NgayKham = reader.GetDateTime(9),
				GhiChu = reader.IsDBNull(10) ? null : reader.GetString(10)
			};
		}
		return null;
	}
	public async Task<List<(int Id, string Ten)>> GetIdAndNameByStatusAsync(string trangThai, DateTime ngayKham)
	{
		const string sql = @"
            SELECT CaKhamID, ISNULL(LyDoKham, N'Chưa xác định!')
            FROM CaKham
            WHERE TrangThai = @TrangThai
              AND NgayKham >= @NgayKham
              AND NgayKham < DATEADD(DAY,1,@NgayKham)
            ORDER BY NgayKham DESC";
		var list = new List<(int, string)>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@TrangThai", trangThai);
		cmd.Parameters.AddWithValue("@NgayKham", ngayKham.Date);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add((
				reader.GetInt32(0),
				reader.GetString(1)
			));
		}
		return list;
	}
	public async Task<List<int>> GetKhungGioConTrongAsync(DateTime ngayKham, string loaiCaKham)
	{
		const string sql = @"
            SELECT KhungGioID
            FROM CaKham
            WHERE CAST(NgayKham AS DATE) = CAST(@NgayKham AS DATE)
              AND LoaiCaKham = @LoaiCaKham
              AND TrangThai != N'Đã hủy'
            GROUP BY KhungGioID
            HAVING 
			(
				(@LoaiCaKham = N'Khám' AND COUNT(CASE WHEN TrangThai != N'Trống' THEN 1 END) < 5)
				OR
				(@LoaiCaKham = N'Điều trị' AND COUNT(CASE WHEN TrangThai != N'Trống' THEN 1 END) < 1)
			)";
		var list = new List<int>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@NgayKham", ngayKham);
		cmd.Parameters.AddWithValue("@LoaiCaKham", loaiCaKham);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
			list.Add(reader.GetInt32(0));
		return list;
	}
    public async Task<int> GetCaKhamAsync(DateTime ngayKham, int khungGioId, string loaiCaKham)
    {
        const string sql = @"SELECT TOP 1 CaKhamID
                         FROM CaKham
                         WHERE CAST(NgayKham AS DATE) = @NgayKham
                           AND KhungGioID = @KhungGioID
                           AND LoaiCaKham = @LoaiCaKham
                           AND TrangThai = N'Trống'
                         ORDER BY CaKhamID ASC";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@NgayKham", ngayKham.Date);
        cmd.Parameters.AddWithValue("@KhungGioID", khungGioId);
        cmd.Parameters.AddWithValue("@LoaiCaKham", loaiCaKham);

        await conn.OpenAsync();
        var result = await cmd.ExecuteScalarAsync();

        return result == null ? 0 : (int)result;
    }
    public async Task<int> GetLichAsync(int CaKhamID)
    {
        const string sql = @"SELECT LichLamViecID
                             FROM CaKham
                             WHERE CaKhamID = @CaKhamID";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@CaKhamID", CaKhamID);
        await conn.OpenAsync();
        var result = await cmd.ExecuteScalarAsync();
        return result == null ? 0 : (int)result;
    }
    public async Task<(List<CaKhamListReadModel>, int)> GetByThongTinAsync(int thongTinID, int pageNumber, int pageSize)
	{
		const string sql = @"
		SELECT ck.CaKhamID, ck.NgayKham, kg.TenKhung, pc.TenPhong, tt.HoTen, ck.LyDoKham, ck.TrangThai
		FROM CaKham ck
		LEFT JOIN KhungGioKham kg ON ck.KhungGioID = kg.KhungGioID
		LEFT JOIN PhongChucNang pc ON ck.PhongChucNangID = pc.PhongChucNangID
		LEFT JOIN ThongTinCaNhan tt ON ck.ThongTinID = tt.ThongTinID
		WHERE ck.ThongTinID = @ThongTinID
		ORDER BY ck.CaKhamID DESC
		OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
		SELECT COUNT(*)
		FROM CaKham
		WHERE ThongTinID = @ThongTinID;
	";
		var list = new List<CaKhamListReadModel>();
		int total = 0;
		int offset = (pageNumber - 1) * pageSize;
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@ThongTinID", thongTinID);
		cmd.Parameters.AddWithValue("@Offset", offset);
		cmd.Parameters.AddWithValue("@PageSize", pageSize);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(new CaKhamListReadModel
			{
				CaKhamID = reader.GetInt32(0),
				NgayKham = reader.GetDateTime(1),
				TenKhungGio = reader.GetString(2),
				TenPhong = reader.GetString(3),
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
	public async Task<int> CountByNgayAndKhungGioAsync(DateTime ngay, int khungGioId, string loaiCaKham)
	{
		const string sql = @"SELECT COUNT(CaKhamID)
                             FROM CaKham
                             WHERE NgayKham = @NgayKham
                               AND KhungGioID = @KhungGioID
                               AND LoaiCaKham = @LoaiCaKham";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@NgayKham", ngay);
		cmd.Parameters.AddWithValue("@KhungGioID", khungGioId);
		cmd.Parameters.AddWithValue("@LoaiCaKham", loaiCaKham);
		await conn.OpenAsync();
		return (int)await cmd.ExecuteScalarAsync();
	}
	public async Task<bool> ExistsAsync(DateTime ngay, int khungGioId, string loaiCaKham)
	{
		const string sql = @"SELECT 1
                             FROM CaKham
                             WHERE NgayKham = @NgayKham
                               AND KhungGioID = @KhungGioID
                               AND LoaiCaKham = @LoaiCaKham";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@NgayKham", ngay.Date);
		cmd.Parameters.AddWithValue("@KhungGioID", khungGioId);
		cmd.Parameters.AddWithValue("@LoaiCaKham", loaiCaKham);
		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		return result != null;
	}
	public async Task<bool> CheckThongTinDaDangKyAsync(
		DateTime ngay,
		int khungGioId,
		string loaiCaKham,
		int thongTinId)
	{
		const string sql = @"SELECT 1
                             FROM CaKham
                             WHERE NgayKham = @NgayKham
                               AND KhungGioID = @KhungGioID
                               AND LoaiCaKham = @LoaiCaKham
                               AND ThongTinID = @ThongTinID
                               AND TrangThai IN (N'Đã đặt', N'Đã xác nhận', N'Hoàn thành')";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@NgayKham", ngay.Date);
		cmd.Parameters.AddWithValue("@KhungGioID", khungGioId);
		cmd.Parameters.AddWithValue("@LoaiCaKham", loaiCaKham);
		cmd.Parameters.AddWithValue("@ThongTinID", thongTinId);
		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		return result != null;
	}
	public async Task<int> AddAsync(CaKham ca)
	{
		const string sql = @"
            INSERT INTO CaKham
            (LoaiCaKham, LichLamViecID, KhungGioID, PhongChucNangID, NgayKham, TrangThai)
            OUTPUT INSERTED.CaKhamID
            SELECT
                CASE
                    WHEN nv.PhongChucNangID = 1 THEN N'Khám'
                    WHEN nv.PhongChucNangID = 2 THEN N'Điều trị'
                END,
                llv.LichLamViecID,
                @KhungGioID,
                nv.PhongChucNangID,
                @NgayKham,
                N'Trống'
            FROM LichLamViecNhanVien llv
            JOIN NhanVien nv ON llv.NhanVienID = nv.NhanVienID
            WHERE llv.LichLamViecID = @LichLamViecID";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@LichLamViecID", ca.LichLamViecID);
		cmd.Parameters.AddWithValue("@KhungGioID", ca.KhungGioID);
		cmd.Parameters.AddWithValue("@NgayKham", ca.NgayKham);
		await conn.OpenAsync();
		return (int)await cmd.ExecuteScalarAsync();
	}
	public async Task UpdateAsync(CaKham ca)
	{
		const string sql = @"UPDATE CaKham
                             SET ThongTinID = @ThongTinID,
                                 LyDoKham = @LyDoKham,
                                 TrangThai = @TrangThai,
                                 NgayDat = @NgayDat,
                                 GhiChu = @GhiChu
                             WHERE CaKhamID = @Id";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@ThongTinID", ca.ThongTinID);
		cmd.Parameters.AddWithValue("@LyDoKham", ca.LyDoKham ?? "");
		cmd.Parameters.AddWithValue("@TrangThai", ca.TrangThai);
		cmd.Parameters.AddWithValue("@NgayDat", ca.NgayDat);
		cmd.Parameters.AddWithValue("@GhiChu", ca.GhiChu ?? "");
		cmd.Parameters.AddWithValue("@Id", ca.CaKhamID);
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task UpdateTrangThaiAsync(int caKhamID, string trangThai, string ghiChu)
	{
		const string sql = @"UPDATE CaKham
                             SET TrangThai = @TrangThai,
                                 GhiChu = @GhiChu
                             WHERE CaKhamID = @Id";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@TrangThai", trangThai);
		cmd.Parameters.AddWithValue("@GhiChu", ghiChu);
		cmd.Parameters.AddWithValue("@Id", caKhamID);
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	private static CaKham MapToEntity(SqlDataReader reader)
	{
		return new CaKham(
			caKhamID: reader.GetInt32(0),
			loaiCaKham: reader.GetString(1),
			lichLamViecID: reader.GetInt32(2),
			khungGioID: reader.GetInt32(3),
			phongChucNangID: reader.GetInt32(4),
			thongTinID: reader.IsDBNull(5) ? null : reader.GetInt32(5),
			lyDoKham: reader.IsDBNull(6) ? null : reader.GetString(6),
			trangThai: reader.GetString(7),
			ngayDat: reader.IsDBNull(8) ? null : reader.GetDateTime(8),
			ngayKham: reader.GetDateTime(9),
			ghiChu: reader.IsDBNull(10) ? null : reader.GetString(10)
		);
	}
}