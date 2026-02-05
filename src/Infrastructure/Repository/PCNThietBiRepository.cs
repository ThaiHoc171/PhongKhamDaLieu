using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using static Application.Interfaces.IPCNThietBiRepository;
using Application.Interfaces;

namespace Infrastructure.Repository;

public class PCNThietBiRepository : IPCNThietBiRepository
{
	private readonly string _connectionString;

	public PCNThietBiRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}


	public async Task<int> AddAsync(PCNThietBi entity)
	{
		const string sql = @"
			INSERT INTO PhongChucNang_ThietBi
				(PhongChucNangID, ThietBiID, SoLuong, TinhTrang)
			VALUES
				(@PCN_ID, @TB_ID, @SoLuong, @TinhTrang);

			SELECT CAST(SCOPE_IDENTITY() AS INT);
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@PCN_ID", entity.PhongChucNangID);
		cmd.Parameters.AddWithValue("@TB_ID", entity.ThietBiID);
		cmd.Parameters.AddWithValue("@SoLuong", entity.SoLuong);
		cmd.Parameters.AddWithValue("@TinhTrang", entity.TinhTrang.ToDbValue());

		await conn.OpenAsync();
		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}

	public async Task UpdateAsync(PCNThietBi entity)
	{
		const string sql = @"
			UPDATE PhongChucNang_ThietBi
			SET
				ThietBiID = @TB_ID,
				SoLuong   = @SoLuong,
				TinhTrang = @TinhTrang
			WHERE PCN_TB_ID = @Id;
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@Id", entity.Id);
		cmd.Parameters.AddWithValue("@TB_ID", entity.ThietBiID);
		cmd.Parameters.AddWithValue("@SoLuong", entity.SoLuong);
		cmd.Parameters.AddWithValue("@TinhTrang", entity.TinhTrang.ToDbValue());

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task DeleteAsync(int id)
	{
		const string sql =
			@"DELETE FROM PhongChucNang_ThietBi WHERE PCN_TB_ID = @Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@Id", id);
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task<bool> ExistsAsync(int pcnId, int thietBiId)
	{
		const string sql = @"
			SELECT 1
			FROM PhongChucNang_ThietBi
			WHERE PhongChucNangID = @PCN_ID
			  AND ThietBiID = @TB_ID;
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@PCN_ID", pcnId);
		cmd.Parameters.AddWithValue("@TB_ID", thietBiId);

		await conn.OpenAsync();
		return await cmd.ExecuteScalarAsync() != null;
	}


	public async Task<PCNThietBi?> GetByIdAsync(int id)
	{
		const string sql = @"
			SELECT
				PCN_TB_ID,
				PhongChucNangID,
				ThietBiID,
				SoLuong,
				TinhTrang,
				NgayNhap
			FROM PhongChucNang_ThietBi
			WHERE PCN_TB_ID = @Id;
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@Id", id);
		await conn.OpenAsync();

		await using var reader = await cmd.ExecuteReaderAsync();
		if (!await reader.ReadAsync())
			return null;

		return MapToEntity(reader);
	}
	public async Task<List<PCNThietBi>> GetByPCNAsync(int phongChucNangId)
	{
		const string sql = @"
			SELECT
				PCN_TB_ID,
				PhongChucNangID,
				ThietBiID,
				SoLuong,
				TinhTrang,
				NgayNhap
			FROM PhongChucNang_ThietBi
			WHERE PhongChucNangID = @PCN_ID;
		";

		var result = new List<PCNThietBi>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@PCN_ID", phongChucNangId);
		await conn.OpenAsync();

		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			result.Add(MapToEntity(reader));
		}

		return result;
	}
	public async Task<List<ThietBiNhapRaw>> GetChiTietNhapAsync(int phongId)
	{
		const string sql = @"
			SELECT 
				tb.ThietBiID,
				tb.TenTB,
				pcn_tb.NgayNhap,
				SUM(pcn_tb.SoLuong) AS SoLuong
			FROM PhongChucNang_ThietBi pcn_tb
			JOIN ThietBi tb ON tb.ThietBiID = pcn_tb.ThietBiID
			WHERE pcn_tb.PhongChucNangID = @PhongId
			GROUP BY tb.ThietBiID, tb.TenThietBi, pcn_tb.NgayNhap
		";

		var list = new List<ThietBiNhapRaw>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@PhongId", phongId);
		await conn.OpenAsync();

		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(new ThietBiNhapRaw(
				reader.GetInt32(0),
				reader.GetString(1),
				reader.GetDateTime(2),
				reader.GetInt32(3)
			));
		}

		return list;
	}


	/// Lấy thông tin phòng + tổng số lượng thiết bị
	public async Task<TongTheoPhongRaw?> GetPhongTongAsync(int phongId)
	{
		const string sql = @"
			SELECT 
				pcn.PhongChucNangID,
				pcn.TenPhong,
				ISNULL(SUM(pcn_tb.SoLuong), 0) AS TongSoLuong
			FROM PhongChucNang pcn
			LEFT JOIN PhongChucNang_ThietBi pcn_tb
				ON pcn.PhongChucNangID = pcn_tb.PhongChucNangID
			WHERE pcn.PhongChucNangID = @PhongId
			GROUP BY pcn.PhongChucNangID, pcn.TenPhong
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@PhongId", phongId);
		await conn.OpenAsync();

		await using var reader = await cmd.ExecuteReaderAsync();
		if (!await reader.ReadAsync())
			return null;

		return new TongTheoPhongRaw(
			reader.GetInt32(0),
			reader.GetString(1),
			reader.GetInt32(2)
		);
	}


	private static PCNThietBi MapToEntity(SqlDataReader r)
	{
		return new PCNThietBi(
			id: r.GetInt32(0),
			phongChucNangId: r.GetInt32(1),
			thietBiId: r.GetInt32(2),
			soLuong: r.GetInt32(3),
			tinhTrangDb: r.GetString(4),
			ngayNhap: r.GetDateTime(5)
		);
	}
}
