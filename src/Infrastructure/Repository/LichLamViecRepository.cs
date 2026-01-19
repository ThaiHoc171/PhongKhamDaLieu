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
