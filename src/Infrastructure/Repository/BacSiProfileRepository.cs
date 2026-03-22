using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repository;

public class BacSiProfileRepository : IBacSiProfileRepository
{
    private readonly string _connectionString;
    public BacSiProfileRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }
    private const string BaseSelectLite = @"
        SELECT BacSiProfileID, NhanVienID, ChuyenMon, HinhAnh, NgayCapNhat
        FROM BacSiProfile";
    private const string BaseSelectDetail = @"
        SELECT BacSiProfileID, NhanVienID, GioiThieu, ChuyenMon, ThanhTuu, HinhAnh, KinhNghiem, NgayCapNhat
        FROM BacSiProfile";
    public async Task<BacSiProfile?> GetByIdAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = BaseSelectDetail + " WHERE BacSiProfileID = @Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToEntity(reader);
        return null;
    }
    public async Task<BacSiProfileReadModel?> GetDetailAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = BaseSelectDetail + " WHERE BacSiProfileID = @Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToDetailDTO(reader);
        return null;
    }
    public async Task<BacSiProfileReadModel?> GetByNhanVienIdAsync(int nhanVienId)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = BaseSelectDetail + " WHERE NhanVienID = @NhanVienID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = nhanVienId;
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToDetailDTO(reader);
        return null;
    }
    public async Task<(List<BacSiProfileListReadModel>, int)> GetPagedAsync(int page, int size)
    {
        var list = new List<BacSiProfileListReadModel>();
        int total = 0;
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        int offset = (page - 1) * size;
        var sql = $@"
            {BaseSelectLite}
            ORDER BY BacSiProfileID
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
            SELECT COUNT(*) FROM BacSiProfile";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
        cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = size;
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToLiteDTO(reader));
        await reader.NextResultAsync();
        if (await reader.ReadAsync())
            total = reader.GetInt32(0);
        return (list, total);
    }
    public async Task<(List<BacSiProfileListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size)
    {
        var list = new List<BacSiProfileListReadModel>();
        int total = 0;
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        int offset = (page - 1) * size;
        var sql = $@"
            {BaseSelectLite}
            WHERE ChuyenMon LIKE @Keyword
            ORDER BY BacSiProfileID
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
            SELECT COUNT(*) FROM BacSiProfile
            WHERE ChuyenMon LIKE @Keyword";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar).Value = $"%{keyword}%";
        cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
        cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = size;
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToLiteDTO(reader));
        await reader.NextResultAsync();
        if (await reader.ReadAsync())
            total = reader.GetInt32(0);
        return (list, total);
    }
    public async Task<int> AddAsync(BacSiProfile entity)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"
        INSERT INTO BacSiProfile
        (NhanVienID, GioiThieu, ChuyenMon, ThanhTuu, HinhAnh, KinhNghiem, NgayCapNhat)
        VALUES (@NhanVienID, @GioiThieu, @ChuyenMon, @ThanhTuu, @HinhAnh, @KinhNghiem, @NgayCapNhat);
        SELECT SCOPE_IDENTITY();";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = entity.NhanVienID;
        cmd.Parameters.Add("@GioiThieu", SqlDbType.NVarChar).Value = (object?)entity.GioiThieu ?? DBNull.Value;
        cmd.Parameters.Add("@ChuyenMon", SqlDbType.NVarChar).Value = (object?)entity.ChuyenMon ?? DBNull.Value;
        cmd.Parameters.Add("@ThanhTuu", SqlDbType.NVarChar).Value = (object?)entity.ThanhTuu ?? DBNull.Value;
        cmd.Parameters.Add("@HinhAnh", SqlDbType.NVarChar).Value = (object?)entity.HinhAnh ?? DBNull.Value;
        cmd.Parameters.Add("@KinhNghiem", SqlDbType.NVarChar).Value = (object?)entity.KinhNghiem ?? DBNull.Value;
        cmd.Parameters.Add("@NgayCapNhat", SqlDbType.DateTime).Value = entity.NgayCapNhat == default ? DateTime.Now : entity.NgayCapNhat;
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    public async Task UpdateAsync(BacSiProfile entity)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"
        UPDATE BacSiProfile
        SET
            GioiThieu = @GioiThieu,
            ChuyenMon = @ChuyenMon,
            ThanhTuu = @ThanhTuu,
            HinhAnh = @HinhAnh,
            KinhNghiem = @KinhNghiem,
            NgayCapNhat = @NgayCapNhat
        WHERE BacSiProfileID = @Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = entity.BacSiProfileID;
        cmd.Parameters.Add("@GioiThieu", SqlDbType.NVarChar).Value = (object?)entity.GioiThieu ?? DBNull.Value;
        cmd.Parameters.Add("@ChuyenMon", SqlDbType.NVarChar).Value = (object?)entity.ChuyenMon ?? DBNull.Value;
        cmd.Parameters.Add("@ThanhTuu", SqlDbType.NVarChar).Value = (object?)entity.ThanhTuu ?? DBNull.Value;
        cmd.Parameters.Add("@HinhAnh", SqlDbType.NVarChar).Value = (object?)entity.HinhAnh ?? DBNull.Value;
        cmd.Parameters.Add("@KinhNghiem", SqlDbType.NVarChar).Value = (object?)entity.KinhNghiem ?? DBNull.Value;
        cmd.Parameters.Add("@NgayCapNhat", SqlDbType.DateTime).Value = entity.NgayCapNhat;

        await cmd.ExecuteNonQueryAsync();
    }
    private BacSiProfile MapToEntity(SqlDataReader r)
    {
        return new BacSiProfile(
            (int)r["BacSiProfileID"],
            (int)r["NhanVienID"],
            r["GioiThieu"] as string,
            r["ChuyenMon"] as string,
            r["ThanhTuu"] as string,
            r["HinhAnh"] as string,
            r["KinhNghiem"] as string,
            (DateTime)r["NgayCapNhat"]
        );
    }
    private BacSiProfileListReadModel MapToLiteDTO(SqlDataReader r)
    {
        return new BacSiProfileListReadModel
        {
            BacSiProfileID = (int)r["BacSiProfileID"],
            NhanVienID = (int)r["NhanVienID"],
            ChuyenMon = r["ChuyenMon"] as string,
            HinhAnh = r["HinhAnh"] as string,
            NgayCapNhat = (DateTime)r["NgayCapNhat"]
        };
    }
    private BacSiProfileReadModel MapToDetailDTO(SqlDataReader r)
    {
        return new BacSiProfileReadModel
        {
            BacSiProfileID = (int)r["BacSiProfileID"],
            NhanVienID = (int)r["NhanVienID"],
            GioiThieu = r["GioiThieu"] as string,
            ChuyenMon = r["ChuyenMon"] as string,
            ThanhTuu = r["ThanhTuu"] as string,
            HinhAnh = r["HinhAnh"] as string,
            KinhNghiem = r["KinhNghiem"] as string,
            NgayCapNhat = (DateTime)r["NgayCapNhat"]
        };
    }
}