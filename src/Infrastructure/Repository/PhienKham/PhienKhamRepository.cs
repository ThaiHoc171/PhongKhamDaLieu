using Application.Interfaces;
using Domain.Enums;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repository;

public class PhienKhamRepository : IPhienKhamRepository
{
	private readonly string _connectionString;
	public PhienKhamRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection") 
			?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
	}
	public async Task<PhienKham?> GetByIdAsync(int phienKhamID)
	{
		const string sql = @" SELECT PhienKhamID, CaKhamID, BenhNhanID, NhanVienID, PhongChucNangID, TrieuChung,
									 GhiChu, HinhAnhJSON, ChanDoanCuoi, NgayKham, TrangThai
							FROM PhienKham
							WHERE PhienKhamID = @PhienKhamID ";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PhienKhamID", phienKhamID);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}
    public async Task<int?> GetBenhNhanIdByPhienKhamIdAsync(int phienKhamID)
    {
        const string sql = @"
		SELECT BenhNhanID
		FROM PhienKham
		WHERE PhienKhamID = @phienKhamID
	";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@phienKhamID", phienKhamID);

        await conn.OpenAsync();
        var result = await cmd.ExecuteScalarAsync();

        return result == null ? null : (int)result;
    }
    public async Task<int> AddAsync(PhienKham phienKham)
	{
		const string sql = @" INSERT INTO PhienKham (CaKhamID, BenhNhanID, NhanVienID, PhongChucNangID, TrieuChung, GhiChu, HinhAnhJSON)
							  OUTPUT INSERTED.PhienKhamID
							  VALUES (@CaKhamID, @BenhNhanID, @NhanVienID, @PhongChucNangID, @TrieuChung, @GhiChu, @HinhAnhJSON);
							  SELECT SCOPE_IDENTITY(); ";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@CaKhamID", phienKham.CaKhamID);
		cmd.Parameters.AddWithValue("@BenhNhanID", phienKham.BenhNhanID);
		cmd.Parameters.AddWithValue("@NhanVienID", phienKham.NhanVienID);
		cmd.Parameters.AddWithValue("@PhongChucNangID", (object?)phienKham.PhongChucNangID ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@TrieuChung", (object?)phienKham.TrieuChung ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@GhiChu", (object?)phienKham.GhiChu ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@HinhAnhJSON", (object?)phienKham.HinhAnhJSON ?? DBNull.Value);
		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		return Convert.ToInt32(result);
	}
	public async Task UpdateAsync(PhienKham phienKham)
	{
		const string sql = @" UPDATE PhienKham
							  SET TrieuChung = @TrieuChung,
								  GhiChu = @GhiChu,
								  PhongChucNangID = @PhongChucNangID,
								  HinhAnhJSON = @HinhAnhJSON
							  WHERE PhienKhamID = @PhienKhamID ";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@TrieuChung", (object?)phienKham.TrieuChung ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@GhiChu", (object?)phienKham.GhiChu ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@PhongChucNangID", (object?)phienKham.PhongChucNangID ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@HinhAnhJSON", (object?)phienKham.HinhAnhJSON ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@PhienKhamID", phienKham.PhienKhamID);
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task KetThucAsync(PhienKham phienKham)
	{
		const string sql = @"UPDATE PhienKham
						 SET ChanDoanCuoi = @ChanDoanCuoi,
							 TrangThai = @TrangThai
						 WHERE PhienKhamID = @PhienKhamID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@ChanDoanCuoi", phienKham.ChanDoanCuoi);
		cmd.Parameters.AddWithValue("@TrangThai", phienKham.TrangThai.ToDbValue());
		cmd.Parameters.AddWithValue("@PhienKhamID", phienKham.PhienKhamID);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task<List<PhienKham>> GetAllAsync()
	{
		const string sql = @"
			SELECT PhienKhamID, CaKhamID, BenhNhanID,
				   NhanVienID, NgayKham, TrangThai, ChanDoanCuoi
			FROM PhienKham
			ORDER BY NgayKham DESC";

		var list = new List<PhienKham>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToEntityLite(reader));

		return list;
	}

	public async Task<List<PhienKham>> FilterAsync(DateTime? tuNgay,DateTime? denNgay,string? trangThai,int? nhanVienID)
	{
		var sql = @"
		SELECT PhienKhamID, CaKhamID, BenhNhanID,
				   NhanVienID, NgayKham, TrangThai, ChanDoanCuoi
			FROM PhienKham
		WHERE 1 = 1";

		var cmd = new SqlCommand();

		if (tuNgay.HasValue)
		{
			sql += " AND NgayKham >= @TuNgay";
			cmd.Parameters.AddWithValue("@TuNgay", tuNgay.Value);
		}

		if (denNgay.HasValue)
		{
			sql += " AND NgayKham <= @DenNgay";
			cmd.Parameters.AddWithValue("@DenNgay", denNgay.Value);
		}

		if (!string.IsNullOrEmpty(trangThai))
		{
			sql += " AND TrangThai = @TrangThai";
			cmd.Parameters.AddWithValue("@TrangThai", trangThai);
		}

		if (nhanVienID.HasValue)
		{
			sql += " AND NhanVienID = @NhanVienID";
			cmd.Parameters.AddWithValue("@NhanVienID", nhanVienID.Value);
		}

		sql += " ORDER BY NgayKham DESC";

		cmd.CommandText = sql;

		var list = new List<PhienKham>();
		await using var conn = new SqlConnection(_connectionString);
		cmd.Connection = conn;

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToEntityLite(reader));

		return list;
	}
	private static PhienKham MapToEntity(SqlDataReader r) => new(
		phienKhamID: r.GetInt32(r.GetOrdinal("PhienKhamID")),
		caKhamID: r.GetInt32(r.GetOrdinal("CaKhamID")),
		benhNhanID: r.GetInt32(r.GetOrdinal("BenhNhanID")),
		nhanVienID: r.GetInt32(r.GetOrdinal("NhanVienID")),
		phongChucNangID: r.IsDBNull("PhongChucNangID") ? null : r.GetInt32("PhongChucNangID"),
		trieuChung: r.IsDBNull("TrieuChung") ? null : r.GetString("TrieuChung"),
		ghiChu: r.IsDBNull("GhiChu") ? null : r.GetString("GhiChu"),
		hinhAnhJSON: r.IsDBNull("HinhAnhJSON") ? null : r.GetString("HinhAnhJSON"),
		chanDoanCuoi: r.IsDBNull("ChanDoanCuoi") ? null : r.GetString("ChanDoanCuoi"),
		ngayKham: r.GetDateTime("NgayKham"),
		trangThai: r.GetString("TrangThai")
	);

	private static PhienKham MapToEntityLite(SqlDataReader r) => new(
		phienKhamID: r.GetInt32("PhienKhamID"),
		caKhamID: r.GetInt32("CaKhamID"),
		benhNhanID: r.GetInt32("BenhNhanID"),
		nhanVienID: r.GetInt32("NhanVienID"),
		ngayKham: r.GetDateTime("NgayKham"),
		trangThai: r.GetString("TrangThai"),
		chanDoanCuoi: r.IsDBNull("ChanDoanCuoi") ? null : r.GetString("ChanDoanCuoi")
	);
}
