using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;
public interface ICaKhamRepository
{
	//Xuất ca khám theo CaKhamID
	Task<CaKham?> GetByIdAsync(int caKhamID);
	//Xuất danh sách tất cả ca khám
	Task<(List<CaKhamListReadModel>, int)> GetCaKhamsAsync(
		DateTime ngayKham, string trangThai, string loaiCaKham, int pageNumber, int pageSize);
	Task<List<(int Id, string Ten)>> GetIdAndNameByStatusAsync(string trangThai, DateTime ngayKham);
	Task<CaKhamReadModel?> GetCaKhamDetailAsync(int caKhamId);
	//Xuất danh sách theo ThongTinID
	Task<(List<CaKhamListReadModel>, int)> GetByThongTinAsync(int thongTinID, int pageNumber, int pageSize);
	//Đếm số ca đã có trong ngày
	Task<int> CountByNgayAndKhungGioAsync(DateTime ngay, int khungGioId, string loaiCaKham);
    //Kiểm tra ca khám đã tồn tại chưa, tránh tạo trùng
    Task<bool> ExistsAsync(DateTime ngay, int khungGioId, string loaiCaKham);
    //Kiểm tra bệnh nhân đã đăng ký khám trong 1 khung giờ
    Task<bool> CheckThongTinDaDangKyAsync(DateTime ngay, int khungGioId, string loaiCaKham, int benhNhanId);
    //Đếm các khung giờ còn ca khám trống
    Task<List<int>> GetKhungGioConTrongAsync(DateTime ngayKham, string loaiCaKham);
    //Lấy ca khám gần nhất còn trống
    Task<int> GetCaKhamAsync(DateTime ngay, int khungGioId, string loaiCaKham);
    //Tạo ca khám
    Task<int> AddAsync(CaKham caKham);
	//Update ca khám sau khi bệnh nhân đăng ký lịch
	Task UpdateAsync(CaKham caKham);
	//Sửa trạng thái ca khám sau khi khám xong
	Task UpdateTrangThaiAsync(int caKhamID, string trangThai, string ghiChu);
}
