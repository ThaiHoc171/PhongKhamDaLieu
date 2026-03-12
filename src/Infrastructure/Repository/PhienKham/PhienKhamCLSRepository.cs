using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

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

		return MapToEntity(reader);
	}

	public async Task<List<PhienKhamClsListReadModel>> GetByPhienKhamAsync(int phienKhamID)
	{
		const string sql = @"
			SELECT 
				pk.PhienKham_CanLamSangID, cls.TenCLS, pk.TrangThai, pk.KetQua, pk.NgayThucHien, pk.GhiChu
			FROM PhienKham_CanLamSang pk
			JOIN CanLamSang cls ON pk.CanLamSangID = cls.CanLamSangID
			WHERE pk.PhienKhamID = @PhienKhamID
			ORDER BY pk.NgayThucHien";

		var list = new List<PhienKhamClsListReadModel>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@PhienKhamID", phienKhamID);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add(new PhienKhamClsListReadModel
			{
				PhienKhamCLSID = reader.GetInt32(0),
				TenCLS = reader.GetString(1),
				TrangThai = reader.GetString(2),
				KetQua = reader.IsDBNull(3) ? null : reader.GetString(3),
				NgayThucHien = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
				GhiChu = reader.IsDBNull(5) ? null : reader.GetString(5)
			});
		}

		return list;
	}
	public async Task<PhienKhamClsReadModel?> GetDetailAsync(int id)
	{
		const string sql = @"
			SELECT pk.PhienKham_CanLamSangID, cls.TenCLS, pk.TrangThai, pk.KetQua, pk.FileDinhKem, pk.NgayThucHien,
				  ttcd.HoTen, nvth.NhanVienID, ttth.HoTen, pk.GhiChu
			FROM PhienKham_CanLamSang pk
			JOIN CanLamSang cls ON pk.CanLamSangID = cls.CanLamSangID
			JOIN NhanVien nvcd ON pk.NhanVienChiDinhID = nvcd.NhanVienID
			JOIN ThongTinCaNhan ttcd ON nvcd.ThongTinID = ttcd.ThongTinID
			LEFT JOIN NhanVien nvth ON pk.NhanVienThucHienID = nvth.NhanVienID
			LEFT JOIN ThongTinCaNhan ttth ON nvth.ThongTinID = ttth.ThongTinID
			WHERE pk.PhienKham_CanLamSangID = @ID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@ID", id);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		if (!await reader.ReadAsync())
			return null;

		return new PhienKhamClsReadModel
		{
			PhienKhamCLSID = reader.GetInt32(0),
			TenCLS = reader.GetString(1),
			TrangThai = reader.GetString(2),
			KetQua = reader.IsDBNull(3) ? null : reader.GetString(3),
			FileDinhKem = reader.IsDBNull(4) ? null : reader.GetString(4),
			NgayThucHien = reader.IsDBNull(5) ? null : reader.GetDateTime(5),
			NhanVienChiDinh = reader.GetString(6),
			NhanVienThucHien = reader.IsDBNull(7) ? null : new NameResponseDTO
			{ 
				Id = reader.GetInt32(7),
				Name = reader.GetString(8),
			},
			GhiChu = reader.IsDBNull(9) ? null : reader.GetString(9)
		};
	}
	public async Task<List<PhienKhamClsListReadModel>> GetListAsync()
	{
		const string sql = @"
		SELECT 
			pk.PhienKham_CanLamSangID, cls.TenCLS, pk.TrangThai, pk.KetQua, pk.NgayThucHien, pk.GhiChu
		FROM PhienKham_CanLamSang pk
		JOIN CanLamSang cls ON pk.CanLamSangID = cls.CanLamSangID
		WHERE pk.TrangThai = N'Đang chờ' OR pk.TrangThai = N'Đang thực hiện'
		ORDER BY pk.PhienKham_CanLamSangID DESC";

		var list = new List<PhienKhamClsListReadModel>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add(new PhienKhamClsListReadModel
			{
				PhienKhamCLSID = reader.GetInt32(0),
				TenCLS = reader.GetString(1),
				TrangThai = reader.GetString(2),
				KetQua = reader.IsDBNull(3) ? null : reader.GetString(3),
				NgayThucHien = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
				GhiChu = reader.IsDBNull(5) ? null : reader.GetString(5)
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
			(@PhienKhamID, @CanLamSangID, @NhanVienChiDinhID, @GhiChu)";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = phienKhamCLS.PhienKhamID;
		cmd.Parameters.Add("@CanLamSangID", SqlDbType.Int).Value = phienKhamCLS.CLSID;
		cmd.Parameters.Add("@NhanVienChiDinhID", SqlDbType.Int).Value = phienKhamCLS.NhanVienChiDinhID;
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value =
			(object?)phienKhamCLS.GhiChu ?? DBNull.Value;

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
			WHERE PhienKham_CanLamSangID = @ID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@ID", SqlDbType.Int).Value = phienKhamCLS.PhienKhamCLSID;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar).Value = phienKhamCLS.TrangThai.ToDbValue();
		cmd.Parameters.Add("@KetQua", SqlDbType.NVarChar).Value = (object?)phienKhamCLS.KetQua ?? DBNull.Value;
		cmd.Parameters.Add("@FileDinhKem", SqlDbType.NVarChar).Value = (object?)phienKhamCLS.FileDinhKem ?? DBNull.Value;
		cmd.Parameters.Add("@NhanVienThucHienID", SqlDbType.Int).Value = (object?)phienKhamCLS.NhanVienThucHienID ?? DBNull.Value;
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = (object?)phienKhamCLS.GhiChu ?? DBNull.Value;
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	private static PhienKhamCLS MapToEntity(SqlDataReader reader)
	{
		return new PhienKhamCLS(
			phienKhamCLSID: reader.GetInt32(0),
			phienKhamID: reader.GetInt32(1),
			clsID: reader.GetInt32(2),
			trangThai: TrangThaiCLSExtensions.ToEnum(reader.GetString(3)),
			ketQua: reader.IsDBNull(4) ? null : reader.GetString(4),
			fileDinhKem: reader.IsDBNull(5) ? null : reader.GetString(5),
			ngayThucHien: reader.IsDBNull(6) ? null : reader.GetDateTime(6),
			nhanVienChiDinhID: reader.GetInt32(7),
			nhanVienThucHienID: reader.IsDBNull(8) ? null : reader.GetInt32(8),
			ghiChu: reader.IsDBNull(9) ? null : reader.GetString(9)
		);
	}
}
