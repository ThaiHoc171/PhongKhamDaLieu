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
	public LichLamViecRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}
	public async Task<int> AddAsync(LichLamViec entity)
	{
		const string sql = @"
			INSERT INTO LichLamViecNhanVien (NhanVienID, Ngay, CaLamViec, GhiChu) OUTPUT INSERTED.LichLamViecID 
			VALUES (@NhanVienID, @Ngay, @CaLamViec, @GhiChu)
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = entity.NhanVienID;
		cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = entity.Ngay;
		cmd.Parameters.Add("@CaLamViec", SqlDbType.Int).Value = entity.CaLamViec;
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = (object?)entity.GhiChu ?? DBNull.Value;
		await conn.OpenAsync();
		return (int)await cmd.ExecuteScalarAsync();
	}
	public async Task UpdateAsync(LichLamViec entity)
	{
		const string sql = @"
			UPDATE LichLamViecNhanVien 
			SET Ngay = @Ngay, CaLamViec = @CaLamViec, GhiChu = @GhiChu 
			WHERE LichLamViecID = @Id
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = entity.LichLamViecID;
		cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = entity.Ngay;
		cmd.Parameters.Add("@CaLamViec", SqlDbType.Int).Value = entity.CaLamViec;
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value = (object?)entity.GhiChu ?? DBNull.Value;
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task<LichLamViec?> GetByIdAsync(int id)
	{
		const string sql = @"
			SELECT LichLamViecID, NhanVienID, Ngay, CaLamViec, GhiChu 
			FROM LichLamViecNhanVien 
			WHERE LichLamViecID = @Id
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (!await reader.ReadAsync())
			return null;
		return new LichLamViec(
			reader.GetInt32(0),
			reader.GetInt32(1),
			reader.GetDateTime(2),
			reader.GetInt32(3),
			reader.IsDBNull(4) ? null : reader.GetString(4)
		);
	}
	public async Task<List<LichLamViecReadModel>> GetWeekByNhanVienAsync(int nhanVienID, DateTime tuNgay, DateTime denNgay)
	{
		const string sql = @"
			SELECT l.LichLamViecID, l.NhanVienID, t.HoTen, l.Ngay, l.CaLamViec, l.GhiChu 
			FROM LichLamViecNhanVien l 
			JOIN NhanVien nv ON nv.NhanVienID = l.NhanVienID 
			JOIN ThongTinCaNhan t ON t.ThongTinID = nv.ThongTinID 
			WHERE l.NhanVienID = @NhanVienID 
				AND l.Ngay BETWEEN @TuNgay 
				AND @DenNgay 
			ORDER BY l.Ngay, l.CaLamViec
		";
		var list = new List<LichLamViecReadModel>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = nhanVienID;
		cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = tuNgay.Date;
		cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = denNgay.Date;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(new LichLamViecReadModel
			{
				LichLamViecID = reader.GetInt32(0),
				NhanVien = new NameResponseDTO
				{
					Id = reader.GetInt32(1),
					Name = reader.GetString(2)
				},
				Ngay = reader.GetDateTime(3),
				CaLamViec = reader.GetInt32(4),
				GhiChu = reader.IsDBNull(5) ? null : reader.GetString(5)
			});
		}
		return list;
	}
	public async Task<List<LichLamViecChucVuReadModel>> GetWeekAsync(DateTime tuNgay, DateTime denNgay)
	{
		const string sql = @"
			SELECT llv.LichLamViecID, nv.NhanVienID, tt.HoTen, cv.ChucVuID, cv.TenChucVu,
				nv.PhongChucNangID, llv.Ngay, llv.CaLamViec, llv.GhiChu 
			FROM LichLamViecNhanVien llv 
			JOIN NhanVien nv ON nv.NhanVienID = llv.NhanVienID 
			JOIN ChucVu cv ON cv.ChucVuID = nv.ChucVuID 
			JOIN ThongTinCaNhan tt ON tt.ThongTinID = nv.ThongTinID 
			WHERE llv.Ngay >= @TuNgay AND llv.Ngay < @DenNgay
		";
		var list = new List<LichLamViecChucVuReadModel>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = tuNgay;
		cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = denNgay;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(new LichLamViecChucVuReadModel
			{
				LichLamViecID = reader.GetInt32(0),
				NhanVien = new NameResponseDTO
				{
					Id = reader.GetInt32(1),
					Name = reader.GetString(2)
				},
				ChucVu = new NameResponseDTO
				{
					Id = reader.GetInt32(3),
					Name = reader.GetString(4)
				},
				PhongChucNang = new NameResponseDTO
				{
					Id = reader.GetInt32(5),
					Name = ""
				},
				Ngay = reader.GetDateTime(6),
				CaLamViec = reader.GetInt32(7),
				GhiChu = reader.IsDBNull(8) ? null : reader.GetString(8)
			});
		}
		return list;
	}
	public async Task<bool> ExistsAsync(int nhanVienID, DateTime ngay, int caLamViec)
	{
		const string sql = "SELECT 1 FROM LichLamViecNhanVien WHERE NhanVienID = @NhanVienID AND Ngay = @Ngay AND CaLamViec = @CaLamViec";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = nhanVienID;
		cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = ngay.Date;
		cmd.Parameters.Add("@CaLamViec", SqlDbType.Int).Value = caLamViec;
		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		return result != null;
	}
	public async Task<int> CountByChucVuAsync(int chucVuId, DateTime ngay, int caLamViec)
	{
		const string sql = "SELECT COUNT(*) FROM LichLamViecNhanVien llv JOIN NhanVien nv ON llv.NhanVienID = nv.NhanVienID WHERE nv.ChucVuID = @ChucVuID AND llv.Ngay = @Ngay AND llv.CaLamViec = @CaLamViec";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@ChucVuID", SqlDbType.Int).Value = chucVuId;
		cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = ngay.Date;
		cmd.Parameters.Add("@CaLamViec", SqlDbType.Int).Value = caLamViec;
		await conn.OpenAsync();
		return (int)await cmd.ExecuteScalarAsync();
	}
}