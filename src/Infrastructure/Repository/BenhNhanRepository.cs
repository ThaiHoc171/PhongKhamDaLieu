using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class BenhNhanRepository : IBenhNhanRepository
{
	private readonly string _connectionString;

	public BenhNhanRepository(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection")!;
	}

	#region Queries

	private const string BaseSelectList = @"
        SELECT bn.BenhNhanID, bn.ThongTinID, tt.HoTen, tt.NgaySinh, tt.GioiTinh, tt.SDT, tt.EmailLienHe
        FROM BenhNhan bn
        JOIN ThongTinCaNhan tt ON bn.ThongTinID = tt.ThongTinID";

	private const string BaseSelectDetail = @"
        SELECT bn.BenhNhanID, bn.ThongTinID, tt.TaiKhoanID, tt.HoTen, tt.NgaySinh, tt.GioiTinh, tt.SDT, tt.EmailLienHe,
               tt.DiaChi, tt.Avatar, bn.GhiChu, bn.NgayTao, bn.NgayCapNhat
        FROM BenhNhan bn
        JOIN ThongTinCaNhan tt ON bn.ThongTinID = tt.ThongTinID";

	#endregion

	public async Task<bool> ExistsByThongTinIdAsync(int thongTinId)
	{
		const string sql = "SELECT COUNT(*) FROM BenhNhan WHERE ThongTinID=@ThongTinID";

		using var conn = new SqlConnection(_connectionString);
		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@ThongTinID", SqlDbType.Int).Value = thongTinId;

		await conn.OpenAsync();
		int count = await cmd.ExecuteNonQueryAsync();

		return count > 0;
	}

	public async Task<BenhNhan?> GetByIdAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectDetail + " WHERE bn.BenhNhanID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}
	public async Task<BenhNhanReadModel?> GetByThongTinIDAsync(int thongTinId)
	{
		var sql = BaseSelectDetail + " WHERE bn.ThongTinID = @ThongTinID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@ThongTinID", SqlDbType.Int).Value = thongTinId;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (await reader.ReadAsync())
			return MapToDetailDTO(reader);
		return null;
	}
	public async Task<(List<BenhNhanReadListModel>, int)> GetPagedAsync(int page, int size)
	{
		var list = new List<BenhNhanReadListModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
            {BaseSelectList}
            ORDER BY bn.BenhNhanID
            OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

            SELECT COUNT(*)
            FROM BenhNhan";

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

	public async Task<(List<BenhNhanReadListModel>, int)> SearchAsync(string keyword, int page, int size)
	{
		var list = new List<BenhNhanReadListModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
            {BaseSelectList}
            WHERE tt.HoTen LIKE @Keyword OR tt.SDT LIKE @Keyword
            ORDER BY bn.BenhNhanID
            OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

            SELECT COUNT(*)
            FROM BenhNhan bn
            JOIN ThongTinCaNhan tt ON bn.ThongTinID = tt.ThongTinID
            WHERE tt.HoTen LIKE @Keyword OR tt.SDT LIKE @Keyword";

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

	public async Task<BenhNhanReadModel?> GetDetailAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectDetail + " WHERE bn.BenhNhanID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToDetailDTO(reader);

		return null;
	}

	public async Task<int> AddAsync(BenhNhan benhNhan)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		const string sql = @"
            INSERT INTO BenhNhan(ThongTinID, GhiChu)
            OUTPUT INSERTED.BenhNhanID
            VALUES(@ThongTinID, @GhiChu)";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@ThongTinID", SqlDbType.Int).Value = benhNhan.ThongTinID;
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, -1)
			.Value = (object?)benhNhan.GhiChu ?? DBNull.Value;

		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}

	public async Task<int> UpdateAsync(BenhNhan benhNhan)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		const string sql = @"
            UPDATE BenhNhan
            SET GhiChu=@GhiChu,
                NgayCapNhat=@NgayCapNhat
            WHERE BenhNhanID=@Id";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = benhNhan.BenhNhanID;
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, -1)
			.Value = (object?)benhNhan.GhiChu ?? DBNull.Value;
		cmd.Parameters.Add("@NgayCapNhat", SqlDbType.DateTime)
			.Value = benhNhan.NgayCapNhat;

		int row = await cmd.ExecuteNonQueryAsync();

		return row;
	}

	public async Task<List<NameResponseDTO>> GetComboboxAsync()
	{
		var list = new List<NameResponseDTO>();

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		const string sql = @"
            SELECT bn.ThongTinID, tt.HoTen
            FROM BenhNhan bn
            JOIN ThongTinCaNhan tt ON bn.ThongTinID = tt.ThongTinID
            ORDER BY tt.HoTen";

		using var cmd = new SqlCommand(sql, conn);

		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add(new NameResponseDTO
			{
				Id = reader.GetInt32(0),
				Name = reader.GetString(1)
			});
		}

		return list;
	}

	#region Mapping

	private BenhNhan MapToEntity(SqlDataReader r)
	{
		return new BenhNhan(
			r.GetInt32(r.GetOrdinal("BenhNhanID")),
			r.GetInt32(r.GetOrdinal("ThongTinID")),
			 r.GetString(r.GetOrdinal("GhiChu")),
			r.GetDateTime(r.GetOrdinal("NgayTao")),
			r.IsDBNull(r.GetOrdinal("NgayCapNhat"))
				? null
				: r.GetDateTime(r.GetOrdinal("NgayCapNhat"))
		);
	}

	private BenhNhanReadListModel MapToListDTO(SqlDataReader r)
	{
		return new BenhNhanReadListModel
		{
			BenhNhanID = r.GetInt32(r.GetOrdinal("BenhNhanID")),
			ThongTinID = r.GetInt32(r.GetOrdinal("ThongTinID")),
			HoTen = r.GetString(r.GetOrdinal("HoTen")),
			NgaySinh = r.GetDateTime(r.GetOrdinal("NgaySinh")),
			GioiTinh = r.GetString(r.GetOrdinal("GioiTinh")),
			SDT = r.GetString(r.GetOrdinal("SDT")),
			EmailLienHe = r.IsDBNull(r.GetOrdinal("EmailLienHe"))
				? null
				: r.GetString(r.GetOrdinal("EmailLienHe"))
		};
	}

	private BenhNhanReadModel MapToDetailDTO(SqlDataReader r)
	{
		return new BenhNhanReadModel
		{
			BenhNhanID = r.GetInt32(r.GetOrdinal("BenhNhanID")),
			ThongTinID = r.GetInt32(r.GetOrdinal("ThongTinID")),
			TaiKhoanID = r.IsDBNull(r.GetOrdinal("TaiKhoanID")) ? null : r.GetInt32(r.GetOrdinal("TaiKhoanID")),
			HoTen = r.GetString(r.GetOrdinal("HoTen")),
			NgaySinh = r.GetDateTime(r.GetOrdinal("NgaySinh")),
			GioiTinh = r.GetString(r.GetOrdinal("GioiTinh")),
			SDT = r.GetString(r.GetOrdinal("SDT")),
			EmailLienHe = r.IsDBNull(r.GetOrdinal("EmailLienHe"))
				? null
				: r.GetString(r.GetOrdinal("EmailLienHe")),
			DiaChi = r.GetString(r.GetOrdinal("DiaChi")),
			Avatar = r.IsDBNull(r.GetOrdinal("Avatar"))
				? null
				: r.GetString(r.GetOrdinal("Avatar")),
			GhiChu = r.IsDBNull(r.GetOrdinal("GhiChu"))
				? ""
				: r.GetString(r.GetOrdinal("GhiChu")),
			NgayTao = r.GetDateTime(r.GetOrdinal("NgayTao")),
			NgayCapNhat = r.IsDBNull(r.GetOrdinal("NgayCapNhat"))
				? r.GetDateTime(r.GetOrdinal("NgayTao"))
				: r.GetDateTime(r.GetOrdinal("NgayCapNhat"))
		};
	}
	#endregion
}