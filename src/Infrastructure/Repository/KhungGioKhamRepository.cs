using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class KhungGioKhamRepository : IKhungGioKhamRepository
{
	private readonly string _connectionString;

	public KhungGioKhamRepository(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection")!;
	}

	#region Queries
	private const string BASE = @"
        SELECT KhungGioID, TenKhung, CaLamViec, GioBatDau, GioKetThuc
        FROM KhungGioKham";
	#endregion

	public async Task<List<KhungGioKhamReadModel>> GetAllAsync()
	{
		var list = new List<KhungGioKhamReadModel>();

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = $@" {BASE} ORDER BY CaLamViec, GioBatDau";

		using var cmd = new SqlCommand(sql, conn);
		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToListDTO(reader));

		return list;
	}

	public async Task<KhungGioKhamReadModel?> GetDetailAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BASE + " WHERE KhungGioID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToDetailDTO(reader);

		return null;
	}

	public async Task<KhungGioKham?> GetByIdAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BASE + " WHERE KhungGioID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}

	public async Task<List<int>> ListKhungGioID()
	{
		var list = new List<int>();

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"SELECT KhungGioID FROM KhungGioKham ORDER BY CaLamViec, GioBatDau";

		using var cmd = new SqlCommand(sql, conn);
		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(reader.GetInt32(0));

		return list;
	}

	public async Task<int> CountKhungGioKhamAsync()
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"SELECT COUNT(*) FROM KhungGioKham";

		using var cmd = new SqlCommand(sql, conn);

		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}

	public async Task<List<int>> GetKhungGioIdsByCaLamViecAsync(int caLamViec)
	{
		var list = new List<int>();

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"
        SELECT KhungGioID
        FROM KhungGioKham
        WHERE CaLamViec=@CaLamViec
        ORDER BY GioBatDau";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@CaLamViec", SqlDbType.Int).Value = caLamViec;

		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(reader.GetInt32(0));

		return list;
	}

	public async Task<int> AddAsync(KhungGioKham kg)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"
        INSERT INTO KhungGioKham(CaLamViec,GioBatDau,GioKetThuc,TenKhung)
        VALUES(@CaLamViec,@GioBatDau,@GioKetThuc,@TenKhung)";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@CaLamViec", SqlDbType.Int).Value = kg.CaLamViec;
		cmd.Parameters.Add("@GioBatDau", SqlDbType.Time).Value = kg.GioBatDau;
		cmd.Parameters.Add("@GioKetThuc", SqlDbType.Time).Value = kg.GioKetThuc;
		cmd.Parameters.Add("@TenKhung", SqlDbType.NVarChar, 50).Value = kg.TenKhung;

		int row = await cmd.ExecuteNonQueryAsync();
		return row;
	}

	public async Task<int> UpdateAsync(KhungGioKham kg)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"
        UPDATE KhungGioKham
        SET CaLamViec=@CaLamViec,
            GioBatDau=@GioBatDau,
            GioKetThuc=@GioKetThuc,
            TenKhung=@TenKhung
        WHERE KhungGioID=@Id";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = kg.KhungGioID;
		cmd.Parameters.Add("@CaLamViec", SqlDbType.Int).Value = kg.CaLamViec;
		cmd.Parameters.Add("@GioBatDau", SqlDbType.Time).Value = kg.GioBatDau;
		cmd.Parameters.Add("@GioKetThuc", SqlDbType.Time).Value = kg.GioKetThuc;
		cmd.Parameters.Add("@TenKhung", SqlDbType.NVarChar, 50).Value = kg.TenKhung;

		int row = await cmd.ExecuteNonQueryAsync();
		return row;
	}

	public async Task<int> DeleteAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"DELETE FROM KhungGioKham WHERE KhungGioID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		int row = await cmd.ExecuteNonQueryAsync();
		return row;
	}

	public async Task<List<NameResponseDTO>> GetComboboxAsync()
	{
		var list = new List<NameResponseDTO>();

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"
        SELECT KhungGioID, TenKhung
        FROM KhungGioKham
        ORDER BY TenKhung";

		using var cmd = new SqlCommand(sql, conn);
		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add(new NameResponseDTO
			{
				Id = reader.GetInt32(reader.GetOrdinal("KhungGioID")),
				Name = reader.GetString(reader.GetOrdinal("TenKhung"))
			});
		}

		return list;
	}

	#region Mapping

	private KhungGioKham MapToEntity(SqlDataReader r)
	{
		return new KhungGioKham(
			r.GetInt32(r.GetOrdinal("KhungGioID")),
			r.GetInt32(r.GetOrdinal("CaLamViec")),
			r.GetTimeSpan(r.GetOrdinal("GioBatDau")),
			r.GetTimeSpan(r.GetOrdinal("GioKetThuc")),
			r.GetString(r.GetOrdinal("TenKhung"))
		);
	}

	private KhungGioKhamReadModel MapToListDTO(SqlDataReader r)
	{
		return new KhungGioKhamReadModel
		{
			KhungGioID = r.GetInt32(r.GetOrdinal("KhungGioID")),
			TenKhung = r.GetString(r.GetOrdinal("TenKhung")),
			CaLamViec = r.GetInt32(r.GetOrdinal("CaLamViec")),
			GioBatDau = r.GetTimeSpan(r.GetOrdinal("GioBatDau")),
			GioKetThuc = r.GetTimeSpan(r.GetOrdinal("GioKetThuc"))
		};
	}

	private KhungGioKhamReadModel MapToDetailDTO(SqlDataReader r)
	{
		return new KhungGioKhamReadModel
		{
			KhungGioID = r.GetInt32(r.GetOrdinal("KhungGioID")),
			TenKhung = r.GetString(r.GetOrdinal("TenKhung")),
			CaLamViec = r.GetInt32(r.GetOrdinal("CaLamViec")),
			GioBatDau = r.GetTimeSpan(r.GetOrdinal("GioBatDau")),
			GioKetThuc = r.GetTimeSpan(r.GetOrdinal("GioKetThuc"))
		};
	}

	#endregion
}