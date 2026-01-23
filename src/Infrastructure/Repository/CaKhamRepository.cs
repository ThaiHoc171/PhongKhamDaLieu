using System.Reflection.Metadata;
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
		const string sql = @"SELECT CaKhamID, LichLamViecID, PhongChucNangID, NgayKham , KhungGioID ,BenhNhanID ,LyDoKham ,TrangThai ,NgayDat ,GhiChu
                                FROM CaKham WHERE CaKhamID = @caKhamID";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@caKhamID", caKhamID);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}
	public async Task<List<CaKham>> GetAllAsync()
	{
		const string sql = @"SELECT CaKhamID, LichLamViecID, PhongChucNangID, NgayKham , KhungGioID ,BenhNhanID ,LyDoKham ,TrangThai ,NgayDat ,GhiChu
                                FROM CaKham";
		var list = new List<CaKham>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(MapToEntity(reader));
		}
		return list;
	}
	public async Task<List<CaKham>> LocAsync(DateTime ngayKham, string trangThai)
	{
		const string sql = @"SELECT CaKhamID, LichLamViecID, PhongChucNangID, NgayKham , KhungGioID ,BenhNhanID ,LyDoKham ,TrangThai ,NgayDat ,GhiChu
                                FROM CaKham WHERE NgayKham = @ngayKham AND TrangThai = @trangThai";
		var list = new List<CaKham>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@ngayKham", ngayKham);
		cmd.Parameters.AddWithValue("@trangThai", trangThai);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(MapToEntity(reader));
		}
		return list;
	}
	public async Task<List<CaKham>> GetByBenhNhanAsync(int benhNhanID)
	{
		const string sql = @"SELECT CaKhamID, LichLamViecID, PhongChucNangID, NgayKham , KhungGioID ,BenhNhanID ,LyDoKham ,TrangThai ,NgayDat ,GhiChu
                                FROM CaKham WHERE BenhNhanID = @benhNhanID";
		var list = new List<CaKham>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@benhNhanID", benhNhanID);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(MapToEntity(reader));
		}
		return list;
	}

	public async Task<int> CountByNgayAndKhungGioAsync(DateTime ngay, int khungGioId)
	{
		const string sql = @"SELECT COUNT(CaKhamID) FROM CaKham WHERE NgayKham = @ngayKham AND KhungGioID = @khungGioId";
		var list = new List<CaKham>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@ngayKham", ngay);
		cmd.Parameters.AddWithValue("@KhungGioID", khungGioId);
		await conn.OpenAsync();
		return (int)await cmd.ExecuteScalarAsync();
	}
	public async Task<bool> ExistsAsync(DateTime ngay, int khungGioId, int lichLamViecId)
	{
		const string sql = @"
        SELECT 1
        FROM CaKham
        WHERE NgayKham = @NgayKham
          AND KhungGioID = @KhungGioID
          AND LichLamViecID = @LichLamViecID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@NgayKham", ngay.Date);
		cmd.Parameters.AddWithValue("@KhungGioID", khungGioId);
		cmd.Parameters.AddWithValue("@LichLamViecID", lichLamViecId);

		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		return result != null;
	}
	public async Task<int> AddAsync(CaKham ca)
	{
		const string sql = @"
            INSERT INTO CaKham
            (LichLamViecID, PhongChucNangID, NgayKham, KhungGioID, TrangThai)
            OUTPUT INSERTED.CaKhamID
            VALUES (@LichLamViecID, @PhongChucNangID, @NgayKham, @KhungGioID, @TrangThai)";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@LichLamViecID", ca.LichLamViecID);
		cmd.Parameters.AddWithValue("@PhongChucNangID", ca.PhongChucNangID);
		cmd.Parameters.AddWithValue("@NgayKham", ca.NgayKham);
		cmd.Parameters.AddWithValue("@KhungGioID", ca.KhungGioID);
		cmd.Parameters.AddWithValue("@TrangThai", ca.TrangThai);

		await conn.OpenAsync();
		return (int)await cmd.ExecuteScalarAsync();
	}
	public async Task UpdateAsync(CaKham ca)
	{
		const string sql = @"
            UPDATE CaKham
            SET BenhNhanID = @BenhNhanID,
                LyDoKham = @LyDoKham,
                TrangThai = @TrangThai,
                NgayDat = @NgayDat,
                GhiChu = @GhiChu
            WHERE CaKhamID = @Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@BenhNhanID", ca.BenhNhanID);
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
		const string sql = @"
            UPDATE CaKham
            SET TrangThai = @TrangThai, GhiChu = @GhiChu
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
			lichLamViecID: reader.GetInt32(1),
			phongChucNangID: reader.GetInt32(2),
			ngayKham: reader.GetDateTime(3),
			khungGioID: reader.GetInt32(4),

			benhNhanID: reader.IsDBNull(5) ? null : reader.GetInt32(5),
			lyDoKham: reader.IsDBNull(6) ? null : reader.GetString(6),
			trangThai: reader.GetString(7),
			ngayDat: reader.IsDBNull(8) ? null : reader.GetDateTime(8),
			ghiChu: reader.IsDBNull(9) ? null : reader.GetString(9)
		);
	}
}