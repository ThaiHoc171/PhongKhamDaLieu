using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class CaKhamRepository : ICaKhamRepository
{
	private readonly string _connectionString;

	public CaKhamRepository(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection")!;
	}

	#region Queries

	private const string BaseSelectList = @"
		SELECT ck.CaKhamID, ck.LoaiCaKham, ck.NgayKham, kg.TenKhung, pc.TenPhong,
			   tt.HoTen AS TenBenhNhan,
			   bs.HoTen AS TenBacSi,
			   ck.LyDoKham, ck.TrangThai, ck.NhanVienID
		FROM CaKham ck
		LEFT JOIN KhungGioKham kg ON ck.KhungGioID = kg.KhungGioID
		LEFT JOIN PhongChucNang pc ON ck.PhongChucNangID = pc.PhongChucNangID
		LEFT JOIN ThongTinCaNhan tt ON ck.ThongTinID = tt.ThongTinID
		LEFT JOIN NhanVien nv ON ck.NhanVienID = nv.NhanVienID
		LEFT JOIN ThongTinCaNhan bs ON nv.ThongTinID = bs.ThongTinID";


	private const string BaseSelectDetail = @"
		SELECT ck.CaKhamID, ck.LoaiCaKham, ck.LichLamViecID, ck.KhungGioID, ck.PhongChucNangID, ck.ThongTinID,
			   ck.LyDoKham, ck.TrangThai, ck.NgayDat, ck.NgayKham, ck.GhiChu,
			   kg.TenKhung, pc.TenPhong,
			   tt.HoTen AS TenBenhNhan,
			   bs.HoTen AS TenBacSi,
			   ck.NhanVienID
		FROM CaKham ck
		LEFT JOIN KhungGioKham kg ON ck.KhungGioID = kg.KhungGioID
		LEFT JOIN PhongChucNang pc ON ck.PhongChucNangID = pc.PhongChucNangID
		LEFT JOIN ThongTinCaNhan tt ON ck.ThongTinID = tt.ThongTinID
		LEFT JOIN NhanVien nv ON ck.NhanVienID = nv.NhanVienID
		LEFT JOIN ThongTinCaNhan bs ON nv.ThongTinID = bs.ThongTinID";
	#endregion

	public async Task<CaKham?> GetByIdAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"
			SELECT CaKhamID,LoaiCaKham,NhanVienID,LichLamViecID,KhungGioID,PhongChucNangID,
				   ThongTinID,LyDoKham,TrangThai,NgayDat,NgayKham,GhiChu
			FROM CaKham 
			WHERE CaKhamID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}

	public async Task<CaKhamReadModel?> GetDetailAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectDetail + " WHERE CaKhamID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToDetailDTO(reader);

		return null;
	}

	public async Task<(List<CaKhamListReadModel>, int)> GetPagedAsync(DateTime ngayKham, string trangThai, string loaiCaKham, int page, int size)
	{
		var list = new List<CaKhamListReadModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
        {BaseSelectList}
        WHERE ck.NgayKham=@Ngay
        AND ck.TrangThai=@TrangThai
        AND ck.LoaiCaKham=@Loai
        ORDER BY ck.CaKhamID
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

        SELECT COUNT(*)
        FROM CaKham
        WHERE NgayKham=@Ngay
        AND TrangThai=@TrangThai
        AND LoaiCaKham=@Loai";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = ngayKham.Date;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = trangThai;
		cmd.Parameters.Add("@Loai", SqlDbType.NVarChar, 50).Value = loaiCaKham;
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
	public async Task<List<int>> GetKhungGioConTrongAsync(DateTime ngayKham, string loaiCaKham, int? nhanVienId)
	{
		const string sql = @"
			SELECT ck.KhungGioID 
			FROM CaKham ck
			JOIN LichLamViecNhanVien llv ON ck.LichLamViecID = llv.LichLamViecID
			WHERE CAST(ck.NgayKham AS DATE)=@NgayKham 
			AND ck.LoaiCaKham=@LoaiCaKham AND ck.TrangThai!=N'Đã hủy'
			AND (@NhanVienID IS NULL OR ck.NhanVienID = @NhanVienID)
			GROUP BY ck.KhungGioID 
			HAVING ((@LoaiCaKham=N'Khám' AND COUNT(CASE WHEN ck.TrangThai!=N'Trống' THEN 1 END)<5) 
				OR (@LoaiCaKham=N'Điều trị' AND COUNT(CASE WHEN ck.TrangThai!=N'Trống' THEN 1 END)<1))
		";
		var list = new List<int>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@NgayKham", SqlDbType.Date).Value = ngayKham.Date;
		cmd.Parameters.Add("@LoaiCaKham", SqlDbType.NVarChar, 50).Value = loaiCaKham;
		cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = nhanVienId;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(reader.GetInt32(0));
		}
		return list;
	}
	
	public async Task<int> GetCaKhamAsync(DateTime ngayKham, int khungGioId, string loaiCaKham, int? nhanVienId)
	{
		const string sql = @"
			SELECT TOP 1 ck.CaKhamID 
			FROM CaKham ck
			JOIN LichLamViecNhanVien llv ON ck.LichLamViecID = llv.LichLamViecID
			WHERE CAST(ck.NgayKham AS DATE)=@NgayKham AND ck.KhungGioID=@KhungGioID
				AND ck.LoaiCaKham=@LoaiCaKham AND TrangThai=N'Trống'
				AND (@NhanVienID IS NULL OR ck.NhanVienID = @NhanVienID)
			ORDER BY CaKhamID ASC
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@NgayKham", SqlDbType.Date).Value = ngayKham.Date;
		cmd.Parameters.Add("@KhungGioID", SqlDbType.Int).Value = khungGioId;
		cmd.Parameters.Add("@LoaiCaKham", SqlDbType.NVarChar, 50).Value = loaiCaKham;
		cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = nhanVienId;
		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		return result == null ? 0 : Convert.ToInt32(result);
	}
	public async Task<(List<CaKhamListReadModel>, int)> GetChoXacNhanAsync(int page, int size)
	{
		var list = new List<CaKhamListReadModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
			{BaseSelectList}
			WHERE ck.TrangThai = N'Đã đặt'
			AND ck.NgayKham >= CAST(GETDATE() AS DATE)
			ORDER BY ck.NgayKham, ck.CaKhamID, ck.LoaiCaKham
			OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

			SELECT COUNT(*)
			FROM CaKham
			WHERE TrangThai = N'Đã đặt'
			AND NgayKham >= CAST(GETDATE() AS DATE)
		";

		using var cmd = new SqlCommand(sql, conn);

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
	public async Task<string?> GetFcmTokenByCaKhamIdAsync(int caKhamId)
	{
		const string sql = @"
        SELECT tk.FcmToken
        FROM CaKham ck
        JOIN ThongTinCaNhan tt ON ck.ThongTinID = tt.ThongTinID
        JOIN TaiKhoan tk ON tt.TaiKhoanID = tk.TaiKhoanID
        WHERE ck.CaKhamID = @Id";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = caKhamId;
		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		return result?.ToString();
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
	public async Task<(List<CaKhamListReadModel>, int)> GetByThongTinAsync(int thongTinID, int page, int size)
	{
		var list = new List<CaKhamListReadModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
        {BaseSelectList}
        WHERE ck.ThongTinID=@ThongTin
        ORDER BY ck.CaKhamID DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

        SELECT COUNT(*)
        FROM CaKham
        WHERE ThongTinID=@ThongTin";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@ThongTin", SqlDbType.Int).Value = thongTinID;
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

	public async Task<int> InsertAsync(CaKham entity)
	{
		const string sql = @"
        INSERT INTO CaKham (LoaiCaKham, KhungGioID, NgayKham, TrangThai, NhanVienID, LichLamViecID, PhongChucNangID )
        VALUES (@Loai, @Khung, @Ngay, N'Trống', @NhanVien, @Lich, @Phong )";

		using var conn = new SqlConnection(_connectionString);
		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@Loai", entity.LoaiCaKham);
		cmd.Parameters.AddWithValue("@Khung", entity.KhungGioID);
		cmd.Parameters.AddWithValue("@Ngay", entity.NgayKham);
		cmd.Parameters.Add("@NhanVien", SqlDbType.Int) .Value = (object?)entity.NhanVienID ?? DBNull.Value;
		cmd.Parameters.Add("@Lich", SqlDbType.Int) .Value = (object?)entity.LichLamViecID ?? DBNull.Value;
		cmd.Parameters.Add("@Phong", SqlDbType.Int) .Value = (object?)entity.PhongChucNangID ?? DBNull.Value;

		await conn.OpenAsync();
		return await cmd.ExecuteNonQueryAsync();
	}

	public async Task<int> UpdateAsync(CaKham entity)
	{
		using var conn = new SqlConnection(_connectionString);

		var sql = @"
        UPDATE CaKham
        SET ThongTinID=@ThongTin,
            LyDoKham=@LyDo,
            TrangThai=@TrangThai,
            NgayDat=@NgayDat,
            GhiChu=@GhiChu
        WHERE CaKhamID=@Id";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = entity.CaKhamID;
		cmd.Parameters.Add("@ThongTin", SqlDbType.Int).Value = (object?)entity.ThongTinID ?? DBNull.Value;
		cmd.Parameters.Add("@LyDo", SqlDbType.NVarChar, 500).Value = (object?)entity.LyDoKham ?? DBNull.Value;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = entity.TrangThai;
		cmd.Parameters.Add("@NgayDat", SqlDbType.DateTime).Value = (object?)entity.NgayDat ?? DBNull.Value;
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, -1).Value = (object?)entity.GhiChu ?? DBNull.Value;

		await conn.OpenAsync();
		return await cmd.ExecuteNonQueryAsync();
	}

	public async Task<int> CountAsync(DateTime ngay, int khungGioId, string loaiCa)
	{
		using var conn = new SqlConnection(_connectionString);

		var sql = @"SELECT COUNT(*) FROM CaKham
                    WHERE NgayKham=@Ngay
                    AND KhungGioID=@Khung
                    AND LoaiCaKham=@Loai";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = ngay.Date;
		cmd.Parameters.Add("@Khung", SqlDbType.Int).Value = khungGioId;
		cmd.Parameters.Add("@Loai", SqlDbType.NVarChar, 50).Value = loaiCa;

		await conn.OpenAsync();
		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
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
	public async Task<bool> ExistsAsync(DateTime ngay, int khungGioId, string loaiCa)
	{
		using var conn = new SqlConnection(_connectionString);

		var sql = @"SELECT 1 FROM CaKham
                    WHERE NgayKham=@Ngay
                    AND KhungGioID=@Khung
                    AND LoaiCaKham=@Loai";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = ngay.Date;
		cmd.Parameters.Add("@Khung", SqlDbType.Int).Value = khungGioId;
		cmd.Parameters.Add("@Loai", SqlDbType.NVarChar, 50).Value = loaiCa;

		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();

		return result != null;
	}

	public async Task UpdateTrangThaiAsync(int id, string trangThai, string ghiChu)
	{
		using var conn = new SqlConnection(_connectionString);

		var sql = @"UPDATE CaKham
                    SET TrangThai=@TrangThai,GhiChu=@GhiChu
                    WHERE CaKhamID=@Id";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = trangThai;
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, -1).Value = (object?)ghiChu ?? DBNull.Value;
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	#region Mapping

	private CaKham MapToEntity(SqlDataReader r)
	{
		return new CaKham(
			r.GetInt32(r.GetOrdinal("CaKhamID")),
			r.GetString(r.GetOrdinal("LoaiCaKham")),
			r.IsDBNull("NhanVienID") ? null : r.GetInt32("NhanVienID"),
			r.IsDBNull(r.GetOrdinal("LichLamViecID")) ? null : r.GetInt32(r.GetOrdinal("LichLamViecID")),
			r.GetInt32(r.GetOrdinal("KhungGioID")),
			r.IsDBNull(r.GetOrdinal("PhongChucNangID")) ? null : r.GetInt32(r.GetOrdinal("PhongChucNangID")),
			r.IsDBNull(r.GetOrdinal("ThongTinID")) ? null : r.GetInt32(r.GetOrdinal("ThongTinID")),
			r.IsDBNull(r.GetOrdinal("LyDoKham")) ? null : r.GetString(r.GetOrdinal("LyDoKham")),
			r.GetString(r.GetOrdinal("TrangThai")),
			r.IsDBNull(r.GetOrdinal("NgayDat")) ? null : r.GetDateTime(r.GetOrdinal("NgayDat")),
			r.GetDateTime(r.GetOrdinal("NgayKham")),
			r.IsDBNull(r.GetOrdinal("GhiChu")) ? null : r.GetString(r.GetOrdinal("GhiChu"))
		);
	}

	private CaKhamListReadModel MapToListDTO(SqlDataReader r)
	{
		return new CaKhamListReadModel
		{
			CaKhamID = r.GetInt32(r.GetOrdinal("CaKhamID")),
			NhanVien = r.IsDBNull(r.GetOrdinal("NhanVienID")) ? null : new NameResponseDTO
			{
				Id = r.GetInt32(r.GetOrdinal("NhanVienID")),
				Name = r.IsDBNull(r.GetOrdinal("TenBacSi")) ? null : r.GetString(r.GetOrdinal("TenBacSi"))
			},
			LoaiCaKham = r.GetString(r.GetOrdinal("LoaiCaKham")),
			NgayKham = r.GetDateTime(r.GetOrdinal("NgayKham")),
			TenKhungGio = r.GetString(r.GetOrdinal("TenKhung")),
			TenPhong = r.IsDBNull(r.GetOrdinal("TenPhong")) ? null : r.GetString(r.GetOrdinal("TenPhong")),
			HoTen = r.IsDBNull(r.GetOrdinal("TenBenhNhan")) ? null : r.GetString(r.GetOrdinal("TenBenhNhan")),
			LyDoKham = r.IsDBNull(r.GetOrdinal("LyDoKham")) ? null : r.GetString(r.GetOrdinal("LyDoKham")),
			TrangThai = r.GetString(r.GetOrdinal("TrangThai"))
		};
	}

	private CaKhamReadModel MapToDetailDTO(SqlDataReader r)
	{
		return new CaKhamReadModel
		{
			CaKhamID = r.GetInt32(r.GetOrdinal("CaKhamID")),

			NhanVien = r.IsDBNull(r.GetOrdinal("NhanVienID")) ? null : new NameResponseDTO
				{
					Id = r.GetInt32(r.GetOrdinal("NhanVienID")),
					Name = r.IsDBNull(r.GetOrdinal("TenBacSi")) ? null : r.GetString(r.GetOrdinal("TenBacSi"))
				},

			LichLamViecID = r.IsDBNull(r.GetOrdinal("LichLamViecID")) ? null : r.GetInt32(r.GetOrdinal("LichLamViecID")),
			LoaiCaKham = r.GetString(r.GetOrdinal("LoaiCaKham")),
			NgayKham = r.GetDateTime(r.GetOrdinal("NgayKham")),
			TenKhungGio = r.GetString(r.GetOrdinal("TenKhung")),
			TenPhong = r.IsDBNull(r.GetOrdinal("TenPhong")) ? null : r.GetString(r.GetOrdinal("TenPhong")),
			HoTen = r.IsDBNull(r.GetOrdinal("TenBenhNhan")) ? null : r.GetString(r.GetOrdinal("TenBenhNhan")),
			LyDoKham = r.IsDBNull(r.GetOrdinal("LyDoKham")) ? null : r.GetString(r.GetOrdinal("LyDoKham")),
			TrangThai = r.GetString(r.GetOrdinal("TrangThai")),
			NgayDat = r.IsDBNull(r.GetOrdinal("NgayDat")) ? null : r.GetDateTime(r.GetOrdinal("NgayDat")),
			GhiChu = r.IsDBNull(r.GetOrdinal("GhiChu")) ? null : r.GetString(r.GetOrdinal("GhiChu"))
		};
	}

	#endregion
}