using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace Infrastructure.Repository;
public class PhienKhamRepository : IPhienKhamRepository
{
	private readonly string _connectionString;
	public PhienKhamRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new InvalidOperationException("Connection string not found.");
	}
	private SqlConnection CreateConnection() => new(_connectionString);
	private const string BaseJoin =
		@"FROM PhienKham pk
      JOIN BenhNhan bn ON pk.BenhNhanID = bn.BenhNhanID
      JOIN ThongTinCaNhan bn_ttc ON bn.ThongTinID = bn_ttc.ThongTinID
      JOIN NhanVien nv ON pk.NhanVienID = nv.NhanVienID
      JOIN ThongTinCaNhan nv_ttc ON nv.ThongTinID = nv_ttc.ThongTinID";
	private const string BaseSelectLite =
		@"SELECT pk.PhienKhamID, pk.CaKhamID, pk.NgayKham, pk.TrangThai, pk.ChanDoanCuoi,
             bn_ttc.HoTen AS TenBenhNhan,
             nv_ttc.HoTen AS TenNhanVien";
	private const string BaseSelectDetail =
		@"SELECT pk.PhienKhamID, pk.CaKhamID, pk.NgayKham, pk.TrangThai,
             pk.TrieuChung, pk.GhiChu, pk.HinhAnh, pk.ChanDoanCuoi, pk.PhongChucNangID,
             bn.BenhNhanID, bn_ttc.HoTen AS TenBenhNhan,
             nv.NhanVienID, nv_ttc.HoTen AS TenNhanVien";
	public async Task<PhienKham?> GetByIdAsync(int id)
	{
		const string sql =
		@"SELECT PhienKhamID, CaKhamID, BenhNhanID, NhanVienID, PhongChucNangID,
                 TrieuChung, GhiChu, HinhAnh, ChanDoanCuoi, NgayKham, TrangThai
          FROM PhienKham
          WHERE PhienKhamID=@Id";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Id", id);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}
	public async Task<(List<PhienKhamListReadModel>, int)> GetPagedAsync(
		int page,
		int size,
		int? nhanVienID,
		string? trangThai)
	{
		var sql =
		$@"{BaseSelectLite}
           {BaseJoin}
           WHERE (@NhanVienID IS NULL OR pk.NhanVienID=@NhanVienID)
             AND (@TrangThai IS NULL OR pk.TrangThai=@TrangThai)
           ORDER BY pk.NgayKham DESC
           OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
           SELECT COUNT(*)
           FROM PhienKham pk
           WHERE (@NhanVienID IS NULL OR pk.NhanVienID=@NhanVienID)
             AND (@TrangThai IS NULL OR pk.TrangThai=@TrangThai)";
		var list = new List<PhienKhamListReadModel>();
		int total = 0;
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@NhanVienID", (object?)nhanVienID ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@TrangThai", (object?)trangThai ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@Offset", (page - 1) * size);
		cmd.Parameters.AddWithValue("@PageSize", size);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
			list.Add(MapToLiteDTO(reader));
		if (await reader.NextResultAsync() && await reader.ReadAsync())
			total = reader.GetInt32(0);
		return (list, total);
	}
	public async Task<(List<PhienKhamListReadModel>, int)> SearchPagedAsync(string? keyword,int page,int size,int? nhanVienID)
	{
		var sql =
		$@"{BaseSelectLite}
		   {BaseJoin}
		   WHERE (@NhanVienID IS NULL OR pk.NhanVienID=@NhanVienID)
			 AND (@Keyword IS NULL OR bn_ttc.HoTen LIKE @Keyword OR pk.TrieuChung LIKE @Keyword)
		   ORDER BY pk.NgayKham DESC
		   OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
		   SELECT COUNT(*)
		   FROM PhienKham pk
		   JOIN BenhNhan bn ON pk.BenhNhanID = bn.BenhNhanID
		   JOIN ThongTinCaNhan bn_ttc ON bn.ThongTinID = bn_ttc.ThongTinID
		   WHERE (@NhanVienID IS NULL OR pk.NhanVienID=@NhanVienID)
			 AND (@Keyword IS NULL OR bn_ttc.HoTen LIKE @Keyword OR pk.TrieuChung LIKE @Keyword)";
		var list = new List<PhienKhamListReadModel>();
		int total = 0;
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@NhanVienID", (object?)nhanVienID ?? DBNull.Value);
		cmd.Parameters.AddWithValue(
			"@Keyword",
			string.IsNullOrWhiteSpace(keyword)
				? DBNull.Value
				: $"%{keyword}%"
		);
		cmd.Parameters.AddWithValue("@Offset", (page - 1) * size);
		cmd.Parameters.AddWithValue("@PageSize", size);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
			list.Add(MapToLiteDTO(reader));
		if (await reader.NextResultAsync() && await reader.ReadAsync())
			total = reader.GetInt32(0);
		return (list, total);
	}
	public async Task<(List<PhienKhamListReadModel>, int)> GetBenhNhanPagedAsync(
		int benhNhanID,
		int page,
		int size)
	{
		var sql =
		$@"{BaseSelectLite}
           {BaseJoin}
           WHERE pk.BenhNhanID=@BenhNhanID
           ORDER BY pk.NgayKham DESC
           OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
           SELECT COUNT(*)
           FROM PhienKham
           WHERE BenhNhanID=@BenhNhanID";
		var list = new List<PhienKhamListReadModel>();
		int total = 0;
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@BenhNhanID", benhNhanID);
		cmd.Parameters.AddWithValue("@Offset", (page - 1) * size);
		cmd.Parameters.AddWithValue("@PageSize", size);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
			list.Add(MapToLiteDTO(reader));
		if (await reader.NextResultAsync() && await reader.ReadAsync())
			total = reader.GetInt32(0);
		return (list, total);
	}
	public async Task<PhienKhamReadModel?> GetDetailAsync(int id)
	{
		var sql =
		$@"{BaseSelectDetail}
           {BaseJoin}
           WHERE pk.PhienKhamID=@Id";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Id", id);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToDetailDTO(reader) : null;
	}
	public async Task<PhienKhamReadModel?> GetByCaKhamIdAsync(int caKhamId)
	{
		var sql =
		$@"{BaseSelectDetail}
           {BaseJoin}
           WHERE pk.CaKhamID=@CaKhamID";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@CaKhamID", caKhamId);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToDetailDTO(reader) : null;
	}
	public async Task<int?> GetBenhNhanByIdAsync(int id)
	{
		const string sql = @"SELECT BenhNhanID FROM PhienKham WHERE PhienKhamID=@Id";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Id", id);
		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		return result == null ? null : (int)result;
	}
	public async Task<int> AddAsync(PhienKham pk)
	{
		const string sql =
		@"INSERT INTO PhienKham
          (CaKhamID,BenhNhanID,NhanVienID,PhongChucNangID)
          OUTPUT INSERTED.PhienKhamID
          VALUES (@CaKhamID,@BenhNhanID,@NhanVienID,@PhongChucNangID)";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@CaKhamID", pk.CaKhamID);
		cmd.Parameters.AddWithValue("@BenhNhanID", pk.BenhNhanID);
		cmd.Parameters.AddWithValue("@NhanVienID", pk.NhanVienID);
		cmd.Parameters.AddWithValue("@PhongChucNangID", (object?)pk.PhongChucNangID ?? DBNull.Value);
		await conn.OpenAsync();
		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}
	public async Task UpdateAsync(PhienKham pk)
	{
		const string sql =
		@"UPDATE PhienKham
          SET TrieuChung=@TrieuChung,
              GhiChu=@GhiChu,
              HinhAnh=@HinhAnh
          WHERE PhienKhamID=@Id";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@TrieuChung", (object?)pk.TrieuChung ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@GhiChu", (object?)pk.GhiChu ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@HinhAnh", (object?)pk.HinhAnh ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@Id", pk.PhienKhamID);
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task KetThucAsync(PhienKham pk)
	{
		const string sql =
		@"UPDATE PhienKham
          SET ChanDoanCuoi=@ChanDoanCuoi,
              TrangThai=@TrangThai
          WHERE PhienKhamID=@Id";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@ChanDoanCuoi", pk.ChanDoanCuoi);
		cmd.Parameters.AddWithValue("@TrangThai", pk.TrangThai.ToDbValue());
		cmd.Parameters.AddWithValue("@Id", pk.PhienKhamID);
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	private static PhienKham MapToEntity(SqlDataReader r)
	{
		var phienKhamID = r.GetOrdinal("PhienKhamID");
		var caKhamID = r.GetOrdinal("CaKhamID");
		var benhNhanID = r.GetOrdinal("BenhNhanID");
		var nhanVienID = r.GetOrdinal("NhanVienID");
		var phongID = r.GetOrdinal("PhongChucNangID");
		var trieuChung = r.GetOrdinal("TrieuChung");
		var ghiChu = r.GetOrdinal("GhiChu");
		var hinhAnh = r.GetOrdinal("HinhAnh");
		var chanDoan = r.GetOrdinal("ChanDoanCuoi");
		var ngayKham = r.GetOrdinal("NgayKham");
		var trangThai = r.GetOrdinal("TrangThai");
		return new PhienKham(
			r.GetInt32(phienKhamID),
			r.GetInt32(caKhamID),
			r.GetInt32(benhNhanID),
			r.GetInt32(nhanVienID),
			r.GetInt32(phongID),
			r.IsDBNull(trieuChung) ? null : r.GetString(trieuChung),
			r.IsDBNull(ghiChu) ? null : r.GetString(ghiChu),
			r.IsDBNull(hinhAnh) ? null : r.GetString(hinhAnh),
			r.IsDBNull(chanDoan) ? null : r.GetString(chanDoan),
			r.GetDateTime(ngayKham),
			r.GetString(trangThai)
		);
	}
	private static PhienKhamListReadModel MapToLiteDTO(SqlDataReader r)
	{
		var phienKhamID = r.GetOrdinal("PhienKhamID");
		var caKhamID = r.GetOrdinal("CaKhamID");
		var ngayKham = r.GetOrdinal("NgayKham");
		var trangThai = r.GetOrdinal("TrangThai");
		var chanDoan = r.GetOrdinal("ChanDoanCuoi");
		var tenBenhNhan = r.GetOrdinal("TenBenhNhan");
		var tenNhanVien = r.GetOrdinal("TenNhanVien");
		return new PhienKhamListReadModel
		{
			PhienKhamID = r.GetInt32(phienKhamID),
			CaKhamID = r.GetInt32(caKhamID),
			NgayKham = r.GetDateTime(ngayKham),
			TrangThai = r.GetString(trangThai),
			ChanDoanCuoi = r.IsDBNull(chanDoan) ? null : r.GetString(chanDoan),
			BenhNhan = r.GetString(tenBenhNhan),
			NhanVien = r.GetString(tenNhanVien)
		};
	}
	private static PhienKhamReadModel MapToDetailDTO(SqlDataReader r)
	{
		var phienKhamID = r.GetOrdinal("PhienKhamID");
		var caKhamID = r.GetOrdinal("CaKhamID");
		var ngayKham = r.GetOrdinal("NgayKham");
		var trangThai = r.GetOrdinal("TrangThai");
		var trieuChung = r.GetOrdinal("TrieuChung");
		var ghiChu = r.GetOrdinal("GhiChu");
		var hinhAnh = r.GetOrdinal("HinhAnh");
		var chanDoan = r.GetOrdinal("ChanDoanCuoi");
		var phong = r.GetOrdinal("PhongChucNangID");
		var tenBenhNhan = r.GetOrdinal("TenBenhNhan");
		var benhNhanID = r.GetOrdinal("BenhNhanID");
		var tenNhanVien = r.GetOrdinal("TenNhanVien");
		var nhanVienID = r.GetOrdinal("NhanVienID");
		return new PhienKhamReadModel
		{
			PhienKhamID = r.GetInt32(phienKhamID),
			CaKhamID = r.GetInt32(caKhamID),
			NgayKham = r.GetDateTime(ngayKham),
			TrangThai = r.GetString(trangThai),
			TrieuChung = r.IsDBNull(trieuChung) ? null : r.GetString(trieuChung),
			GhiChu = r.IsDBNull(ghiChu) ? null : r.GetString(ghiChu),
			HinhAnh = r.IsDBNull(hinhAnh) ? null : r.GetString(hinhAnh),
			ChanDoanCuoi = r.IsDBNull(chanDoan) ? null : r.GetString(chanDoan),
			PhongChucNangID = r.IsDBNull(phong) ? null : r.GetInt32(phong),
			BenhNhan = new NameResponseDTO
			{
				Id = r.GetInt32(benhNhanID),
				Name = r.GetString(tenBenhNhan)
			},
			NhanVien = new NameResponseDTO
			{
				Id = r.GetInt32(nhanVienID),
				Name = r.GetString(tenNhanVien)
			}
		};
	}
}