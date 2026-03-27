using Application.DTOs;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace Infrastructure.Repositories;
public class ThietBiRepository : IThietBiRepository
{
    private readonly string _connectionString;
    public ThietBiRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }
    #region Queries

    private const string BaseSelectList = @"
        SELECT ThietBiID, TenTB, LoaiTB, TrangThai
        FROM ThietBi";

    private const string BaseSelectDetail = @"
        SELECT ThietBiID, TenTB, LoaiTB, TrangThai, NgayTao, NgayCapNhat
        FROM ThietBi";

    #endregion
    public async Task<(List<ThietBiReadListModel>, int)> GetPagedAsync(int page, int size)
    {
        var list = new List<ThietBiReadListModel>();
        int total = 0;
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        int offset = (page - 1) * size;
        var sql = $@"
            {BaseSelectList}
            ORDER BY TenTB ASC
            OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;
            SELECT COUNT(*) FROM ThietBi
        ";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
        cmd.Parameters.Add("@Size", SqlDbType.Int).Value = size;
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToDTO(reader));
        await reader.NextResultAsync();
        if (await reader.ReadAsync())
            total = reader.GetInt32(0);
        return (list, total);
    }
    public async Task<(List<ThietBiReadListModel>, int)> SearchPagedAsync(string keyword, int page, int size)
    {
        var list = new List<ThietBiReadListModel>();
        int total = 0;
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        int offset = (page - 1) * size;
        var sql = $@"
            {BaseSelectList}
            WHERE TenTB LIKE @Keyword
            ORDER BY TenTB ASC
            OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;
            SELECT COUNT(*)
            FROM ThietBi
            WHERE TenTB LIKE @Keyword
        ";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 200).Value = $"%{keyword}%";
        cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
        cmd.Parameters.Add("@Size", SqlDbType.Int).Value = size;
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToDTO(reader));
        await reader.NextResultAsync();
        if (await reader.ReadAsync())
            total = reader.GetInt32(0);
        return (list, total);
    }
    public async Task<ThietBiReadModel?> GetDetailAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = BaseSelectDetail + " WHERE ThietBiID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToDetailDTO(reader);
        return null;
    }
    public async Task<ThietBi?> GetByIdAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = BaseSelectDetail + " WHERE ThietBiID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToEntity(reader);
        return null;
    }
    public async Task<int> AddAsync(ThietBi entity)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = "INSERT INTO ThietBi (TenTB, LoaiTB, TrangThai) VALUES (@TenTB, @LoaiTB, @TrangThai)";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@TenTB", SqlDbType.NVarChar, 200).Value = entity.TenTB;
        cmd.Parameters.Add("@LoaiTB", SqlDbType.NVarChar, 100).Value = entity.LoaiTB;
        cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = entity.TrangThai;
        int row = await cmd.ExecuteNonQueryAsync();
        return row;
    }
    public async Task<int> UpdateAsync(ThietBi entity)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = "UPDATE ThietBi SET TenTB=@TenTB, LoaiTB=@LoaiTB, TrangThai=@TrangThai, NgayCapNhat=@NgayCapNhat WHERE ThietBiID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = entity.ThietBiID;
        cmd.Parameters.Add("@TenTB", SqlDbType.NVarChar, 200).Value = entity.TenTB;
        cmd.Parameters.Add("@LoaiTB", SqlDbType.NVarChar, 100).Value = entity.LoaiTB;
        cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = entity.TrangThai;
        cmd.Parameters.Add("@NgayCapNhat", SqlDbType.DateTime).Value = entity.NgayCapNhat;
        int row = await cmd.ExecuteNonQueryAsync();
        return row;
    }
    public async Task BulkInsertAsync(List<ThietBi> list)
    {
        using var conn = new SqlConnection(_connectionString);
        var table = new DataTable();

		table.Columns.Add("TenTB", typeof(string));
		table.Columns.Add("LoaiTB", typeof(string));
		table.Columns.Add("TrangThai", typeof(string));

		foreach (var item in list)
        {
            table.Rows.Add(item.TenTB, item.LoaiTB, item.TrangThai);
        }

        using var bulk = new SqlBulkCopy(conn);

        bulk.DestinationTableName = "ThietBi";

        bulk.ColumnMappings.Add("TenTB", "TenTB");
        bulk.ColumnMappings.Add("LoaiTB", "LoaiTB");
        bulk.ColumnMappings.Add("TrangThai", "TrangThai");

        await conn.OpenAsync();

        await bulk.WriteToServerAsync(table);
    }
	public async Task<List<NameResponseDTO>> GetComboboxAsync()
	{
		var list = new List<NameResponseDTO>();
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();
		var sql = @"SELECT ThietBiID, TenTB
                    FROM ThietBi
                    WHERE TrangThai = N'Hoạt động'
                    ORDER BY TenTB";
		using var cmd = new SqlCommand(sql, conn);
		using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(new NameResponseDTO
			{
				Id = reader.GetInt32(reader.GetOrdinal("ThietBiID")),
				Name = reader.GetString(reader.GetOrdinal("TenTB"))
			});
		}

		return list;
	}
	#region Mapping
	private ThietBi MapToEntity(SqlDataReader r)
    {
        return new ThietBi(
            r.GetInt32(r.GetOrdinal("ThietBiID")),
            r.GetString(r.GetOrdinal("TenTB")),
            r.GetString(r.GetOrdinal("LoaiTB")),
            r.GetString(r.GetOrdinal("TrangThai")),
            r.GetDateTime(r.GetOrdinal("NgayTao")),
            r.IsDBNull(r.GetOrdinal("NgayCapNhat")) ? null : r.GetDateTime(r.GetOrdinal("NgayCapNhat"))
        );
    }

    private ThietBiReadListModel MapToDTO(SqlDataReader r)
    {
        return new ThietBiReadListModel
        {
            ThietBiID = r.GetInt32(r.GetOrdinal("ThietBiID")),
            TenTB = r.GetString(r.GetOrdinal("TenTB")),
            LoaiTB = r.GetString(r.GetOrdinal("LoaiTB")),
            TrangThai = r.GetString(r.GetOrdinal("TrangThai"))
        };
    }

	private ThietBiReadModel MapToDetailDTO(SqlDataReader r)
	{
		return new ThietBiReadModel
		{
			ThietBiID = r.GetInt32(r.GetOrdinal("ThietBiID")),
			TenTB = r.GetString(r.GetOrdinal("TenTB")),
			LoaiTB = r.GetString(r.GetOrdinal("LoaiTB")),
			TrangThai = r.GetString(r.GetOrdinal("TrangThai")),
			NgayTao = r.GetDateTime(r.GetOrdinal("NgayTao")),
			NgayCapNhat = r.IsDBNull(r.GetOrdinal("NgayCapNhat")) ? null : r.GetDateTime(r.GetOrdinal("NgayCapNhat"))
		};
	}
	#endregion
}