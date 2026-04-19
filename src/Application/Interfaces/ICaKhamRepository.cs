using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces;
public interface ICaKhamRepository
{
	//Xuất ca khám theo CaKhamID
	Task<CaKham?> GetByIdAsync(int caKhamID);
	Task<CaKhamReadModel?> GetDetailAsync(int caKhamId);
	//Xuất danh sách tất cả ca khám	
	Task<(List<CaKhamListReadModel>, int)> GetPagedAsync(
		DateTime ngayKham, string trangThai, string loaiCaKham, int? nhanVienId,int pageNumber, int pageSize);
	Task<int> CountAsync(DateTime ngay, int khungGioId, string loaiCa);
	Task<int> InsertAsync(CaKham entity);
	Task<int> CountNotAssignedAsync(DateTime tuNgay, DateTime denNgay);
	Task<(List<CaKhamListReadModel>, int)> GetChoXacNhanAsync(int page, int size);
	//Xuất danh sách theo ThongTinID
	Task<(List<CaKhamListReadModel>, int)> GetByThongTinAsync(int thongTinID, int pageNumber, int pageSize);
    //Kiểm tra bệnh nhân đã đăng ký khám trong 1 khung giờ
    Task<bool> CheckThongTinDaDangKyAsync(DateTime ngay, int khungGioId, string loaiCaKham, int benhNhanId);
	//Đếm các khung giờ còn ca khám trống
	Task<List<int>> GetKhungGioConTrongAsync(DateTime ngayKham, string loaiCaKham, int? nhanVienId);
	//Lấy ca khám gần nhất còn trống
	Task<int> GetCaKhamAsync(DateTime ngay, int khungGioId, string loaiCaKham, int? nhanVienId);
	//Lấy FCM Token qua CaKhamID, xác nhận thiết bị của tài khoản đã đăng ký khám
    Task<string?> GetFcmTokenByCaKhamIdAsync(int caKhamId);
	//Update ca khám sau khi bệnh nhân đăng ký lịch
	Task<int> UpdateAsync(CaKham caKham);
	//Sửa trạng thái ca khám sau khi khám xong
	Task UpdateTrangThaiAsync(int caKhamID, string trangThai, string ghiChu);
}
