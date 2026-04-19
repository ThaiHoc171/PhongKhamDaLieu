using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class TaiKhamRepository : ITaiKhamRepository
{
	private readonly string _connectionString;

	public TaiKhamRepository(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection")!;
	}

	#region Queries

	private const string BaseSelectList = @"
        SELECT tk.TaiKhamID, bn.BenhNhanID, ttc.HoTen, tk.NgayDuKien, tk.LyDo, tk.TrangThai
        FROM TaiKham tk
        JOIN BenhNhan bn ON tk.BenhNhanID = bn.BenhNhanID
        JOIN ThongTinCaNhan ttc ON bn.ThongTinID = ttc.ThongTinID";

	private const string BaseSelectDetail = @"
        SELECT tk.TaiKhamID, tk.PhienKhamID, bn.BenhNhanID, ttc.HoTen,
               tk.NgayDuKien, tk.LyDo, tk.TrangThai, tk.CaKhamID, tk.NgayTao
        FROM TaiKham tk
        JOIN BenhNhan bn ON tk.BenhNhanID = bn.BenhNhanID
        JOIN ThongTinCaNhan ttc ON bn.ThongTinID = ttc.ThongTinID";

	private const string BaseSelectEntity = @"
        SELECT TaiKhamID, PhienKhamID, BenhNhanID, NgayDuKien, LyDo, TrangThai, CaKhamID, NgayTao
        FROM TaiKham";

	#endregion

	public async Task<TaiKham?> GetByIdAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectEntity + " WHERE TaiKhamID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}
	public async Task<int> GetIdByCaKham(int caKhamId)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();
		var sql = @"SELECT TaiKhamID FROM TaiKham WHERE CaKhamID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = caKhamId;

		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}
	public async Task<TaiKhamReadModel?> GetDetailAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectDetail + " WHERE tk.TaiKhamID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToDetailDTO(reader) : null;
	}
	public async Task<(List<TaiKhamReadListModel>, int)> GetPagedAsync(int page, int size, string? trangThai)
	{
		var list = new List<TaiKhamReadListModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
        {BaseSelectList}
        WHERE (@TrangThai IS NULL OR tk.TrangThai = @TrangThai)
        ORDER BY tk.NgayDuKien DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

        SELECT COUNT(*) FROM TaiKham
        WHERE (@TrangThai IS NULL OR TrangThai = @TrangThai)";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = (object?)trangThai ?? DBNull.Value;
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@Size", SqlDbType.Int).Value = size;

		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToReadModel(reader));

		await reader.NextResultAsync();

		if (await reader.ReadAsync())
			total = reader.GetInt32(0);

		return (list, total);
	}

	public async Task<(List<TaiKhamReadListModel>, int)> SearchAsync(string? keyword, int page, int size)
	{
		var list = new List<TaiKhamReadListModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
        {BaseSelectList}
        WHERE (@Keyword IS NULL OR ttc.HoTen LIKE @Keyword)
        ORDER BY tk.NgayDuKien DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

        SELECT COUNT(*)
        FROM TaiKham tk
        JOIN BenhNhan bn ON tk.BenhNhanID = bn.BenhNhanID
        JOIN ThongTinCaNhan ttc ON bn.ThongTinID = ttc.ThongTinID
        WHERE (@Keyword IS NULL OR ttc.HoTen LIKE @Keyword)";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 200).Value =
			string.IsNullOrWhiteSpace(keyword) ? DBNull.Value : $"%{keyword}%";
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@Size", SqlDbType.Int).Value = size;

		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToReadModel(reader));

		await reader.NextResultAsync();

		if (await reader.ReadAsync())
			total = reader.GetInt32(0);

		return (list, total);
	}
	public async Task<(List<TaiKhamReadListModel>, int)> GetPagedByBenhNhanAsync(int benhNhanID, int page, int size)
	{
		var list = new List<TaiKhamReadListModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
		{BaseSelectList}
		WHERE tk.BenhNhanID=@BenhNhanID
		ORDER BY tk.NgayDuKien DESC
		OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

		SELECT COUNT(*)
		FROM TaiKham
		WHERE BenhNhanID=@BenhNhanID";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@BenhNhanID", SqlDbType.Int).Value = benhNhanID;
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@Size", SqlDbType.Int).Value = size;

		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToReadModel(reader));

		await reader.NextResultAsync();

		if (await reader.ReadAsync())
			total = reader.GetInt32(0);

		return (list, total);
	}
	public async Task<TaiKham?> GetTaiKhamDangChoAsync(int benhNhanID)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectEntity + @"
        WHERE BenhNhanID=@BenhNhanID
        AND TrangThai = N'Chờ khám'
        ORDER BY NgayDuKien DESC";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@BenhNhanID", SqlDbType.Int).Value = benhNhanID;

		using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}
	public async Task<bool> ExistsByPhienKhamAsync(int phienKhamID)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		const string sql = "SELECT 1 FROM TaiKham WHERE PhienKhamID=@PhienKhamID";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = phienKhamID;

		var result = await cmd.ExecuteScalarAsync();
		return result != null;
	}
	public async Task<int> AddAsync(TaiKham tk)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"INSERT INTO TaiKham (PhienKhamID,CaKhamID,BenhNhanID,NgayDuKien,LyDo)
                    OUTPUT INSERTED.TaiKhamID
                    VALUES (@PhienKhamID,@CaKhamID,@BenhNhanID,@NgayDuKien,@LyDo)";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = tk.PhienKhamID;
		cmd.Parameters.Add("@CaKhamID", SqlDbType.Int).Value = tk.CaKhamID;
		cmd.Parameters.Add("@BenhNhanID", SqlDbType.Int).Value = tk.BenhNhanID;
		cmd.Parameters.Add("@NgayDuKien", SqlDbType.Date).Value = tk.NgayDuKien;
		cmd.Parameters.Add("@LyDo", SqlDbType.NVarChar, 500).Value = (object?)tk.LyDo ?? DBNull.Value;

		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}

	public async Task<int> UpdateAsync(TaiKham tk)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"UPDATE TaiKham
                    SET TrangThai=@TrangThai,
                        CaKhamID=@CaKhamID
                    WHERE TaiKhamID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = tk.TaiKhamID;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = TaiKhamExtensions.ToDbValue(tk.TrangThai);
		cmd.Parameters.Add("@CaKhamID", SqlDbType.Int).Value = (object?)tk.CaKhamID ?? DBNull.Value;

		return await cmd.ExecuteNonQueryAsync();
	}

	#region Mapping

	private TaiKham MapToEntity(SqlDataReader r)
	{
		return new TaiKham(
			r.GetInt32(0),
			r.GetInt32(1),
			r.GetInt32(2),
			r.GetDateTime(3),
			r.IsDBNull(4) ? null : r.GetString(4),
			r.IsDBNull(5) ? null : r.GetString(5),
			r.IsDBNull(6) ? null : r.GetInt32(6),
			r.GetDateTime(7)
		);
	}

	private TaiKhamReadListModel MapToReadModel(SqlDataReader r)
	{
		return new TaiKhamReadListModel
		{
			TaiKhamID = r.GetInt32(0),
			BenhNhan = new NameResponseDTO
			{
				Id = r.GetInt32(1),
				Name = r.GetString(2)
			},
			NgayDuKien = r.GetDateTime(3),
			LyDo = r.IsDBNull(4) ? null : r.GetString(4),
			TrangThai = r.IsDBNull(5) ? null : r.GetString(5)
		};
	}

	private TaiKhamReadModel MapToDetailDTO(SqlDataReader r)
	{
		return new TaiKhamReadModel
		{
			TaiKhamID = r.GetInt32(0),
			PhienKhamID = r.GetInt32(1),
			BenhNhan = new NameResponseDTO
			{
				Id = r.GetInt32(2),
				Name = r.GetString(3)
			},
			NgayDuKien = r.GetDateTime(4),
			LyDo = r.IsDBNull(5) ? null : r.GetString(5),
			TrangThai = r.IsDBNull(6) ? null : r.GetString(6),
			CaKhamID = r.IsDBNull(7) ? null : r.GetInt32(7),
			NgayTao = r.GetDateTime(8)
		};
	}

	#endregion
}