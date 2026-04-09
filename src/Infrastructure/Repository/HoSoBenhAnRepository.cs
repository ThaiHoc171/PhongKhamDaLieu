using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class HoSoBenhAnRepository : IHoSoBenhAnRepository
{
	private readonly string _connectionString;

	public HoSoBenhAnRepository(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection")!;
	}

	#region Queries
	private const string BaseSelectList = @"
        SELECT HoSoBenhAnID,BenhNhanID,NgayTao
        FROM HoSoBenhAn";

	private const string BaseSelectDetail = @"
        SELECT HoSoBenhAnID,BenhNhanID,BenhNen,DiUng,
               TienSuBenh,TienSuGiaDinh,ThoiQuenSong,
               ThongTinKhac,NgayTao,NgayCapNhat
        FROM HoSoBenhAn";
	#endregion


	public async Task<(List<HoSoBenhAnListReadModel>, int)> GetPagedAsync(int page, int size)
	{
		var list = new List<HoSoBenhAnListReadModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
        {BaseSelectList}
        ORDER BY NgayCapNhat DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

        SELECT COUNT(*)
        FROM HoSoBenhAn";

		using var cmd = new SqlCommand(sql, conn);

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


	public async Task<(List<HoSoBenhAnListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size)
	{
		var list = new List<HoSoBenhAnListReadModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
        {BaseSelectList}
        WHERE BenhNen LIKE @Keyword OR DiUng LIKE @Keyword
        ORDER BY NgayCapNhat DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

        SELECT COUNT(*)
        FROM HoSoBenhAn
        WHERE BenhNen LIKE @Keyword OR DiUng LIKE @Keyword";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, -1).Value = $"%{keyword}%";
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


	public async Task<HoSoBenhAnReadModel?> GetDetailAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectDetail + " WHERE HoSoBenhAnID=@Id";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToDetailDTO(reader);

		return null;
	}


	public async Task<HoSoBenhAn?> GetByIdAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectDetail + " WHERE HoSoBenhAnID=@Id";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}


	public async Task<HoSoBenhAnReadModel?> GetByBenhNhanIdAsync(int benhNhanId)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectDetail + " WHERE BenhNhanID=@BenhNhanID";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@BenhNhanID", SqlDbType.Int).Value = benhNhanId;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToDetailDTO(reader);

		return null;
	}


	public async Task<int> AddAsync(HoSoBenhAn hs)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"INSERT INTO HoSoBenhAn
                (BenhNhanID, BenhNen, DiUng, TienSuBenh, TienSuGiaDinh, ThoiQuenSong,ThongTinKhac)
                VALUES
                (@BenhNhanID, @BenhNen, @DiUng, @TienSuBenh, @TienSuGiaDinh, @ThoiQuenSong, @ThongTinKhac)";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@BenhNhanID", SqlDbType.Int).Value = hs.BenhNhanID;
		cmd.Parameters.Add("@BenhNen", SqlDbType.NVarChar).Value = (object?)hs.BenhNen ?? DBNull.Value;
		cmd.Parameters.Add("@DiUng", SqlDbType.NVarChar).Value = (object?)hs.DiUng ?? DBNull.Value;
		cmd.Parameters.Add("@TienSuBenh", SqlDbType.NVarChar).Value = (object?)hs.TienSuBenh ?? DBNull.Value;
		cmd.Parameters.Add("@TienSuGiaDinh", SqlDbType.NVarChar).Value = (object?)hs.TienSuGiaDinh ?? DBNull.Value;
		cmd.Parameters.Add("@ThoiQuenSong", SqlDbType.NVarChar).Value = (object?)hs.ThoiQuenSong ?? DBNull.Value;
		cmd.Parameters.Add("@ThongTinKhac", SqlDbType.NVarChar).Value = (object?)hs.ThongTinKhac ?? DBNull.Value;
		int row = await cmd.ExecuteNonQueryAsync();

		return row;
	}


	public async Task<int> UpdateAsync(HoSoBenhAn hs)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"UPDATE HoSoBenhAn
                SET BenhNen=@BenhNen,
                    DiUng=@DiUng,
                    TienSuBenh=@TienSuBenh,
                    TienSuGiaDinh=@TienSuGiaDinh,
                    ThoiQuenSong=@ThoiQuenSong,
                    ThongTinKhac=@ThongTinKhac,
                    NgayCapNhat=@NgayCapNhat
                WHERE HoSoBenhAnID=@Id";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = hs.HoSoBenhAnID;
		cmd.Parameters.Add("@BenhNen", SqlDbType.NVarChar).Value = (object?)hs.BenhNen ?? DBNull.Value;
		cmd.Parameters.Add("@DiUng", SqlDbType.NVarChar).Value = (object?)hs.DiUng ?? DBNull.Value;
		cmd.Parameters.Add("@TienSuBenh", SqlDbType.NVarChar).Value = (object?)hs.TienSuBenh ?? DBNull.Value;
		cmd.Parameters.Add("@TienSuGiaDinh", SqlDbType.NVarChar).Value = (object?)hs.TienSuGiaDinh ?? DBNull.Value;
		cmd.Parameters.Add("@ThoiQuenSong", SqlDbType.NVarChar).Value = (object?)hs.ThoiQuenSong ?? DBNull.Value;
		cmd.Parameters.Add("@ThongTinKhac", SqlDbType.NVarChar).Value = (object?)hs.ThongTinKhac ?? DBNull.Value;
		cmd.Parameters.Add("@NgayCapNhat", SqlDbType.DateTime).Value = hs.NgayCapNhat;

		int row = await cmd.ExecuteNonQueryAsync();

		return row;
	}


	#region Mapping

	private HoSoBenhAn MapToEntity(SqlDataReader r)
	{
		return new HoSoBenhAn(
			r.GetInt32(r.GetOrdinal("HoSoBenhAnID")),
			r.GetInt32(r.GetOrdinal("BenhNhanID")),
			r["BenhNen"] as string,
			r["DiUng"] as string,
			r["TienSuBenh"] as string,
			r["TienSuGiaDinh"] as string,
			r["ThoiQuenSong"] as string,
			r["ThongTinKhac"] as string,
			r.GetDateTime(r.GetOrdinal("NgayTao")),
			r.GetDateTime(r.GetOrdinal("NgayCapNhat"))
		);
	}

	private HoSoBenhAnListReadModel MapToListDTO(SqlDataReader r)
	{
		return new HoSoBenhAnListReadModel
		{
			HoSoBenhAnID = r.GetInt32(r.GetOrdinal("HoSoBenhAnID")),
			BenhNhanID = r.GetInt32(r.GetOrdinal("BenhNhanID")),
			NgayTao = r.GetDateTime(r.GetOrdinal("NgayTao"))
		};
	}

	private HoSoBenhAnReadModel MapToDetailDTO(SqlDataReader r)
	{
		return new HoSoBenhAnReadModel
		{
			HoSoBenhAnID = r.GetInt32(r.GetOrdinal("HoSoBenhAnID")),
			BenhNhanID = r.GetInt32(r.GetOrdinal("BenhNhanID")),
			BenhNen = r["BenhNen"] as string,
			DiUng = r["DiUng"] as string,
			TienSuBenh = r["TienSuBenh"] as string,
			TienSuGiaDinh = r["TienSuGiaDinh"] as string,
			ThoiQuenSong = r["ThoiQuenSong"] as string,
			ThongTinKhac = r["ThongTinKhac"] as string,
			NgayTao = r.GetDateTime(r.GetOrdinal("NgayTao")),
			NgayCapNhat = r.IsDBNull(r.GetOrdinal("NgayCapNhat"))
			? DateTime.MinValue
			: r.GetDateTime(r.GetOrdinal("NgayCapNhat"))
		};
	}

	#endregion
}