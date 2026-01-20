using Application.Common;
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
				if (await _repo.IsChucVuExitsAsync(lich.ChucVuID, lich.Ngay, lich.CaLamViec))
					throw new Exception("Trùng chức vụ");
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
	public async Task<LichLamViecRespondDTO?> GetByIdAsync(int id)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return null;

		return new LichLamViecRespondDTO
		{
			LichLamViecID = entity.LichLamViecID,
			NhanVienID = entity.NhanVienID,
			Ngay = entity.Ngay,
			CaLamViec = entity.CaLamViec,
			GhiChu = entity.GhiChu
		};
	}
	public async Task<List<LichLamViecRespondDTO>> GetAllAsync()
	{
		var entities =  await _repo.GetAllAsync();
		var result = new List<LichLamViecRespondDTO>();
		foreach (var entity in entities)
		{
			result.Add(new LichLamViecRespondDTO
			{
				LichLamViecID = entity.LichLamViecID,
				NhanVienID = entity.NhanVienID,
				Ngay = entity.Ngay,
				CaLamViec = entity.CaLamViec,
				GhiChu = entity.GhiChu
			});
		}
		return result;
	}
	public async Task<WeekLichLamViecDTO> GetLichTheoTuanAsync(
			int nhanVienID,
			int page
		)
	{
		var (start, end) = DateTimeHelper.GetWeekByPage(page);

		var entities = await _repo.GetByNhanVienIdTheoTuanAsync(nhanVienID,start,end);
		return new WeekLichLamViecDTO
		{
			Page = page,
			TuanBatDau = start,
			TuanKetThuc = end,
			LichLamViecs = entities.Select(e => new LichLamViecRespondDTO
			{
				LichLamViecID = e.LichLamViecID,
				NhanVienID = e.NhanVienID,
				Ngay = e.Ngay,
				CaLamViec = e.CaLamViec,
				GhiChu = e.GhiChu
			}).ToList()
		};
	}
}
