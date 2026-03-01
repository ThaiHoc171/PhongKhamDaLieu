using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
	public class KhungGioKhamService
	{
		private readonly IKhungGioKhamRepository _repo;

		public KhungGioKhamService(IKhungGioKhamRepository repo)
		{
			_repo = repo;
		}

		public async Task<List<KhungGioKhamResponseDTO>> DanhSachKhungGioAsync()
		{
			var list = await _repo.GetAllAsync();
			return list.Select(MapToDto).ToList();
		}

		public async Task<KhungGioKhamResponseDTO?> LayKhungGioTheoIdAsync(int id)
		{
			var kg = await _repo.GetByIdAsync(id);
			if (kg == null) return null;

			return MapToDto(kg);
		}

		public async Task ThemKhungGioAsync(KhungGioKhamRequestDTO dto)
		{
			var khungGio = new KhungGioKham(
				dto.CaLamViec,
				dto.GioBatDau,
				dto.GioKetThuc,
				dto.TenKhung
			);

			var danhSachKhungGio = await _repo.GetAllAsync();

			if (danhSachKhungGio.Any(kg => kg.KiemTraTrung(khungGio)))
			{
				throw new InvalidOperationException(
					"Khung giờ khám bị trùng trong cùng ca làm việc."
				);
			}

			await _repo.AddAsync(khungGio);
		}

		public async Task<bool> CapNhatKhungGioAsync(int id, KhungGioKhamRequestDTO dto)
		{
			var kg = await _repo.GetByIdAsync(id);
			if (kg == null) return false;

			kg.CapNhat(
				dto.CaLamViec,
				dto.GioBatDau,
				dto.GioKetThuc,
				dto.TenKhung
			);

			var danhSachKhungGio = await _repo.GetAllAsync();

			if (danhSachKhungGio.Any(
				other => other.KhungGioID != id && other.KiemTraTrung(kg)))
			{
				throw new InvalidOperationException(
					"Khung giờ khám bị trùng trong cùng ca làm việc."
				);
			}

			await _repo.UpdateAsync(kg);
			return true;
		}
		public async Task<List<NameResponseDTO>> GetComboboxAsync()
		{
			var list = await _repo.GetIdAndNameAsync();
			return list.Select(e => new NameResponseDTO
			{
				Id = e.Id,
				Name = e.Ten
			}).ToList();
		}
		private static KhungGioKhamResponseDTO MapToDto(KhungGioKham kg)
		{
			return new KhungGioKhamResponseDTO
			{
				KhungGioID = kg.KhungGioID,
				CaLamViec = kg.CaLamViec,
				GioBatDau = kg.GioBatDau,
				GioKetThuc = kg.GioKetThuc,
				TenKhung = kg.TenKhung
			};
		}
	}
}
