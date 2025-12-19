-- ============================================
--   CLEAN & REBUILD DATABASE
-- ============================================
use master
IF DB_ID('PhongKham') IS NOT NULL
    DROP DATABASE PhongKham;
GO

CREATE DATABASE PhongKham;
GO

USE PhongKham;
GO

-- ============================================
-- 1. TÀI KHOẢN VÀ THÔNG TIN CÁ NHÂN
-- ============================================

-- Quản lý tài khoản chung --
CREATE TABLE TaiKhoan (
    TaiKhoanID      INT IDENTITY(1,1) PRIMARY KEY,
    Email           NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash    NVARCHAR(255) NOT NULL,
    Role            NVARCHAR(20) CHECK (Role IN ('customer', 'employee', 'admin')) NOT NULL,
    Status          NVARCHAR(20) CHECK (Status IN ('active', 'inactive')) DEFAULT 'active',
    NgayTao         DATETIME DEFAULT GETDATE(),
    NgayCapNhat     DATETIME DEFAULT GETDATE()
);
GO
-- Quản lý thông tin cá nhân --
CREATE TABLE ThongTinCaNhan (
    ThongTinID  INT IDENTITY(1,1) PRIMARY KEY,
    TaiKhoanID  INT NULL,
    HoTen       NVARCHAR(200) NOT NULL,
    NgaySinh    DATE,
    GioiTinh    NVARCHAR(10) CHECK (GioiTinh IN ('Nam', 'Nu', 'Khac')),
    SDT         NVARCHAR(20),
    EmailLienHe NVARCHAR(150),
    DiaChi      NVARCHAR(255),
    Avatar      NVARCHAR(300),
    Loai        NVARCHAR(20) CHECK (Loai IN ('benhnhan', 'nhanvien')) NOT NULL,
    NgayTao     DATETIME DEFAULT GETDATE(),
    NgayCapNhat DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TaiKhoanID) REFERENCES TaiKhoan(TaiKhoanID) ON DELETE SET NULL
);
GO

-- Mã định danh bệnh nhân để thực hiện các nghiệp vụ trong hệ thống --
CREATE TABLE BenhNhan (
    BenhNhanID       INT IDENTITY(1,1) PRIMARY KEY,
    ThongTinID INT NOT NULL UNIQUE,
    MaSoBN     NVARCHAR(50),
    FOREIGN KEY (ThongTinID) REFERENCES ThongTinCaNhan(ThongTinID) ON DELETE CASCADE
);
GO


-- ============================================
-- 2. NHÂN VIÊN VÀ CHỨC VỤ
-- ============================================

-- Bảng chức vụ để phân loại các nhân viên trong phòng khám --
CREATE TABLE ChucVu (
    ChucVuID INT IDENTITY(1,1) PRIMARY KEY,
    TenChucVu NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(MAX),
    NgayTao DATETIME DEFAULT GETDATE(),
    TrangThai BIT DEFAULT 1
);
GO

-- Mã định danh nhân viên để thực hiện các nghiệp vụ trong hệ thống --
CREATE TABLE NhanVien (
    NhanVienID   INT IDENTITY(1,1) PRIMARY KEY,
    ThongTinID   INT NOT NULL UNIQUE,
    ChucVuID	 INT NOT NULL,
    Luong       DECIMAL(12,2),
    NgayVaoLam     DATE,
    BangCap      NVARCHAR(500),
    KinhNghiem   NVARCHAR(500),
    FOREIGN KEY (ThongTinID) REFERENCES ThongTinCaNhan(ThongTinID) ON DELETE CASCADE,
    FOREIGN KEY (ChucVuID) REFERENCES ChucVu(ChucVuID)
);
GO

-- Public bác sĩ cho app bệnh nhân --
CREATE TABLE BacSiProfile (
    BacSiProfileID INT IDENTITY(1,1) PRIMARY KEY,
    NhanVienID INT UNIQUE NOT NULL,
    GioiThieu NVARCHAR(MAX),
    ChuyenMon NVARCHAR(300),
    ThanhTuu NVARCHAR(MAX),
    HinhAnh NVARCHAR(500),
    KinhNghiem NVARCHAR(MAX),
    NgayCapNhat DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID) ON DELETE CASCADE
);
GO


-- ============================================
-- 3. THÔNG TIN PHÒNG KHÁM VÀ CƠ SƠ VẬT CHẤT
-- ============================================

-- Thông tin phòng khám --
CREATE TABLE PhongKham (
    PhongKhamID INT IDENTITY(1,1) PRIMARY KEY,
    TenPhongKham NVARCHAR(255) NOT NULL,
    GioiThieu NVARCHAR(MAX),
    DiaChi NVARCHAR(300),
    Hotline NVARCHAR(50),
    Email NVARCHAR(100),
    Website NVARCHAR(200),
    GioMoCua NVARCHAR(100),      
    HinhAnhBanner NVARCHAR(500),
    NgayCapNhat DATETIME DEFAULT GETDATE()
);
GO

-- Phân loại các phòng riêng với các chức năng riêng --
CREATE TABLE PhongChucNang (
    PhongChucNangID INT IDENTITY(1,1) PRIMARY KEY,
    TenPhong NVARCHAR(200) NOT NULL,
    LoaiPhong NVARCHAR(100),
    MoTa NVARCHAR(MAX),
    TrangThai NVARCHAR(50) CHECK (TrangThai IN (N'Hoạt động', N'Hỏng', N'Bảo trì')) DEFAULT N'Hoạt động',
    NgayNhap DATETIME DEFAULT GETDATE()
);
GO

-- Các thiết bị có trong phòng khám --
CREATE TABLE ThietBi (
    ThietBiID INT IDENTITY(1,1) PRIMARY KEY,
    TenTB NVARCHAR(200) NOT NULL,
    LoaiTB NVARCHAR(100),
    TinhTrang NVARCHAR(100) CHECK (TinhTrang IN (N'Hoạt động', N'Hỏng', N'Bảo trì')) DEFAULT N'Hoạt động',
    NgayNhap DATETIME DEFAULT GETDATE()
);
GO

-- Các thiết bị có trong 1 phòng chức năng cụ thể --
CREATE TABLE PhongChucNang_ThietBi (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    PhongChucNangID INT NOT NULL,
    ThietBiID INT NOT NULL,
    SoLuong INT DEFAULT 1,
    GhiChu NVARCHAR(MAX),

    FOREIGN KEY (PhongChucNangID) REFERENCES PhongChucNang(PhongChucNangID) ON DELETE CASCADE,
    FOREIGN KEY (ThietBiID) REFERENCES ThietBi(ThietBiID)
);
GO

-- ============================================
-- 4. LỊCH LÀM VIỆC (NHÂN VIÊN)
-- ============================================

-- Lịch làm việc của nhân viên trong phòng khám, những chức vụ khác nhau sẽ có lịch làm việc khác nhau --
CREATE TABLE LichLamViecNhanVien (
    LichLamViecID INT IDENTITY(1,1) PRIMARY KEY,
    NhanVienID INT NOT NULL,
	PhongChucNangID INT NOT NULL,
    Ngay DATE NOT NULL,
    CaLamViec INT NOT NULL,
    GhiChu NVARCHAR(500),
    FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID) ON DELETE CASCADE,
	FOREIGN KEY (PhongChucNangID) REFERENCES PhongChucNang(PhongChucNangID)
);
GO

-- Lịch nghỉ của nhân viên trong phòng khám, những chức vụ khác nhau sẽ có lịch nghỉ khác nhau --
CREATE TABLE NgayNghiNhanVien (
    NgayNghiID INT IDENTITY(1,1) PRIMARY KEY,
    NhanVienID INT NOT NULL,
    Ngay DATE NOT NULL,
    LyDo NVARCHAR(300),
    FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID) ON DELETE CASCADE,
    CONSTRAINT UQ_NgayNghi UNIQUE (NhanVienID, Ngay)
);
GO
-- Các khung giờ làm việc trong 1 ngày --
CREATE TABLE KhungGioKham (
    KhungGioID INT IDENTITY(1,1) PRIMARY KEY,
    GioBatDau TIME NOT NULL,
    GioKetThuc TIME NOT NULL,
    TenKhung NVARCHAR(50),
    MaxSlot INT NOT NULL DEFAULT 5,
    CONSTRAINT UQ_Khung UNIQUE (GioBatDau, GioKetThuc)
);
GO

-- Số ca khám cụ thể trong 1 khung giờ cố định --
CREATE TABLE CaKham (
    CaKhamID INT IDENTITY(1,1) PRIMARY KEY,
    LichLamViecID INT NOT NULL,
	PhongChucNangID INT NULL, 
    NgayKham DATE NOT NULL,
    KhungGioID INT NOT NULL,
    BenhNhanID INT NULL,
    LyDoKham NVARCHAR(500),
    TrangThai NVARCHAR(50) CHECK (TrangThai IN ('available', 'booked', 'confirmed', 'completed', 'cancelled', 'no-show')) DEFAULT 'available',
    NgayDat DATETIME DEFAULT GETDATE(),
    GhiChu NVARCHAR(MAX),
    FOREIGN KEY (LichLamViecID) REFERENCES LichLamViecNhanVien(LichLamViecID) ON DELETE CASCADE,
    FOREIGN KEY (BenhNhanID) REFERENCES BenhNhan(BenhNhanID),
	FOREIGN KEY (PhongChucNangID) REFERENCES PhongChucNang(PhongChucNangID),
    FOREIGN KEY (KhungGioID) REFERENCES KhungGioKham(KhungGioID),
    CONSTRAINT CHK_CaKham_BenhNhan CHECK (
        (TrangThai = 'available' AND BenhNhanID IS NULL) OR
        (TrangThai != 'available' AND BenhNhanID IS NOT NULL)
    )
);
GO


-- ============================================
-- 5. MEDICAL EXAMINATION
-- ============================================

-- Hồ sơ bệnh án của mỗi bệnh nhân --
CREATE TABLE HoSoBenhAn (
    HoSoBenhAnID INT IDENTITY(1,1) PRIMARY KEY,
    BenhNhanID INT NOT NULL UNIQUE,
    BenhNen NVARCHAR(MAX),
    DiUng NVARCHAR(MAX),
    TienSuBenh NVARCHAR(MAX),
    TienSuGiaDinh NVARCHAR(MAX),
    ThoiQuenSong NVARCHAR(MAX),
    ThongTinKhac NVARCHAR(MAX),
    NgayTao DATETIME DEFAULT GETDATE(),
    NgayCapNhat DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (BenhNhanID) REFERENCES BenhNhan(BenhNhanID) ON DELETE CASCADE
);
GO

-- Phiên khám của bệnh nhân --
CREATE TABLE PhienKham (
    PhienKhamID INT IDENTITY(1,1) PRIMARY KEY,
    CaKhamID INT NOT NULL,
    BenhNhanID INT NOT NULL,
    NhanVienID INT NOT NULL,
    PhongChucNangID INT NULL,
    TrieuChung NVARCHAR(MAX),
    GhiChu NVARCHAR(MAX),
    HinhAnhJSON NVARCHAR(MAX),
    AI_KetQuaJSON NVARCHAR(MAX),
    ChuanDoanCuoi NVARCHAR(300),
    NgayKham DATETIME DEFAULT GETDATE(),
    TrangThai NVARCHAR(50) CHECK (TrangThai IN ('in-progress', 'completed', 'cancelled')) DEFAULT 'in-progress',
    FOREIGN KEY (CaKhamID) REFERENCES CaKham(CaKhamID),
    FOREIGN KEY (BenhNhanID) REFERENCES BenhNhan(BenhNhanID),
    FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID),
    FOREIGN KEY (PhongChucNangID) REFERENCES PhongChucNang(PhongChucNangID)
);
GO

-- Các thiết bị được sử dụng trong phiên khám --
CREATE TABLE PhienKham_ThietBi (
    PhienKham_ThietBiID INT IDENTITY(1,1) PRIMARY KEY,
    PhienKhamID INT NOT NULL,
    ThietBiID INT NOT NULL,
    SoLuong INT DEFAULT 1,
    GhiChu NVARCHAR(MAX),
    FOREIGN KEY (PhienKhamID) REFERENCES PhienKham(PhienKhamID) ON DELETE CASCADE,
    FOREIGN KEY (ThietBiID) REFERENCES ThietBi(ThietBiID)
);
GO


-- ============================================
-- 6. XÉT NGHIỆM CẬN LÂM SÀNG
-- ============================================

-- Các loại xét nghiệm của phòng khám --
CREATE TABLE CanLamSang (
    CanLamSangID INT IDENTITY(1,1) PRIMARY KEY,
    TenCLS NVARCHAR(200) NOT NULL,
    MoTa NVARCHAR(MAX),
    Gia DECIMAL(18,2),
    LoaiXetNghiem NVARCHAR(100),
    ChiDinhTuBacSi BIT DEFAULT 1,
    NgayTao DATETIME DEFAULT GETDATE()
);
GO

-- Các loại xét nghiệm được sử dụng trong phiên khám cụ thể --
CREATE TABLE PhienKham_CanLamSang (
    PhienKham_CanLamSangID INT IDENTITY(1,1) PRIMARY KEY,
    PhienKhamID INT NOT NULL,
    CanLamSangID INT NOT NULL,
    TrangThai NVARCHAR(50) CHECK (TrangThai IN ('pending', 'in-progress', 'completed', 'cancelled')) DEFAULT 'pending',
    KetQua NVARCHAR(MAX),
    FileDinhKem NVARCHAR(500),
    NgayChiDinh DATETIME DEFAULT GETDATE(),
    NgayThucHien DATETIME NULL,
    NhanVienChiDinhID INT NULL,
    NhanVienThucHienID INT NULL,
    GhiChu NVARCHAR(MAX),
    FOREIGN KEY (PhienKhamID) REFERENCES PhienKham(PhienKhamID) ON DELETE CASCADE,
    FOREIGN KEY (CanLamSangID) REFERENCES CanLamSang(CanLamSangID),
    FOREIGN KEY (NhanVienChiDinhID) REFERENCES NhanVien(NhanVienID),
    FOREIGN KEY (NhanVienThucHienID) REFERENCES NhanVien(NhanVienID)
);
GO


-- ============================================
-- 7. THUỐC VÀ TOA THUỐC ĐIỀU TRỊ
-- ============================================

-- Thông tin các loại thuốc điều trị --
CREATE TABLE Thuoc (
    ThuocID INT IDENTITY(1,1) PRIMARY KEY,
    TenThuoc NVARCHAR(200) NOT NULL,
    HoatChat NVARCHAR(MAX),
    TrangThai NVARCHAR(50) CHECK (TrangThai IN ('available', 'out-of-stock', 'discontinued')) DEFAULT 'available'
);
GO

-- Toa thuốc cần dùng để điều trị cho bệnh nhân trong phiên khám cụ thể --
CREATE TABLE ToaThuoc (
    ToaThuocID INT IDENTITY(1,1) PRIMARY KEY,
    PhienKhamID INT NOT NULL,
    NhanVienKeDonID INT NOT NULL,
    NgayLap DATETIME DEFAULT GETDATE(),
    TongTien DECIMAL(18,2),
    TrangThai NVARCHAR(50) CHECK (TrangThai IN ('pending', 'dispensed', 'cancelled')) DEFAULT 'pending',
    GhiChu NVARCHAR(MAX),
    FOREIGN KEY (PhienKhamID) REFERENCES PhienKham(PhienKhamID),
    FOREIGN KEY (NhanVienKeDonID) REFERENCES NhanVien(NhanVienID)
);
GO

-- Các loại thuốc cần dùng để điều trị cho bệnh nhân trong phiên khám cụ thể --
CREATE TABLE ChiTietToaThuoc (
    ChiTietToaThuocID INT IDENTITY(1,1) PRIMARY KEY,
    ToaThuocID INT NOT NULL,
    ThuocID INT NOT NULL,
    LieuDung NVARCHAR(500),
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18,2),
    ThanhTien DECIMAL(18,2),
    FOREIGN KEY (ToaThuocID) REFERENCES ToaThuoc(ToaThuocID) ON DELETE CASCADE,
    FOREIGN KEY (ThuocID) REFERENCES Thuoc(ThuocID)
);
GO



-- ============================================
-- 8. CÁC LOẠI BỆNH DA LIỄU
-- ============================================

-- Các loại bệnh da liễu --
CREATE TABLE LoaiBenh (
    LoaiBenhID INT IDENTITY(1,1) PRIMARY KEY,
    TenBenh NVARCHAR(200) NOT NULL,
    TenKhoaHoc NVARCHAR(200),
    NhomBenh NVARCHAR(100),
    MoTa NVARCHAR(MAX),
    DoPhoBien NVARCHAR(50) CHECK (DoPhoBien IN (N'phổ biến', N'ít gặp', N'hiếm')),
    MucDoNghiemTrong NVARCHAR(50) CHECK (MucDoNghiemTrong IN (N'nhẹ', N'trung bình', N'nặng')),
    NgayTao DATETIME DEFAULT GETDATE()
);
GO

-- Các loại bệnh được khám ra trong phiên khám cụ thể --
CREATE TABLE PhienKham_Benh (
    PhienKham_BenhID INT IDENTITY(1,1) PRIMARY KEY,
    PhienKhamID INT NOT NULL,
    LoaiBenhID INT NOT NULL,
    LoaiChuanDoan NVARCHAR(50) CHECK (LoaiChuanDoan IN ('primary', 'secondary', 'suspected')),
    GhiChu NVARCHAR(MAX),
    FOREIGN KEY (PhienKhamID) REFERENCES PhienKham(PhienKhamID) ON DELETE CASCADE,
    FOREIGN KEY (LoaiBenhID) REFERENCES LoaiBenh(LoaiBenhID)
);
GO


-- ============================================
-- 9. TÁI KHÁM VÀ LIỆU TRÌNH ĐIỀU TRỊ
-- ============================================

-- Tái khám sau phiên khám đầu tiên --
CREATE TABLE TaiKham (
    TaiKhamID INT IDENTITY(1,1) PRIMARY KEY,
    PhienKhamID INT NOT NULL,          -- phiên khám gốc
    BenhNhanID INT NOT NULL,           -- bệnh nhân được yêu cầu tái khám
    NgayDuKien DATE NOT NULL,          -- ngày bác sĩ chỉ định
    LyDo NVARCHAR(500),                -- lý do tái khám
    TrangThai NVARCHAR(50) CHECK (TrangThai IN ('pending', 'scheduled', 'completed', 'cancelled'))
        DEFAULT 'pending',
    CaKhamID INT NULL,                 -- nếu đã gán lịch tái khám vào ca khám
    NgayTao DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (PhienKhamID) REFERENCES PhienKham(PhienKhamID) ON DELETE CASCADE,
    FOREIGN KEY (BenhNhanID) REFERENCES BenhNhan(BenhNhanID),
    FOREIGN KEY (CaKhamID) REFERENCES CaKham(CaKhamID)
);
GO

-- Liệu trình điều trị sau phiên khám --
CREATE TABLE LieuTrinhDieuTri (
    LieuTrinhID INT IDENTITY(1,1) PRIMARY KEY,
    BenhNhanID INT NOT NULL,
    PhienKhamID INT NOT NULL,          -- phiên khám bắt đầu liệu trình
    TenLieuTrinh NVARCHAR(200) NOT NULL, 
    TongSoBuoi INT NOT NULL,           -- ví dụ 4 buổi / 6 buổi
    TrangThai NVARCHAR(50) CHECK (TrangThai IN ('in-progress', 'completed', 'cancelled'))
        DEFAULT 'in-progress',
    GhiChu NVARCHAR(MAX),
    NgayBatDau DATE DEFAULT GETDATE(),
    NgayKetThuc DATE NULL,

    FOREIGN KEY (BenhNhanID) REFERENCES BenhNhan(BenhNhanID),
    FOREIGN KEY (PhienKhamID) REFERENCES PhienKham(PhienKhamID)
);
GO

-- Số buổi cụ thể của bệnh nhân đã tiến hành trong liệu trình --
CREATE TABLE LieuTrinh_BuoiDieuTri (
    BuoiDieuTriID INT IDENTITY(1,1) PRIMARY KEY,
    LieuTrinhID INT NOT NULL,
    SoBuoi INT NOT NULL,                       -- buổi số mấy (1,2,3..)
    NgayDuKien DATE,
    NgayThucHien DATE NULL,
    NhanVienID INT NULL,                       -- bác sĩ thực hiện
    TrangThai NVARCHAR(50) CHECK (TrangThai IN ('pending','completed','skipped'))
        DEFAULT 'pending',
    GhiChu NVARCHAR(MAX),
    HinhAnhJSON NVARCHAR(MAX),                 -- ảnh theo dõi liệu trình
    FOREIGN KEY (LieuTrinhID) REFERENCES LieuTrinhDieuTri(LieuTrinhID) ON DELETE CASCADE,
    FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID)
);
GO

-- Các xét nghiệm đẫ tiến hành trong buổi liệu trình điều trị --
CREATE TABLE BuoiDieuTri_CanLamSang (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    BuoiDieuTriID INT NOT NULL,
    CanLamSangID INT NOT NULL,
    KetQua NVARCHAR(MAX),
    TrangThai NVARCHAR(50) CHECK (TrangThai IN ('pending','done')) DEFAULT 'pending',

    FOREIGN KEY (BuoiDieuTriID) REFERENCES LieuTrinh_BuoiDieuTri(BuoiDieuTriID) ON DELETE CASCADE,
    FOREIGN KEY (CanLamSangID) REFERENCES CanLamSang(CanLamSangID)
);
GO

-- ============================================
-- 10. AI TRAINING
-- ============================================

CREATE TABLE AI_TrainingFeedback (
    FeedbackID INT IDENTITY(1,1) PRIMARY KEY,
    PhienKhamID INT NOT NULL,
    ImagePath NVARCHAR(300),
    AIPredictionsJSON NVARCHAR(MAX),
    Doctor_FinalDiagnosis NVARCHAR(300),
    ErrorType NVARCHAR(100),
    ErrorScore DECIMAL(5,2),
    IsUsedForTraining BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (PhienKhamID) REFERENCES PhienKham(PhienKhamID) ON DELETE CASCADE
);
GO



-- ============================================
-- 11. BÀI VIẾT
-- ============================================

CREATE TABLE BaiViet (
    BaiVietID INT IDENTITY(1,1) PRIMARY KEY,
    TieuDe NVARCHAR(300) NOT NULL,
    TomTat NVARCHAR(500),
    NoiDung NVARCHAR(MAX),
    HinhAnh NVARCHAR(500),
    TacGiaID INT NULL,
    LoaiBenhID INT NULL,
    LuotXem INT DEFAULT 0,
    NgayDang DATETIME DEFAULT GETDATE(),
    NgayCapNhat DATETIME,
    TrangThai NVARCHAR(50) CHECK (TrangThai IN ('draft', 'published', 'hidden', 'archived')) DEFAULT 'draft',
	FOREIGN KEY (TacGiaID) REFERENCES TaiKhoan(TaiKhoanID) ON DELETE SET NULL,
    FOREIGN KEY (LoaiBenhID) REFERENCES LoaiBenh(LoaiBenhID)
);
GO

PRINT N'Database ClinicDB_New đã được tạo thành công (phiên bản gộp bác sĩ & nhân viên)!';
GO
