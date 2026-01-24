using Domain.Entities;

namespace Application.Interfaces;
public interface ICaKhamRepository
{
	//Xuất ca khám theo CaKhamID
	Task<CaKham?> GetByIdAsync(int caKhamID);
	//Xuất danh sách tất cả ca khám
	Task<List<CaKham>> GetAllAsync();
	//Xuất danh sách ca khám theo ngày và trạng thái
	Task<List<CaKham>> LocAsync(DateTime ngayKham, string trangThai);
	//Xuất danh sách theo BenhNhanID
	Task<List<CaKham>> GetByBenhNhanAsync(int benhNhanID);
	//Đếm số ca đã có trong ngày
	Task<int> CountByNgayAndKhungGioAsync(DateTime ngay, int khungGioId);
	//Kiểm tra ca khám đã tồn tại chưa, tránh tạo trùng
	Task<bool> ExistsAsync(DateTime ngay, int khungGioId, int lichLamViecId);
	//Tạo ca khám
	Task<int> AddAsync(CaKham caKham);
	//Update ca khám sau khi bệnh nhân đăng ký lịch
	Task UpdateAsync(CaKham caKham);
	//Sửa trạng thái ca khám sau khi khám xong
	Task UpdateTrangThaiAsync(int caKhamID, string trangThai, string ghiChu);
}
