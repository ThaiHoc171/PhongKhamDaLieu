using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace Infrastructure.Repositories;
public class LieuTrinhDieuTriRepository : ILieuTrinhDieuTriRepository
{
	private readonly string _connectionString;
	public LieuTrinhDieuTriRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new InvalidOperationException("Connection string not found.");
	}
	private SqlConnection CreateConnection() => new(_connectionString);
	private const string BaseJoin =@"
		FROM LieuTrinhDieuTri lt
		JOIN BenhNhan bn ON lt.BenhNhanID = bn.BenhNhanID
		JOIN ThongTinCaNhan ttc ON bn.ThongTinID = ttc.ThongTinID
	";
	private const string BaseSelectLite = @"
		SELECT lt.LieuTrinhID, lt.TenLieuTrinh, lt.TongSoBuoi, lt.TrangThai,
			lt.NgayBatDau, lt.NgayKetThuc, ttc.HoTen AS TenBenhNhan
	";
	private const string BaseSelectDetail =@"
		SELECT lt.LieuTrinhID, lt.BenhNhanID, lt.PhienKhamID, lt.TenLieuTrinh, lt.TongSoBuoi, 
		lt.TrangThai, lt.GhiChu, lt.NgayBatDau, lt.NgayKetThuc, ttc.HoTen AS TenBenhNhan
	";
	public async Task<LieuTrinhDieuTri?> GetByIdAsync(int id)
	{
		const string sql =@"
			SELECT LieuTrinhID, BenhNhanID, PhienKhamID, TenLieuTrinh,
				TongSoBuoi, TrangThai, GhiChu, NgayBatDau, NgayKetThuc
			FROM LieuTrinhDieuTri
			WHERE LieuTrinhID=@Id
		";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}
	public async Task<LieuTrinhDieuTri?> GetByBenhNhanIdAsync(int benhNhanID)
	{
		const string sql =@"
			SELECT TOP 1 LieuTrinhID, BenhNhanID, PhienKhamID, TenLieuTrinh,
				TongSoBuoi, TrangThai, GhiChu, NgayBatDau, NgayKetThuc
			FROM LieuTrinhDieuTri
			WHERE BenhNhanID=@BenhNhanID
			ORDER BY NgayBatDau DESC
		";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@BenhNhanID", SqlDbType.Int).Value = benhNhanID;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}
	public async Task<int?> GetIdByBenhNhanIdAsync(int benhNhanID)
	{
		const string sql =@"
			SELECT TOP 1 LieuTrinhID
			FROM LieuTrinhDieuTri
			WHERE BenhNhanID=@BenhNhanID
			ORDER BY NgayBatDau DESC
		";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@BenhNhanID", SqlDbType.Int).Value = benhNhanID;
		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		return result == null ? null : (int)result;
	}
	public async Task<(List<LieuTrinhDieuTriListReadModel>, int)> GetPagedAsync(int page,int size,string? trangThai)
	{
		var sql =
		$@"{BaseSelectLite}
	   {BaseJoin}
	   WHERE (@TrangThai IS NULL OR lt.TrangThai=@TrangThai)
	   ORDER BY lt.NgayBatDau DESC
	   OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
	   SELECT COUNT(*)
	   FROM LieuTrinhDieuTri lt
	   WHERE (@TrangThai IS NULL OR lt.TrangThai=@TrangThai)";
		var list = new List<LieuTrinhDieuTriListReadModel>();
		int total = 0;
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value =
			(object?)trangThai ?? DBNull.Value;
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = (page - 1) * size;
		cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = size;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
			list.Add(MapToLiteDTO(reader));
		if (await reader.NextResultAsync() && await reader.ReadAsync())
			total = reader.GetInt32(0);
		return (list, total);
	}
	public async Task<(List<LieuTrinhDieuTriListReadModel>, int)> SearchAsync(	string? keyword,int page,int size)
	{
		var sql =$@"
			{BaseSelectLite}
			{BaseJoin}
			WHERE (@Keyword IS NULL OR lt.TenLieuTrinh LIKE @Keyword OR ttc.HoTen LIKE @Keyword)
			ORDER BY lt.NgayBatDau DESC
			OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
			SELECT COUNT(*)
			FROM LieuTrinhDieuTri lt
			JOIN BenhNhan bn ON lt.BenhNhanID = bn.BenhNhanID
			JOIN ThongTinCaNhan ttc ON bn.ThongTinID = ttc.ThongTinID
			WHERE (@Keyword IS NULL OR lt.TenLieuTrinh LIKE @Keyword OR ttc.HoTen LIKE @Keyword)
		";
		var list = new List<LieuTrinhDieuTriListReadModel>();
		int total = 0;
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 255).Value =
			string.IsNullOrWhiteSpace(keyword)
				? DBNull.Value
				: $"%{keyword}%";
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = (page - 1) * size;
		cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = size;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
			list.Add(MapToLiteDTO(reader));
		if (await reader.NextResultAsync() && await reader.ReadAsync())
			total = reader.GetInt32(0);
		return (list, total);
	}
	public async Task<(List<LieuTrinhDieuTriListReadModel>, int)> GetBenhNhanPagedAsync(int benhNhanID, int page, int size)
	{
		var sql =$@"
			{BaseSelectLite}
			{BaseJoin}
			WHERE lt.BenhNhanID=@BenhNhanID
			ORDER BY lt.NgayBatDau DESC
			OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
			SELECT COUNT(*)
			FROM LieuTrinhDieuTri
			WHERE BenhNhanID=@BenhNhanID
		";
		var list = new List<LieuTrinhDieuTriListReadModel>();
		int total = 0;
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@BenhNhanID", SqlDbType.Int).Value = benhNhanID;
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = (page - 1) * size;
		cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = size;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
			list.Add(MapToLiteDTO(reader));
		if (await reader.NextResultAsync() && await reader.ReadAsync())
			total = reader.GetInt32(0);
		return (list, total);
	}
	public async Task<LieuTrinhDieuTriReadModel?> GetDetailAsync(int id)
	{
		var sql =
		$@"{BaseSelectDetail}
           {BaseJoin}
           WHERE lt.LieuTrinhID=@Id";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToDetailDTO(reader) : null;
	}
	public async Task<int> AddAsync(LieuTrinhDieuTri lt)
	{
		const string sql =
		@"INSERT INTO LieuTrinhDieuTri
          (BenhNhanID,PhienKhamID,TenLieuTrinh,TongSoBuoi,GhiChu,NgayBatDau,NgayKetThuc)
          OUTPUT INSERTED.LieuTrinhID
          VALUES (@BenhNhanID,@PhienKhamID,@TenLieuTrinh,@TongSoBuoi,@GhiChu,@NgayBatDau,@NgayKetThuc)";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@BenhNhanID", SqlDbType.Int).Value = lt.BenhNhanID;
		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = lt.PhienKhamID;
		cmd.Parameters.Add("@TenLieuTrinh", SqlDbType.NVarChar, 255).Value = lt.TenLieuTrinh;
		cmd.Parameters.Add("@TongSoBuoi", SqlDbType.Int).Value = lt.TongSoBuoi;
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = (object?)lt.GhiChu ?? DBNull.Value;
		cmd.Parameters.Add("@NgayBatDau", SqlDbType.DateTime).Value = lt.NgayBatDau;
		cmd.Parameters.Add("@NgayKetThuc", SqlDbType.DateTime).Value = lt.NgayKetThuc;
		await conn.OpenAsync();
		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}
	public async Task UpdateAsync(LieuTrinhDieuTri lt)
	{
		const string sql =
		@"UPDATE LieuTrinhDieuTri
          SET TenLieuTrinh=@TenLieuTrinh,TongSoBuoi=@TongSoBuoi,NgayKetThuc=@NgayKetThuc
          WHERE LieuTrinhID=@Id";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TenLieuTrinh", SqlDbType.NVarChar, 255).Value = lt.TenLieuTrinh;
		cmd.Parameters.Add("@TongSoBuoi", SqlDbType.Int).Value = lt.TongSoBuoi;
		cmd.Parameters.Add("@NgayKetThuc", SqlDbType.DateTime).Value = lt.NgayKetThuc;
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = lt.LieuTrinhID;
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task UpdateTrangThaiAsync(LieuTrinhDieuTri lt)
	{
		const string sql =
		@"UPDATE LieuTrinhDieuTri
          SET TrangThai=@TrangThai,GhiChu=@GhiChu
          WHERE LieuTrinhID=@Id";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = lt.TrangThai.ToDb();
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = (object?)lt.GhiChu ?? DBNull.Value;
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = lt.LieuTrinhID;
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	private static LieuTrinhDieuTri MapToEntity(SqlDataReader r)
	{
		var id = r.GetOrdinal("LieuTrinhID");
		var benhNhan = r.GetOrdinal("BenhNhanID");
		var phienKham = r.GetOrdinal("PhienKhamID");
		var ten = r.GetOrdinal("TenLieuTrinh");
		var soBuoi = r.GetOrdinal("TongSoBuoi");
		var trangThai = r.GetOrdinal("TrangThai");
		var ghiChu = r.GetOrdinal("GhiChu");
		var ngayBD = r.GetOrdinal("NgayBatDau");
		var ngayKT = r.GetOrdinal("NgayKetThuc");
		return new LieuTrinhDieuTri(
			r.GetInt32(id),
			r.GetInt32(benhNhan),
			r.GetInt32(phienKham),
			r.GetString(ten),
			r.GetInt32(soBuoi),
			r.GetString(trangThai),
			r.IsDBNull(ghiChu) ? null : r.GetString(ghiChu),
			r.GetDateTime(ngayBD),
			r.GetDateTime(ngayKT)
		);
	}
	private static LieuTrinhDieuTriListReadModel MapToLiteDTO(SqlDataReader r)
	{
		return new LieuTrinhDieuTriListReadModel
		{
			LieuTrinhID = r.GetInt32(r.GetOrdinal("LieuTrinhID")),
			TenLieuTrinh = r.GetString(r.GetOrdinal("TenLieuTrinh")),
			BenhNhan = r.GetString(r.GetOrdinal("TenBenhNhan")),
			TongSoBuoi = r.GetInt32(r.GetOrdinal("TongSoBuoi")),
			TrangThai = r.GetString(r.GetOrdinal("TrangThai")),
			NgayBatDau = r.IsDBNull(r.GetOrdinal("NgayBatDau")) ? null : r.GetDateTime(r.GetOrdinal("NgayBatDau")),
			NgayKetThuc = r.IsDBNull(r.GetOrdinal("NgayKetThuc")) ? null : r.GetDateTime(r.GetOrdinal("NgayKetThuc"))
		};
	}
	private static LieuTrinhDieuTriReadModel MapToDetailDTO(SqlDataReader r)
	{
		return new LieuTrinhDieuTriReadModel
		{
			LieuTrinhID = r.GetInt32(r.GetOrdinal("LieuTrinhID")),
			PhienKhamID = r.GetInt32(r.GetOrdinal("PhienKhamID")),
			TenLieuTrinh = r.GetString(r.GetOrdinal("TenLieuTrinh")),
			TongSoBuoi = r.GetInt32(r.GetOrdinal("TongSoBuoi")),
			TrangThai = r.GetString(r.GetOrdinal("TrangThai")),
			GhiChu = r.IsDBNull(r.GetOrdinal("GhiChu")) ? null : r.GetString(r.GetOrdinal("GhiChu")),
			NgayBatDau = r.IsDBNull(r.GetOrdinal("NgayBatDau")) ? null : r.GetDateTime(r.GetOrdinal("NgayBatDau")),
			NgayKetThuc = r.IsDBNull(r.GetOrdinal("NgayKetThuc")) ? null : r.GetDateTime(r.GetOrdinal("NgayKetThuc")),
			BenhNhan = new NameResponseDTO
			{
				Id = r.GetInt32(r.GetOrdinal("BenhNhanID")),
				Name = r.GetString(r.GetOrdinal("TenBenhNhan"))
			}
		};
	}
}