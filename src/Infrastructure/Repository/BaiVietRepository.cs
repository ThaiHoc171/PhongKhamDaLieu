using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace Infrastructure.Repository;
public class BaiVietRepository : IBaiVietRepository
{
    private readonly string _connectionString;
    public BaiVietRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }
    private const string BaseSelectList = @"
        SELECT BaiVietID,TieuDe,TomTat,HinhAnh,LuotXem,NgayDang
        FROM BaiViet";
    private const string BaseSelectDetail = @"
        SELECT BaiVietID,TieuDe,TomTat,NoiDung,HinhAnh,TacGiaID,LoaiBenhID,LuotXem,NgayDang,NgayCapNhat,TrangThai
        FROM BaiViet";
    public async Task<int> AddAsync(BaiViet entity)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"
        INSERT INTO BaiViet
        (TieuDe,TomTat,NoiDung,HinhAnh,TacGiaID,LoaiBenhID,LuotXem,NgayDang,NgayCapNhat,TrangThai)
        VALUES(@TieuDe,@TomTat,@NoiDung,@HinhAnh,@TacGiaID,@LoaiBenhID,0,GETDATE(),GETDATE(),N'Bản nháp');";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@TieuDe", SqlDbType.NVarChar).Value = entity.TieuDe;
        cmd.Parameters.Add("@TomTat", SqlDbType.NVarChar).Value = (object?)entity.TomTat ?? DBNull.Value;
        cmd.Parameters.Add("@NoiDung", SqlDbType.NVarChar).Value = (object?)entity.NoiDung ?? DBNull.Value;
        cmd.Parameters.Add("@HinhAnh", SqlDbType.NVarChar).Value = (object?)entity.HinhAnh ?? DBNull.Value;
        cmd.Parameters.Add("@TacGiaID", SqlDbType.Int).Value = (object?)entity.TacGiaID ?? DBNull.Value;
        cmd.Parameters.Add("@LoaiBenhID", SqlDbType.Int).Value = (object?)entity.LoaiBenhID ?? DBNull.Value;
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    public async Task UpdateAsync(BaiViet entity)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"
        UPDATE BaiViet
        SET TieuDe=@TieuDe,
            TomTat=@TomTat,
            NoiDung=@NoiDung,
            HinhAnh=@HinhAnh,
            LoaiBenhID=@LoaiBenhID,
            NgayCapNhat=GETDATE()
        WHERE BaiVietID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = entity.BaiVietID;
        cmd.Parameters.Add("@TieuDe", SqlDbType.NVarChar).Value = entity.TieuDe;
        cmd.Parameters.Add("@TomTat", SqlDbType.NVarChar).Value = (object?)entity.TomTat ?? DBNull.Value;
        cmd.Parameters.Add("@NoiDung", SqlDbType.NVarChar).Value = (object?)entity.NoiDung ?? DBNull.Value;
        cmd.Parameters.Add("@HinhAnh", SqlDbType.NVarChar).Value = (object?)entity.HinhAnh ?? DBNull.Value;
        cmd.Parameters.Add("@LoaiBenhID", SqlDbType.Int).Value = (object?)entity.LoaiBenhID ?? DBNull.Value;
        await cmd.ExecuteNonQueryAsync();
    }
    public async Task DeleteAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = "DELETE FROM BaiViet WHERE BaiVietID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        await cmd.ExecuteNonQueryAsync();
    }
    public async Task<BaiViet?> GetByIdAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = BaseSelectDetail + " WHERE BaiVietID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToEntity(reader);
        return null;
    }
    public async Task<(List<BaiVietListReadModel>, int)> GetPagedAsync(int page, int size)
    {
        var list = new List<BaiVietListReadModel>();
        int total = 0;
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        int offset = (page - 1) * size;
        var sql = $@"
        {BaseSelectList}
        ORDER BY NgayDang DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;
        SELECT COUNT(*) FROM BaiViet";
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
    public async Task<List<BaiVietListReadModel>> GetByLoaiBenhAsync(int loaiBenhID)
    {
        var list = new List<BaiVietListReadModel>();
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = BaseSelectList + " WHERE LoaiBenhID=@LoaiBenhID ORDER BY NgayDang DESC";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@LoaiBenhID", SqlDbType.Int).Value = loaiBenhID;
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToListDTO(reader));
        return list;
    }
    public async Task<List<BaiVietListReadModel>> GetTopLuotXemAsync(int top)
    {
        var list = new List<BaiVietListReadModel>();
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"
        SELECT TOP(@Top) BaiVietID,TieuDe,TomTat,HinhAnh,LuotXem,NgayDang
        FROM BaiViet
        ORDER BY LuotXem DESC";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Top", SqlDbType.Int).Value = top;
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToListDTO(reader));
        return list;
    }
    public async Task<BaiVietReadModel?> GetDetailAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = BaseSelectDetail + " WHERE BaiVietID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToDetailDTO(reader);
        return null;
    }
    private BaiViet MapToEntity(SqlDataReader r)
    {
        return new BaiViet(
            (int)r["BaiVietID"],
            (string)r["TieuDe"],
            r["TomTat"] as string,
            r["NoiDung"] as string,
            r["HinhAnh"] as string,
            r["TacGiaID"] as int?,
            r["LoaiBenhID"] as int?,
            (int)r["LuotXem"],
            (DateTime)r["NgayDang"],
            r["NgayCapNhat"] as DateTime?,
            (string)r["TrangThai"]
        );
    }
    private BaiVietListReadModel MapToListDTO(SqlDataReader r)
    {
        return new BaiVietListReadModel
        {
            BaiVietID = (int)r["BaiVietID"],
            TieuDe = (string)r["TieuDe"],
            TomTat = r["TomTat"] as string,
            HinhAnh = r["HinhAnh"] as string,
            LuotXem = (int)r["LuotXem"],
            NgayDang = (DateTime)r["NgayDang"]
        };
    }
    private BaiVietReadModel MapToDetailDTO(SqlDataReader r)
    {
        return new BaiVietReadModel
        {
            BaiVietID = (int)r["BaiVietID"],
            TieuDe = (string)r["TieuDe"],
            TomTat = r["TomTat"] as string,
            NoiDung = r["NoiDung"] as string,
            HinhAnh = r["HinhAnh"] as string,
            TacGiaID = r["TacGiaID"] as int?,
            LoaiBenhID = r["LoaiBenhID"] as int?,
            LuotXem = (int)r["LuotXem"],
            NgayDang = (DateTime)r["NgayDang"],
            NgayCapNhat = r["NgayCapNhat"] as DateTime?,
            TrangThai = (string)r["TrangThai"]
        };
    }
}