using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Azure;
using Domain.Entities;


namespace Application.Services;

public class LichLamViecService
{
	private readonly ILichLamViecRepository _repo;
	private readonly INgayNghiNhanVienRepository _nghiRepo;
	private readonly INhanVienRepository _nvRepo;
	private readonly IChucVuRepository _chucVuRepo;

	public LichLamViecService(ILichLamViecRepository repo, INgayNghiNhanVienRepository nghiRepo, INhanVienRepository nvRepo, IChucVuRepository chucVuRepo)
	{
		_repo = repo;
		_nghiRepo = nghiRepo;
		_nvRepo = nvRepo;
		_chucVuRepo = chucVuRepo;
	}
	public async Task ThemLichLamViecAsync(LichLamViecBatchDTO dto)
	{
		await _repo.BeginTransactionAsync();

		try
		{
			foreach (var lich in dto.LichLamViecs)
			{
				// Check ngày hợp lệ
				if (lich.Ngay.Date < DateTime.Today)
					throw new Exception("Ngày làm việc không hợp lệ.");

				if (lich.Ngay.Month != dto.Thang || lich.Ngay.Year != dto.Nam)
					throw new Exception("Ngày làm việc không thuộc tháng.");

				// Trùng lịch cá nhân
				if (await _repo.IsExitsAsync(
					lich.NhanVienID,
					lich.Ngay,
					lich.CaLamViec))
				{
					throw new Exception("Nhân viên đã có lịch trong ca này.");
				}

				// Check ngày nghỉ ❗ ĐÚNG MODULE
				if (await _nghiRepo.IsNgayNghiAsync(
					lich.NhanVienID,
					lich.Ngay))
				{
					throw new Exception("Nhân viên đang nghỉ ngày này.");
				}

				// Check rule chức vụ
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

				// Tạo entity THUẦN
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
	public async Task<LichLamViecResponseDTO?> GetByIdAsync(int id)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return null;
		var nv = await _nvRepo.GetNameByIdAsync(entity.NhanVienID);
		return new LichLamViecResponseDTO
		{
			LichLamViecID = entity.LichLamViecID,
			NhanVien = new NameResponseDTO
			{
				Id = entity.NhanVienID,
				Name = nv
			},
			Ngay = entity.Ngay,
			CaLamViec = entity.CaLamViec,
			GhiChu = entity.GhiChu
		};
	}
	public async Task<List<LichLamViecResponseDTO>> GetAllAsync()
	{
		var entities =  await _repo.GetAllAsync();
		var result = new List<LichLamViecResponseDTO>();
		foreach (var entity in entities)
		{
			var nv = await _nvRepo.GetNameByIdAsync(entity.NhanVienID);
			result.Add(new LichLamViecResponseDTO
			{
				LichLamViecID = entity.LichLamViecID,
				NhanVien = new NameResponseDTO
				{
					Id = entity.NhanVienID,
					Name = nv
				},
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
		var nv = await _nvRepo.GetNameByIdAsync(nhanVienID);
		return new WeekLichLamViecDTO
		{
			
			Page = page,
			TuanBatDau = start,
			TuanKetThuc = end,
			LichLamViecs = entities.Select(e => new LichLamViecResponseDTO
			{
				
				LichLamViecID = e.LichLamViecID,
				NhanVien = new NameResponseDTO
				{
					Id = nhanVienID,
					Name = nv
				},
				Ngay = e.Ngay,
				CaLamViec = e.CaLamViec,
				GhiChu = e.GhiChu
			}).ToList()
		};
	}

	public async Task<List<LichLamViecChucVuResponseDTO>> GetByWeekAsync(int page)
	{
		var (start, end) = DateTimeHelper.GetWeekByPage(page);
		var entities = await _repo.GetByWeekAsync(start, end);

		var result = new List<LichLamViecChucVuResponseDTO>();

		foreach (var e in entities)
		{
			var tenNhanVien = await _nvRepo.GetNameByIdAsync(e.NhanVienID);
			var tenChucVu = await _chucVuRepo.GetByNhanVienIdAsync(e.NhanVienID);
			var phongId = await _nvRepo.GetPhongChucNangIdByNhanVienIdAsync(e.NhanVienID);

			result.Add(new LichLamViecChucVuResponseDTO
			{
				LichLamViecID = e.LichLamViecID,
				TenChucVu = tenChucVu ?? "",
				PhongChucNangID = phongId ?? 0,
				NhanVien = new NameResponseDTO
				{
					Id = e.NhanVienID,
					Name = tenNhanVien ?? ""
				},
				Ngay = e.Ngay,
				CaLamViec = e.CaLamViec,
				GhiChu = e.GhiChu
			});
		}

		return result;
	}
}
