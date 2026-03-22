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
    public BenhNhanRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException("Connection string not found");
    }
    #region Queries
    private const string BaseSelect = @"
        SELECT bn.BenhNhanID, bn.ThongTinID, bn.GhiChu, bn.NgayTao, bn.NgayCapNhat
        FROM BenhNhan bn";
    private const string BaseSelectJoin = @"
        SELECT bn.BenhNhanID, bn.ThongTinID, tt.HoTen, tt.NgaySinh, tt.GioiTinh,
               tt.SDT, tt.EmailLienHe, tt.DiaChi, tt.Avatar,
               bn.GhiChu, bn.NgayTao, bn.NgayCapNhat
        FROM BenhNhan bn
        JOIN ThongTinCaNhan tt ON bn.ThongTinID = tt.ThongTinID";
    #endregion
    public async Task<bool> ExistsByThongTinIdAsync(int thongTinId)
    {
        const string sql = @"
            SELECT COUNT(1) 
            FROM BenhNhan 
            WHERE ThongTinID = @ThongTinID";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@ThongTinID", SqlDbType.Int).Value = thongTinId;
        await conn.OpenAsync();
        var result = Convert.ToInt32(await cmd.ExecuteScalarAsync());
        return result > 0;
    }
    public async Task<BenhNhan?> GetByIdAsync(int id)
    {
        var sql = BaseSelect + " WHERE bn.BenhNhanID = @Id";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToEntity(reader);
        return null;
    }
    public async Task<(List<BenhNhanReadModel> Data, int TotalCount)> SearchAsync(string? keyword, int pageNumber, int pageSize)
    {
        var list = new List<BenhNhanReadModel>();
        int total = 0;
        int offset = (pageNumber - 1) * pageSize;
        var sql = @"
            SELECT bn.BenhNhanID, bn.ThongTinID, tt.HoTen, tt.SDT, tt.EmailLienHe, bn.GhiChu
            FROM BenhNhan bn
            JOIN ThongTinCaNhan tt ON bn.ThongTinID = tt.ThongTinID
            WHERE (@Keyword IS NULL OR tt.HoTen LIKE @Keyword)
            ORDER BY bn.BenhNhanID
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
            SELECT COUNT(*)
            FROM BenhNhan bn
            JOIN ThongTinCaNhan tt ON bn.ThongTinID = tt.ThongTinID
            WHERE (@Keyword IS NULL OR tt.HoTen LIKE @Keyword)";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 255)
            .Value = string.IsNullOrWhiteSpace(keyword) ? DBNull.Value : $"%{keyword}%";
        cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
        cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToListDTO(reader));
        await reader.NextResultAsync();
        if (await reader.ReadAsync())
            total = reader.GetInt32(0);
        return (list, total);
    }
    public async Task<(List<BenhNhanReadModel> Data, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
    {
        var list = new List<BenhNhanReadModel>();
        int total = 0;
        int offset = (pageNumber - 1) * pageSize;
        var sql = @"
            SELECT bn.BenhNhanID, bn.ThongTinID, tt.HoTen, tt.SDT, tt.EmailLienHe, bn.GhiChu
            FROM BenhNhan bn
            JOIN ThongTinCaNhan tt ON bn.ThongTinID = tt.ThongTinID
            ORDER BY bn.BenhNhanID
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
            SELECT COUNT(*) FROM BenhNhan";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
        cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToListDTO(reader));
        await reader.NextResultAsync();
        if (await reader.ReadAsync())
            total = reader.GetInt32(0);
        return (list, total);
    }
    public async Task<BenhNhanDetailReadModel?> GetDetailAsync(int id)
    {
        var sql = BaseSelectJoin + " WHERE bn.BenhNhanID = @Id";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToDetailDTO(reader);
        return null;
    }
    public async Task<BenhNhanDetailReadModel?> GetByThongTinIDAsync(int thongTinId)
    {
        var sql = BaseSelectJoin + " WHERE bn.ThongTinID = @Id";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = thongTinId;
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToDetailDTO(reader);
        return null;
    }
    public async Task<int> AddAsync(BenhNhan benhNhan)
    {
        const string sql = @"
            INSERT INTO BenhNhan(ThongTinID, GhiChu)
            OUTPUT INSERTED.BenhNhanID
            VALUES(@ThongTinID, @GhiChu)";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@ThongTinID", SqlDbType.Int).Value = benhNhan.ThongTinID;
        cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, -1)
            .Value = (object?)benhNhan.GhiChu ?? DBNull.Value;
        await conn.OpenAsync();
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    public async Task UpdateAsync(BenhNhan benhNhan)
    {
        const string sql = @"
            UPDATE BenhNhan
            SET GhiChu = @GhiChu,
                NgayCapNhat = GETDATE()
            WHERE BenhNhanID = @Id";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = benhNhan.BenhNhanID;
        cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, -1)
            .Value = (object?)benhNhan.GhiChu ?? DBNull.Value;
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
    public async Task<List<NameResponseDTO>> GetComboboxAsync()
    {
        const string sql = @"
            SELECT bn.BenhNhanID, tt.HoTen
            FROM BenhNhan bn
            JOIN ThongTinCaNhan tt ON bn.ThongTinID = tt.ThongTinID
            ORDER BY tt.HoTen";
        var list = new List<NameResponseDTO>();
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
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
            r.GetInt32(0),
            r.GetInt32(1),
            r.IsDBNull(2) ? "" : r.GetString(2),
            r.GetDateTime(3),
            r.IsDBNull(4) ? r.GetDateTime(3) : r.GetDateTime(4)
        );
    }
    private BenhNhanReadModel MapToListDTO(SqlDataReader r)
    {
        return new BenhNhanReadModel
        {
            BenhNhanID = r.GetInt32(0),
            ThongTinID = r.GetInt32(1),
            HoTen = r.IsDBNull(2) ? null : r.GetString(2),
            SDT = r.IsDBNull(3) ? null : r.GetString(3),
            EmailLienHe = r.IsDBNull(4) ? null : r.GetString(4),
            GhiChu = r.IsDBNull(5) ? null : r.GetString(5)
        };
    }
    private BenhNhanDetailReadModel MapToDetailDTO(SqlDataReader r)
    {
        return new BenhNhanDetailReadModel
        {
            BenhNhanID = r.GetInt32(0),
            ThongTinID = r.GetInt32(1),
            HoTen = r.IsDBNull(2) ? null : r.GetString(2),
            NgaySinh = r.IsDBNull(3) ? null : r.GetDateTime(3),
            GioiTinh = r.IsDBNull(4) ? null : r.GetString(4),
            SDT = r.IsDBNull(5) ? null : r.GetString(5),
            EmailLienHe = r.IsDBNull(6) ? null : r.GetString(6),
            DiaChi = r.IsDBNull(7) ? null : r.GetString(7),
            Avatar = r.IsDBNull(8) ? null : r.GetString(8),
            GhiChu = r.IsDBNull(9) ? null : r.GetString(9),
            NgayTao = r.GetDateTime(10),
            NgayCapNhat = r.IsDBNull(11) ? r.GetDateTime(10) : r.GetDateTime(11)
        };
    }
    #endregion
}