using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class PhienKhamCLSRepository : IPhienKhamCLSRepository
{
	private readonly string _connectionString;

	public PhienKhamCLSRepository(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection")!;
	}

	#region Queries

	private const string BaseSelect = @"
        SELECT PhienKham_CanLamSangID, PhienKhamID, CanLamSangID,
               TrangThai, KetQua, FileDinhKem, NgayThucHien,
               NhanVienChiDinhID, NhanVienThucHienID, GhiChu
        FROM PhienKham_CanLamSang";

	private const string BaseListJoin = @"
        SELECT pk.PhienKham_CanLamSangID, cls.TenCLS, pk.TrangThai, pk.KetQua, pk.NgayThucHien, pk.GhiChu
        FROM PhienKham_CanLamSang pk
        JOIN CanLamSang cls ON pk.CanLamSangID = cls.CanLamSangID";

	#endregion

	public async Task<PhienKhamCLS?> GetByIdAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelect + " WHERE PhienKham_CanLamSangID = @ID";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}

	public async Task<List<PhienKhamClsReadListModel>> GetByPhienKhamAsync(int phienKhamID)
	{
		var list = new List<PhienKhamClsReadListModel>();

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseListJoin + @"
            WHERE pk.PhienKhamID = @PhienKhamID
            ORDER BY pk.NgayThucHien";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = phienKhamID;

		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToListDTO(reader));

		return list;
	}

	public async Task<PhienKhamClsReadModel?> GetDetailAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"
        SELECT pk.PhienKham_CanLamSangID, cls.TenCLS, pk.TrangThai,
               pk.KetQua, pk.FileDinhKem, pk.NgayThucHien,
               ttcd.HoTen, nvth.NhanVienID, ttth.HoTen, pk.GhiChu
        FROM PhienKham_CanLamSang pk
        JOIN CanLamSang cls ON pk.CanLamSangID = cls.CanLamSangID
        JOIN NhanVien nvcd ON pk.NhanVienChiDinhID = nvcd.NhanVienID
        JOIN ThongTinCaNhan ttcd ON nvcd.ThongTinID = ttcd.ThongTinID
        LEFT JOIN NhanVien nvth ON pk.NhanVienThucHienID = nvth.NhanVienID
        LEFT JOIN ThongTinCaNhan ttth ON nvth.ThongTinID = ttth.ThongTinID
        WHERE pk.PhienKham_CanLamSangID = @ID";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (!await reader.ReadAsync())
			return null;

		return MapToDetailDTO(reader);
	}
	public async Task<(List<PhienKhamClsReadListModel>, int)> 
		GetPagedAsync(string? trangThai, int page, int size)
	{
		var list = new List<PhienKhamClsReadListModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
        {BaseListJoin}
        WHERE (@TrangThai IS NULL OR pk.TrangThai = @TrangThai)
        ORDER BY pk.PhienKham_CanLamSangID DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

        SELECT COUNT(*)
        FROM PhienKham_CanLamSang pk
        WHERE (@TrangThai IS NULL OR pk.TrangThai = @TrangThai)";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value =
			(object?)trangThai ?? DBNull.Value;

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
	public async Task<(List<PhienKhamClsReadListModel>, int)> 
		SearchPagedAsync(string keyword, string? trangThai, int page, int size)
	{
		var list = new List<PhienKhamClsReadListModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
        {BaseListJoin}
        WHERE cls.TenCLS LIKE @Keyword
        AND (@TrangThai IS NULL OR pk.TrangThai = @TrangThai)
        ORDER BY pk.PhienKham_CanLamSangID DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

        SELECT COUNT(*)
        FROM PhienKham_CanLamSang pk
        JOIN CanLamSang cls ON pk.CanLamSangID = cls.CanLamSangID
        WHERE cls.TenCLS LIKE @Keyword
        AND (@TrangThai IS NULL OR pk.TrangThai = @TrangThai)";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 200).Value = $"%{keyword}%";
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value =
			(object?)trangThai ?? DBNull.Value;

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

	public async Task<int> AddAsync(PhienKhamCLS phienKhamCLS)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"
        INSERT INTO PhienKham_CanLamSang
        (PhienKhamID, CanLamSangID, NhanVienChiDinhID, GhiChu)
        VALUES
        (@PhienKhamID, @CanLamSangID, @NhanVienChiDinhID, @GhiChu)";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = phienKhamCLS.PhienKhamID;
		cmd.Parameters.Add("@CanLamSangID", SqlDbType.Int).Value = phienKhamCLS.CLSID;
		cmd.Parameters.Add("@NhanVienChiDinhID", SqlDbType.Int).Value = phienKhamCLS.NhanVienChiDinhID;
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value =
			(object?)phienKhamCLS.GhiChu ?? DBNull.Value;
		int row = await cmd.ExecuteNonQueryAsync();
		return row;
	}

	public async Task<int> UpdateAsync(PhienKhamCLS phienKhamCLS)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"
        UPDATE PhienKham_CanLamSang
        SET TrangThai = @TrangThai,
            KetQua = @KetQua,
            FileDinhKem = @FileDinhKem,
            NhanVienThucHienID = @NhanVienThucHienID,
            GhiChu = @GhiChu
        WHERE PhienKham_CanLamSangID = @ID";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@ID", SqlDbType.Int).Value = phienKhamCLS.PhienKhamCLSID;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar).Value = phienKhamCLS.TrangThai.ToDbValue();
		cmd.Parameters.Add("@KetQua", SqlDbType.NVarChar).Value = (object?)phienKhamCLS.KetQua ?? DBNull.Value;
		cmd.Parameters.Add("@FileDinhKem", SqlDbType.NVarChar).Value = (object?)phienKhamCLS.FileDinhKem ?? DBNull.Value;
		cmd.Parameters.Add("@NhanVienThucHienID", SqlDbType.Int).Value =
			(object?)phienKhamCLS.NhanVienThucHienID ?? DBNull.Value;
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value =
			(object?)phienKhamCLS.GhiChu ?? DBNull.Value;
		int row = await cmd.ExecuteNonQueryAsync();
		return row;
	}

	#region Mapping

	private PhienKhamCLS MapToEntity(SqlDataReader r)
	{
		return new PhienKhamCLS(
			r.GetInt32(0),
			r.GetInt32(1),
			r.GetInt32(2),
			TrangThaiCLSExtensions.ToEnum(r.GetString(3)),
			r.IsDBNull(4) ? null : r.GetString(4),
			r.IsDBNull(5) ? null : r.GetString(5),
			r.IsDBNull(6) ? null : r.GetDateTime(6),
			r.GetInt32(7),
			r.IsDBNull(8) ? null : r.GetInt32(8),
			r.IsDBNull(9) ? null : r.GetString(9)
		);
	}

	private PhienKhamClsReadListModel MapToListDTO(SqlDataReader r)
	{
		return new PhienKhamClsReadListModel
		{
			PhienKhamCLSID = r.GetInt32(0),
			TenCLS = r.GetString(1),
			TrangThai = r.GetString(2),
			KetQua = r.IsDBNull(3) ? null : r.GetString(3),
			NgayThucHien = r.IsDBNull(4) ? null : r.GetDateTime(4),
			GhiChu = r.IsDBNull(5) ? null : r.GetString(5)
		};
	}

	private PhienKhamClsReadModel MapToDetailDTO(SqlDataReader r)
	{
		return new PhienKhamClsReadModel
		{
			PhienKhamCLSID = r.GetInt32(0),
			TenCLS = r.GetString(1),
			TrangThai = r.GetString(2),
			KetQua = r.IsDBNull(3) ? null : r.GetString(3),
			FileDinhKem = r.IsDBNull(4) ? null : r.GetString(4),
			NgayThucHien = r.IsDBNull(5) ? null : r.GetDateTime(5),
			NhanVienChiDinh = r.GetString(6),
			NhanVienThucHien = r.IsDBNull(7) ? null : new NameResponseDTO
			{
				Id = r.GetInt32(7),
				Name = r.GetString(8)
			},
			GhiChu = r.IsDBNull(9) ? null : r.GetString(9)
		};
	}

	#endregion
}