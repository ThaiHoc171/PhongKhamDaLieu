using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
namespace Application.Services;
public class BuoiDieuTriService
{
	private readonly IBuoiDieuTriRepository _repo;
	private readonly IBenhNhanRepository _benhNhan;
	private readonly ILieuTrinhDieuTriRepository _lieutrinhrepo;
	private readonly ICaKhamRepository _cakhamrepo;
	public BuoiDieuTriService(IBuoiDieuTriRepository repo, IBenhNhanRepository benhNhan, ILieuTrinhDieuTriRepository lieutrinhrepo, ICaKhamRepository cakhamrepo)
	{
		_repo = repo;
		_benhNhan = benhNhan;
        _lieutrinhrepo = lieutrinhrepo;
        _cakhamrepo = cakhamrepo;

    }
    public async Task<ApiResponse<int>> CreateAsync(BuoiDieuTriRequestDTO dto)
    {
        if (dto.LieuTrinhID <= 0)
            return ApiResponse<int>.Fail("Liệu trình không hợp lệ");
        if (dto.CaKhamID <= 0)
            return ApiResponse<int>.Fail("Ca khám không hợp lệ");
		var cakham = await _cakhamrepo.GetByIdAsync(dto.CaKhamID);
		if (cakham == null)
			return ApiResponse<int>.Fail("Ca khám không tồn tại");
		if(TrangThaiCaKhamExtensions.FromDb(cakham.TrangThai) != TrangThaiCaKham.Trong)
			return ApiResponse<int>.Fail("Ca khám đã được đặt lịch");
		var lieuTrinh = await _lieutrinhrepo.GetByIdAsync(dto.LieuTrinhID);
        if (lieuTrinh == null)
            return ApiResponse<int>.Fail("Liệu trình không tồn tại");
		var benhNhan = await _benhNhan.GetByIdAsync(lieuTrinh.BenhNhanID);
		if(benhNhan == null)
			return ApiResponse<int>.Fail("Bệnh nhân không tồn tại");
		var maxSoBuoi = await _repo.GetMaxSoBuoiAsync(dto.LieuTrinhID);
		var complete = await _repo.CountHoanThanhAsync(dto.LieuTrinhID);

        if (complete >= lieuTrinh.TongSoBuoi)
            return ApiResponse<int>.Fail($"Liệu trình đã đủ {lieuTrinh.TongSoBuoi} buổi, không thể tạo thêm");

        var soBuoi = maxSoBuoi + 1;
        BuoiDieuTri entity;
        try
        {
            entity = new BuoiDieuTri(
                dto.LieuTrinhID,
                dto.CaKhamID,
                soBuoi,
                cakham.NgayKham);
			cakham.DangKyKham(benhNhan.ThongTinID, "Điều trị", DateTime.Now, "Buổi điều trị: " + soBuoi.ToString());
        }
        catch (ArgumentException ex)
        {
            return ApiResponse<int>.Fail(ex.Message);
        }

		var res = await _cakhamrepo.UpdateAsync(cakham);
		if (res != 0)
		{
			try
			{
				var id = await _repo.AddAsync(entity);
				return ApiResponse<int>.SuccessResponse(id);
			}
			catch
			{
				cakham.HuyDangKy();
				await _cakhamrepo.UpdateAsync(cakham);
				return ApiResponse<int>.Fail("Tạo buổi thất bại");
			}
		}
		else
			return ApiResponse<int>.Fail("Tạo buổi điều trị thất bại");
    }
 //   public async Task<ApiResponse<bool>> StartAsync(int id, int nhanVienID)
	//{
	//	if (nhanVienID <= 0)
	//		return ApiResponse<bool>.Fail("Nhân viên không hợp lệ");
	//	var entity = await _repo.GetByIdAsync(id);
	//	if (entity == null)
	//		return ApiResponse<bool>.Fail("Buổi điều trị không tồn tại");
	//	try
	//	{
	//		entity.BatDauDieuTri(nhanVienID);
	//	}
	//	catch (InvalidOperationException ex)
	//	{
	//		return ApiResponse<bool>.Fail(ex.Message);
	//	}
	//	await _repo.UpdateAsync(entity);
	//	return ApiResponse<bool>.SuccessResponse(true);
	//}
	//public async Task<ApiResponse<bool>> CompleteAsync(int id, BuoiDieuTriUpdateDTO dto)
	//{
	//	if (dto.NgayThucHien == null)
	//		return ApiResponse<bool>.Fail("Ngày thực hiện không hợp lệ");
	//	var entity = await _repo.GetByIdAsync(id);
	//	if (entity == null)
	//		return ApiResponse<bool>.Fail("Buổi điều trị không tồn tại");
	//	try
	//	{
	//		entity.HoanThanh(dto.NgayThucHien.Value, dto.GhiChu);
	//	}
	//	catch (InvalidOperationException ex)
	//	{
	//		return ApiResponse<bool>.Fail(ex.Message);
	//	}
	//	await _repo.UpdateAsync(entity);
	//	return ApiResponse<bool>.SuccessResponse(true);
	//}
	//public async Task<ApiResponse<bool>> CancleAsync(int id, string? ghiChu)
	//{
	//	var entity = await _repo.GetByIdAsync(id);
	//	if (entity == null)
	//		return ApiResponse<bool>.Fail("Buổi điều trị không tồn tại");
	//	try
	//	{
	//		entity.Huy(ghiChu);
	//	}
	//	catch (InvalidOperationException ex)
	//	{
	//		return ApiResponse<bool>.Fail(ex.Message);
	//	}
	//	await _repo.UpdateAsync(entity);
	//	return ApiResponse<bool>.SuccessResponse(true);
	////}
	//public async Task<ApiResponse<bool>> UpdateImageAsync(int id, string? hinhAnhJson)
	//{
	//	var entity = await _repo.GetByIdAsync(id);
	//	if (entity == null)
	//		return ApiResponse<bool>.Fail("Buổi điều trị không tồn tại");
	//	entity.CapNhatHinhAnh(hinhAnhJson);
	//	await _repo.UpdateAsync(entity);
	//	return ApiResponse<bool>.SuccessResponse(true);
	//}
	public async Task<ApiResponse<BuoiDieuTriReadModel>> GetByIdAsync(int id)
	{
		var result = await _repo.GetDetailAsync(id);
		if (result == null)
			return ApiResponse<BuoiDieuTriReadModel>.Fail("Buổi điều trị không tồn tại");
		return ApiResponse<BuoiDieuTriReadModel>.SuccessResponse(result);
	}
	public async Task<ApiResponse<List<BuoiDieuTriListReadModel>>> GetByLieuTrinhAsync(int lieuTrinhID)
	{
		if (lieuTrinhID <= 0)
			return ApiResponse<List<BuoiDieuTriListReadModel>>.Fail("Liệu trình không hợp lệ");
		var result = await _repo.GetByLieuTrinhAsync(lieuTrinhID);
		return ApiResponse<List<BuoiDieuTriListReadModel>>.SuccessResponse(result);
	}
	public async Task<ApiResponse<int>> CountCompleteAsync(int lieuTrinhID)
	{
		if (lieuTrinhID <= 0)
			return ApiResponse<int>.Fail("Liệu trình không hợp lệ");
		var count = await _repo.CountHoanThanhAsync(lieuTrinhID);
		return ApiResponse<int>.SuccessResponse(count);
	}
}