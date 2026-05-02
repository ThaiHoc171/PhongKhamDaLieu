using System.Text;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Client;

public class ThongKeClient : AppClientBase
{
	private const string BASE = "api/thongke";

	// ==================== HELPER ====================
	private string BuildQuery(ThongKeFilterRequest f, int? top = null)
	{
		var q = new List<string>();

		if (f.TuNgay.HasValue) q.Add($"tuNgay={f.TuNgay:yyyy-MM-dd}");
		if (f.DenNgay.HasValue) q.Add($"denNgay={f.DenNgay:yyyy-MM-dd}");
		if (!string.IsNullOrWhiteSpace(f.LoaiKhoang)) q.Add($"loaiKhoang={f.LoaiKhoang}");
		if (f.Nam.HasValue) q.Add($"nam={f.Nam}");
		if (f.Thang.HasValue) q.Add($"thang={f.Thang}");

		if (top.HasValue) q.Add($"top={top}");

		return q.Any() ? "?" + string.Join("&", q) : "";
	}

	// ==================== BỆNH NHÂN ====================
	public Task<ApiResult<TongQuanBenhNhanReadModel>> GetTongQuanBenhNhan(ThongKeFilterRequest f)
		=> GetAsync<TongQuanBenhNhanReadModel>($"{BASE}/benh-nhan/tong-quan{BuildQuery(f)}");

	public Task<ApiResult<List<BenhNhanTheoNgayReadModel>>> GetBenhNhanTheoNgay(ThongKeFilterRequest f)
		=> GetAsync<List<BenhNhanTheoNgayReadModel>>($"{BASE}/benh-nhan/theo-ngay{BuildQuery(f)}");

	public Task<ApiResult<List<BenhNhanTheoGioiTinhReadModel>>> GetBenhNhanTheoGioiTinh(ThongKeFilterRequest f)
		=> GetAsync<List<BenhNhanTheoGioiTinhReadModel>>($"{BASE}/benh-nhan/theo-gioi-tinh{BuildQuery(f)}");

	public Task<ApiResult<List<BenhNhanTheoDoTuoiReadModel>>> GetBenhNhanTheoDoTuoi(ThongKeFilterRequest f)
		=> GetAsync<List<BenhNhanTheoDoTuoiReadModel>>($"{BASE}/benh-nhan/theo-do-tuoi{BuildQuery(f)}");

	// ==================== CA KHÁM ====================
	public Task<ApiResult<TongQuanCaKhamReadModel>> GetTongQuanCaKham(ThongKeFilterRequest f)
		=> GetAsync<TongQuanCaKhamReadModel>($"{BASE}/ca-kham/tong-quan{BuildQuery(f)}");

	public Task<ApiResult<List<CaKhamTheoKhoangReadModel>>> GetCaKhamTheoKhoang(ThongKeFilterRequest f)
		=> GetAsync<List<CaKhamTheoKhoangReadModel>>($"{BASE}/ca-kham/theo-khoang{BuildQuery(f)}");

	// ==================== PHIÊN KHÁM ====================
	public Task<ApiResult<TongQuanPhienKhamReadModel>> GetTongQuanPhienKham(ThongKeFilterRequest f)
		=> GetAsync<TongQuanPhienKhamReadModel>($"{BASE}/phien-kham/tong-quan{BuildQuery(f)}");

	public Task<ApiResult<List<PhienKhamTheoNgayReadModel>>> GetPhienKhamTheoNgay(ThongKeFilterRequest f)
		=> GetAsync<List<PhienKhamTheoNgayReadModel>>($"{BASE}/phien-kham/theo-ngay{BuildQuery(f)}");

	public Task<ApiResult<List<PhienKhamTheoPhongReadModel>>> GetPhienKhamTheoPhong(ThongKeFilterRequest f)
		=> GetAsync<List<PhienKhamTheoPhongReadModel>>($"{BASE}/phien-kham/theo-phong{BuildQuery(f)}");

	public Task<ApiResult<List<PhienKhamTheoLoaiBenhReadModel>>> GetPhienKhamTheoLoaiBenh(ThongKeFilterRequest f, int top = 10)
		=> GetAsync<List<PhienKhamTheoLoaiBenhReadModel>>($"{BASE}/phien-kham/theo-loai-benh{BuildQuery(f, top)}");

	// ==================== TOA THUỐC ====================
	public Task<ApiResult<TongQuanToaThuocReadModel>> GetTongQuanToaThuoc(ThongKeFilterRequest f)
		=> GetAsync<TongQuanToaThuocReadModel>($"{BASE}/toa-thuoc/tong-quan{BuildQuery(f)}");

	public Task<ApiResult<List<ToaThuocTheoKhoangReadModel>>> GetToaThuocTheoKhoang(ThongKeFilterRequest f)
		=> GetAsync<List<ToaThuocTheoKhoangReadModel>>($"{BASE}/toa-thuoc/theo-khoang{BuildQuery(f)}");

	public Task<ApiResult<List<TopThuocReadModel>>> GetTopThuoc(ThongKeFilterRequest f, int top = 10)
		=> GetAsync<List<TopThuocReadModel>>($"{BASE}/toa-thuoc/top-thuoc{BuildQuery(f, top)}");

	public Task<ApiResult<List<TopBacSiKeDonReadModel>>> GetTopBacSiKeDon(ThongKeFilterRequest f, int top = 5)
		=> GetAsync<List<TopBacSiKeDonReadModel>>($"{BASE}/toa-thuoc/top-bac-si-ke-don{BuildQuery(f, top)}");

	// ==================== NHÂN VIÊN ====================
	public Task<ApiResult<TongQuanNhanVienReadModel>> GetTongQuanNhanVien()
		=> GetAsync<TongQuanNhanVienReadModel>($"{BASE}/nhan-vien/tong-quan");

	public Task<ApiResult<List<NhanVienTheoChucVuReadModel>>> GetNhanVienTheoChucVu()
		=> GetAsync<List<NhanVienTheoChucVuReadModel>>($"{BASE}/nhan-vien/theo-chuc-vu");

	public Task<ApiResult<List<NhanVienTheoPhongReadModel>>> GetNhanVienTheoPhong()
		=> GetAsync<List<NhanVienTheoPhongReadModel>>($"{BASE}/nhan-vien/theo-phong");

	public Task<ApiResult<List<HieuSuatBacSiReadModel>>> GetHieuSuatBacSi(ThongKeFilterRequest f)
		=> GetAsync<List<HieuSuatBacSiReadModel>>($"{BASE}/nhan-vien/hieu-suat{BuildQuery(f)}");

	public Task<ApiResult<List<NgayNghiNhanVienReadModel>>> GetNgayNghiNhanVien(ThongKeFilterRequest f)
		=> GetAsync<List<NgayNghiNhanVienReadModel>>($"{BASE}/nhan-vien/ngay-nghi{BuildQuery(f)}");
}