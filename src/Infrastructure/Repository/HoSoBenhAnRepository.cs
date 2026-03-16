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
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    private const string BaseSelectList = @"
        SELECT HoSoBenhAnID,BenhNhanID,NgayTao
        FROM HoSoBenhAn";
    private const string BaseSelectDetail = @"
        SELECT HoSoBenhAnID,BenhNhanID,BenhNen,DiUng,TienSuBenh,TienSuGiaDinh,ThoiQuenSong,ThongTinKhac,NgayTao,NgayCapNhat
        FROM HoSoBenhAn";
    public async Task<(List<HoSoBenhAnListReadModel>, int)> GetPagedAsync(int page, int size)
    {
        var list = new List<HoSoBenhAnListReadModel>();
        int total = 0;
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        int offset = (page - 1) * size;
        var sql = $@"
        {BaseSelectList}
        ORDER BY NgayCapNhat DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;
        SELECT COUNT(*) FROM HoSoBenhAn";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Offset", offset);
        cmd.Parameters.AddWithValue("@Size", size);
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToListDTO(reader));
        await reader.NextResultAsync();
        if (await reader.ReadAsync())
            total = reader.GetInt32(0);
        return (list, total);
    }
    public async Task<(List<HoSoBenhAnListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size)
    {
        var list = new List<HoSoBenhAnListReadModel>();
        int total = 0;
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        int offset = (page - 1) * size;
        var sql = $@"
        {BaseSelectList}
        WHERE BenhNen LIKE @Keyword OR DiUng LIKE @Keyword
        ORDER BY NgayCapNhat DESC
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;
        SELECT COUNT(*)
        FROM HoSoBenhAn
        WHERE BenhNen LIKE @Keyword OR DiUng LIKE @Keyword";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");
        cmd.Parameters.AddWithValue("@Offset", offset);
        cmd.Parameters.AddWithValue("@Size", size);
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToListDTO(reader));
        await reader.NextResultAsync();
        if (await reader.ReadAsync())
            total = reader.GetInt32(0);
        return (list, total);
    }
    public async Task<HoSoBenhAnReadModel?> GetDetailAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = BaseSelectDetail + " WHERE HoSoBenhAnID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToDetailDTO(reader);
        return null;
    }
    public async Task<HoSoBenhAn?> GetByIdAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = BaseSelectDetail + " WHERE HoSoBenhAnID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToEntity(reader);
        return null;
    }
    public async Task<HoSoBenhAnReadModel?> GetByBenhNhanIdAsync(int benhNhanId)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = BaseSelectDetail + " WHERE BenhNhanID=@BenhNhanID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@BenhNhanID", benhNhanId);
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToDetailDTO(reader);
        return null;
    }
    public async Task AddAsync(HoSoBenhAn hs)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"INSERT INTO HoSoBenhAn
        (BenhNhanID,BenhNen,DiUng,TienSuBenh,TienSuGiaDinh,ThoiQuenSong,ThongTinKhac,NgayTao,NgayCapNhat)
        VALUES
        (@BenhNhanID,@BenhNen,@DiUng,@TienSuBenh,@TienSuGiaDinh,@ThoiQuenSong,@ThongTinKhac,@NgayTao,@NgayCapNhat)";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@BenhNhanID", hs.BenhNhanID);
        cmd.Parameters.AddWithValue("@BenhNen", (object?)hs.BenhNen ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@DiUng", (object?)hs.DiUng ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@TienSuBenh", (object?)hs.TienSuBenh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@TienSuGiaDinh", (object?)hs.TienSuGiaDinh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@ThoiQuenSong", (object?)hs.ThoiQuenSong ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@ThongTinKhac", (object?)hs.ThongTinKhac ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@NgayTao", hs.NgayTao);
        cmd.Parameters.AddWithValue("@NgayCapNhat", hs.NgayCapNhat);
        await cmd.ExecuteNonQueryAsync();
    }
    public async Task UpdateAsync(HoSoBenhAn hs)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"UPDATE HoSoBenhAn
                    SET BenhNen=@BenhNen,
                        DiUng=@DiUng,
                        TienSuBenh=@TienSuBenh,
                        TienSuGiaDinh=@TienSuGiaDinh,
                        ThoiQuenSong=@ThoiQuenSong,
                        ThongTinKhac=@ThongTinKhac,
                        NgayCapNhat=@NgayCapNhat
                    WHERE HoSoBenhAnID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", hs.HoSoBenhAnID);
        cmd.Parameters.AddWithValue("@BenhNen", (object?)hs.BenhNen ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@DiUng", (object?)hs.DiUng ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@TienSuBenh", (object?)hs.TienSuBenh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@TienSuGiaDinh", (object?)hs.TienSuGiaDinh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@ThoiQuenSong", (object?)hs.ThoiQuenSong ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@ThongTinKhac", (object?)hs.ThongTinKhac ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@NgayCapNhat", hs.NgayCapNhat);
        await cmd.ExecuteNonQueryAsync();
    }
    private HoSoBenhAn MapToEntity(SqlDataReader r)
    {
        return new HoSoBenhAn(
            (int)r["HoSoBenhAnID"],
            (int)r["BenhNhanID"],
            r["BenhNen"] as string,
            r["DiUng"] as string,
            r["TienSuBenh"] as string,
            r["TienSuGiaDinh"] as string,
            r["ThoiQuenSong"] as string,
            r["ThongTinKhac"] as string,
            (DateTime)r["NgayTao"],
            (DateTime)r["NgayCapNhat"]
        );
    }
    private HoSoBenhAnListReadModel MapToListDTO(SqlDataReader r)
    {
        return new HoSoBenhAnListReadModel
        {
            HoSoBenhAnID = (int)r["HoSoBenhAnID"],
            BenhNhanID = (int)r["BenhNhanID"],
            NgayTao = (DateTime)r["NgayTao"]
        };
    }
    private HoSoBenhAnReadModel MapToDetailDTO(SqlDataReader r)
    {
        return new HoSoBenhAnReadModel
        {
            HoSoBenhAnID = (int)r["HoSoBenhAnID"],
            BenhNhanID = (int)r["BenhNhanID"],
            BenhNen = r["BenhNen"] as string,
            DiUng = r["DiUng"] as string,
            TienSuBenh = r["TienSuBenh"] as string,
            TienSuGiaDinh = r["TienSuGiaDinh"] as string,
            ThoiQuenSong = r["ThoiQuenSong"] as string,
            ThongTinKhac = r["ThongTinKhac"] as string,
            NgayTao = (DateTime)r["NgayTao"],
            NgayCapNhat = (DateTime)r["NgayCapNhat"]
        };
    }
}