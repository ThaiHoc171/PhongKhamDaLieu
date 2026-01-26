using Application.Interfaces;
using Application.ReadModels;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository;

public class PCNThietBiRepository : IPCNThietBiRepository
{
	private readonly string _connectionString;
	public PCNThietBiRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}

	public async Task<int> AddAsync(PCN_TB pcn)
	{	
		const string sql = @"INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID, SoLuong, GhiChu)
						 VALUES (@PCN_Id, @TB_Id, @SoLuong, @GhiChu);
						 SELECT CAST(SCOPE_IDENTITY() as int);";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PCN_Id", pcn.PCN_Id);
		cmd.Parameters.AddWithValue("@TB_Id", pcn.TB_Id);
		cmd.Parameters.AddWithValue("@SoLuong", pcn.SoLuong);
		cmd.Parameters.AddWithValue("@GhiChu", (object?)pcn.GhiChu ?? DBNull.Value);
		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		return Convert.ToInt32(result);
	}
	public async Task DeleteAsync(int id)
	{
		const string sql = @"DELETE FROM PhongChucNang_ThietBi WHERE PCN_TB_ID = @Id;";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Id", id);
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task<PCNThietBiReadModel?> GetByIdAsync(int id)
	{
		const string sql = @"SELECT pcn.PCN_TB_ID, pcn.PhongChucNangID, pcn.ThietBiID, tb.TenTB, pcn.SoLuong, pcn.GhiChu
							FROM PhongChucNang_ThietBi pcn
							JOIN ThietBi tb ON pcn.ThietBiID = tb.ThietBiID
							WHERE pcn.PCN_TB_ID = @Id;";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Id", id);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		var result = await reader.ReadAsync();
		return result ? MapToEntity(reader) : null!;
	}
	public async Task<List<PCNThietBiReadModel>> GetByPCNAsync(int pcnId)
	{
		const string sql = @"SELECT pcn.PCN_TB_ID, pcn.PhongChucNangID, pcn.ThietBiID, tb.TenTB, pcn.SoLuong, pcn.GhiChu
							FROM PhongChucNang_ThietBi pcn
							JOIN ThietBi tb ON pcn.ThietBiID = tb.ThietBiID
							WHERE pcn.PhongChucNangID = @PCN_Id;";
		var results = new List<PCNThietBiReadModel>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PCN_Id", pcnId);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			results.Add(MapToEntity(reader));
		}
		return results;
	}
	public async Task UpdateAsync(PCN_TB entity)
	{
		
		string sql = @"UPDATE PhongChucNang_ThietBi
					   SET ThietBiID = @TB_Id,
						   SoLuong = @SoLuong,
						   GhiChu = @GhiChu
					   WHERE PCN_TB_ID = @Id;";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Id", entity.Id);
		cmd.Parameters.AddWithValue("@TB_Id", entity.TB_Id);
		cmd.Parameters.AddWithValue("@SoLuong", entity.SoLuong);
		cmd.Parameters.AddWithValue("@GhiChu", (object?)entity.GhiChu ?? DBNull.Value);
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
			
	}
	private static PCNThietBiReadModel MapToEntity(SqlDataReader r)
	{
		return new PCNThietBiReadModel
		{
			Id = r.GetInt32(0),
			PCN_Id = r.GetInt32(1),
			TB_Id = r.GetInt32(2),
			TB_Name = r.GetString(3),
			SoLuong = r.GetInt32(4),
			GhiChu = r.IsDBNull(5) ? null : r.GetString(5)
		};
	}
}
