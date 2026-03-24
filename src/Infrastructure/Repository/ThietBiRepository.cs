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
	public async Task<(List<ThietBiReadModel>, int)> GetPagedAsync(int page, int size)
    {
        var list = new List<ThietBiReadModel>();
        int total = 0;
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        int offset = (page - 1) * size;
        var sql = @"
            SELECT ThietBiID, TenTB, LoaiTB, TrangThai
            FROM ThietBi
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
    public async Task<(List<ThietBiReadModel>, int)> SearchPagedAsync(string keyword, int page, int size)
    {
        var list = new List<ThietBiReadModel>();
        int total = 0;
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        int offset = (page - 1) * size;
        var sql = @"
            SELECT ThietBiID, TenTB, LoaiTB, TrangThai
            FROM ThietBi
            WHERE TenTB LIKE @Keyword
            ORDER BY TenTB ASC
            OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;
            SELECT COUNT(*)
            FROM ThietBi
            WHERE TenTB LIKE @Keyword
        ";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar).Value = $"%{keyword}%";
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
        var sql = @"
            SELECT ThietBiID, TenTB, LoaiTB, TrangThai
            FROM ThietBi 
            WHERE ThietBiID=@Id
        ";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToDTO(reader);
        return null;
    }
    public async Task<ThietBi?> GetByIdAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"
            SELECT ThietBiID, TenTB, LoaiTB, TrangThai
            FROM ThietBi
            WHERE ThietBiID=@Id
        ";
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
        cmd.Parameters.Add("@TenTB", SqlDbType.NVarChar).Value = entity.TenTB;
		cmd.Parameters.Add("@LoaiTB", SqlDbType.NVarChar).Value = entity.LoaiTB;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar).Value = entity.TrangThai;
        return await cmd.ExecuteNonQueryAsync();
    }
    public async Task<int> UpdateAsync(ThietBi entity)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = "UPDATE ThietBi SET TenTB=@TenTB, LoaiTB=@LoaiTB, TrangThai=@TrangThai WHERE ThietBiID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = entity.ThietBiID;
        cmd.Parameters.Add("@TenTB", SqlDbType.NVarChar).Value = entity.TenTB;
        cmd.Parameters.Add("@LoaiTB", SqlDbType.NVarChar).Value = entity.LoaiTB;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar).Value = entity.TrangThai;
		return await cmd.ExecuteNonQueryAsync();
	}
	#region Mapping
	private ThietBi MapToEntity(SqlDataReader r)
    {
        return new ThietBi(
            (int)r["ThietBiID"],
            (string)r["TenTB"],
            (string)r["LoaiTB"],
            (string)r["TrangThai"]
        );
    }
    private ThietBiReadModel MapToDTO(SqlDataReader r)
    {
        return new ThietBiReadModel
        {
            ThietBiID = (int)r["ThietBiID"],
            TenTB = (string)r["TenTB"],
            LoaiTB = (string)r["LoaiTB"],
            TrangThai = (string)r["TrangThai"]
		};
    }
	#endregion
}