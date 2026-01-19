using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace Application.Services;

public class LichLamViecService
{
	private readonly ILichLamViecRepository _repo;
	private readonly string _connectionString;

	public LichLamViecService(ILichLamViecRepository repo, IConfiguration config)
	{
		_repo = repo;
		_connectionString = config.GetConnectionString("DefaultConnection")!;
	}
	public async Task ThemLichLamViecAsync(LichLamViecBatchDTO dto)
	{
		await _repo.BeginTransactionAsync();

		try
		{
			foreach (var lich in dto.LichLamViecs)
			{
				if (lich.Ngay < DateTime.Today)
					throw new Exception("Ngày làm việc không hợp lệ.");

				if (lich.Ngay.Month != dto.Thang || lich.Ngay.Year != dto.Nam)
					throw new Exception("Ngày làm việc không thuộc tháng.");

				if (await _repo.IsExitsAsync(lich.NhanVienID, lich.Ngay, lich.CaLamViec))
					throw new Exception("Trùng lịch");

				bool isNgayNghi = await _repo.IsNgayNghiAsync(lich.Ngay, lich.NhanVienID);

				var entity = new LichLamViec(
					lich.NhanVienID,
					lich.Ngay,
					lich.CaLamViec,
					lich.GhiChu,
					isNgayNghi
				);

				await _repo.AddAsync(entity);
			}

			await _repo.CommitAsync();
		}
		catch
		{
			await _repo.RollbackAsync();
			throw;
		}
	}
}
