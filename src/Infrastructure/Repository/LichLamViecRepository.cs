using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace Infrastructure.Repository;
public class LichLamViecRepository : ILichLamViecRepository
{
	private readonly string _connectionString;
	private SqlConnection? _conn;
	private SqlTransaction? _tran;
	public LichLamViecRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}
	public async Task<LichLamViec?> GetByIdAsync(int ID)
	{
		const string sql = @"SELECT LichLamViecID, NhanVienID, Ngay, CaLamViec, GhiChu
					FROM LichLamViecNhanVien
					WHERE LichLamViecID = @Id";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd =  new SqlCommand(sql,conn);
		cmd.Parameters.AddWithValue("@Id", ID);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (!await reader.ReadAsync())
			return null;
		return new LichLamViec(
			lichLamViecID: reader.GetInt32(0),
			nhanVienID: reader.GetInt32(1),
			ngay: reader.GetDateTime(2),
			caLamViec: reader.GetInt32(3),
			ghiChu: reader.IsDBNull(4) ? null : reader.GetString(4)
		);
	}
	public async Task<List<LichLamViec>> GetAllAsync()
	{
		const string sql = @"SELECT LichLamViecID, NhanVienID, Ngay, CaLamViec, GhiChu
					FROM LichLamViecNhanVien";
		var result = new List<LichLamViec>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			result.Add(new LichLamViec(
				lichLamViecID: reader.GetInt32(0),
				nhanVienID: reader.GetInt32(1),
				ngay:  reader.GetDateTime(2),
				caLamViec: reader.GetInt32(3),
				ghiChu: reader.IsDBNull(4) ? null : reader.GetString(4)
			));
		}
		return result;
	}
	public async Task<List<LichLamViecResponseDTO>> GetByNhanVienTheoTuanAsync(int nhanVienID, DateTime tuNgay, DateTime denNgay)
	{
		const string sql = @"
				SELECT l.LichLamViecID, l.NhanVienID, t.HoTen, l.Ngay, l.CaLamViec, l.GhiChu
				FROM LichLamViecNhanVien l
				JOIN NhanVien nv ON nv.NhanVienID = l.NhanVienID
				JOIN ThongTinCaNhan t ON t.ThongTinID = nv.ThongTinID
				WHERE l.NhanVienID = @NhanVienID AND l.Ngay BETWEEN @TuNgay AND @DenNgay
				ORDER BY l.Ngay, l.CaLamViec";

		var result = new List<LichLamViecResponseDTO>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@NhanVienID", nhanVienID);
		cmd.Parameters.AddWithValue("@TuNgay", tuNgay.Date);
		cmd.Parameters.AddWithValue("@DenNgay", denNgay.Date);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			result.Add(new LichLamViecResponseDTO
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

		return result;
	}
	public async Task<int?> GetChucVuIdByLichLamViecIdAsync(int lichLamViecId)
    {
        const string sql = @"
        SELECT n.ChucVuID
        FROM LichLamViecNhanVien l, NhanVien n
        WHERE LichLamViecID = @Id AND l.NhanVienID = n.NhanVienID";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", lichLamViecId);
        await conn.OpenAsync();
        return (int?)await cmd.ExecuteScalarAsync();
    }
	public async Task<List<LichLamViecChucVuReadModel>> GetByWeekAsync(DateTime tuNgay, DateTime denNgay)
	{
		const string sql = @"
			SELECT llv.LichLamViecID, nv.NhanVienID, tt.HoTen, cv.TenChucVu,
				nv.PhongChucNangID, llv.Ngay, llv.CaLamViec, llv.GhiChu
			FROM LichLamViecNhanVien llv
			JOIN NhanVien nv ON nv.NhanVienID = llv.NhanVienID
			JOIN ChucVu cv ON cv.ChucVuID = nv.ChucVuID
			JOIN ThongTinCaNhan tt ON tt.ThongTinID = nv.ThongTinID
			WHERE llv.Ngay >= @tuNgay AND llv.Ngay < @denNgay";

		var list = new List<LichLamViecChucVuReadModel>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@tuNgay", SqlDbType.Date).Value = tuNgay;
		cmd.Parameters.Add("@denNgay", SqlDbType.Date).Value = denNgay;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add(new LichLamViecChucVuReadModel
			{
				LichLamViecID = reader.GetInt32(0),
				TenChucVu = reader.GetString(3),
				PhongChucNangID = reader.GetInt32(4),

				NhanVien = new NameResponseDTO
				{
					Id = reader.GetInt32(1),
					Name = reader.GetString(2)
				},

				Ngay = reader.GetDateTime(5),
				CaLamViec = reader.GetInt32(6),
				GhiChu = reader.IsDBNull(7) ? null : reader.GetString(7)
			});
		}

		return list;
	}
	public async Task<(int nhanvien, int phong)> GetNhanVienById(int id)
	{
		const string sql = @"
		SELECT llv.NhanVienID, nv.PhongChucNangID
		FROM LichLamViecNhanVien llv
		JOIN NhanVien nv ON llv.NhanVienID = nv.NhanVienID
		WHERE LichLamViecID = @id";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@id", id);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (await reader.ReadAsync())
		{
			var nhanvien = reader.GetInt32(reader.GetOrdinal("NhanVienID"));
			var phong = reader.GetInt32(reader.GetOrdinal("PhongChucNangID"));
			return (nhanvien, phong);
		}
		throw new Exception("Không tìm thấy lịch làm việc");
	}
	public async Task AddAsync(LichLamViec entity)
	{
		const string sql = @"
			INSERT INTO LichLamViecNhanVien
			(NhanVienID, Ngay, CaLamViec, GhiChu)
			VALUES (@NhanVienID, @Ngay, @Ca, @GhiChu)
		";
		await using var cmd = new SqlCommand(sql, _conn!, _tran);
		cmd.Parameters.AddWithValue("@NhanVienID", entity.NhanVienID);
		cmd.Parameters.AddWithValue("@Ngay", entity.Ngay);
		cmd.Parameters.AddWithValue("@Ca", entity.CaLamViec);
		cmd.Parameters.AddWithValue("@GhiChu", (object?)entity.GhiChu ?? DBNull.Value);
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task<bool> IsExitsAsync(int nhanVienID, DateTime ngay, int caLamViec)
	{
		const string sql = @"
		SELECT 1 
		FROM LichLamViecNhanVien
		WHERE NhanVienID = @NhanVienID
		  AND Ngay = @Ngay
		  AND CaLamViec = @CaLamViec";
		await using var cmd = new SqlCommand(sql, _conn!, _tran);
		cmd.Parameters.AddWithValue("@NhanVienID", nhanVienID);
		cmd.Parameters.AddWithValue("@Ngay", ngay.Date);
		cmd.Parameters.AddWithValue("@CaLamViec", caLamViec);
		var result = await cmd.ExecuteScalarAsync();
		return result != null;
	}
	public async Task<int> CountNhanVienTheoChucVuAsync(int chucVuId,DateTime ngay,	int caLamViec)
	{
		const string sql = @"
		SELECT COUNT(*)
		FROM LichLamViecNhanVien llv
		JOIN NhanVien nv ON llv.NhanVienID = nv.NhanVienID
		WHERE nv.ChucVuID = @ChucVuID
		  AND llv.Ngay = @Ngay
		  AND llv.CaLamViec = @CaLamViec
	";
		await using var cmd = new SqlCommand(sql, _conn!, _tran);
		cmd.Parameters.AddWithValue("@ChucVuID", chucVuId);
		cmd.Parameters.AddWithValue("@Ngay", ngay.Date);
		cmd.Parameters.AddWithValue("@CaLamViec", caLamViec);
		return (int)(await cmd.ExecuteScalarAsync() ?? 0);
	}
	public async Task BeginTransactionAsync()
	{
		_conn = new SqlConnection(_connectionString);
		await _conn.OpenAsync();
		_tran = _conn.BeginTransaction();
	}
	public async Task CommitAsync()
	{
		await _tran!.CommitAsync();
		await _conn!.CloseAsync();
	}
	public async Task RollbackAsync()
	{
		await _tran!.RollbackAsync();
		await _conn!.CloseAsync();
	}
}
