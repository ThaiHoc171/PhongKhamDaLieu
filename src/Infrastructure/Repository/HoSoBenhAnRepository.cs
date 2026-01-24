using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class HoSoBenhAnRepository : IHoSoBenhAnRepository
{
    private readonly string _connectionString;

    public HoSoBenhAnRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException("Connection string not found");
    }

    public async Task<List<HoSoBenhAn>> GetAllAsync()
    {
        const string sql = @"SELECT HoSoBenhAnID, BenhNhanID, BenhNen, DiUng, TienSuBenh, TienSuGiaDinh, ThoiQuenSong, ThongTinKhac, NgayTao, NgayCapNhat
                            FROM HoSoBenhAn";

        var list = new List<HoSoBenhAn>();

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(MapToEntity(reader));
        }

        return list;
    }
    public async Task<HoSoBenhAn?> GetByIdAsync(int hoSoBenhAnID)
    {
        const string sql = @"SELECT HoSoBenhAnID, BenhNhanID, BenhNen, DiUng, TienSuBenh, TienSuGiaDinh, ThoiQuenSong, ThongTinKhac, NgayTao, NgayCapNhat
                            FROM HoSoBenhAn WHERE HoSoBenhAnID = @hoSoBenhAnID";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@hoSoBenhAnID", hoSoBenhAnID);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }
    public async Task<HoSoBenhAn?> GetByBenhNhanIdAsync(int benhNhanID)
    {
        const string sql = @"SELECT HoSoBenhAnID, BenhNhanID, BenhNen, DiUng, TienSuBenh, TienSuGiaDinh, ThoiQuenSong, ThongTinKhac, NgayTao, NgayCapNhat
                            FROM HoSoBenhAn WHERE BenhNhanID = @benhNhanID";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@benhNhanID", benhNhanID);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }
    public async Task<int> AddAsync(HoSoBenhAn hs)
    {
        const string sql = @"INSERT INTO HoSoBenhAn(BenhNhanID, BenhNen, DiUng, TienSuBenh, TienSuGiaDinh, ThoiQuenSong, ThongTinKhac, NgayTao, NgayCapNhat)
                            VALUES (@benhNhanID, @benhNen, @diUng, @tienSuBenh, @tienSuGiaDinh, @thoiQuenSong, @thongTinKhac, @ngayTao, @ngayCapNhat)";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@benhNhanID", hs.BenhNhanID);
        cmd.Parameters.AddWithValue("@benhNen", hs.BenhNen ?? "");
        cmd.Parameters.AddWithValue("@diUng", hs.DiUng ?? "");
        cmd.Parameters.AddWithValue("@tienSuBenh", hs.TienSuBenh ?? "");
        cmd.Parameters.AddWithValue("@tienSuGiaDinh", hs.TienSuGiaDinh ?? "");
        cmd.Parameters.AddWithValue("@thoiQuenSong", hs.ThoiQuenSong ?? "");
        cmd.Parameters.AddWithValue("@thongTinKhac", hs.ThongTinKhac ?? "");
        cmd.Parameters.AddWithValue("@ngayTao", hs.NgayTao);
        cmd.Parameters.AddWithValue("@ngayCapNhat", hs.NgayCapNhat);

        await conn.OpenAsync();
        return (int)await cmd.ExecuteScalarAsync();
    }

    public async Task UpdateAsync(HoSoBenhAn hs)
    {
        const string sql = @"UPDATE HoSoBenhAn
                            SET BenhNen = @benhNen, DiUng = @diUng TienSuBenh = @tienSuBenh, TienSuGiaDinh = @tienSuGiaDinh, ThoiQuenSong = @thoiQuenSong, ThongTinKhac = @thongTinKhac, NgayTao = @ngayTao, NgayCapNhat = @ngayCapNhat)
                            WHERE HoSoBenhAnID =  @hoSoBenhAnID";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@hoSoBenhAnID", hs.HoSoBenhAnID);
        cmd.Parameters.AddWithValue("@benhNen", hs.BenhNen ?? "");
        cmd.Parameters.AddWithValue("@diUng", hs.DiUng ?? "");
        cmd.Parameters.AddWithValue("@tienSuBenh", hs.TienSuBenh ?? "");
        cmd.Parameters.AddWithValue("@tienSuGiaDinh", hs.TienSuGiaDinh ?? "");
        cmd.Parameters.AddWithValue("@thoiQuenSong", hs.ThoiQuenSong ?? "");
        cmd.Parameters.AddWithValue("@thongTinKhac", hs.ThongTinKhac ?? "");
        cmd.Parameters.AddWithValue("@ngayTao", hs.NgayTao);
        cmd.Parameters.AddWithValue("@ngayCapNhat", hs.NgayCapNhat);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
    private static HoSoBenhAn MapToEntity(SqlDataReader reader)
    {
        return new HoSoBenhAn(
            hoSoBenhAnID: reader.GetInt32(0),
            benhNhanID: reader.GetInt32(1),
            benhNen: reader.IsDBNull(2) ? null : reader.GetString(2),
            diUng: reader.IsDBNull(3) ? null : reader.GetString(3),
            tienSuBenh: reader.IsDBNull(4) ? null : reader.GetString(4),
            tienSuGiaDinh: reader.IsDBNull(5) ? null : reader.GetString(5),
            thoiQuenSong: reader.IsDBNull(6) ? null : reader.GetString(6),
            thongTinKhac: reader.IsDBNull(7) ? null : reader.GetString(7),
            ngayTao: reader.GetDateTime(8),
            ngayCapNhat: reader.GetDateTime(9)
        );
    }
}

