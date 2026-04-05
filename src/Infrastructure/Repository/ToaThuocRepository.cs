using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class ToaThuocRepository : IToaThuocRepository
{
	private readonly string _connectionString;

	public ToaThuocRepository(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection")!;
	}

	#region Queries

	private const string BaseSelectList = @"
        SELECT t.ToaThuocID,t.NgayLap,t.GhiChu,tt.HoTen
        FROM ToaThuoc t
        INNER JOIN NhanVien nv ON t.NhanVienKeDonID = nv.NhanVienID
        INNER JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID";

	private const string BaseSelectDetail = @"
        SELECT t.ToaThuocID,t.PhienKhamID,t.NhanVienKeDonID,t.NgayLap,t.GhiChu,
               nv.NhanVienID,tt.HoTen
        FROM ToaThuoc t
        INNER JOIN NhanVien nv ON t.NhanVienKeDonID = nv.NhanVienID
        INNER JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID";

	#endregion

	public async Task<bool> ExistsByPhienKhamAsync(int phienKhamID)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		const string sql = @"SELECT 1 FROM ToaThuoc WHERE PhienKhamID=@PhienKhamID";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = phienKhamID;

		var result = await cmd.ExecuteScalarAsync();

		return result != null;
	}

	public async Task<int> AddAsync(ToaThuoc entity)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"INSERT INTO ToaThuoc(PhienKhamID,NhanVienKeDonID,GhiChu)
                    OUTPUT INSERTED.ToaThuocID
                    VALUES(@PhienKhamID,@NhanVienKeDonID,@GhiChu)";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = entity.PhienKhamID;
		cmd.Parameters.Add("@NhanVienKeDonID", SqlDbType.Int).Value = entity.NhanVienKeDonID;
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, -1).Value =
			(object?)entity.GhiChu ?? DBNull.Value;

		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}

	public async Task<ToaThuoc?> GetByIdAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectDetail + " WHERE t.ToaThuocID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}

	public async Task<ToaThuocReadModel?> GetByPhienKhamAsync(int phienKhamID)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectDetail + " WHERE t.PhienKhamID=@PhienKhamID";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = phienKhamID;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToDetailDTO(reader);

		return null;
	}

	public async Task<(List<ToaThuocListReadModel>, int)> GetPagedAsync(int page, int size)
	{
		var list = new List<ToaThuocListReadModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
        {BaseSelectList}
        ORDER BY t.NgayLap DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

        SELECT COUNT(*) FROM ToaThuoc";

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
	public async Task<(List<ToaThuocListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size)
	{
		var list = new List<ToaThuocListReadModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
    {BaseSelectList}
    WHERE tt.HoTen LIKE @Keyword OR t.GhiChu LIKE @Keyword
    ORDER BY t.NgayLap DESC
    OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

    SELECT COUNT(*)
    FROM ToaThuoc t
    INNER JOIN NhanVien nv ON t.NhanVienKeDonID = nv.NhanVienID
    INNER JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
    WHERE tt.HoTen LIKE @Keyword OR t.GhiChu LIKE @Keyword";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 200).Value = $"%{keyword}%";
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
	public async Task DeleteAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		const string sql = @"DELETE FROM ToaThuoc WHERE ToaThuocID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		await cmd.ExecuteNonQueryAsync();
	}

	#region Mapping

	private ToaThuoc MapToEntity(SqlDataReader r)
	{
		return new ToaThuoc(
			r.GetInt32(r.GetOrdinal("ToaThuocID")),
			r.GetInt32(r.GetOrdinal("PhienKhamID")),
			r.GetInt32(r.GetOrdinal("NhanVienKeDonID")),
			r.GetDateTime(r.GetOrdinal("NgayLap")),
			r.IsDBNull(r.GetOrdinal("GhiChu")) ? null : r.GetString(r.GetOrdinal("GhiChu"))
		);
	}

	private ToaThuocListReadModel MapToListDTO(SqlDataReader r)
	{
		return new ToaThuocListReadModel
		{
			ToaThuocID = r.GetInt32(r.GetOrdinal("ToaThuocID")),
			NgayLap = r.GetDateTime(r.GetOrdinal("NgayLap")),
			GhiChu = r.IsDBNull(r.GetOrdinal("GhiChu")) ? null : r.GetString(r.GetOrdinal("GhiChu")),
			NguoiLap = r.GetString(r.GetOrdinal("HoTen"))
		};
	}

	private ToaThuocReadModel MapToDetailDTO(SqlDataReader r)
	{
		return new ToaThuocReadModel
		{
			ToaThuocID = r.GetInt32(r.GetOrdinal("ToaThuocID")),
			PhienKhamID = r.GetInt32(r.GetOrdinal("PhienKhamID")),
			NgayLap = r.GetDateTime(r.GetOrdinal("NgayLap")),
			GhiChu = r.IsDBNull(r.GetOrdinal("GhiChu")) ? null : r.GetString(r.GetOrdinal("GhiChu")),
			NguoiLap = new NameResponseDTO
			{
				Id = r.GetInt32(r.GetOrdinal("NhanVienID")),
				Name = r.GetString(r.GetOrdinal("HoTen"))
			}
		};
	}

	#endregion
}