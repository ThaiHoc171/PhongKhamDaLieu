using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class PhienKhamBenhRepository : IPhienKhamBenhRepository
{
	private readonly string _connectionString;

	public PhienKhamBenhRepository(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection")!;
	}

	#region Queries

	private const string BaseSelect = @"
		SELECT PhienKham_BenhID, PhienKhamID, LoaiBenhID, LoaiChanDoan, GhiChu
		FROM PhienKham_Benh";

	private const string BaseSelectJoin = @"
		SELECT pk.PhienKham_BenhID, pk.PhienKhamID, lb.LoaiBenhID, lb.TenBenh, pk.LoaiChanDoan, pk.GhiChu
		FROM PhienKham_Benh pk
		JOIN LoaiBenh lb ON pk.LoaiBenhID = lb.LoaiBenhID";

	#endregion

	// ================= COUNT =================

	public async Task<int> CountAsync(int phienKhamID)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"SELECT COUNT(*) FROM PhienKham_Benh WHERE PhienKhamID=@PhienKhamID";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = phienKhamID;

		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}

	// ================= CHECK PRIMARY =================

	public async Task<bool> PrimaryExistsAsync(int phienKhamID)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"
			SELECT COUNT(*)
			FROM PhienKham_Benh
			WHERE PhienKhamID=@PhienKhamID
			AND LoaiChanDoan=N'Chẩn đoán chính'";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = phienKhamID;

		var count = Convert.ToInt32(await cmd.ExecuteScalarAsync());

		return count > 0;
	}

	// ================= GET BY ID =================

	public async Task<PhienKhamBenh?> GetByIdAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelect + " WHERE PhienKham_BenhID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}

	// ================= GET BY PHIEN KHAM =================

	public async Task<List<PhienKhamBenhReadModel>> GetByPhienKhamIdAsync(int phienKhamID)
	{
		var list = new List<PhienKhamBenhReadModel>();

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectJoin + " WHERE pk.PhienKhamID=@PhienKhamID";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = phienKhamID;

		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToReadDTO(reader));

		return list;
	}

	// ================= ADD =================

	public async Task<int> AddAsync(PhienKhamBenh entity)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"
			INSERT INTO PhienKham_Benh
			(PhienKhamID, LoaiBenhID, LoaiChanDoan, GhiChu)
			VALUES
			(@PhienKhamID, @LoaiBenhID, @LoaiChanDoan, @GhiChu)";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = entity.PhienKhamID;
		cmd.Parameters.Add("@LoaiBenhID", SqlDbType.Int).Value = entity.LoaiBenhID;
		cmd.Parameters.Add("@LoaiChanDoan", SqlDbType.NVarChar, 50)
			.Value = entity.LoaiChanDoan.ToDbValue();

		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, -1)
			.Value = entity.GhiChu ?? (object)DBNull.Value;

		int row = await cmd.ExecuteNonQueryAsync();
		return row;
	}

	// ================= UPDATE =================

	public async Task<int> UpdateAsync(PhienKhamBenh entity)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"
			UPDATE PhienKham_Benh
			SET LoaiBenhID=@LoaiBenhID,
				LoaiChanDoan=@LoaiChanDoan,
				GhiChu=@GhiChu
			WHERE PhienKham_BenhID=@Id";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@LoaiBenhID", SqlDbType.Int).Value = entity.LoaiBenhID;

		cmd.Parameters.Add("@LoaiChanDoan", SqlDbType.NVarChar, 50)
			.Value = entity.LoaiChanDoan.ToDbValue();

		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, -1)
			.Value = entity.GhiChu ?? (object)DBNull.Value;

		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = entity.PhienKham_BenhID;
		int row = await cmd.ExecuteNonQueryAsync();
		return row;
	}
	// ================= DELETE =================

	public async Task<int> DeleteAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"
		DELETE FROM PhienKham_Benh
		WHERE PhienKham_BenhID = @Id";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		int row = await cmd.ExecuteNonQueryAsync();
		return row;
	}
	#region Mapping

	private PhienKhamBenh MapToEntity(SqlDataReader r)
	{
		return new PhienKhamBenh(
			r.GetInt32(r.GetOrdinal("PhienKham_BenhID")),
			r.GetInt32(r.GetOrdinal("PhienKhamID")),
			r.GetInt32(r.GetOrdinal("LoaiBenhID")),
			r.GetString(r.GetOrdinal("LoaiChanDoan")),
			r.IsDBNull(r.GetOrdinal("GhiChu")) ? null : r.GetString(r.GetOrdinal("GhiChu"))
		);
	}

	private PhienKhamBenhReadModel MapToReadDTO(SqlDataReader r)
	{
		return new PhienKhamBenhReadModel
		{
			Id = r.GetInt32(r.GetOrdinal("PhienKham_BenhID")),
			PhienKhamID = r.GetInt32(r.GetOrdinal("PhienKhamID")),
			LoaiBenh = new NameResponseDTO
			{
				Id = r.GetInt32(r.GetOrdinal("LoaiBenhID")),
				Name = r.GetString(r.GetOrdinal("TenBenh"))
			},
			LoaiChanDoan = r.GetString(r.GetOrdinal("LoaiChanDoan")),
			GhiChu = r.IsDBNull(r.GetOrdinal("GhiChu"))
				? null
				: r.GetString(r.GetOrdinal("GhiChu"))
		};
	}

	#endregion
}