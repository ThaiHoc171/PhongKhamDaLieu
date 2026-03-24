using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace Infrastructure.Repositories;
public class HoSoBenhAnRepository : IHoSoBenhAnRepository
{
    private readonly string _connectionString;
    public HoSoBenhAnRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string not found.");
    }
    private SqlConnection CreateConnection() => new(_connectionString);
    private const string BaseFrom = @"FROM HoSoBenhAn";
    private const string BaseSelectList = @"
        SELECT HoSoBenhAnID, BenhNhanID, NgayTao";
    private const string BaseSelectDetail = @"
        SELECT HoSoBenhAnID, BenhNhanID, BenhNen, DiUng,
               TienSuBenh, TienSuGiaDinh, ThoiQuenSong,
               ThongTinKhac, NgayTao, NgayCapNhat";
    public async Task<(List<HoSoBenhAnListReadModel>, int)> GetPagedAsync(int page, int size)
    {
        var list = new List<HoSoBenhAnListReadModel>();
        int total = 0;
        var sql = $@"
            {BaseSelectList}
            {BaseFrom}
            ORDER BY NgayCapNhat DESC
            OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;
            SELECT COUNT(*) {BaseFrom}";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Offset", System.Data.SqlDbType.Int).Value = (page - 1) * size;
        cmd.Parameters.Add("@Size", System.Data.SqlDbType.Int).Value = size;
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToListDTO(reader));
        if (await reader.NextResultAsync() && await reader.ReadAsync())
            total = reader.GetInt32(0);
        return (list, total);
    }
    public async Task<(List<HoSoBenhAnListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size)
    {
        var list = new List<HoSoBenhAnListReadModel>();
        int total = 0;
        var sql = $@"
            {BaseSelectList}
            {BaseFrom}
            WHERE (@Keyword IS NULL OR BenhNen LIKE @Keyword OR DiUng LIKE @Keyword)
            ORDER BY NgayCapNhat DESC
            OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;
            SELECT COUNT(*)
            {BaseFrom}
            WHERE (@Keyword IS NULL OR BenhNen LIKE @Keyword OR DiUng LIKE @Keyword)";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Keyword", System.Data.SqlDbType.NVarChar).Value =
            string.IsNullOrWhiteSpace(keyword) ? DBNull.Value : $"%{keyword}%";
        cmd.Parameters.Add("@Offset", System.Data.SqlDbType.Int).Value = (page - 1) * size;
        cmd.Parameters.Add("@Size", System.Data.SqlDbType.Int).Value = size;
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToListDTO(reader));
        if (await reader.NextResultAsync() && await reader.ReadAsync())
            total = reader.GetInt32(0);
        return (list, total);
    }
    public async Task<HoSoBenhAnReadModel?> GetDetailAsync(int id)
    {
        var sql = $@"
            {BaseSelectDetail}
            {BaseFrom}
            WHERE HoSoBenhAnID = @Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToDetailDTO(reader) : null;
    }
    public async Task<HoSoBenhAn?> GetByIdAsync(int id)
    {
        var sql = $@"
            {BaseSelectDetail}
            {BaseFrom}
            WHERE HoSoBenhAnID = @Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }
    public async Task<HoSoBenhAnReadModel?> GetByBenhNhanIdAsync(int benhNhanId)
    {
        var sql = $@"
            {BaseSelectDetail}
            {BaseFrom}
            WHERE BenhNhanID = @BenhNhanID";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@BenhNhanID", System.Data.SqlDbType.Int).Value = benhNhanId;
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToDetailDTO(reader) : null;
    }
    public async Task AddAsync(HoSoBenhAn hs)
    {
        const string sql = @"
            INSERT INTO HoSoBenhAn
            (BenhNhanID, BenhNen, DiUng, TienSuBenh, TienSuGiaDinh,
             ThoiQuenSong, ThongTinKhac, NgayTao, NgayCapNhat)
            VALUES
            (@BenhNhanID, @BenhNen, @DiUng, @TienSuBenh, @TienSuGiaDinh,
             @ThoiQuenSong, @ThongTinKhac, @NgayTao, @NgayCapNhat)";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@BenhNhanID", System.Data.SqlDbType.Int).Value = hs.BenhNhanID;
        cmd.Parameters.Add("@BenhNen", System.Data.SqlDbType.NVarChar).Value = (object?)hs.BenhNen ?? DBNull.Value;
        cmd.Parameters.Add("@DiUng", System.Data.SqlDbType.NVarChar).Value = (object?)hs.DiUng ?? DBNull.Value;
        cmd.Parameters.Add("@TienSuBenh", System.Data.SqlDbType.NVarChar).Value = (object?)hs.TienSuBenh ?? DBNull.Value;
        cmd.Parameters.Add("@TienSuGiaDinh", System.Data.SqlDbType.NVarChar).Value = (object?)hs.TienSuGiaDinh ?? DBNull.Value;
        cmd.Parameters.Add("@ThoiQuenSong", System.Data.SqlDbType.NVarChar).Value = (object?)hs.ThoiQuenSong ?? DBNull.Value;
        cmd.Parameters.Add("@ThongTinKhac", System.Data.SqlDbType.NVarChar).Value = (object?)hs.ThongTinKhac ?? DBNull.Value;
        cmd.Parameters.Add("@NgayTao", System.Data.SqlDbType.DateTime).Value = hs.NgayTao;
        cmd.Parameters.Add("@NgayCapNhat", System.Data.SqlDbType.DateTime).Value = hs.NgayCapNhat;
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
    public async Task UpdateAsync(HoSoBenhAn hs)
    {
        const string sql = @"
            UPDATE HoSoBenhAn
            SET BenhNen = @BenhNen,
                DiUng = @DiUng,
                TienSuBenh = @TienSuBenh,
                TienSuGiaDinh = @TienSuGiaDinh,
                ThoiQuenSong = @ThoiQuenSong,
                ThongTinKhac = @ThongTinKhac,
                NgayCapNhat = @NgayCapNhat
            WHERE HoSoBenhAnID = @Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = hs.HoSoBenhAnID;
        cmd.Parameters.Add("@BenhNen", System.Data.SqlDbType.NVarChar).Value = (object?)hs.BenhNen ?? DBNull.Value;
        cmd.Parameters.Add("@DiUng", System.Data.SqlDbType.NVarChar).Value = (object?)hs.DiUng ?? DBNull.Value;
        cmd.Parameters.Add("@TienSuBenh", System.Data.SqlDbType.NVarChar).Value = (object?)hs.TienSuBenh ?? DBNull.Value;
        cmd.Parameters.Add("@TienSuGiaDinh", System.Data.SqlDbType.NVarChar).Value = (object?)hs.TienSuGiaDinh ?? DBNull.Value;
        cmd.Parameters.Add("@ThoiQuenSong", System.Data.SqlDbType.NVarChar).Value = (object?)hs.ThoiQuenSong ?? DBNull.Value;
        cmd.Parameters.Add("@ThongTinKhac", System.Data.SqlDbType.NVarChar).Value = (object?)hs.ThongTinKhac ?? DBNull.Value;
        cmd.Parameters.Add("@NgayCapNhat", System.Data.SqlDbType.DateTime).Value = hs.NgayCapNhat;
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
    private static HoSoBenhAn MapToEntity(SqlDataReader r)
    {
        return new HoSoBenhAn(
            r.GetInt32(r.GetOrdinal("HoSoBenhAnID")),
            r.GetInt32(r.GetOrdinal("BenhNhanID")),
            r["BenhNen"] as string,
            r["DiUng"] as string,
            r["TienSuBenh"] as string,
            r["TienSuGiaDinh"] as string,
            r["ThoiQuenSong"] as string,
            r["ThongTinKhac"] as string,
            r.GetDateTime(r.GetOrdinal("NgayTao")),
            r.GetDateTime(r.GetOrdinal("NgayCapNhat"))
        );
    }
    private static HoSoBenhAnListReadModel MapToListDTO(SqlDataReader r)
    {
        return new HoSoBenhAnListReadModel
        {
            HoSoBenhAnID = r.GetInt32(r.GetOrdinal("HoSoBenhAnID")),
            BenhNhanID = r.GetInt32(r.GetOrdinal("BenhNhanID")),
            NgayTao = r.GetDateTime(r.GetOrdinal("NgayTao"))
        };
    }
    private static HoSoBenhAnReadModel MapToDetailDTO(SqlDataReader r)
    {
        return new HoSoBenhAnReadModel
        {
            HoSoBenhAnID = r.GetInt32(r.GetOrdinal("HoSoBenhAnID")),
            BenhNhanID = r.GetInt32(r.GetOrdinal("BenhNhanID")),
            BenhNen = r["BenhNen"] as string,
            DiUng = r["DiUng"] as string,
            TienSuBenh = r["TienSuBenh"] as string,
            TienSuGiaDinh = r["TienSuGiaDinh"] as string,
            ThoiQuenSong = r["ThoiQuenSong"] as string,
            ThongTinKhac = r["ThongTinKhac"] as string,
            NgayTao = r.GetDateTime(r.GetOrdinal("NgayTao")),
            NgayCapNhat = r.GetDateTime(r.GetOrdinal("NgayCapNhat"))
        };
    }
}