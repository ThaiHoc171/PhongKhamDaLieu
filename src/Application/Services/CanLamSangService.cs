using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using OfficeOpenXml;
namespace Application.Services;

public class CanLamSangService
{
    private readonly ICanLamSangRepository _repo;

    public CanLamSangService(ICanLamSangRepository repo)
    {
        _repo = repo;
    }
    public async Task<ApiResponse<int>> TaoMoiAsync(CanLamSangRequestDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.TenCLS))
            return ApiResponse<int>.Fail("Tên cận lâm sàng không được để trống");

        if (string.IsNullOrWhiteSpace(dto.LoaiXetNghiem))
            return ApiResponse<int>.Fail("Loại xét nghiệm không hợp lệ");

        var entity = new CanLamSang(dto.TenCLS, dto.MoTa, dto.LoaiXetNghiem);

        var id = await _repo.AddAsync(entity);

        return ApiResponse<int>.SuccessResponse(id);
    }
    public async Task<ApiResponse<bool>> CapNhatAsync(int id, CanLamSangUpdateDTO dto)
    {
        var cls = await _repo.GetByIdAsync(id);

        if (cls == null)
            return ApiResponse<bool>.Fail("Cận lâm sàng không tồn tại");

        cls.CapNhat(dto.TenCLS, dto.MoTa, dto.LoaiXetNghiem, dto.TrangThai);

        await _repo.UpdateAsync(cls);

        return ApiResponse<bool>.SuccessResponse(true);
    }
    public async Task<ApiResponse<CanLamSangReadModel>> GetByIdAsync(int id)
    {
        var result = await _repo.GetDetailAsync(id);

        if (result == null)
            return ApiResponse<CanLamSangReadModel>.Fail("Không tìm thấy cận lâm sàng");

        return ApiResponse<CanLamSangReadModel>.SuccessResponse(result);
    }
    public async Task<ApiResponse<PagedResult<CanLamSangListReadModel>>> GetPagedAsync(int pageNumber, int pageSize, string? loaiXetNghiem, string? trangThai)
    {
        var (items, totalCount) = await _repo.GetPagedAsync(pageNumber, pageSize, loaiXetNghiem, trangThai);

        return ApiResponse<PagedResult<CanLamSangListReadModel>>.SuccessResponse(
            new PagedResult<CanLamSangListReadModel>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
    }
    public async Task<ApiResponse<PagedResult<CanLamSangListReadModel>>> SearchAsync(string keyword, int pageNumber, int pageSize)
    {
        var (items, totalCount) = await _repo.SearchPagedAsync(keyword, pageNumber, pageSize);

        return ApiResponse<PagedResult<CanLamSangListReadModel>>.SuccessResponse(
            new PagedResult<CanLamSangListReadModel>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
    }
    public async Task<ApiResponse<List<CanLamSangListReadModel>>> GetByLoaiXetNghiemAsync(string loai)
    {
        var result = await _repo.GetByLoaiXetNghiemAsync(loai);

        return ApiResponse<List<CanLamSangListReadModel>>.SuccessResponse(result);
    }
    public async Task<ApiResponse<int>> ImportExcelAsync(Stream stream)
    {
        ExcelPackage.License.SetNonCommercialPersonal("ClinicApp");

        using var package = new ExcelPackage(stream);
        var sheet = package.Workbook.Worksheets.FirstOrDefault();

        if (sheet == null)
            return ApiResponse<int>.Fail("File Excel không hợp lệ");

        var rowCount = sheet.Dimension.Rows;
        int success = 0;

        for (int row = 2; row <= rowCount; row++)
        {
            var tenCLS = sheet.Cells[row, 1].Text?.Trim();
            var moTa = sheet.Cells[row, 2].Text?.Trim();
            var loaiXetNghiem = sheet.Cells[row, 3].Text?.Trim();

            if (string.IsNullOrWhiteSpace(tenCLS) || string.IsNullOrWhiteSpace(loaiXetNghiem))
                continue;

            var entity = new CanLamSang(
                tenCLS,
                moTa,
                loaiXetNghiem
            );

            await _repo.AddAsync(entity);
            success++;
        }

        return ApiResponse<int>.SuccessResponse(success, "Import cận lâm sàng thành công");
    }
}