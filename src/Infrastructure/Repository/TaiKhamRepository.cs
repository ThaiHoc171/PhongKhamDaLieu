using System.Reflection.Metadata;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace Infrastructure.Repository;

public class TaiKhamRepository : ITaiKhamRepository
{
    private readonly string _connectionString;

    public TaiKhamRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException("Connection string not found");
    }
    public async Task<TaiKham?> GetByIdAsync(int taiKhamID)
    {
        const string sql = @"SELECT TaiKhamID, PhienKhamID, BenhNhanID, NgayDuKien, LyDo, TrangThai, CaKhamID, NgayTao 
                             FROM TaiKham WHERE TaiKhamID = @taiKhamID";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@taiKhamID", taiKhamID);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }
    public async Task<TaiKham?> GetByBenhNhanIdAsync(int benhNhanID)
    {
        const string sql = @"SELECT TOP 1 TaiKhamID, PhienKhamID, BenhNhanID, NgayDuKien, LyDo, TrangThai, CaKhamID, NgayTao
                                FROM TaiKham
                                WHERE BenhNhanID = @benhNhanID
                                ORDER BY NgayDuKien DESC;";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@benhNhanID", benhNhanID);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }
    public async Task<int?> GetIdByBenhNhanIdAsync(int benhNhanID)
    {
        const string sql = @"SELECT TaiKhamID
                                FROM TaiKham
                                WHERE BenhNhanID = @benhNhanID
                                ORDER BY NgayDuKien DESC;";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@benhNhanID", benhNhanID);
        await conn.OpenAsync();
        var result = await cmd.ExecuteScalarAsync();
        return result == null ? null : (int)result;
    }
    public async Task<List<TaiKham>> GetAllAsync()
    {
        const string sql = @"SELECT TaiKhamID, PhienKhamID, BenhNhanID, NgayDuKien, LyDo, TrangThai, CaKhamID, NgayTao 
                             FROM TaiKham";
        var list = new List<TaiKham>();
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
    public async Task<List<TaiKham>> LocAsync(DateTime ngayDuKien, string trangThai)
    {
        const string sql = @"SELECT TaiKhamID, PhienKhamID, BenhNhanID, NgayDuKien, LyDo, TrangThai, CaKhamID, NgayTao 
                             FROM TaiKham WHERE NgayDuKien = @ngayDuKien AND TrangThai = @trangThai";
        var list = new List<TaiKham>();
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ngayDuKien", ngayDuKien);
        cmd.Parameters.AddWithValue("@trangThai", trangThai);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(MapToEntity(reader));
        }
        return list;
    }
    public async Task<List<TaiKham>> GetListByBenhNhanAsync(int benhNhanID)
    {
        const string sql = @"SELECT TaiKhamID, PhienKhamID, BenhNhanID, NgayDuKien, LyDo, TrangThai, CaKhamID, NgayTao 
                             FROM TaiKham WHERE BenhNhanID = @benhNhanID";
        var list = new List<TaiKham>();
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@benhNhanID", benhNhanID);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(MapToEntity(reader));
        }
        return list;
    }

    public async Task<int> AddAsync(TaiKham taiKham)
    {
        const string sql = @"
			INSERT INTO TaiKham (PhienKhamID, BenhNhanID, NgayDuKien, LyDo) 
			OUTPUT INSERTED.TaiKhamID
			VALUES (@PhienKhamID, @BenhNhanID, @NgayDuKien, @LyDo)";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@PhienKhamID", taiKham.PhienKhamID);
        cmd.Parameters.AddWithValue("@BenhNhanID", taiKham.BenhNhanID);
        cmd.Parameters.AddWithValue("@NgayDuKien", taiKham.NgayDuKien);
        cmd.Parameters.AddWithValue("@LyDo", taiKham.LyDo);

        await conn.OpenAsync();
        return (int)await cmd.ExecuteScalarAsync();
    }

    public async Task UpdateAsync(TaiKham taiKham)
    {
        const string sql = @"
        UPDATE TaiKham 
        SET TrangThai = @TrangThai,
            CaKhamID = @CaKhamID
        WHERE TaiKhamID = @Id";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@TrangThai", taiKham.TrangThai ?? "");
        cmd.Parameters.AddWithValue("@CaKhamID", (object?)taiKham.CaKhamID ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Id", taiKham.TaiKhamID); 

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
    private static TaiKham MapToEntity(SqlDataReader reader)
    {
        return new TaiKham(
            taiKhamID: reader.GetInt32(0),
            phienKhamID: reader.GetInt32(1),
            benhNhanID: reader.GetInt32(2),
            ngayDuKien: reader.GetDateTime(3),
            lyDo: reader.IsDBNull(4) ? null : reader.GetString(4),
            trangThai: reader.IsDBNull(5) ? null : reader.GetString(5),
            caKhamID: reader.IsDBNull(6) ? null : reader.GetInt32(6),
            ngayTao: reader.GetDateTime(7)
            );
    }
}
