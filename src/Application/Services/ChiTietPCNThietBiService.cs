using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

public class ChiTietPCNThietBiService
{
	private readonly IChiTietPCNThietBiRepository _repo;
	private readonly IPCNThietBiRepository _pcnRepo;

	public ChiTietPCNThietBiService(
		IChiTietPCNThietBiRepository repo,
		IPCNThietBiRepository pcnRepo)
	{
		_repo = repo;
		_pcnRepo = pcnRepo;
	}
	#region Import

	// PREVIEW FILE EXCEL
	public async Task<ApiResponse<ExcelImportResult<ChiTietPCNThietBiImport>>> PreviewImport(Stream stream, string sheet)
	{
		return ExcelImporter.Preview<ChiTietPCNThietBiImport>(stream, sheet, (item, row) =>
		{
			var errors = new List<string>();

			if (item.PhongChucNangID <= 0)
				errors.Add($"Dòng {row}: Phòng chức năng không hợp lệ");

			if (item.ThietBiID <= 0)
				errors.Add($"Dòng {row}: Thiết bị không hợp lệ");

			if (string.IsNullOrWhiteSpace(item.MaTaiSan))
				errors.Add($"Dòng {row}: Mã tài sản không hợp lệ");

			return errors;
		});
	}

	// VALIDATE BUSINESS
	public async Task<ApiResponse<ExcelImportResult<ChiTietPCNThietBiImport>>> 
		ValidateImport(List<ChiTietPCNThietBiImport> list)
	{
		var result = new ExcelImportResult<ChiTietPCNThietBiImport>();

		int row = 2;
		var maSet = new HashSet<string>();

		foreach (var item in list)
		{
			var errors = new List<string>();

			if (item.PhongChucNangID <= 0)
				errors.Add($"Dòng {row}: Phòng chức năng không hợp lệ");

			if (item.ThietBiID <= 0)
				errors.Add($"Dòng {row}: Thiết bị không hợp lệ");

			if (string.IsNullOrWhiteSpace(item.MaTaiSan))
				errors.Add($"Dòng {row}: Mã tài sản rỗng");

			// trùng trong file
			if (!maSet.Add(item.MaTaiSan))
				errors.Add($"Dòng {row}: Mã tài sản bị trùng trong file");

			// kiểm tra PCN thiết bị
			var pcn = await _pcnRepo.GetByPhongAndThietBiAsync(item.PhongChucNangID, item.ThietBiID);
			if (errors.Any())
			{
				result.Errors.Add(new ExcelImportError
				{
					Row = row,
					Errors = errors
				});
			}
			else
			{
				result.Data.Add(item);
			}

			row++;
		}

		return ApiResponse<ExcelImportResult<ChiTietPCNThietBiImport>>
			.SuccessResponse(result);
	}

	// IMPORT DATA
	public async Task<ApiResponse<bool>> ImportAsync(List<ChiTietPCNThietBiImport> list)
	{
		if (list == null || list.Count == 0)
			return ApiResponse<bool>.Fail("Danh sách import rỗng");

		foreach (var group in list.GroupBy(x => new { x.PhongChucNangID, x.ThietBiID }))
		{
			var phongId = group.Key.PhongChucNangID;
			var thietBiId = group.Key.ThietBiID;

			var pcn = await _pcnRepo.GetByPhongAndThietBiAsync(phongId, thietBiId);

			// nếu chưa có thì tạo
			if (pcn == null)
			{
				var newPCN = new PCNThietBi(phongId, thietBiId);
				var id = await _pcnRepo.AddAsync(newPCN);
				pcn = await _pcnRepo.GetByIdAsync(id);

				if (pcn == null)
					return ApiResponse<bool>.Fail("Không thể tạo PCN thiết bị");
			}

			// lấy danh sách mã tài sản
			var maList = group.Select(x => x.MaTaiSan.Trim()).ToList();

			// tạo entity list
			var entities = ChiTietPCNThietBi.TaoDanhSach(pcn.PCN_TB_ID, maList);

			// bulk insert
			await _repo.BulkInsertAsync(entities);

			// update tổng số lượng
			pcn.Update(pcn.TongSoLuong + entities.Count);
			await _pcnRepo.UpdateAsync(pcn);
		}

		return ApiResponse<bool>.SuccessResponse(true, "Import thiết bị thành công");
	}

	#endregion
	// Thêm mới chi tiết PCN thiết bị
	public async Task<ApiResponse<int>> CreateAsync(ChiTietPCNThietBiRequestDTO dto)
	{
		if (dto == null)
			return ApiResponse<int>.Fail("Dữ liệu không hợp lệ");

		var pcn = await _pcnRepo.GetByPhongAndThietBiAsync(dto.PhongChucNangID, dto.ThietBiID);
		if (pcn == null)
		{
			var req = new PCNThietBi(dto.PhongChucNangID, dto.ThietBiID);
			var res = await _pcnRepo.AddAsync(req);
			pcn = await _pcnRepo.GetByIdAsync(res);
			if (pcn == null)
				return ApiResponse<int>.Fail("Không thể tạo PCN thiết bị");
		}

		var entity = new ChiTietPCNThietBi(pcn.PCN_TB_ID, dto.MaTaiSan, dto.GhiChu);
		var id = await _repo.AddAsync(entity);

		// Tăng tổng số lượng trong PCN
		pcn.Update(pcn.TongSoLuong + 1);
		await _pcnRepo.UpdateAsync(pcn);

		return ApiResponse<int>.SuccessResponse(id, "Thêm chi tiết thiết bị thành công");
	}

	// Cập nhật chi tiết PCN thiết bị
	public async Task<ApiResponse<bool>> UpdateAsync(int id, ChiTietPCNThietBiUpdateDTO dto)
	{
		if (id <= 0 || dto == null)
			return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Chi tiết thiết bị không tồn tại");

		try
		{
			entity.CapNhat(dto.MaTaiSan, dto.GhiChu);
			if(!string.IsNullOrWhiteSpace(dto.TinhTrang))
				entity.ChuyenTinhTrang(dto.TinhTrang);
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}

		await _repo.UpdateAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true, "Cập nhật chi tiết thiết bị thành công");
	}

	// Xóa chi tiết PCN thiết bị
	public async Task<ApiResponse<bool>> DeleteAsync(int id)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Chi tiết thiết bị không tồn tại");

		if (entity.TinhTrang == TinhTrang.HoatDong)
			return ApiResponse<bool>.Fail("Không thể xoá thiết bị đang hoạt động");

		await _repo.DeleteAsync(id);

		// Cập nhật tổng số lượng PCN
		var pcn = await _pcnRepo.GetByIdAsync(entity.PCN_TB_ID);
		if (pcn == null)
			return ApiResponse<bool>.Fail("PCN thiết bị không tồn tại");

		pcn.Update(pcn.TongSoLuong - 1);
		if (pcn.IsDelete())
			await _pcnRepo.DeleteAsync(pcn.PCN_TB_ID);
		else
			await _pcnRepo.UpdateAsync(pcn);

		return ApiResponse<bool>.SuccessResponse(true, "Xóa chi tiết thiết bị thành công");
	}

	// Lấy chi tiết theo ID
	public async Task<ApiResponse<ChiTietPCNThietBiReadModel>> GetByIdAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<ChiTietPCNThietBiReadModel>.Fail("ID không hợp lệ");

		var result = await _repo.GetDetailAsync(id);
		if (result == null)
			return ApiResponse<ChiTietPCNThietBiReadModel>.Fail("Chi tiết thiết bị không tồn tại");

		return ApiResponse<ChiTietPCNThietBiReadModel>.SuccessResponse(result);
	}
	public async Task<ApiResponse<List<ChiTietPCNThietBiListReadModel>>> GetListAsync(int pcnTbID)
	{
		if (pcnTbID <= 0)
			return ApiResponse<List<ChiTietPCNThietBiListReadModel>>.Fail("ID không hợp lệ");

		var result = await _repo.GetListAsync(pcnTbID);
		if (result == null)
			return ApiResponse<List<ChiTietPCNThietBiListReadModel>>.Fail("Danh sách không tồn tại");

		return ApiResponse<List<ChiTietPCNThietBiListReadModel>>.SuccessResponse(result);
	}
}