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
    private const string BaseSelectList = @"
        SELECT ChucVuID,TenChucVu,TrangThai
        FROM ChucVu";
    private const string BaseSelectDetail = @"
        SELECT ChucVuID,TenChucVu,MoTa,NgayTao,TrangThai
        FROM ChucVu";
    public async Task<(List<ChucVuListReadModel>, int)> GetPagedAsync(int page, int size, string? trangThai)
    {
        var list = new List<ChucVuListReadModel>();
        int total = 0;
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        int offset = (page - 1) * size;
        var sql = $@"
        {BaseSelectList}
        WHERE (@TrangThai IS NULL OR TrangThai = @TrangThai)
        ORDER BY TenChucVu
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;
        SELECT COUNT(*)
        FROM ChucVu
        WHERE (@TrangThai IS NULL OR TrangThai = @TrangThai)";
        using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar).Value = (object?)trangThai ?? DBNull.Value;
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = size;
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
		cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar).Value = $"%{keyword}%";

		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = size;
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
    public async Task AddAsync(ChucVu chucVu)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"INSERT INTO ChucVu(TenChucVu,MoTa)
                    VALUES(@TenChucVu,@MoTa)";
        using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TenChucVu", SqlDbType.NVarChar).Value = chucVu.TenChucVu;
		cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = (object?)chucVu.MoTa ?? DBNull.Value;
		await cmd.ExecuteNonQueryAsync();
    }
    public async Task UpdateAsync(ChucVu chucVu)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"UPDATE ChucVu
                    SET TenChucVu=@TenChucVu,
                        MoTa=@MoTa,
                        TrangThai=@TrangThai
                    WHERE ChucVuID=@Id";
        using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = chucVu.ChucVuID;
		cmd.Parameters.Add("@TenChucVu", SqlDbType.NVarChar).Value = chucVu.TenChucVu;
		cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value = (object?)chucVu.MoTa ?? DBNull.Value;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar)
			.Value = chucVu.TrangThai;
		await cmd.ExecuteNonQueryAsync();
    }
    public async Task<string?> GetNameByIdAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"SELECT TenChucVu
                    FROM ChucVu
                    WHERE ChucVuID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        return await cmd.ExecuteScalarAsync() as string;
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
    public async Task<List<(int Id, string Ten)>> GetIdAndNameAsync()
    {
        var list = new List<(int Id, string Ten)>();
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"SELECT ChucVuID,TenChucVu
                    FROM ChucVu
                    ORDER BY TenChucVu";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add((reader.GetInt32(0), reader.GetString(1)));
        }
        return list;
    }
    private ChucVu MapToEntity(SqlDataReader r)
    {
        return new ChucVu(
            (int)r["ChucVuID"],
            (string)r["TenChucVu"],
            r["MoTa"] as string,
            (DateTime)r["NgayTao"],
            (string)r["TrangThai"]
        );
    }
    private ChucVuListReadModel MapToListDTO(SqlDataReader r)
    {
        return new ChucVuListReadModel
        {
            ChucVuID = (int)r["ChucVuID"],
            TenChucVu = (string)r["TenChucVu"],
            TrangThai = (string)r["TrangThai"]
        };
    }
    private ChucVuReadModel MapToDetailDTO(SqlDataReader r)
    {
        return new ChucVuReadModel
        {
            ChucVuID = (int)r["ChucVuID"],
            TenChucVu = (string)r["TenChucVu"],
            MoTa = r["MoTa"] as string,
            NgayTao = (DateTime)r["NgayTao"],
            TrangThai = (string)r["TrangThai"]
        };
    }
}