using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Services;

public class CanLamSangService
{
	private readonly ICanLamSangRepository _repo;

	public CanLamSangService(ICanLamSangRepository repo)
	{
		_repo = repo;
	}

	// GET ALL
	public async Task<PagedResult<CanLamSangResponseDTO>> DanhSachCanLamSangAsync(int pageNumber, int pageSize)
	{
		var (data, totalCount) = await _repo.GetPagedAsync(pageNumber, pageSize);

		return new PagedResult<CanLamSangResponseDTO>
		{
			Items = data.Select(MapToDto).ToList(),
			TotalCount = totalCount,
			PageNumber = pageNumber,
			PageSize = pageSize
		};
	}

	// GET BY ID
	public async Task<CanLamSangResponseDTO?> LayCanLamSangTheoIdAsync(int id)
	{
		var cls = await _repo.GetByIdAsync(id);
		if (cls == null) return null;

		return MapToDto(cls);
	}
	//
    public async Task<List<NameResponseDTO>> GetComboboxAsync()
    {
        var list = await _repo.GetIdAndNameAsync();
        return list.Select(e => new NameResponseDTO
        {
            Id = e.Id,
            Name = e.Ten
        }).ToList();
    }
    // POST
    public async Task ThemCanLamSangAsync(CanLamSangRequestDTO dto)
	{
		var cls = new CanLamSang(
			dto.TenCLS,
			dto.MoTa,
			dto.LoaiXetNghiem
		);

		await _repo.AddAsync(cls);
	}

	// PUT
	public async Task<bool> CapNhatCanLamSangAsync(int id, CanLamSangRequestDTO dto)
	{
		var cls = await _repo.GetByIdAsync(id);
		if (cls == null) return false;

		cls.CapNhat(dto.TenCLS, dto.MoTa, dto.LoaiXetNghiem);
		await _repo.UpdateAsync(cls);

		return true;
	}

	// PUT trạng thái
	public async Task<bool> CapNhatTrangThaiAsync(int id, string trangThaiMoi)
	{
		var cls = await _repo.GetByIdAsync(id);
		if (cls == null) return false;

		cls.CapNhatTrangThai(trangThaiMoi);
		await _repo.UpdateAsync(cls);

		return true;
	}
	public async Task<List<CanLamSangResponseDTO>> TimTheoTenAsync(string tenCLS)
	{
		var list = await _repo.SearchByTenAsync(tenCLS);
		return list.Select(MapToDto).ToList();
	}
	// MAP ENTITY → DTO
	private static CanLamSangResponseDTO MapToDto(CanLamSang cls)
	{
		return new CanLamSangResponseDTO
		{
			CanLamSangID = cls.CanLamSangID,
			TenCLS = cls.TenCLS,
			MoTa = cls.MoTa,
			LoaiXetNghiem = cls.LoaiXetNghiem,
			NgayTao = cls.NgayTao,
			TrangThai = cls.TrangThai
		};
	}
}
