using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Domain.Enums;

namespace Infrastructure.Repository;

public class ChiTietPCNThietBiRepository : IChiTietPCNThietBiRepository
{
	private readonly string _connectionString;
	public ChiTietPCNThietBiRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}

	public async Task<List<ChiTietPCNThietBi>> GetByPCNTBIdAsync(int pcnTbId)
	{
		const string sql = @"
			SELECT ChiTietID, PCN_TB_ID, MaTaiSan, NgayNhap, TinhTrang, GhiChu
			FROM ChiTiet_PCNTB
			WHERE PCN_TB_ID = @PCN_TB_ID
		";

		var list = new List<ChiTietPCNThietBi>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PCN_TB_ID", pcnTbId);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add(MapToEntity(reader));
		}

		return list;
	}
	public async Task<ChiTietPCNThietBi?> GetByIdAsync(int chiTietId)
	{
		const string sql = @"
			SELECT ChiTietID, PCN_TB_ID, MaTaiSan, NgayNhap, TinhTrang, GhiChu
			FROM ChiTiet_PCNTB
			WHERE ChiTietID = @ChiTietID
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@ChiTietID", chiTietId);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}
	public async Task AddAsync(ChiTietPCNThietBi chiTiet)
	{
		const string sql = @"
			INSERT INTO ChiTiet_PCNTB (PCN_TB_ID, MaTaiSan, TinhTrang, GhiChu)
			VALUES (@PCN_TB_ID, @MaTaiSan, @TinhTrang, @GhiChu)
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@PCN_TB_ID", chiTiet.PCN_TB_ID);
		cmd.Parameters.AddWithValue("@MaTaiSan", chiTiet.MaTaiSan);
		cmd.Parameters.AddWithValue("@TinhTrang", chiTiet.TinhTrang.ToDbValue());
		cmd.Parameters.AddWithValue("@GhiChu", (object?)chiTiet.GhiChu ?? DBNull.Value);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task UpdateAsync(ChiTietPCNThietBi chiTiet)
	{
		const string sql = @"
			UPDATE ChiTiet_PCNTB
			SET TinhTrang = @TinhTrang,
			    GhiChu = @GhiChu
			WHERE ChiTietID = @ChiTietID
		";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@ChiTietID", chiTiet.ChiTietID);
		cmd.Parameters.AddWithValue("@TinhTrang", chiTiet.TinhTrang.ToDbValue());
		cmd.Parameters.AddWithValue("@GhiChu", (object?)chiTiet.GhiChu ?? DBNull.Value);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task DeleteAsync(int chiTietId)
	{
		const string sql = @"DELETE FROM ChiTiet_PCNTB WHERE ChiTietID = @ChiTietID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@ChiTietID", chiTietId);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}


	private static ChiTietPCNThietBi MapToEntity(SqlDataReader reader)
	{
		return new ChiTietPCNThietBi(
			chiTietId: reader.GetInt32(0),
			pcnTbId: reader.GetInt32(1),
			maTaiSan: reader.GetString(2),
			ngayNhap: reader.GetDateTime(3),
			tinhTrangDb: reader.GetString(4),
			ghiChu: reader.IsDBNull(5) ? null : reader.GetString(5)
		);
	}

}