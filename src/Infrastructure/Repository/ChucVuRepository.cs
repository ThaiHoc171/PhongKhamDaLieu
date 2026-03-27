using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class ChucVuRepository : IChucVuRepository
{
    private readonly string _connectionString;

    public ChucVuRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }
	#region Queries
	private const string BaseSelectList = @"
        SELECT ChucVuID,TenChucVu,TrangThai
        FROM ChucVu";
    private const string BaseSelectDetail = @"
        SELECT ChucVuID,TenChucVu,MoTa,TrangThai,NgayTao,NgayCapNhat
        FROM ChucVu";
	#endregion
	public async Task<(List<ChucVuListReadModel>, int)> GetPagedAsync(int page, int size)
    {
        var list = new List<ChucVuListReadModel>();
        int total = 0;
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        int offset = (page - 1) * size;
        var sql = $@"
        {BaseSelectList}
        ORDER BY TenChucVu
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;
        SELECT COUNT(*)
        FROM ChucVu";
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
    public async Task<(List<ChucVuListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size)
    {
        var list = new List<ChucVuListReadModel>();
        int total = 0;
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        int offset = (page - 1) * size;
        var sql = $@"
        {BaseSelectList}
        WHERE TenChucVu LIKE @Keyword
        ORDER BY TenChucVu
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

        SELECT COUNT(*)
        FROM ChucVu
        WHERE TenChucVu LIKE @Keyword";
        using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 100).Value = $"%{keyword}%";
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
    public async Task<ChucVuReadModel?> GetDetailAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = BaseSelectDetail + " WHERE ChucVuID=@Id";
        using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
		using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToDetailDTO(reader);
        return null;
    }
    public async Task<ChucVu?> GetByIdAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = BaseSelectDetail + " WHERE ChucVuID=@Id";
        using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
		using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToEntity(reader);
        return null;
    }
    public async Task<int> AddAsync(ChucVu chucVu)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"INSERT INTO ChucVu(TenChucVu,MoTa,TrangThai)
                    VALUES(@TenChucVu,@MoTa,@TrangThai)";
        using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TenChucVu", SqlDbType.NVarChar, 100).Value = chucVu.TenChucVu;
        cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar, -1).Value = chucVu.MoTa;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = chucVu.TrangThai;
		int row = await cmd.ExecuteNonQueryAsync();
        return row;
    }
	public async Task BulkInsertAsync(List<ChucVu> list)
	{
		using var conn = new SqlConnection(_connectionString);
		var table = new DataTable();

		table.Columns.Add("TenChucVu");
		table.Columns.Add("MoTa");
		table.Columns.Add("TrangThai");

		foreach (var item in list)
		{
			table.Rows.Add(item.TenChucVu, item.MoTa, item.TrangThai);
		}

		using var bulk = new SqlBulkCopy(conn);
		bulk.DestinationTableName = "ChucVu";
		bulk.ColumnMappings.Add("TenChucVu", "TenChucVu");
		bulk.ColumnMappings.Add("MoTa", "MoTa");
		bulk.ColumnMappings.Add("TrangThai", "TrangThai");
		await conn.OpenAsync();
		await bulk.WriteToServerAsync(table);
	}
	public async Task<int> UpdateAsync(ChucVu chucVu)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"UPDATE ChucVu
                    SET TenChucVu=@TenChucVu,
                        MoTa=@MoTa,
                        TrangThai=@TrangThai,
                        NgayCapNhat=@NgayCapNhat
                    WHERE ChucVuID=@Id";
        using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = chucVu.ChucVuID;
		cmd.Parameters.Add("@TenChucVu", SqlDbType.NVarChar, 100).Value = chucVu.TenChucVu;
        cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar, -1).Value = chucVu.MoTa;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = chucVu.TrangThai;
		cmd.Parameters.Add("@NgayCapNhat", SqlDbType.DateTime).Value = chucVu.NgayCapNhat;
		int row = await cmd.ExecuteNonQueryAsync();
		return row;
	}
    public async Task<string?> GetByNhanVienIdAsync(int nhanVienId)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"
        SELECT cv.TenChucVu
        FROM NhanVien nv
        INNER JOIN ChucVu cv ON nv.ChucVuID = cv.ChucVuID
        WHERE nv.NhanVienID=@NhanVienID";
        using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = nhanVienId;
		using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return reader.GetString(0);
        return null;
    }
    public async Task<List<NameResponseDTO>> GetComboboxAsync()
    {
        var list = new List<NameResponseDTO>();
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"SELECT ChucVuID,TenChucVu
                    FROM ChucVu
                    WHERE TrangThai = N'Hoạt động'
                    ORDER BY TenChucVu ASC";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new NameResponseDTO
            {
                Id = reader.GetInt32(reader.GetOrdinal("ChucVuID")),
				Name = reader.GetString(reader.GetOrdinal("TenChucVu"))
			});
        }
        return list;
    }
	#region Mapping
	private ChucVu MapToEntity(SqlDataReader r)
    {
		return new ChucVu(
			r.GetInt32(r.GetOrdinal("ChucVuID")),
			r.GetString(r.GetOrdinal("TenChucVu")),
			r.GetString(r.GetOrdinal("MoTa")),
			r.GetString(r.GetOrdinal("TrangThai")),
			r.GetDateTime(r.GetOrdinal("NgayTao")),
			r.IsDBNull(r.GetOrdinal("NgayCapNhat")) ? null : r.GetDateTime(r.GetOrdinal("NgayCapNhat"))
		);
	}
    private ChucVuListReadModel MapToListDTO(SqlDataReader r)
    {
		return new ChucVuListReadModel
		{
			ChucVuID = r.GetInt32(r.GetOrdinal("ChucVuID")),
			TenChucVu = r.GetString(r.GetOrdinal("TenChucVu")),
			TrangThai = r.GetString(r.GetOrdinal("TrangThai"))
		};
	}
    private ChucVuReadModel MapToDetailDTO(SqlDataReader r)
    {

		return new ChucVuReadModel
		{
			ChucVuID = r.GetInt32(r.GetOrdinal("ChucVuID")),
			TenChucVu = r.GetString(r.GetOrdinal("TenChucVu")),
			MoTa = r.GetString(r.GetOrdinal("MoTa")),
			NgayTao = r.GetDateTime(r.GetOrdinal("NgayTao")),
			TrangThai = r.GetString(r.GetOrdinal("TrangThai")),
			NgayCapNhat = r.IsDBNull(r.GetOrdinal("NgayCapNhat")) ? null : r.GetDateTime(r.GetOrdinal("NgayCapNhat"))
		};
	}
	#endregion
}
