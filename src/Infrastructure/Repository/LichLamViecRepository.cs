using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Domain.Entities;
using Application.Interfaces;

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
	public async Task<List<LichLamViec>> GetByNhanVienIdTheoTuanAsync(int NhanVienID, DateTime tuNgay, DateTime denNgay)
	{
		const string sql = @"SELECT LichLamViecID, NhanVienID, Ngay, CaLamViec, GhiChu
					FROM LichLamViecNhanVien
					WHERE NhanVienID = @NhanVienID AND Ngay >= @TuNgay AND Ngay <= @DenNgay
					ORDER BY Ngay, CaLamViec	";
		var result = new List<LichLamViec>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@NhanVienID", NhanVienID);
		cmd.Parameters.AddWithValue("@TuNgay", tuNgay.Date);
		cmd.Parameters.AddWithValue("@DenNgay", denNgay.Date);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			result.Add(new LichLamViec(
				lichLamViecID: reader.GetInt32(0),
				nhanVienID: reader.GetInt32(1),
				ngay: reader.GetDateTime(2),
				caLamViec: reader.GetInt32(3),
				ghiChu: reader.IsDBNull(4) ? null : reader.GetString(4)
			));
		}
		return result;
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
	public async Task<bool> IsChucVuExitsAsync(int ChucVuID, DateTime ngay, int caLamViec)
	{
		const string sql = @"
			SELECT COUNT(*) 
			FROM LichLamViecNhanVien llv
			JOIN NhanVien nv ON llv.NhanVienID = nv.NhanVienID
			WHERE nv.ChucVuID = @ChucVuID
			AND CONVERT(date, llv.Ngay) = @Ngay
			AND llv.CaLamViec = @CaLamViec
			";
		await using var cmd = new SqlCommand( sql, _conn!, _tran);
		cmd.Parameters.AddWithValue("@ChucVuID", ChucVuID);
		cmd.Parameters.AddWithValue("@Ngay", ngay.Date);
		cmd.Parameters.AddWithValue("@CaLamViec", caLamViec);
		var result = (int) (await cmd.ExecuteScalarAsync() ?? 0);
		return result > 0;
	}
	public async Task<bool> IsNgayNghiAsync(DateTime ngay, int nhanVienID)
	{
		const string sql = @"
		SELECT 1 
		FROM NgayNghiNhanVien
		WHERE Ngay = @Ngay
		  AND NhanVienID = @NhanVienID";

		await using var cmd = new SqlCommand(sql, _conn!, _tran);
		cmd.Parameters.AddWithValue("@Ngay", ngay.Date);
		cmd.Parameters.AddWithValue("@NhanVienID", nhanVienID);

		var result = await cmd.ExecuteScalarAsync();
		return result != null;
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
