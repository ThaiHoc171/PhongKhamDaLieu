using Application.Interfaces;
using Application.ReadModels;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

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
									 GhiChu, HinhAnhJSON, ChuanDoanCuoi, NgayKham, TrangThai
							FROM PhienKham
							WHERE PhienKhamID = @PhienKhamID ";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PhienKhamID", phienKhamID);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (await reader.ReadAsync())
		{
			return new PhienKham(
				reader.GetInt32(0),
				reader.GetInt32(1),
				reader.GetInt32(2),
				reader.GetInt32(3),
				reader.IsDBNull(4) ? null : reader.GetInt32(4),
				reader.IsDBNull(5) ? null : reader.GetString(5),
				reader.IsDBNull(6) ? null :  reader.GetString(6),
				reader.IsDBNull(7) ? null : reader.GetString(7),
				reader.IsDBNull(8) ? null : reader.GetString(8),
				reader.GetDateTime(9),
				reader.GetString(10)
			);
		}
		return null;
	}
	public async Task<int> AddAsync(PhienKham phienKham)
	{
		const string sql = @" INSERT INTO PhienKham (CaKhamID, BenhNhanID, NhanVienID, PhongChucNangID, TrieuChung, GhiChu, HinhAnhJSON, TrangThai)
							  OUTPUT INSERTED.PhienKhamID
							  VALUES (@CaKhamID, @BenhNhanID, @NhanVienID, @PhongChucNangID, @TrieuChung, @GhiChu, @HinhAnhJSON, @TrangThai);
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
		cmd.Parameters.AddWithValue("@TrangThai", phienKham.TrangThai);
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
						 SET ChuanDoanCuoi = @ChuanDoanCuoi,
							 TrangThai = @TrangThai
						 WHERE PhienKhamID = @PhienKhamID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@ChuanDoanCuoi", phienKham.ChuanDoanCuoi);
		cmd.Parameters.AddWithValue("@TrangThai", phienKham.TrangThai);
		cmd.Parameters.AddWithValue("@PhienKhamID", phienKham.PhienKhamID);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	// Readmodels

	public async Task<PhienKhamReadModel?> GetReadModelByIdAsync(int phienKhamID)
	{
		const string sql = @"
		SELECT 
			pk.PhienKhamID, pk.CaKhamID, pk.BenhNhanID, tbn.HoTen, pk.NhanVienID,
			tnv.HoTen, pk.NgayKham, pk.TrangThai, pk.ChuanDoanCuoi
		FROM PhienKham pk
		JOIN BenhNhan bn ON pk.BenhNhanID = bn.BenhNhanID
		JOIN ThongTinCaNhan tbn ON bn.ThongTinID = tbn.ThongTinID
		JOIN NhanVien nv ON pk.NhanVienID = nv.NhanVienID
		JOIN ThongTinCaNhan tnv ON nv.ThongTinID = tnv.ThongTinID
		WHERE pk.PhienKhamID = @ID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@ID", phienKhamID);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		if (!await reader.ReadAsync()) return null;

		return MapReadModel(reader);
	}
	public async Task<List<PhienKhamReadModel>> GetAllAsync()
	{
		const string sql = @"
		SELECT 
			pk.PhienKhamID, pk.CaKhamID, pk.BenhNhanID, tbn.HoTen,
			pk.NhanVienID, tnv.HoTen, pk.NgayKham, pk.TrangThai,
			pk.ChuanDoanCuoi
		FROM PhienKham pk
		JOIN BenhNhan bn ON pk.BenhNhanID = bn.BenhNhanID
		JOIN ThongTinCaNhan tbn ON bn.ThongTinID = tbn.ThongTinID
		JOIN NhanVien nv ON pk.NhanVienID = nv.NhanVienID
		JOIN ThongTinCaNhan tnv ON nv.ThongTinID = tnv.ThongTinID
		ORDER BY pk.NgayKham DESC";

		var list = new List<PhienKhamReadModel>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapReadModel(reader));

		return list;
	}

	public async Task<List<PhienKhamReadModel>> FilterAsync(DateTime? tuNgay,DateTime? denNgay,string? trangThai,int? nhanVienID)
	{
		var sql = @"
		SELECT 
			pk.PhienKhamID, pk.CaKhamID, pk.BenhNhanID, tbn.HoTen,
			pk.NhanVienID, tnv.HoTen, pk.NgayKham, pk.TrangThai,
			pk.ChuanDoanCuoi
		FROM PhienKham pk
		JOIN BenhNhan bn ON pk.BenhNhanID = bn.BenhNhanID
		JOIN ThongTinCaNhan tbn ON bn.ThongTinID = tbn.ThongTinID
		JOIN NhanVien nv ON pk.NhanVienID = nv.NhanVienID
		JOIN ThongTinCaNhan tnv ON nv.ThongTinID = tnv.ThongTinID
		WHERE 1 = 1";

		var cmd = new SqlCommand();

		if (tuNgay.HasValue)
		{
			sql += " AND pk.NgayKham >= @TuNgay";
			cmd.Parameters.AddWithValue("@TuNgay", tuNgay.Value);
		}

		if (denNgay.HasValue)
		{
			sql += " AND pk.NgayKham <= @DenNgay";
			cmd.Parameters.AddWithValue("@DenNgay", denNgay.Value);
		}

		if (!string.IsNullOrEmpty(trangThai))
		{
			sql += " AND pk.TrangThai = @TrangThai";
			cmd.Parameters.AddWithValue("@TrangThai", trangThai);
		}

		if (nhanVienID.HasValue)
		{
			sql += " AND pk.NhanVienID = @NhanVienID";
			cmd.Parameters.AddWithValue("@NhanVienID", nhanVienID.Value);
		}

		sql += " ORDER BY pk.NgayKham DESC";

		cmd.CommandText = sql;

		var list = new List<PhienKhamReadModel>();
		await using var conn = new SqlConnection(_connectionString);
		cmd.Connection = conn;

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapReadModel(reader));

		return list;
	}
	private static PhienKhamReadModel MapReadModel(SqlDataReader reader)
	{
		return new PhienKhamReadModel
		{
			PhienKhamID = reader.GetInt32(0),
			CaKhamID = reader.GetInt32(1),
			BenhNhanID = reader.GetInt32(2),
			TenBenhNhan = reader.GetString(3),
			NhanVienID = reader.GetInt32(4),
			TenNhanVien = reader.GetString(5),
			NgayKham = reader.GetDateTime(6),
			TrangThai = reader.GetString(7),
			ChuanDoanCuoi = reader.IsDBNull(8) ? null : reader.GetString(8)
		};
	}

}
