using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class PhienKhamRepository : IPhienKhamRepository
{
	private readonly string _connectionString;

	public PhienKhamRepository(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection")!;
	}

	#region Queries
	private const string BaseSelectList = @"
        SELECT pk.PhienKhamID,pk.CaKhamID,pk.NgayKham,pk.TrangThai,pk.ChanDoanCuoi,
               bn_ttc.HoTen AS TenBenhNhan,
               nv_ttc.HoTen AS TenNhanVien
		FROM PhienKham pk
        JOIN BenhNhan bn ON pk.BenhNhanID = bn.BenhNhanID
        JOIN ThongTinCaNhan bn_ttc ON bn.ThongTinID = bn_ttc.ThongTinID
        JOIN NhanVien nv ON pk.NhanVienID = nv.NhanVienID
        JOIN ThongTinCaNhan nv_ttc ON nv.ThongTinID = nv_ttc.ThongTinID";

	private const string BaseSelectDetail = @"

		SELECT pk.PhienKhamID,pk.CaKhamID,pk.NgayKham,pk.TrangThai,
               pk.TrieuChung,pk.GhiChu,pk.HinhAnh,pk.ChanDoanCuoi,pk.PhongChucNangID,
               bn.BenhNhanID,bn_ttc.HoTen AS TenBenhNhan,
               nv.NhanVienID,nv_ttc.HoTen AS TenNhanVien
		FROM PhienKham pk
        JOIN BenhNhan bn ON pk.BenhNhanID = bn.BenhNhanID
        JOIN ThongTinCaNhan bn_ttc ON bn.ThongTinID = bn_ttc.ThongTinID
        JOIN NhanVien nv ON pk.NhanVienID = nv.NhanVienID
        JOIN ThongTinCaNhan nv_ttc ON nv.ThongTinID = nv_ttc.ThongTinID";

	#endregion


	public async Task<PhienKham?> GetByIdAsync(int id)
	{
		const string sql = @"

		SELECT PhienKhamID,CaKhamID,BenhNhanID,NhanVienID,PhongChucNangID,
               TrieuChung,GhiChu,HinhAnh,ChanDoanCuoi,NgayKham,TrangThai
        FROM PhienKham
        WHERE PhienKhamID=@Id";

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}


	public async Task<(List<PhienKhamReadListModel>, int)> GetPagedAsync(int page, int size, int? nhanVienID, string? trangThai)
	{
		var list = new List<PhienKhamReadListModel>();
		int total = 0;
		int offset = (page - 1) * size;

		var sql = $@"
        {BaseSelectList}
        WHERE (@NhanVienID IS NULL OR pk.NhanVienID=@NhanVienID)
          AND (@TrangThai IS NULL OR pk.TrangThai=@TrangThai)
        ORDER BY pk.NgayKham DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

        SELECT COUNT(*)
        FROM PhienKham pk
        WHERE (@NhanVienID IS NULL OR pk.NhanVienID=@NhanVienID)
          AND (@TrangThai IS NULL OR pk.TrangThai=@TrangThai)";

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = (object?)nhanVienID ?? DBNull.Value;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = (object?)trangThai ?? DBNull.Value;
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

	public async Task<(List<PhienKhamReadListModel>, int)> GetBenhNhanPagedAsync(int benhNhanID, int page, int size)
	{
		var list = new List<PhienKhamReadListModel>();
		int total = 0;
		int offset = (page - 1) * size;

		var sql = $@"
        {BaseSelectList}
        WHERE pk.BenhNhanID=@BenhNhanID
        ORDER BY pk.NgayKham DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

        SELECT COUNT(*)
        FROM PhienKham
        WHERE BenhNhanID=@BenhNhanID";

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@BenhNhanID", SqlDbType.Int).Value = benhNhanID;
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
	public async Task<(List<PhienKhamReadListModel>, int)> SearchPagedAsync(string? keyword, int page, int size, int? nhanVienID)
	{
		var list = new List<PhienKhamReadListModel>();
		int total = 0;
		int offset = (page - 1) * size;

		var sql = $@"
        {BaseSelectList}
        WHERE (@NhanVienID IS NULL OR pk.NhanVienID=@NhanVienID)
          AND (@Keyword IS NULL OR bn_ttc.HoTen LIKE @Keyword OR pk.TrieuChung LIKE @Keyword)
        ORDER BY pk.NgayKham DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

        SELECT COUNT(*)
        FROM PhienKham pk
        JOIN BenhNhan bn ON pk.BenhNhanID = bn.BenhNhanID
        JOIN ThongTinCaNhan bn_ttc ON bn.ThongTinID = bn_ttc.ThongTinID
        WHERE (@NhanVienID IS NULL OR pk.NhanVienID=@NhanVienID)
          AND (@Keyword IS NULL OR bn_ttc.HoTen LIKE @Keyword OR pk.TrieuChung LIKE @Keyword)";

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = (object?)nhanVienID ?? DBNull.Value;
		cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 255).Value =
			string.IsNullOrWhiteSpace(keyword) ? DBNull.Value : $"%{keyword}%";
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


	public async Task<PhienKhamReadModel?> GetDetailAsync(int id)
	{
		var sql = $@"
        {BaseSelectDetail}
        WHERE pk.PhienKhamID=@Id";

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToDetailDTO(reader);

		return null;
	}
	public async Task<PhienKhamReadModel?> GetByCaKhamIdAsync(int id)
	{
		var sql = $@"
        {BaseSelectDetail}
        WHERE pk.CaKhamID=@Id";

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToDetailDTO(reader);

		return null;
	}
	public async Task<int?> GetBenhNhanByIdAsync(int phienKhamID)
	{
		const string sql = @"
        SELECT BenhNhanID
        FROM PhienKham	
        WHERE PhienKhamID=@Id";

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = phienKhamID;

		var result = await cmd.ExecuteScalarAsync();

		if (result == null || result == DBNull.Value)
			return null;

		return Convert.ToInt32(result);
	}
	public async Task<int> AddAsync(PhienKham pk)
	{
		const string sql = @"
        INSERT INTO PhienKham
        (CaKhamID,BenhNhanID,NhanVienID,PhongChucNangID)
        OUTPUT INSERTED.PhienKhamID
        VALUES(@CaKhamID,@BenhNhanID,@NhanVienID,@PhongChucNangID)";

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@CaKhamID", SqlDbType.Int).Value = pk.CaKhamID;
		cmd.Parameters.Add("@BenhNhanID", SqlDbType.Int).Value = pk.BenhNhanID;
		cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = pk.NhanVienID;
		cmd.Parameters.Add("@PhongChucNangID", SqlDbType.Int).Value = (object?)pk.PhongChucNangID ?? DBNull.Value;

		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}

	public async Task<int> UpdateAsync(PhienKham pk)
	{
		const string sql = @"
        UPDATE PhienKham
        SET TrieuChung=@TrieuChung,
            GhiChu=@GhiChu,
            HinhAnh=@HinhAnh
        WHERE PhienKhamID=@Id";

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@TrieuChung", SqlDbType.NVarChar).Value = (object?)pk.TrieuChung ?? DBNull.Value;
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = (object?)pk.GhiChu ?? DBNull.Value;
		cmd.Parameters.Add("@HinhAnh", SqlDbType.NVarChar).Value = (object?)pk.HinhAnh ?? DBNull.Value;
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = pk.PhienKhamID;
		var row = await cmd.ExecuteNonQueryAsync();
		return Convert.ToInt32(row);
	}
	public async Task<int> KetThucAsync(PhienKham pk)
	{
		const string sql = @"
        UPDATE PhienKham
        SET ChanDoanCuoi=@ChanDoanCuoi,
            TrangThai=@TrangThai
        WHERE PhienKhamID=@Id";

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@ChanDoanCuoi", SqlDbType.NVarChar).Value = pk.ChanDoanCuoi;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = pk.TrangThai.ToDbValue();
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = pk.PhienKhamID;
		var row = await cmd.ExecuteNonQueryAsync();
		return Convert.ToInt32(row);
	}
	#region Mapping
	private PhienKham MapToEntity(SqlDataReader r)
	{
		return new PhienKham(
			r.GetInt32(r.GetOrdinal("PhienKhamID")),
			r.GetInt32(r.GetOrdinal("CaKhamID")),
			r.GetInt32(r.GetOrdinal("BenhNhanID")),
			r.GetInt32(r.GetOrdinal("NhanVienID")),
			r.GetInt32(r.GetOrdinal("PhongChucNangID")),
			r.IsDBNull(r.GetOrdinal("TrieuChung")) ? null : r.GetString(r.GetOrdinal("TrieuChung")),
			r.IsDBNull(r.GetOrdinal("GhiChu")) ? null : r.GetString(r.GetOrdinal("GhiChu")),
			r.IsDBNull(r.GetOrdinal("HinhAnh")) ? null : r.GetString(r.GetOrdinal("HinhAnh")),
			r.IsDBNull(r.GetOrdinal("ChanDoanCuoi")) ? null : r.GetString(r.GetOrdinal("ChanDoanCuoi")),
			r.GetDateTime(r.GetOrdinal("NgayKham")),
			r.GetString(r.GetOrdinal("TrangThai"))
		);
	}

	private PhienKhamReadListModel MapToListDTO(SqlDataReader r)
	{
		return new PhienKhamReadListModel
		{
			PhienKhamID = r.GetInt32(r.GetOrdinal("PhienKhamID")),
			CaKhamID = r.GetInt32(r.GetOrdinal("CaKhamID")),
			NgayKham = r.GetDateTime(r.GetOrdinal("NgayKham")),
			TrangThai = r.GetString(r.GetOrdinal("TrangThai")),
			ChanDoanCuoi = r.IsDBNull(r.GetOrdinal("ChanDoanCuoi")) ? null : r.GetString(r.GetOrdinal("ChanDoanCuoi")),
			BenhNhan = r.GetString(r.GetOrdinal("TenBenhNhan")),
			NhanVien = r.GetString(r.GetOrdinal("TenNhanVien"))
		};
	}

	private PhienKhamReadModel MapToDetailDTO(SqlDataReader r)
	{
		return new PhienKhamReadModel
		{
			PhienKhamID = r.GetInt32(r.GetOrdinal("PhienKhamID")),
			CaKhamID = r.GetInt32(r.GetOrdinal("CaKhamID")),
			NgayKham = r.GetDateTime(r.GetOrdinal("NgayKham")),
			TrangThai = r.GetString(r.GetOrdinal("TrangThai")),
			TrieuChung = r.IsDBNull(r.GetOrdinal("TrieuChung")) ? null : r.GetString(r.GetOrdinal("TrieuChung")),
			GhiChu = r.IsDBNull(r.GetOrdinal("GhiChu")) ? null : r.GetString(r.GetOrdinal("GhiChu")),
			HinhAnh = r.IsDBNull(r.GetOrdinal("HinhAnh")) ? null : r.GetString(r.GetOrdinal("HinhAnh")),
			ChanDoanCuoi = r.IsDBNull(r.GetOrdinal("ChanDoanCuoi")) ? null : r.GetString(r.GetOrdinal("ChanDoanCuoi")),
			PhongChucNangID = r.IsDBNull(r.GetOrdinal("PhongChucNangID")) ? null : r.GetInt32(r.GetOrdinal("PhongChucNangID")),
			BenhNhan = new NameResponseDTO
			{
				Id = r.GetInt32(r.GetOrdinal("BenhNhanID")),
				Name = r.GetString(r.GetOrdinal("TenBenhNhan"))
			},
			NhanVien = new NameResponseDTO
			{
				Id = r.GetInt32(r.GetOrdinal("NhanVienID")),
				Name = r.GetString(r.GetOrdinal("TenNhanVien"))
			}
		};
	}

	#endregion
}