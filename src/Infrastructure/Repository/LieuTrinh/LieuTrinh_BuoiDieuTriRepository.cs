using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace Infrastructure.Repositories;
public class BuoiDieuTriRepository : IBuoiDieuTriRepository
{
	private readonly string _connectionString;
	public BuoiDieuTriRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new InvalidOperationException("Connection string not found.");
	}
	private SqlConnection CreateConnection() => new(_connectionString);
	private const string BaseSelect = @"
		SELECT BuoiDieuTriID, LieuTrinhID, CaKhamID, SoBuoi, NgayDuKien,
			NgayThucHien, NhanVienID, TrangThai, GhiChu, HinhAnh";
	public async Task<BuoiDieuTri?> GetByIdAsync(int id)
	{
		var sql =$@"
			{BaseSelect}
			FROM LieuTrinh_BuoiDieuTri
			WHERE BuoiDieuTriID=@Id
		";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapEntity(reader) : null;
	}
	public async Task<BuoiDieuTriReadModel?> GetDetailAsync(int id)
	{
		var sql =$@"
			{BaseSelect}
			FROM LieuTrinh_BuoiDieuTri
			WHERE BuoiDieuTriID=@Id
		";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapDetail(reader) : null;
	}
	public async Task<List<BuoiDieuTriListReadModel>> GetByLieuTrinhAsync(int lieuTrinhID)
	{
		var sql =@"
			SELECT BuoiDieuTriID, LieuTrinhID, CaKhamID, SoBuoi, NgayDuKien, TrangThai
			FROM LieuTrinh_BuoiDieuTri
			WHERE LieuTrinhID=@LieuTrinhID
			ORDER BY SoBuoi
		";
		var list = new List<BuoiDieuTriListReadModel>();
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@LieuTrinhID", SqlDbType.Int).Value = lieuTrinhID;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
			list.Add(MapList(reader));
		return list;
	}
	public async Task<bool> ExistsByCaKhamAsync(int caKhamID)
	{
		const string sql =@"
			SELECT 1
			FROM LieuTrinh_BuoiDieuTri
			WHERE CaKhamID=@CaKhamID
		";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@CaKhamID", SqlDbType.Int).Value = caKhamID;
		await conn.OpenAsync();
		return await cmd.ExecuteScalarAsync() != null;
	}
	public async Task<int> CountHoanThanhAsync(int lieuTrinhID)
	{
		const string sql =@"
			SELECT COUNT(*)
			FROM LieuTrinh_BuoiDieuTri
			WHERE LieuTrinhID=@LieuTrinhID AND TrangThai=N'Hoàn thành'
		";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@LieuTrinhID", SqlDbType.Int).Value = lieuTrinhID;
		await conn.OpenAsync();
		return (int)await cmd.ExecuteScalarAsync();
	}
	public async Task<int> GetMaxSoBuoiAsync(int lieuTrinhID)
	{
		const string sql =@"
			SELECT ISNULL(MAX(SoBuoi),0)
			FROM LieuTrinh_BuoiDieuTri
			WHERE LieuTrinhID=@LieuTrinhID
		";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@LieuTrinhID", SqlDbType.Int).Value = lieuTrinhID;
		await conn.OpenAsync();
		return (int)await cmd.ExecuteScalarAsync();
	}
	public async Task<BuoiDieuTri?> GetLastAsync(int lieuTrinhID)
	{
		var sql =$@"
			{BaseSelect}
			FROM LieuTrinh_BuoiDieuTri
			WHERE LieuTrinhID=@LieuTrinhID AND NgayThucHien IS NOT NULL
			ORDER BY NgayThucHien DESC
		";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@LieuTrinhID", SqlDbType.Int).Value = lieuTrinhID;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapEntity(reader) : null;
	}
	public async Task<int> AddAsync(BuoiDieuTri buoi)
	{
		const string sql =@"
			INSERT INTO LieuTrinh_BuoiDieuTri (LieuTrinhID,CaKhamID,SoBuoi,NgayDuKien)
			OUTPUT INSERTED.BuoiDieuTriID VALUES (@LieuTrinhID,@CaKhamID,@SoBuoi,@NgayDuKien)
		";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@LieuTrinhID", SqlDbType.Int).Value = buoi.LieuTrinhID;
		cmd.Parameters.Add("@CaKhamID", SqlDbType.Int).Value = buoi.CaKhamID;
		cmd.Parameters.Add("@SoBuoi", SqlDbType.Int).Value = buoi.SoBuoi;
		cmd.Parameters.Add("@NgayDuKien", SqlDbType.DateTime).Value =
			(object?)buoi.NgayDuKien ?? DBNull.Value;
		await conn.OpenAsync();
		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}
	public async Task UpdateAsync(BuoiDieuTri buoi)
	{
		const string sql =@"
			UPDATE LieuTrinh_BuoiDieuTri
			SET TrangThai=@TrangThai,
				NhanVienID=@NhanVienID,
				NgayThucHien=@NgayThucHien,
				GhiChu=@GhiChu,
				HinhAnh=@HinhAnh
			WHERE BuoiDieuTriID=@Id
		";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar).Value = buoi.TrangThai.ToDb();
		cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value =
			(object?)buoi.NhanVienID ?? DBNull.Value;
		cmd.Parameters.Add("@NgayThucHien", SqlDbType.DateTime).Value =
			(object?)buoi.NgayThucHien ?? DBNull.Value;
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value =
			(object?)buoi.GhiChu ?? DBNull.Value;
		cmd.Parameters.Add("@HinhAnh", SqlDbType.NVarChar).Value =
			(object?)buoi.HinhAnhJSON ?? DBNull.Value;
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = buoi.BuoiDieuTriID;
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	private static BuoiDieuTri MapEntity(SqlDataReader r)
	{
		return new BuoiDieuTri(
			r.GetInt32(r.GetOrdinal("BuoiDieuTriID")),
			r.GetInt32(r.GetOrdinal("LieuTrinhID")),
			r.GetInt32(r.GetOrdinal("CaKhamID")),
			r.GetInt32(r.GetOrdinal("SoBuoi")),
			r.IsDBNull(r.GetOrdinal("NgayDuKien")) ? null : r.GetDateTime(r.GetOrdinal("NgayDuKien")),
			r.IsDBNull(r.GetOrdinal("NgayThucHien")) ? null : r.GetDateTime(r.GetOrdinal("NgayThucHien")),
			r.IsDBNull(r.GetOrdinal("NhanVienID")) ? null : r.GetInt32(r.GetOrdinal("NhanVienID")),
			r.GetString(r.GetOrdinal("TrangThai")),
			r.IsDBNull(r.GetOrdinal("GhiChu")) ? null : r.GetString(r.GetOrdinal("GhiChu")),
			r.IsDBNull(r.GetOrdinal("HinhAnh")) ? null : r.GetString(r.GetOrdinal("HinhAnh"))
		);
	}
	private static BuoiDieuTriListReadModel MapList(SqlDataReader r)
	{
		return new BuoiDieuTriListReadModel
		{
			BuoiDieuTriID = r.GetInt32(r.GetOrdinal("BuoiDieuTriID")),
			LieuTrinhID = r.GetInt32(r.GetOrdinal("LieuTrinhID")),
			CaKhamID = r.GetInt32(r.GetOrdinal("CaKhamID")),
			SoBuoi = r.GetInt32(r.GetOrdinal("SoBuoi")),
			NgayDuKien = r.IsDBNull(r.GetOrdinal("NgayDuKien")) ? null : r.GetDateTime(r.GetOrdinal("NgayDuKien")),
			TrangThai = r.GetString(r.GetOrdinal("TrangThai"))
		};
	}
	private static BuoiDieuTriReadModel MapDetail(SqlDataReader r)
	{
		return new BuoiDieuTriReadModel
		{
			BuoiDieuTriID = r.GetInt32(r.GetOrdinal("BuoiDieuTriID")),
			LieuTrinhID = r.GetInt32(r.GetOrdinal("LieuTrinhID")),
			CaKhamID = r.GetInt32(r.GetOrdinal("CaKhamID")),
			SoBuoi = r.GetInt32(r.GetOrdinal("SoBuoi")),
			NgayDuKien = r.IsDBNull(r.GetOrdinal("NgayDuKien")) ? null : r.GetDateTime(r.GetOrdinal("NgayDuKien")),
			NgayThucHien = r.IsDBNull(r.GetOrdinal("NgayThucHien")) ? null : r.GetDateTime(r.GetOrdinal("NgayThucHien")),
			NhanVienID = r.IsDBNull(r.GetOrdinal("NhanVienID")) ? null : r.GetInt32(r.GetOrdinal("NhanVienID")),
			TrangThai = r.GetString(r.GetOrdinal("TrangThai")),
			GhiChu = r.IsDBNull(r.GetOrdinal("GhiChu")) ? null : r.GetString(r.GetOrdinal("GhiChu")),
			HinhAnhJSON = r.IsDBNull(r.GetOrdinal("HinhAnh")) ? null : r.GetString(r.GetOrdinal("HinhAnh"))
		};
	}
}