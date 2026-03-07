using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
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
			?? throw new InvalidOperationException("Connection string not found.");
	}

	private SqlConnection CreateConnection() => new(_connectionString);

	public async Task<PhienKham?> GetByIdAsync(int id)
	{
		const string sql =
		@"SELECT PhienKhamID, CaKhamID, BenhNhanID, NhanVienID, PhongChucNangID,
                 TrieuChung, GhiChu, HinhAnhJSON, ChanDoanCuoi, NgayKham, TrangThai
          FROM PhienKham
          WHERE PhienKhamID=@Id";

		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@Id", id);

		await conn.OpenAsync();

		await using var reader = await cmd.ExecuteReaderAsync();

		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}

	public async Task<(List<PhienKhamListReadModel>, int)> GetPagedAsync(int page, int size, int? nhanVienID, string? trangThai)
	{
		var sql =
		@"SELECT pk.PhienKhamID, pk.CaKhamID, pk.NgayKham, pk.TrangThai, pk.ChanDoanCuoi,
		   bn.BenhNhanID, bn_ttc.HoTen AS TenBenhNhan,
		   nv.NhanVienID, nv_ttc.HoTen AS TenNhanVien
		FROM PhienKham pk
		JOIN BenhNhan bn ON pk.BenhNhanID = bn.BenhNhanID
		JOIN ThongTinCaNhan bn_ttc ON bn.ThongTinID = bn_ttc.ThongTinID
		JOIN NhanVien nv ON pk.NhanVienID = nv.NhanVienID
		JOIN ThongTinCaNhan nv_ttc ON nv.ThongTinID = nv_ttc.ThongTinID
		WHERE 1=1";

		var cmd = new SqlCommand();

		ApplyFilter(ref sql, cmd, nhanVienID, trangThai);

		sql +=
		@" ORDER BY pk.NgayKham DESC
           OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

           SELECT COUNT(*) FROM PhienKham pk WHERE 1=1 ";

		if (nhanVienID.HasValue)
			sql += " AND pk.NhanVienID=@NhanVienID";

		if (!string.IsNullOrEmpty(trangThai))
			sql += " AND pk.TrangThai=@TrangThai";

		cmd.Parameters.AddWithValue("@Offset", (page - 1) * size);
		cmd.Parameters.AddWithValue("@PageSize", size);

		cmd.CommandText = sql;

		var list = new List<PhienKhamListReadModel>();
		int total = 0;

		await using var conn = CreateConnection();
		cmd.Connection = conn;

		await conn.OpenAsync();

		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToLiteDTO(reader));

		if (await reader.NextResultAsync() && await reader.ReadAsync())
			total = reader.GetInt32(0);

		return (list, total);
	}

	public async Task<(List<PhienKhamListReadModel>, int)> GetByBenhNhanPagedAsync(int benhNhanID, int page, int size)
	{
		const string sql =
		@"SELECT pk.PhienKhamID, pk.CaKhamID, pk.NgayKham, pk.TrangThai, pk.ChanDoanCuoi,
			   bn.BenhNhanID, bn_ttc.HoTen AS TenBenhNhan,
			   nv.NhanVienID, nv_ttc.HoTen AS TenNhanVien
		FROM PhienKham pk
		JOIN BenhNhan bn ON pk.BenhNhanID = bn.BenhNhanID
		JOIN ThongTinCaNhan bn_ttc ON bn.ThongTinID = bn_ttc.ThongTinID
		JOIN NhanVien nv ON pk.NhanVienID = nv.NhanVienID
		JOIN ThongTinCaNhan nv_ttc ON nv.ThongTinID = nv_ttc.ThongTinID
		WHERE pk.BenhNhanID=@BenhNhanID
		ORDER BY pk.NgayKham DESC
		OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

          SELECT COUNT(*) FROM PhienKham WHERE BenhNhanID=@BenhNhanID";

		var list = new List<PhienKhamListReadModel>();
		int total = 0;

		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@BenhNhanID", benhNhanID);
		cmd.Parameters.AddWithValue("@Offset", (page - 1) * size);
		cmd.Parameters.AddWithValue("@PageSize", size);

		await conn.OpenAsync();

		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToLiteDTO(reader));

		if (await reader.NextResultAsync() && await reader.ReadAsync())
			total = reader.GetInt32(0);

		return (list, total);
	}

	public async Task<int?> GetBenhNhanIdByPhienKhamIdAsync(int id)
	{
		const string sql = @"SELECT BenhNhanID FROM PhienKham WHERE PhienKhamID=@Id";

		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@Id", id);

		await conn.OpenAsync();

		var result = await cmd.ExecuteScalarAsync();

		return result == null ? null : (int)result;
	}

	public async Task<List<PhienKhamListReadModel>> SearchAsync(string keyword, int? nhanVienID)
	{
		var sql =
		@"SELECT pk.PhienKhamID, pk.CaKhamID, pk.NgayKham, pk.TrangThai, pk.ChanDoanCuoi,
			   bn.BenhNhanID, bn_ttc.HoTen AS TenBenhNhan,
			   nv.NhanVienID, nv_ttc.HoTen AS TenNhanVien
		FROM PhienKham pk
		JOIN BenhNhan bn ON pk.BenhNhanID = bn.BenhNhanID
		JOIN ThongTinCaNhan bn_ttc ON bn.ThongTinID = bn_ttc.ThongTinID
		JOIN NhanVien nv ON pk.NhanVienID = nv.NhanVienID
		JOIN ThongTinCaNhan nv_ttc ON nv.ThongTinID = nv_ttc.ThongTinID
		WHERE (bn_ttc.HoTen LIKE @kw OR pk.TrieuChung LIKE @kw)";

		var cmd = new SqlCommand();

		if (nhanVienID.HasValue)
		{
			sql += " AND pk.NhanVienID=@NhanVienID";
			cmd.Parameters.AddWithValue("@NhanVienID", nhanVienID.Value);
		}

		sql += " ORDER BY pk.NgayKham DESC";

		cmd.Parameters.AddWithValue("@kw", $"%{keyword}%");
		cmd.CommandText = sql;

		var list = new List<PhienKhamListReadModel>();

		await using var conn = CreateConnection();
		cmd.Connection = conn;

		await conn.OpenAsync();

		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToLiteDTO(reader));

		return list;
	}

	public async Task<PhienKhamReadModel?> GetDetailAsync(int id)
	{
		const string sql =
		@"SELECT pk.PhienKhamID, pk.CaKhamID, pk.NgayKham, pk.TrangThai,
			   pk.TrieuChung, pk.GhiChu, pk.HinhAnhJSON, pk.ChanDoanCuoi, pk.PhongChucNangID,
			   bn.BenhNhanID, bn_ttc.HoTen AS TenBenhNhan,
			   nv.NhanVienID, nv_ttc.HoTen AS TenNhanVien
		FROM PhienKham pk
		JOIN BenhNhan bn ON pk.BenhNhanID = bn.BenhNhanID
		JOIN ThongTinCaNhan bn_ttc ON bn.ThongTinID = bn_ttc.ThongTinID
		JOIN NhanVien nv ON pk.NhanVienID = nv.NhanVienID
		JOIN ThongTinCaNhan nv_ttc ON nv.ThongTinID = nv_ttc.ThongTinID
		WHERE pk.PhienKhamID=@Id";

		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@Id", id);

		await conn.OpenAsync();

		await using var reader = await cmd.ExecuteReaderAsync();

		return await reader.ReadAsync() ? MapToDetailDTO(reader) : null;
	}

	public async Task<int> AddAsync(PhienKham pk)
	{
		const string sql =
		@"INSERT INTO PhienKham
          (CaKhamID,BenhNhanID,NhanVienID,PhongChucNangID,TrieuChung,GhiChu,HinhAnhJSON)
          OUTPUT INSERTED.PhienKhamID
          VALUES (@CaKhamID,@BenhNhanID,@NhanVienID,@PhongChucNangID,@TrieuChung,@GhiChu,@HinhAnhJSON)";

		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@CaKhamID", pk.CaKhamID);
		cmd.Parameters.AddWithValue("@BenhNhanID", pk.BenhNhanID);
		cmd.Parameters.AddWithValue("@NhanVienID", pk.NhanVienID);
		cmd.Parameters.AddWithValue("@PhongChucNangID", (object?)pk.PhongChucNangID ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@TrieuChung", (object?)pk.TrieuChung ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@GhiChu", (object?)pk.GhiChu ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@HinhAnhJSON", (object?)pk.HinhAnhJSON ?? DBNull.Value);

		await conn.OpenAsync();

		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}

	public async Task UpdateAsync(PhienKham pk)
	{
		const string sql =
		@"UPDATE PhienKham
          SET TrieuChung=@TrieuChung,
              GhiChu=@GhiChu,
              PhongChucNangID=@PhongChucNangID,
              HinhAnhJSON=@HinhAnhJSON
          WHERE PhienKhamID=@Id";

		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@TrieuChung", (object?)pk.TrieuChung ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@GhiChu", (object?)pk.GhiChu ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@PhongChucNangID", (object?)pk.PhongChucNangID ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@HinhAnhJSON", (object?)pk.HinhAnhJSON ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@Id", pk.PhienKhamID);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task KetThucAsync(PhienKham pk)
	{
		const string sql =
		@"UPDATE PhienKham
          SET ChanDoanCuoi=@ChanDoanCuoi,
              TrangThai=@TrangThai
          WHERE PhienKhamID=@Id";

		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@ChanDoanCuoi", pk.ChanDoanCuoi);
		cmd.Parameters.AddWithValue("@TrangThai", pk.TrangThai.ToDbValue());
		cmd.Parameters.AddWithValue("@Id", pk.PhienKhamID);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	private static void ApplyFilter(ref string sql, SqlCommand cmd, int? nhanVienID, string? trangThai)
	{
		if (nhanVienID.HasValue)
		{
			sql += " AND pk.NhanVienID=@NhanVienID";
			cmd.Parameters.AddWithValue("@NhanVienID", nhanVienID.Value);
		}

		if (!string.IsNullOrEmpty(trangThai))
		{
			sql += " AND pk.TrangThai=@TrangThai";
			cmd.Parameters.AddWithValue("@TrangThai", trangThai);
		}
	}

	private static PhienKham MapToEntity(SqlDataReader r) => new(
		r.GetInt32("PhienKhamID"),
		r.GetInt32("CaKhamID"),
		r.GetInt32("BenhNhanID"),
		r.GetInt32("NhanVienID"),
		r.IsDBNull("PhongChucNangID") ? null : r.GetInt32("PhongChucNangID"),
		r.IsDBNull("TrieuChung") ? null : r.GetString("TrieuChung"),
		r.IsDBNull("GhiChu") ? null : r.GetString("GhiChu"),
		r.IsDBNull("HinhAnhJSON") ? null : r.GetString("HinhAnhJSON"),
		r.IsDBNull("ChanDoanCuoi") ? null : r.GetString("ChanDoanCuoi"),
		r.GetDateTime("NgayKham"),
		r.GetString("TrangThai")
	);

	private static PhienKhamListReadModel MapToLiteDTO(SqlDataReader r) => new()
	{
		PhienKhamID = r.GetInt32("PhienKhamID"),
		CaKhamID = r.GetInt32("CaKhamID"),
		NgayKham = r.GetDateTime("NgayKham"),
		TrangThai = r.GetString("TrangThai"),
		ChanDoanCuoi = r.IsDBNull("ChanDoanCuoi") ? null : r.GetString("ChanDoanCuoi"),
		BenhNhan = new NameResponseDTO { Id = r.GetInt32("BenhNhanID"), Name = r.GetString("TenBenhNhan") },
		NhanVien = new NameResponseDTO { Id = r.GetInt32("NhanVienID"), Name = r.GetString("TenNhanVien") }
	};

	private static PhienKhamReadModel MapToDetailDTO(SqlDataReader r) => new()
	{
		PhienKhamID = r.GetInt32("PhienKhamID"),
		CaKhamID = r.GetInt32("CaKhamID"),
		NgayKham = r.GetDateTime("NgayKham"),
		TrangThai = r.GetString("TrangThai"),
		TrieuChung = r.IsDBNull("TrieuChung") ? null : r.GetString("TrieuChung"),
		GhiChu = r.IsDBNull("GhiChu") ? null : r.GetString("GhiChu"),
		HinhAnhJSON = r.IsDBNull("HinhAnhJSON") ? null : r.GetString("HinhAnhJSON"),
		ChanDoanCuoi = r.IsDBNull("ChanDoanCuoi") ? null : r.GetString("ChanDoanCuoi"),
		PhongChucNangID = r.IsDBNull("PhongChucNangID") ? null : r.GetInt32("PhongChucNangID"),
		BenhNhan = new NameResponseDTO { Id = r.GetInt32("BenhNhanID"), Name = r.GetString("TenBenhNhan") },
		NhanVien = new NameResponseDTO { Id = r.GetInt32("NhanVienID"), Name = r.GetString("TenNhanVien") }
	};
}