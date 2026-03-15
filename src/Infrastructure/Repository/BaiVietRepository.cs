using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository;

public class BaiVietRepository : IBaiVietRepository
{
    private readonly string _connectionString;

    public BaiVietRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private const string BaseSelectLite = @"
        SELECT BaiVietID, TieuDe, TomTat, HinhAnh, LuotXem, NgayDang
        FROM BaiViet";

    private const string BaseSelectDetail = @"
        SELECT BaiVietID, TieuDe, TomTat, NoiDung, HinhAnh,
               TacGiaID, LoaiBenhID, LuotXem, NgayDang, NgayCapNhat, TrangThai
        FROM BaiViet";

    public async Task<BaiViet?> GetByIdAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        var sql = BaseSelectDetail + " WHERE BaiVietID = @Id";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);

        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
            return MapToEntity(reader);

        return null;
    }

    public async Task<BaiVietReadModel?> GetDetailAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        var sql = BaseSelectDetail + " WHERE BaiVietID = @Id";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);

        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
            return MapToDetailDTO(reader);

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
            {BaseSelectLite}
            ORDER BY NgayDang DESC
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

            SELECT COUNT(*) FROM BaiViet
        ";

        using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Offset", offset);
        cmd.Parameters.AddWithValue("@PageSize", size);

        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(MapToLiteDTO(reader));
        }

        await reader.NextResultAsync();

        if (await reader.ReadAsync())
            total = reader.GetInt32(0);

        return (list, total);
    }

    public async Task<(List<BaiVietListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size)
    {
        var list = new List<BaiVietListReadModel>();
        int total = 0;

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        int offset = (page - 1) * size;

        var sql = $@"
            {BaseSelectLite}
            WHERE TieuDe LIKE @Keyword
            ORDER BY NgayDang DESC
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

            SELECT COUNT(*) FROM BaiViet
            WHERE TieuDe LIKE @Keyword
        ";

        using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");
        cmd.Parameters.AddWithValue("@Offset", offset);
        cmd.Parameters.AddWithValue("@PageSize", size);

        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(MapToLiteDTO(reader));
        }

        await reader.NextResultAsync();

        if (await reader.ReadAsync())
            total = reader.GetInt32(0);

        return (list, total);
    }

    public async Task<int> AddAsync(BaiViet entity)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        var sql = @"
        INSERT INTO BaiViet
        (TieuDe, TomTat, NoiDung, HinhAnh, TacGiaID, LoaiBenhID, LuotXem, NgayDang, TrangThai)
        VALUES (@TieuDe, @TomTat, @NoiDung, @HinhAnh, @TacGiaID, @LoaiBenhID, 0, GETDATE(), 'Bản nháp');

        SELECT SCOPE_IDENTITY();";

        using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@TieuDe", entity.TieuDe);
        cmd.Parameters.AddWithValue("@TomTat", (object?)entity.TomTat ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@NoiDung", entity.NoiDung);
        cmd.Parameters.AddWithValue("@HinhAnh", (object?)entity.HinhAnh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@TacGiaID", (object?)entity.TacGiaID ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@LoaiBenhID", (object?)entity.LoaiBenhID ?? DBNull.Value);

        var result = await cmd.ExecuteScalarAsync();

        return Convert.ToInt32(result);
    }

    public async Task UpdateAsync(BaiViet entity)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        var sql = @"
        UPDATE BaiViet
        SET
            TieuDe = @TieuDe,
            TomTat = @TomTat,
            NoiDung = @NoiDung,
            HinhAnh = @HinhAnh,
            LoaiBenhID = @LoaiBenhID,
            NgayCapNhat = @NgayCapNhat
        WHERE BaiVietID = @Id
        ";

        using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Id", entity.BaiVietID);
        cmd.Parameters.AddWithValue("@TieuDe", entity.TieuDe);
        cmd.Parameters.AddWithValue("@TomTat", (object?)entity.TomTat ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@NoiDung", entity.NoiDung);
        cmd.Parameters.AddWithValue("@HinhAnh", (object?)entity.HinhAnh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@LoaiBenhID", (object?)entity.LoaiBenhID ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@NgayCapNhat", entity.NgayCapNhat);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        var sql = "DELETE FROM BaiViet WHERE BaiVietID=@Id";

        using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Id", id);

        await cmd.ExecuteNonQueryAsync();
    }
    public async Task<List<BaiViet>> GetByLoaiBenhAsync(int loaiBenhID)
    {
        var list = new List<BaiViet>();

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        var sql = BaseSelectDetail + " WHERE LoaiBenhID = @LoaiBenhID";

        using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@LoaiBenhID", loaiBenhID);

        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(MapToEntity(reader));
        }

        return list;
    }
    public async Task<List<BaiViet>> GetTopLuotXemAsync(int top)
    {
        var list = new List<BaiViet>();

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        var sql = $@"
            SELECT TOP (@Top) *
            FROM BaiViet
            ORDER BY LuotXem DESC";

        using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Top", top);

        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(MapToEntity(reader));
        }

        return list;
    }
    private BaiViet MapToEntity(SqlDataReader r)
    {
        return new BaiViet
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
    private BaiVietListReadModel MapToLiteDTO(SqlDataReader r)
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