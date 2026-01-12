using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Services
{
	public class ChucVuService
	{
		private readonly IChucVuRepository _repo;

		public ChucVuService(IChucVuRepository repo)
		{
			_repo = repo;
		}
		public async Task<List<ChucVuDTO>> DanhSachChucVuAsync()
		{
			var list = await _repo.GetAllAsync();

			return list.Select(MapToDto).ToList();
		}
		public async Task<ChucVuDTO?> LayChucVuTheoIdAsync(int id)
		{
			var cv = await _repo.GetByIdAsync(id);
			if (cv == null) return null;

			return MapToDto(cv);
		}
		public async Task ThemChucVuAsync(ThemChucVuDTO dto)
		{
			var cv = new ChucVu(dto.TenChucVu, dto.MoTa);
			await _repo.AddAsync(cv);
		}

		public async Task<bool> CapNhatChucVuAsync(int id, CapNhatChucVuDTO dto)
		{
			var cv = await _repo.GetByIdAsync(id);
			if (cv == null) return false;

			cv.CapNhat(dto.TenChucVu, dto.MoTa);
			await _repo.UpdateAsync(cv);

			return true;
		}

		public async Task<bool> CapNhatTrangThaiChucVuAsync(int id, string trangThaiMoi)
		{
			var cv = await _repo.GetByIdAsync(id);
			if (cv == null) return false;

			cv.CapNhatTrangThai(trangThaiMoi);
			await _repo.UpdateAsync(cv);

			return true;
		}

		private static ChucVuDTO MapToDto(ChucVu cv)
		{
			return new ChucVuDTO
			{
				ChucVuID = cv.ChucVuID,
				TenChucVu = cv.TenChucVu,
				MoTa = cv.MoTa,
				NgayTao = cv.NgayTao
			};
		}
	}
}
