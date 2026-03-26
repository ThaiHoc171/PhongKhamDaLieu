using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace Infrastructure.Repository;
public class CanLamSangRepository : ICanLamSangRepository
{
    private readonly string _connectionString;
    public CanLamSangRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string not found.");
    }
    private SqlConnection CreateConnection() => new(_connectionString);
	#region Queries
	private const string BaseSelectLite =
    @"SELECT CanLamSangID, TenCLS, LoaiXetNghiem, TrangThai, NgayTao
      FROM CanLamSang";
    private const string BaseSelectDetail =
    @"SELECT CanLamSangID, TenCLS, MoTa, LoaiXetNghiem, TrangThai, NgayTao
      FROM CanLamSang";
	#endregion
	public async Task<CanLamSang?> GetByIdAsync(int id)
    {
        var sql = BaseSelectDetail + " WHERE CanLamSangID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
		await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }
    public async Task<(List<CanLamSangListReadModel>, int)>GetPagedAsync(int page, int size)
    {
        var sql =$@"
            {BaseSelectLite}
            ORDER BY TenCLS
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
            SELECT COUNT(*)
            FROM CanLamSang
        ";
        var list = new List<CanLamSangListReadModel>();
        int total = 0;
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = (page - 1) * size;
		cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = size;
		await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToLiteDTO(reader));
        if (await reader.NextResultAsync() && await reader.ReadAsync())
            total = reader.GetInt32(0);
        return (list, total);
    }
    public async Task<(List<CanLamSangListReadModel>, int)>SearchPagedAsync(string keyword, int page, int size)
    {
        var sql = $@"
            {BaseSelectLite}
            WHERE TenCLS LIKE @Keyword
            ORDER BY TenCLS
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
            SELECT COUNT(*)
            FROM CanLamSang
            WHERE TenCLS LIKE @Keyword";
        var list = new List<CanLamSangListReadModel>();
        int total = 0;
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar).Value = $"%{keyword}%";
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = (page - 1) * size;
		cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = size;
		await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToLiteDTO(reader));
        if (await reader.NextResultAsync() && await reader.ReadAsync())
            total = reader.GetInt32(0);
        return (list, total);
    }
    public async Task<List<CanLamSangListReadModel>>GetByLoaiXetNghiemAsync(string loaiXetNghiem)
    {
        var sql =
        $@"{BaseSelectLite}
           WHERE LoaiXetNghiem=@Loai
           ORDER BY TenCLS";
        var list = new List<CanLamSangListReadModel>();
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Loai", SqlDbType.NVarChar).Value = loaiXetNghiem;
		await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToLiteDTO(reader));
        return list;
    }
    public async Task<CanLamSangReadModel?> GetDetailAsync(int id)
    {
        var sql = BaseSelectDetail + " WHERE CanLamSangID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
		await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToDetailDTO(reader) : null;
    }
    public async Task<int> AddAsync(CanLamSang cls)
    {
        const string sql = @"
            INSERT INTO CanLamSang (TenCLS, MoTa, LoaiXetNghiem, TrangThai)
            VALUES (@TenCLS, @MoTa, @LoaiXetNghiem, @TrangThai)";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TenCLS", SqlDbType.NVarChar).Value = cls.TenCLS;
		cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = (object?)cls.MoTa ?? DBNull.Value;
		cmd.Parameters.Add("@LoaiXetNghiem", SqlDbType.NVarChar).Value = cls.LoaiXetNghiem;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar).Value = cls.TrangThai;
		await conn.OpenAsync();
        int row = await cmd.ExecuteNonQueryAsync();
        return row;
    }
    public async Task<int> UpdateAsync(CanLamSang cls)
    {
        const string sql =
        @"UPDATE CanLamSang
          SET TenCLS=@TenCLS,
              MoTa=@MoTa,
              LoaiXetNghiem=@LoaiXetNghiem,
              TrangThai=@TrangThai
          WHERE CanLamSangID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TenCLS", SqlDbType.NVarChar).Value = cls.TenCLS;
		cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = (object?)cls.MoTa ?? DBNull.Value;
		cmd.Parameters.Add("@LoaiXetNghiem", SqlDbType.NVarChar).Value = cls.LoaiXetNghiem;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar).Value = cls.TrangThai;
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = cls.CanLamSangID;
		await conn.OpenAsync();
		int row = await cmd.ExecuteNonQueryAsync();
		return row;
	}
	public async Task BulkInsertAsync(List<CanLamSang> list)
	{
		using var conn = new SqlConnection(_connectionString);
		var table = new DataTable();

		table.Columns.Add("TenCLS");
		table.Columns.Add("MoTa");
        table.Columns.Add("LoaiXetNghiem");
		table.Columns.Add("TrangThai");

		foreach (var item in list)
		{
			table.Rows.Add(item.TenCLS, item.MoTa,item.LoaiXetNghiem, item.TrangThai);
		}

		using var bulk = new SqlBulkCopy(conn);
		bulk.DestinationTableName = "CanLamSang";
		bulk.ColumnMappings.Add("TenCLS", "TenCLS");
		bulk.ColumnMappings.Add("MoTa", "MoTa");
        bulk.ColumnMappings.Add("LoaiXetNghiem", "LoaiXetNghiem");
		bulk.ColumnMappings.Add("TrangThai", "TrangThai");
		await conn.OpenAsync();
		await bulk.WriteToServerAsync(table);
	}
	public async Task<List<NameResponseDTO>> GetComboboxAsync()
    {
        var list = new List<NameResponseDTO>();
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"SELECT CanLamSangID, TenCLS
                    FROM CanLamSang
                    WHERE TrangThai = N'Hoạt động'
                    ORDER BY TenCLS";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
        {
			list.Add(new NameResponseDTO
			{
				Id = reader.GetInt32(reader.GetOrdinal("CanLamSangID")),
				Name = reader.GetString(reader.GetOrdinal("TenCLS"))
			});
		}

		return list;
    }
	#region Mapping
	private static CanLamSang MapToEntity(SqlDataReader r)
    {
        var id = r.GetOrdinal("CanLamSangID");
        var ten = r.GetOrdinal("TenCLS");
        var mota = r.GetOrdinal("MoTa");
        var loai = r.GetOrdinal("LoaiXetNghiem");
        var trangThai = r.GetOrdinal("TrangThai");
        var ngay = r.GetOrdinal("NgayTao");
        return new CanLamSang(
            r.GetInt32(id),
            r.GetString(ten),
            r.IsDBNull(mota) ? null : r.GetString(mota),
            r.GetString(loai),
            r.GetDateTime(ngay),
            r.GetString(trangThai)
        );
    }
    private static CanLamSangListReadModel MapToLiteDTO(SqlDataReader r)
    {
        var id = r.GetOrdinal("CanLamSangID");
        var ten = r.GetOrdinal("TenCLS");
        var loai = r.GetOrdinal("LoaiXetNghiem");
        var trangThai = r.GetOrdinal("TrangThai");
        var ngay = r.GetOrdinal("NgayTao");
        return new CanLamSangListReadModel
        {
            CanLamSangID = r.GetInt32(id),
            TenCLS = r.GetString(ten),
            LoaiXetNghiem = r.GetString(loai),
            TrangThai = r.GetString(trangThai)
        };
    }
    private static CanLamSangReadModel MapToDetailDTO(SqlDataReader r)
    {
        var id = r.GetOrdinal("CanLamSangID");
        var ten = r.GetOrdinal("TenCLS");
        var mota = r.GetOrdinal("MoTa");
        var loai = r.GetOrdinal("LoaiXetNghiem");
        var trangThai = r.GetOrdinal("TrangThai");
        var ngay = r.GetOrdinal("NgayTao");
        return new CanLamSangReadModel
        {
            CanLamSangID = r.GetInt32(id),
            TenCLS = r.GetString(ten),
            MoTa = r.IsDBNull(mota) ? null : r.GetString(mota),
            LoaiXetNghiem = r.GetString(loai),
            TrangThai = r.GetString(trangThai),
            NgayTao = r.GetDateTime(ngay)
        };
    }
	#endregion
}