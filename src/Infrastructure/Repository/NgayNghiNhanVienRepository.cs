using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class NgayNghiNhanVienRepository : INgayNghiNhanVienRepository
{
	private readonly string _connectionString;

	public NgayNghiNhanVienRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}

	#region Queries

	private const string BaseSelectList = @"
		SELECT nn.NgayNghiID,
			   nn.NhanVienID,
			   ttc.HoTen,
			   nn.Ngay,
			   nn.LyDo
		FROM NgayNghiNhanVien nn
		JOIN NhanVien nv ON nv.NhanVienID = nn.NhanVienID
		JOIN ThongTinCaNhan ttc ON ttc.ThongTinID = nv.ThongTinID";

	#endregion

	// ================= PAGED =================
	public async Task<(List<NgayNghiReadModel>, int)> GetPagedAsync(int page, int size)
	{
		var list = new List<NgayNghiReadModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
		{BaseSelectList}
		ORDER BY nn.Ngay DESC
		OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

		SELECT COUNT(*) FROM NgayNghiNhanVien";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@Size", SqlDbType.Int).Value = size;

		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapReadModel(reader));

		await reader.NextResultAsync();

		if (await reader.ReadAsync())
			total = reader.GetInt32(0);

		return (list, total);
	}

	// ================= SEARCH =================
	public async Task<(List<NgayNghiReadModel>, int)> SearchPagedAsync(string keyword, int page, int size)
	{
		var list = new List<NgayNghiReadModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
		{BaseSelectList}
		WHERE ttc.HoTen LIKE @Keyword OR nn.LyDo LIKE @Keyword
		ORDER BY nn.Ngay DESC
		OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

		SELECT COUNT(*)
		FROM NgayNghiNhanVien nn
		JOIN NhanVien nv ON nv.NhanVienID = nn.NhanVienID
		JOIN ThongTinCaNhan ttc ON ttc.ThongTinID = nv.ThongTinID
		WHERE ttc.HoTen LIKE @Keyword OR nn.LyDo LIKE @Keyword";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 200).Value = $"%{keyword}%";
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@Size", SqlDbType.Int).Value = size;

		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapReadModel(reader));

		await reader.NextResultAsync();

		if (await reader.ReadAsync())
			total = reader.GetInt32(0);

		return (list, total);
	}

	// ================= DETAIL =================
	public async Task<NgayNghiNhanVien?> GetByIdAsync(int id)
	{
		const string sql = @"
		SELECT NgayNghiID, NhanVienID, Ngay, LyDo
		FROM NgayNghiNhanVien
		WHERE NgayNghiID = @Id";

		using var conn = new SqlConnection(_connectionString);
		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		await conn.OpenAsync();

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapEntity(reader);

		return null;
	}
	public async Task<NgayNghiReadModel?> GetDetailAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectList + " WHERE nn.NgayNghiID = @Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapReadModel(reader);

		return null;
	}

	// ================= CRUD =================
	public async Task AddAsync(NgayNghiNhanVien entity)
	{
		const string sql = @"
			INSERT INTO NgayNghiNhanVien (NhanVienID, Ngay, LyDo) 
			VALUES (@NhanVienID, @Ngay, @LyDo)";

		using var conn = new SqlConnection(_connectionString);
		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = entity.NhanVienID;
		cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = entity.Ngay;
		cmd.Parameters.Add("@LyDo", SqlDbType.NVarChar, 500)
			.Value = (object?)entity.LyDo ?? DBNull.Value;

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task UpdateAsync(NgayNghiNhanVien entity)
	{
		const string sql = @"
			UPDATE NgayNghiNhanVien
			SET Ngay=@Ngay, LyDo=@LyDo
			WHERE NgayNghiID=@Id";

		using var conn = new SqlConnection(_connectionString);
		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = entity.NgayNghiID;
		cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = entity.Ngay;
		cmd.Parameters.Add("@LyDo", SqlDbType.NVarChar, 500)
			.Value = (object?)entity.LyDo ?? DBNull.Value;

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task DeleteAsync(int id)
	{
		const string sql = @"DELETE FROM NgayNghiNhanVien WHERE NgayNghiID=@Id";

		using var conn = new SqlConnection(_connectionString);
		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task<bool> ExistsAsync(int nhanVienID, DateTime ngay)
	{
		const string sql = @"
			SELECT 1 FROM NgayNghiNhanVien
			WHERE NhanVienID=@NhanVienID AND Ngay=@Ngay";

		using var conn = new SqlConnection(_connectionString);
		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = nhanVienID;
		cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = ngay.Date;

		await conn.OpenAsync();

		return await cmd.ExecuteScalarAsync() != null;
	}

	// ================= BULK =================
	public async Task BulkInsertAsync(List<NgayNghiNhanVien> list)
	{
		using var conn = new SqlConnection(_connectionString);

		var table = new DataTable();
		table.Columns.Add("NhanVienID", typeof(int));
		table.Columns.Add("Ngay", typeof(DateTime));
		table.Columns.Add("LyDo", typeof(string));

		foreach (var item in list)
		{
			table.Rows.Add(item.NhanVienID, item.Ngay, item.LyDo ?? (object)DBNull.Value);
		}

		using var bulk = new SqlBulkCopy(conn);
		bulk.DestinationTableName = "NgayNghiNhanVien";

		bulk.ColumnMappings.Add("NhanVienID", "NhanVienID");
		bulk.ColumnMappings.Add("Ngay", "Ngay");
		bulk.ColumnMappings.Add("LyDo", "LyDo");

		await conn.OpenAsync();
		await bulk.WriteToServerAsync(table);
	}

	#region Mapping
	private static NgayNghiNhanVien MapEntity(SqlDataReader r)
	{
		return new NgayNghiNhanVien(
			r.GetInt32(r.GetOrdinal("NgayNghiID")),
			r.GetInt32(r.GetOrdinal("NhanVienID")),
			r.GetDateTime(r.GetOrdinal("Ngay")),
			r.IsDBNull(r.GetOrdinal("LyDo"))
				? null
				: r.GetString(r.GetOrdinal("LyDo"))
		);
	}
	private static NgayNghiReadModel MapReadModel(SqlDataReader r)
	{
		return new NgayNghiReadModel
		{
			NgayNghiID = r.GetInt32(r.GetOrdinal("NgayNghiID")),
			NhanVien = new NameResponseDTO
			{
				Id = r.GetInt32(r.GetOrdinal("NhanVienID")),
				Name = r.GetString(r.GetOrdinal("HoTen"))
			},
			Ngay = r.GetDateTime(r.GetOrdinal("Ngay")),
			LyDo = r.IsDBNull(r.GetOrdinal("LyDo")) ? null : r.GetString(r.GetOrdinal("LyDo"))
		};
	}

	#endregion
}