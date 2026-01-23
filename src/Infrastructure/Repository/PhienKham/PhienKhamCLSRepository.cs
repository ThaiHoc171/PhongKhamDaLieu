using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Domain.Entities;
using Application.ReadModels;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repository;

public class PhienKhamCLSRepository : IPhienKhamCLSRepository
{
	private readonly string _connectionString;
	public PhienKhamCLSRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}
	public async Task<PhienKhamCLS?> GetByIdAsync(int id)
	{
		const string sql = @"
		SELECT PhienKham_CanLamSangID, PhienKhamID, CanLamSangID,
			   TrangThai, KetQua, FileDinhKem, NgayThucHien,
			   NhanVienChiDinhID, NhanVienThucHienID, GhiChu
		FROM PhienKham_CanLamSang
		WHERE PhienKham_CanLamSangID = @ID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@ID", id);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		if (!await reader.ReadAsync()) return null;

		return new PhienKhamCLS(
			reader.GetInt32(0),
			reader.GetInt32(1),
			reader.GetInt32(2),
			reader.GetString(3),
			reader.IsDBNull(4) ? null : reader.GetString(4),
			reader.IsDBNull(5) ? null : reader.GetString(5),
			reader.GetDateTime(6),
			reader.GetInt32(7),
			reader.IsDBNull(8) ? 0 : reader.GetInt32(8),
			reader.IsDBNull(9) ? null : reader.GetString(9)
		);
	}

	public async Task<List<PhienKhamCLSReadModel>> GetByPhienKhamAsync(int phienKhamID)
	{
		const string sql = @"
			SELECT pkcls.PhienKham_CanLamSangID, pkcls.PhienKhamID, pkcls.CanLamSangID, cls.TenCLS,
				pkcls.KetQua, pkcls.FileDinhKem, pkcls.NgayThucHien, pkcls.NhanVienThucHienID,
				ttc.HoTen, pkcls.GhiChu, pkcls.TrangThai
			FROM PhienKham_CanLamSang pkcls
			INNER JOIN CanLamSang cls ON pkcls.CanLamSangID = cls.CanLamSangID
			LEFT JOIN NhanVien nv ON pkcls.NhanVienThucHienID = nv.NhanVienID
			LEFT JOIN ThongTinCaNhan ttc ON nv.ThongTinID = ttc.ThongTinID
			WHERE pkcls.PhienKhamID = @PhienKhamID
			ORDER BY pkcls.NgayThucHien
			";
		var list = new List<PhienKhamCLSReadModel>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PhienKhamID", phienKhamID);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (reader.Read())
		{
			list.Add(new PhienKhamCLSReadModel
			{
				Id = reader.GetInt32(0),
				PhienKhamID = reader.GetInt32(1),
				CLSID = reader.GetInt32(2),
				TenCLS = reader.GetString(3),
				KetQua = reader.IsDBNull(4) ? null : reader.GetString(4),
				FiledinhKem = reader.IsDBNull(5) ? null : reader.GetString(5),
				NgayThucHien = reader.GetDateTime(6),
				NhanVienThucHienID = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
				NhanVienThucHienHoTen = reader.IsDBNull(8) ? "" : reader.GetString(8),
				GhiChu = reader.IsDBNull(9) ? null : reader.GetString(9),
				TrangThai = reader.GetString(10)
			});
		}
		return list;
	}
	public async Task AddAsync(PhienKhamCLS phienKhamCLS)
	{
		const string sql = @"
			INSERT INTO PhienKham_CanLamSang
			(PhienKhamID, CanLamSangID, NhanVienChiDinhID, GhiChu)
			VALUES
			(@PhienKhamID, @CanLamSangID, @NhanVienChiDinhID, @GhiChu);
			SELECT SCOPE_IDENTITY();
			";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PhienKhamID", phienKhamCLS.PhienKhamID);
		cmd.Parameters.AddWithValue("@CanLamSangID", phienKhamCLS.CLSID);
		cmd.Parameters.AddWithValue("@NhanVienChiDinhID", phienKhamCLS.NhanVienChiDinhID);
		cmd.Parameters.AddWithValue("@GhiChu", (object?)phienKhamCLS.GhiChu ?? DBNull.Value);
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task UpdateAsync(PhienKhamCLS phienKhamCLS)
	{
		const string sql = @"
			UPDATE PhienKham_CanLamSang
			SET TrangThai = @TrangThai,
				KetQua = @KetQua,
				FileDinhKem = @FileDinhKem,
				NhanVienThucHienID = @NhanVienThucHienID,
				GhiChu = @GhiChu
			WHERE PhienKham_CanLamSangID = @PhienKham_CanLamSangID;
			";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PhienKham_CanLamSangID", phienKhamCLS.PhienKhamCLSID);
		cmd.Parameters.AddWithValue("@TrangThai", phienKhamCLS.TrangThai);
		cmd.Parameters.AddWithValue("@KetQua", (object?)phienKhamCLS.KetQua ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@FileDinhKem", (object?)phienKhamCLS.FileDinhKem ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@NhanVienThucHienID", phienKhamCLS.NhanVienThucHienID);
		cmd.Parameters.AddWithValue("@GhiChu", (object?)phienKhamCLS.GhiChu ?? DBNull.Value);
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
}
