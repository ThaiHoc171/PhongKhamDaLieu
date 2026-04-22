using Application.DTOs.ThongKe;

namespace Application.Interfaces;

public interface IThongKeRepository
{
    // ── Bệnh nhân ──────────────────────────────────────────────────────────
    Task<TongQuanBenhNhanReadModel>              GetTongQuanBenhNhanAsync(DateTime tuNgay, DateTime denNgay);
    Task<List<BenhNhanTheoNgayReadModel>>        GetBenhNhanTheoNgayAsync(DateTime tuNgay, DateTime denNgay);
    Task<List<BenhNhanTheoGioiTinhReadModel>>    GetBenhNhanTheoGioiTinhAsync(DateTime tuNgay, DateTime denNgay);
    Task<List<BenhNhanTheoDoTuoiReadModel>>      GetBenhNhanTheoDoTuoiAsync(DateTime tuNgay, DateTime denNgay);

    // ── Ca khám ────────────────────────────────────────────────────────────
    Task<TongQuanCaKhamReadModel>                GetTongQuanCaKhamAsync(DateTime tuNgay, DateTime denNgay);
    Task<List<CaKhamTheoKhoangReadModel>>        GetCaKhamTheoKhoangAsync(DateTime tuNgay, DateTime denNgay, string loaiKhoang);

    // ── Phiên khám theo trạng thái ─────────────────────────────────────────
    Task<TongQuanPhienKhamReadModel>             GetTongQuanPhienKhamAsync(DateTime tuNgay, DateTime denNgay);
    Task<List<PhienKhamTheoNgayReadModel>>       GetPhienKhamTheoNgayAsync(DateTime tuNgay, DateTime denNgay);
    Task<List<PhienKhamTheoPhongReadModel>>      GetPhienKhamTheoPhongAsync(DateTime tuNgay, DateTime denNgay);
    Task<List<PhienKhamTheoLoaiBenhReadModel>>   GetPhienKhamTheoLoaiBenhAsync(DateTime tuNgay, DateTime denNgay, int top = 10);

    // ── Toa thuốc ──────────────────────────────────────────────────────────
    Task<TongQuanToaThuocReadModel>              GetTongQuanToaThuocAsync(DateTime tuNgay, DateTime denNgay);
    Task<List<ToaThuocTheoKhoangReadModel>>      GetToaThuocTheoKhoangAsync(DateTime tuNgay, DateTime denNgay, string loaiKhoang);
    Task<List<TopThuocReadModel>>                GetTopThuocAsync(DateTime tuNgay, DateTime denNgay, int top = 10);
    Task<List<TopBacSiKeDonReadModel>>           GetTopBacSiKeDonAsync(DateTime tuNgay, DateTime denNgay, int top = 5);

    // ── Nhân viên & hiệu suất ──────────────────────────────────────────────
    Task<TongQuanNhanVienReadModel>              GetTongQuanNhanVienAsync();
    Task<List<NhanVienTheoChucVuReadModel>>      GetNhanVienTheoChucVuAsync();
    Task<List<NhanVienTheoPhongReadModel>>       GetNhanVienTheoPhongAsync();
    Task<List<HieuSuatBacSiReadModel>>           GetHieuSuatBacSiAsync(DateTime tuNgay, DateTime denNgay);
    Task<List<NgayNghiNhanVienReadModel>>        GetNgayNghiNhanVienAsync(DateTime tuNgay, DateTime denNgay);
}
