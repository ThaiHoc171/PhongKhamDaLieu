using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;


namespace Application.Services;

public class LichLamViecService
{
	private readonly ILichLamViecRepository _repo;
	private readonly INgayNghiNhanVienRepository _nghiRepo;

	public LichLamViecService(ILichLamViecRepository repo, INgayNghiNhanVienRepository nghiRepo)
	{
		_repo = repo;
		_nghiRepo = nghiRepo;
	}
	public async Task ThemLichLamViecAsync(LichLamViecBatchDTO dto)
	{
		await _repo.BeginTransactionAsync();

		try
		{
			foreach (var lich in dto.LichLamViecs)
			{
				// 1️⃣ Check ngày hợp lệ
				if (lich.Ngay.Date < DateTime.Today)
					throw new Exception("Ngày làm việc không hợp lệ.");

				if (lich.Ngay.Month != dto.Thang || lich.Ngay.Year != dto.Nam)
					throw new Exception("Ngày làm việc không thuộc tháng.");

				// 2️⃣ Trùng lịch cá nhân
				if (await _repo.IsExitsAsync(
					lich.NhanVienID,
					lich.Ngay,
					lich.CaLamViec))
				{
					throw new Exception("Nhân viên đã có lịch trong ca này.");
				}

				// 3️⃣ Check ngày nghỉ ❗ ĐÚNG MODULE
				if (await _nghiRepo.IsNgayNghiAsync(
					lich.NhanVienID,
					lich.Ngay))
				{
					throw new Exception("Nhân viên đang nghỉ ngày này.");
				}

				// 4️⃣ Check rule chức vụ
				var soLuongCungChucVu =
					await _repo.CountNhanVienTheoChucVuAsync(
						lich.ChucVuID,
						lich.Ngay,
						lich.CaLamViec);

				if (lich.ChucVuID == 3) // hardcode
				{
					if (soLuongCungChucVu >= 2)
						throw new Exception("Ca làm việc đã đủ 2 y tá.");
				}
				else
				{
					if (soLuongCungChucVu >= 1)
						throw new Exception("Ca làm việc đã có nhân viên cùng chức vụ.");
				}

				// 5️⃣ Tạo entity THUẦN
				var entity = new LichLamViec(
					lich.NhanVienID,
					lich.Ngay,
					lich.CaLamViec,
					lich.GhiChu
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

	public async Task<List<LichLamViec>> GetByKhoangNgayAsync(DateTime tuNgay, DateTime denNgay)
	{
		return await _repo.GetByKhoangNgayAsync(tuNgay, denNgay);
	}
}
