using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class LichLamViecRepository : ILichLamViecRepository
{
	private readonly string _connectionString;

	public LichLamViecRepository(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection")!;
	}

	#region Queries

	private const string BaseSelectList = @"
		SELECT llv.LichLamViecID,
			   nv.NhanVienID,
			   tt.HoTen,
			   llv.Ngay,
			   llv.CaLamViec,
			   pcn.TenPhong,
			   llv.GhiChu
		FROM LichLamViecNhanVien llv
		JOIN NhanVien nv ON nv.NhanVienID = llv.NhanVienID
		JOIN ThongTinCaNhan tt ON tt.ThongTinID = nv.ThongTinID
		JOIN PhongChucNang pcn ON pcn.PhongChucNangID = nv.PhongChucNangID";

	#endregion


	public async Task BulkInsertAsync(List<LichLamViec> list)
	{
		using var conn = new SqlConnection(_connectionString);
		var table = new DataTable();

		table.Columns.Add("NhanVienID");
		table.Columns.Add("Ngay");
		table.Columns.Add("CaLamViec");
		table.Columns.Add("GhiChu");
		foreach (var item in list)
		{
			table.Rows.Add(item.NhanVienID, item.Ngay, item.CaLamViec,item.GhiChu);
		}

		using var bulk = new SqlBulkCopy(conn);
		bulk.DestinationTableName = "LichLamViecNhanVien";
		bulk.ColumnMappings.Add("NhanVienID", "NhanVienID");
		bulk.ColumnMappings.Add("Ngay", "Ngay");
		bulk.ColumnMappings.Add("CaLamViec", "CaLamViec");
		bulk.ColumnMappings.Add("GhiChu", "GhiChu");
		await conn.OpenAsync();
		await bulk.WriteToServerAsync(table);
	}

	public async Task<LichLamViec?> GetByIdAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"SELECT LichLamViecID,NhanVienID,Ngay,CaLamViec,GhiChu
					FROM LichLamViecNhanVien
					WHERE LichLamViecID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}


	public async Task<List<LichLamViecReadListModel>> GetWeekByNhanVienAsync(int nhanVienID, DateTime tuNgay, DateTime denNgay)
	{
		var list = new List<LichLamViecReadListModel>();

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = $@"
		{BaseSelectList}
		WHERE nv.NhanVienID=@NhanVienID
		AND llv.Ngay BETWEEN @TuNgay AND @DenNgay
		ORDER BY llv.Ngay,llv.CaLamViec";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = nhanVienID;
		cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = tuNgay.Date;
		cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = denNgay.Date;

		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToListDTO(reader));

		return list;
	}


	public async Task<List<LichLamViecReadListModel>> GetWeekAsync(DateTime tuNgay, DateTime denNgay)
	{
		var list = new List<LichLamViecReadListModel>();

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = $@"
		{BaseSelectList}
		WHERE llv.Ngay >= @TuNgay AND llv.Ngay < @DenNgay
		ORDER BY llv.Ngay,llv.CaLamViec";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = tuNgay.Date;
		cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = denNgay.Date;

		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToListDTO(reader));

		return list;
	}


	public async Task<bool> ExistsAsync(int nhanVienID, DateTime ngay, int caLamViec)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"SELECT 1
					FROM LichLamViecNhanVien
					WHERE NhanVienID=@NhanVienID
					AND Ngay=@Ngay
					AND CaLamViec=@CaLamViec";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = nhanVienID;
		cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = ngay.Date;
		cmd.Parameters.Add("@CaLamViec", SqlDbType.Int).Value = caLamViec;

		var result = await cmd.ExecuteScalarAsync();

		return result != null;
	}


	public async Task<int> CountByChucVuAsync(int chucVuId, DateTime ngay, int caLamViec)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"
		SELECT COUNT(*)
		FROM LichLamViecNhanVien llv
		JOIN NhanVien nv ON nv.NhanVienID = llv.NhanVienID
		WHERE nv.ChucVuID=@ChucVuID
		AND llv.Ngay=@Ngay
		AND llv.CaLamViec=@CaLamViec";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@ChucVuID", SqlDbType.Int).Value = chucVuId;
		cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = ngay.Date;
		cmd.Parameters.Add("@CaLamViec", SqlDbType.Int).Value = caLamViec;

		var result = await cmd.ExecuteScalarAsync();

		return (int)result!;
	}

	#region Mapping

	private LichLamViec MapToEntity(SqlDataReader r)
	{
		return new LichLamViec(
			r.GetInt32(r.GetOrdinal("LichLamViecID")),
			r.GetInt32(r.GetOrdinal("NhanVienID")),
			r.GetDateTime(r.GetOrdinal("Ngay")),
			r.GetInt32(r.GetOrdinal("CaLamViec")),
			r.IsDBNull(r.GetOrdinal("GhiChu")) ? null : r.GetString(r.GetOrdinal("GhiChu"))
		);
	}

	private LichLamViecReadListModel MapToListDTO(SqlDataReader r)
	{
		return new LichLamViecReadListModel
		{
			LichLamViecID = r.GetInt32(r.GetOrdinal("LichLamViecID")),
			NhanVien = new NameResponseDTO
			{
				Id = r.GetInt32(r.GetOrdinal("NhanVienID")),
				Name = r.GetString(r.GetOrdinal("HoTen"))
			},
			Ngay = r.GetDateTime(r.GetOrdinal("Ngay")),
			CaLamViec = r.GetInt32(r.GetOrdinal("CaLamViec")),
			TenPhong = r.GetString(r.GetOrdinal("TenPhong")),
			GhiChu = r.IsDBNull(r.GetOrdinal("GhiChu")) ? null : r.GetString(r.GetOrdinal("GhiChu"))
		};
	}
	#endregion
}