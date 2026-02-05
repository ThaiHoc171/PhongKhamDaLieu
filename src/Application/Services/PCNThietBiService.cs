using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using static Application.Interfaces.IPCNThietBiRepository;

public class PCNThietBiService
{
	private readonly IPCNThietBiRepository _repository;
	private readonly IThietBiRepository _tbRepository;

	public PCNThietBiService(
		IPCNThietBiRepository repository,
		IThietBiRepository tbRepository)
	{
		_repository = repository;
		_tbRepository = tbRepository;
	}

	public async Task<PCNThietBiResponseDTO> CreateAsync(
		int phongChucNangId,
		PCNThietBiRequestCreateDTO dto)
	{
		//if (await _repository.ExistsAsync(phongChucNangId, dto.ThietBiID))
		//	throw new ArgumentException("Thiết bị đã tồn tại trong phòng chức năng");

		var entity = new PCNThietBi(
			phongChucNangId,
			dto.ThietBiID,
			dto.SoLuong
		);

		var id = await _repository.AddAsync(entity);
		var saved = await _repository.GetByIdAsync(id)
			?? throw new Exception("Không thể tạo PCN - Thiết bị");

		return await MapToResponseAsync(saved);
	}

	public async Task<bool> UpdateAsync(int id, PCNThietBiRequestUpdateDTO dto)
	{
		var entity = await _repository.GetByIdAsync(id);
		if (entity == null)
			return false;

		entity.CapNhatSoLuong(dto.SoLuong);

		if (entity.CanXoa())
		{
			await _repository.DeleteAsync(id);
			return true;
		}
		await _repository.UpdateAsync(entity);
		return true;
	}
	public async Task<bool> ChuyenTrangThaiAsync(int id, TinhTrang TrangThaiMoi)
	{
		var entity = await _repository.GetByIdAsync(id);
		if (entity == null)
			return false;
		entity.ChuyenTinhTrang(TrangThaiMoi);
		await _repository.UpdateAsync(entity);

		return true;
	}
	public async Task<bool> DeleteAsync(int id)
	{
		var entity = await _repository.GetByIdAsync(id);
		if (entity == null)
			return false;

		await _repository.DeleteAsync(id);
		return true;
	}

	public async Task<PCNThietBiResponseDTO?> GetByIdAsync(int id)
	{
		var entity = await _repository.GetByIdAsync(id);
		return entity == null ? null : await MapToResponseAsync(entity);
	}

	public async Task<List<PCNThietBiResponseDTO>> GetByPhongChucNangAsync(
		int phongChucNangId)
	{
		var list = await _repository.GetByPCNAsync(phongChucNangId);
		var tasks = list.Select(MapToResponseAsync);
		return (await Task.WhenAll(tasks)).ToList();
	}
	public Task<TongTheoPhongRaw?> GetTongTheoPhongAsync(int phongId)
	{
		return _repository.GetPhongTongAsync(phongId);
	}
	public Task<List<ThietBiNhapRaw>> GetThietBiNhapAsync(int phongId)
	{
		return _repository.GetChiTietNhapAsync(phongId);
	}

	private async Task<PCNThietBiResponseDTO> MapToResponseAsync(PCNThietBi entity)
	{
		var tenThietBi = await _tbRepository.GetNameByIdAsync(entity.ThietBiID);

		return new PCNThietBiResponseDTO
		{
			Id = entity.Id,
			PhongChucNangID = entity.PhongChucNangID,
			ThietBi = new NameResponseDTO
			{
				Id = entity.ThietBiID,
				Name = tenThietBi
			},
			SoLuong = entity.SoLuong,
			TinhTrang = entity.TinhTrang.ToDbValue(),
			NgayNhap = entity.NgayNhap
		};
	}
}
