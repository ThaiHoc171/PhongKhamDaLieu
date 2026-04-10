-- phongkhamdalieu
USE master;
CREATE DATABASE HoanMyClinic;
--- TAI KHOAN VA THONG TIN ---
CREATE TABLE TaiKhoan(
    TaiKhoanID INT IDENTITY PRIMARY KEY,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    MatKhau NVARCHAR(255) NOT NULL,
    VaiTro NVARCHAR(20) NOT NULL
        CONSTRAINT CK_TaiKhoan_VaiTro
        CHECK (VaiTro IN (N'Bệnh nhân', N'Nhân viên', N'Admin', N'Khách')),
    TrangThai NVARCHAR(50) NOT NULL
        CONSTRAINT DF_TaiKhoan_TrangThai DEFAULT N'Hoạt động'
        CONSTRAINT CK_TaiKhoan_TrangThai
        CHECK (TrangThai IN (N'Hoạt động', N'Bị khóa')),
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    NgayCapNhat DATETIME NULL,
    FCMToken NVARCHAR(500) NULL
);
CREATE TABLE ThongTinCaNhan (
        ThongTinID INT IDENTITY PRIMARY KEY,
        TaiKhoanID INT NULL,
        HoTen NVARCHAR(200) NOT NULL,
        NgaySinh DATE NOT NULL,
        GioiTinh NVARCHAR(10) NOT NULL
            CONSTRAINT CK_TTCN_GioiTinh
            CHECK (GioiTinh IN (N'Nam', N'Nữ', N'Khác')),
        SDT NVARCHAR(20) NOT NULL,
        EmailLienHe NVARCHAR(150) NULL,  
        DiaChi NVARCHAR(255) NOT NULL,
        Avatar NVARCHAR(300) NULL,
        Loai NVARCHAR(20) NOT NULL
            CONSTRAINT FK_TTCN_Loai
            CHECK (Loai IN (N'Bệnh nhân', N'Nhân viên',N'Admin',N'Khách')),
        NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
        NgayCapNhat DATETIME NULL,

        CONSTRAINT FK_TTCN_TaiKhoan
            FOREIGN KEY (TaiKhoanID)
            REFERENCES TaiKhoan(TaiKhoanID)
            ON DELETE SET NULL,
        CONSTRAINT UQ_TTCN_Email UNIQUE (EmailLienHe),
        CONSTRAINT UQ_TTCN_SDT UNIQUE (SDT)
);
    CREATE TABLE BenhNhan (
        BenhNhanID INT IDENTITY PRIMARY KEY,
        ThongTinID INT NOT NULL UNIQUE,
        NgayTao DATETIME NOT NULL 
            CONSTRAINT DF_BenhNhan_NgayTao DEFAULT GETDATE(),
        NgayCapNhat DATETIME NULL,
        GhiChu NVARCHAR(MAX),
        CONSTRAINT FK_BenhNhan_TTCN
            FOREIGN KEY (ThongTinID)
            REFERENCES ThongTinCaNhan(ThongTinID)
            ON DELETE CASCADE
    );
--- TỔ CHỨC VÀ NHÂN SỰ ---
CREATE TABLE ChucVu (
    ChucVuID INT IDENTITY(1,1) PRIMARY KEY,
    TenChucVu NVARCHAR(100) NOT NULL,
    MoTa NVARCHAR(MAX) NOT NULL,
    TrangThai NVARCHAR(50) NOT NULL
        CONSTRAINT DF_ChucVu_TrangThai DEFAULT N'Hoạt động'
        CONSTRAINT CK_ChucVu_TrangThai
        CHECK (TrangThai IN (N'Hoạt động', N'Vô hiệu')),
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    NgayCapNhat DATETIME NULL,
    CONSTRAINT UQ_ChucVu_Ten UNIQUE (TenChucVu)
);
CREATE TABLE PhongChucNang (
    PhongChucNangID INT IDENTITY(1,1) PRIMARY KEY,
    TenPhong NVARCHAR(200) NOT NULL UNIQUE,
    MoTa NVARCHAR(MAX),
    TrangThai NVARCHAR(50) NOT NULL
        CONSTRAINT DF_Phong_TrangThai DEFAULT N'Hoạt động'
        CONSTRAINT CK_Phong_TrangThai
        CHECK (TrangThai IN (N'Hoạt động', N'Hỏng', N'Bảo trì')),

    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    NgayCapNhat DATETIME NULL
);
CREATE TABLE NhanVien (
    NhanVienID   INT IDENTITY(1,1) PRIMARY KEY,
    ThongTinID   INT NOT NULL UNIQUE,
    ChucVuID	 INT NOT NULL,
    NgayVaoLam     DATE,
    BangCap      NVARCHAR(500),
    KinhNghiem   NVARCHAR(500),
    TrangThai NVARCHAR(50) NOT NULL
        CONSTRAINT DF_NhanVien_TrangThai DEFAULT N'Đang làm việc'
        CONSTRAINT CK_NhanVien_TrangThai
        CHECK (TrangThai IN (N'Đang làm việc', N'Nghỉ việc')),
    PhongChucNangID INT NOT NULL,
    NgayTao DATETIME NOT NULL 
        CONSTRAINT DF_NhanVien_NgayTao DEFAULT GETDATE(),
    NgayCapNhat DATETIME NULL,
    CONSTRAINT FK_NhanVien_TTCN FOREIGN KEY (ThongTinID) REFERENCES ThongTinCaNhan(ThongTinID) ON DELETE CASCADE,
    CONSTRAINT FK_NhanVien_ChucVu FOREIGN KEY (ChucVuID) REFERENCES ChucVu(ChucVuID),
    CONSTRAINT FK_NhanVien_PCN FOREIGN KEY (PhongChucNangID ) REFERENCES PhongChucNang(PhongChucNangID)
);
CREATE TABLE BacSiProfile (
    BacSiProfileID INT IDENTITY(1,1) PRIMARY KEY,
    NhanVienID INT NOT NULL UNIQUE,
    GioiThieu NVARCHAR(MAX),
    ChuyenMon NVARCHAR(300),
    ThanhTuu NVARCHAR(MAX),
    HinhAnh NVARCHAR(500),
    KinhNghiem NVARCHAR(MAX),
    NgayCapNhat DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_BacSiProfile_NhanVien
        FOREIGN KEY (NhanVienID)
        REFERENCES NhanVien(NhanVienID)
        ON DELETE CASCADE
);
--- CƠ SỞ VẬT CHẤT ---
CREATE TABLE PhongKham (
    PhongKhamID INT IDENTITY(1,1) PRIMARY KEY,
    TenPhongKham NVARCHAR(255) NOT NULL,
    GioiThieu NVARCHAR(MAX),

    DiaChi NVARCHAR(300),
    Hotline NVARCHAR(50),
    Email NVARCHAR(100),
    Website NVARCHAR(200),

    HinhAnhBanner NVARCHAR(500),

    TrangThai NVARCHAR(50) NOT NULL
        CONSTRAINT CK_PhongKham_TrangThai
        CHECK (TrangThai IN (N'Hoạt động', N'Đóng cửa', N'Ngưng hoạt động'))
        DEFAULT N'Hoạt động',

    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    NgayCapNhat DATETIME NOT NULL DEFAULT GETDATE()
);
CREATE TABLE ThietBi (
    ThietBiID INT IDENTITY(1,1) PRIMARY KEY,
    TenTB NVARCHAR(200) NOT NULL,
    LoaiTB NVARCHAR(100) NOT NULL,
    TrangThai NVARCHAR(50)
    CONSTRAINT DF_ThietBi_TrangThai DEFAULT N'Hoạt động'
    CONSTRAINT CK_ThietBi_TrangThai CHECK (TrangThai IN (N'Hoạt động', N'Vô hiệu')),
    NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
    NgayCapNhat DATETIME NULL,
    CONSTRAINT UQ_ThietBi_Ten UNIQUE (TenTB)
);
CREATE TABLE PhongChucNang_ThietBi (
    PCN_TB_ID INT IDENTITY(1,1) PRIMARY KEY,
    PhongChucNangID INT NOT NULL,
    ThietBiID INT NOT NULL,

    TongSoLuong INT NOT NULL DEFAULT 0
        CHECK (TongSoLuong >= 0),

    CONSTRAINT UQ_PCN_TB UNIQUE (PhongChucNangID, ThietBiID),

    FOREIGN KEY (PhongChucNangID)
        REFERENCES PhongChucNang(PhongChucNangID)
        ON DELETE CASCADE,

    FOREIGN KEY (ThietBiID)
        REFERENCES ThietBi(ThietBiID)
);
CREATE TABLE ChiTiet_PCNTB (
    ChiTietID INT IDENTITY(1,1) PRIMARY KEY,
    PCN_TB_ID INT NOT NULL,

    MaTaiSan NVARCHAR(100) NOT NULL UNIQUE, -- SERIAL / MÃ QL
    NgayNhap DATETIME NOT NULL DEFAULT GETDATE(),

    TinhTrang NVARCHAR(50) NOT NULL
        CHECK (TinhTrang IN (N'Hoạt động', N'Hỏng', N'Bảo trì'))
        DEFAULT N'Hoạt động',

    GhiChu NVARCHAR(MAX),

    FOREIGN KEY (PCN_TB_ID)
        REFERENCES PhongChucNang_ThietBi(PCN_TB_ID)
        ON DELETE CASCADE
);
--- LỊCH LÀM VIỆC VÀ THỜI GIAN ---
CREATE TABLE LichLamViecNhanVien (
    LichLamViecID INT IDENTITY(1,1) PRIMARY KEY,
    NhanVienID INT NOT NULL,
    Ngay DATE NOT NULL,

    CaLamViec INT NOT NULL
        CONSTRAINT CK_LichLamViec_Ca
        CHECK (CaLamViec IN (1, 2)),

    GhiChu NVARCHAR(500),

    CONSTRAINT UQ_LichLamViec
        UNIQUE (NhanVienID, Ngay, CaLamViec),

    CONSTRAINT FK_LichLamViec_NhanVien
        FOREIGN KEY (NhanVienID)
        REFERENCES NhanVien(NhanVienID)
        ON DELETE CASCADE
);
CREATE TABLE NgayNghiNhanVien (
    NgayNghiID INT IDENTITY(1,1) PRIMARY KEY,
    NhanVienID INT NOT NULL,
    Ngay DATE NOT NULL,
    LyDo NVARCHAR(300),
    CONSTRAINT FK_NgayNghi_NhanVien FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID) ON DELETE CASCADE,
    CONSTRAINT UQ_NgayNghi UNIQUE (NhanVienID, Ngay)
);
CREATE TABLE KhungGioKham (
    KhungGioID INT IDENTITY(1,1) PRIMARY KEY,
    CaLamViec INT NOT NULL
        CONSTRAINT CK_KhungGio_Ca
        CHECK (CaLamViec IN (1, 2)),
    GioBatDau TIME NOT NULL,
    GioKetThuc TIME NOT NULL,
    TenKhung NVARCHAR(50),
    CONSTRAINT CK_KhungGio_Time
        CHECK (GioBatDau < GioKetThuc),

    CONSTRAINT UQ_Khung
        UNIQUE (CaLamViec, GioBatDau, GioKetThuc)
);
--- CA KHÁM VÀ ĐẶT LỊCH ---
CREATE TABLE CaKham (
    CaKhamID INT IDENTITY(1,1) PRIMARY KEY,

    LoaiCaKham NVARCHAR(50) NOT NULL
        CONSTRAINT CK_CaKham_Loai
        CHECK (LoaiCaKham IN (N'Khám', N'Điều trị')),

    LichLamViecID INT NULL,
    KhungGioID INT NOT NULL,
    PhongChucNangID INT NULL,

    ThongTinID INT NULL,

    LyDoKham NVARCHAR(500),

    TrangThai NVARCHAR(50) NOT NULL
        CONSTRAINT DF_CaKham_TrangThai DEFAULT N'Trống'
        CONSTRAINT CK_CaKham_TrangThai
        CHECK (TrangThai IN (N'Trống',N'Đã đặt',N'Đã xác nhận',N'Đang khám', N'Hoàn thành', N'Đã hủy', N'Không đến')),

    NgayDat DATE NULL,
    NgayKham DATE NOT NULL,
    GhiChu NVARCHAR(MAX),

    CONSTRAINT FK_CaKham_LichLamViec
        FOREIGN KEY (LichLamViecID)
        REFERENCES LichLamViecNhanVien(LichLamViecID)
        ON DELETE CASCADE,

    CONSTRAINT FK_CaKham_KhungGio
        FOREIGN KEY (KhungGioID)
        REFERENCES KhungGioKham(KhungGioID),

    CONSTRAINT FK_CaKham_ThongTin
        FOREIGN KEY (ThongTinID)
        REFERENCES ThongTinCaNhan(ThongTinID),

    CONSTRAINT FK_CaKham_Phong
        FOREIGN KEY (PhongChucNangID)
        REFERENCES PhongChucNang(PhongChucNangID),

    CONSTRAINT CK_CaKham_ThongTin
        CHECK (
            (TrangThai = N'Trống' AND ThongTinID IS NULL) OR
            (TrangThai <> N'Trống' AND ThongTinID IS NOT NULL)
        )
);--- HỒ SƠ Y TẾ VÀ KHÁM BỆNH ---
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
-- Phiên khám của bệnh nhân --
CREATE TABLE PhienKham (
    PhienKhamID INT IDENTITY(1,1) PRIMARY KEY,
    CaKhamID INT NOT NULL,
    BenhNhanID INT NOT NULL,
    NhanVienID INT NOT NULL,
    PhongChucNangID INT NULL,
    TrieuChung NVARCHAR(MAX),
    GhiChu NVARCHAR(MAX),
    HinhAnh NVARCHAR(MAX),
    ChanDoanCuoi NVARCHAR(300),
    NgayKham DATETIME DEFAULT GETDATE(),
    TrangThai NVARCHAR(50) CHECK (TrangThai IN (N'Đang chờ',N'Đang khám', N'Hoàn thành', N'Đã hủy')) DEFAULT N'Đang chờ',
    FOREIGN KEY (CaKhamID) REFERENCES CaKham(CaKhamID),
    FOREIGN KEY (BenhNhanID) REFERENCES BenhNhan(BenhNhanID),
    FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID),
    FOREIGN KEY (PhongChucNangID) REFERENCES PhongChucNang(PhongChucNangID)
);

-- Các thiết bị được sử dụng trong phiên khám --
CREATE TABLE PhienKham_ThietBi (
    PhienKham_ThietBiID INT IDENTITY(1,1) PRIMARY KEY,
    PhienKhamID INT NOT NULL,
    ChiTietID INT NOT NULL, -- THIẾT BỊ CỤ THỂ
    GhiChu NVARCHAR(MAX),

    FOREIGN KEY (PhienKhamID)
        REFERENCES PhienKham(PhienKhamID)
        ON DELETE CASCADE,

    FOREIGN KEY (ChiTietID)
        REFERENCES ChiTiet_PCNTB(ChiTietID)
);
GO
CREATE TABLE CanLamSang (
    CanLamSangID INT IDENTITY(1,1) PRIMARY KEY,
    TenCLS NVARCHAR(200) NOT NULL,
    MoTa NVARCHAR(MAX) NOT NULL,
    LoaiXetNghiem NVARCHAR(100) NOT NULL,
    TrangThai NVARCHAR(50) NOT NULL CHECK (TrangThai IN (N'Hoạt động',  N'Vô hiệu')) DEFAULT N'Hoạt động',
    NgayTao DATETIME DEFAULT GETDATE(),
    NgayCapNhat DATETIME NULL,
    CONSTRAINT UQ_CLS_Ten UNIQUE (TenCLS)
);
GO

-- Các loại xét nghiệm được sử dụng trong phiên khám cụ thể --
CREATE TABLE PhienKham_CanLamSang (
    PhienKham_CanLamSangID INT IDENTITY(1,1) PRIMARY KEY,
    PhienKhamID INT NOT NULL,
    CanLamSangID INT NOT NULL,
    TrangThai NVARCHAR(50) CHECK (TrangThai IN (N'Đang chờ',N'Đang thực hiện', N'Hoàn thành', N'Đã hủy')) DEFAULT N'Đang chờ',
    KetQua NVARCHAR(MAX),
    FileDinhKem NVARCHAR(500),
    NgayThucHien DATETIME DEFAULT GETDATE(),
    NhanVienChiDinhID INT NULL,
    NhanVienThucHienID INT NULL,
    GhiChu NVARCHAR(MAX),
    FOREIGN KEY (PhienKhamID) REFERENCES PhienKham(PhienKhamID) ON DELETE CASCADE,
    FOREIGN KEY (CanLamSangID) REFERENCES CanLamSang(CanLamSangID),
    FOREIGN KEY (NhanVienChiDinhID) REFERENCES NhanVien(NhanVienID),
    FOREIGN KEY (NhanVienThucHienID) REFERENCES NhanVien(NhanVienID)
);
--- THUỐC & ĐIỀU TRỊ ---
CREATE TABLE Thuoc (
    ThuocID INT IDENTITY(1,1) PRIMARY KEY,
    TenThuoc NVARCHAR(200) NOT NULL,
    HoatChat NVARCHAR(MAX)
);
GO

CREATE TABLE ToaThuoc (
    ToaThuocID INT IDENTITY(1,1) PRIMARY KEY,
    PhienKhamID INT NOT NULL,
    NhanVienKeDonID INT NOT NULL,
    NgayLap DATETIME DEFAULT GETDATE(),
    GhiChu NVARCHAR(MAX),

    CONSTRAINT UQ_ToaThuoc_PhienKham UNIQUE (PhienKhamID),

    FOREIGN KEY (PhienKhamID) REFERENCES PhienKham(PhienKhamID),
    FOREIGN KEY (NhanVienKeDonID) REFERENCES NhanVien(NhanVienID)
);
GO

CREATE TABLE ChiTietToaThuoc (
    ChiTietToaThuocID INT IDENTITY(1,1) PRIMARY KEY,
    ToaThuocID INT NOT NULL,
    ThuocID INT NOT NULL,
    LieuDung NVARCHAR(500),
    SoLuong INT NOT NULL CHECK (SoLuong > 0),

    CONSTRAINT UQ_ToaThuoc_Thuoc UNIQUE (ToaThuocID, ThuocID),

    FOREIGN KEY (ToaThuocID) REFERENCES ToaThuoc(ToaThuocID) ON DELETE CASCADE,
    FOREIGN KEY (ThuocID) REFERENCES Thuoc(ThuocID)
);
GO
--- BỆNH LÝ VÀ CHẨN ĐOÁN ---
CREATE TABLE LoaiBenh (
    LoaiBenhID INT IDENTITY(1,1) PRIMARY KEY,
    TenBenh NVARCHAR(200) NOT NULL UNIQUE,
    TenKhoaHoc NVARCHAR(200) UNIQUE,
    NhomBenh NVARCHAR(100),
    MoTa NVARCHAR(MAX),

    DoPhoBien NVARCHAR(50)
        CHECK (DoPhoBien IN (N'phổ biến', N'ít gặp', N'hiếm')),

    MucDoNghiemTrong NVARCHAR(50)
        CHECK (MucDoNghiemTrong IN (N'nhẹ', N'trung bình', N'nặng')),

    NgayTao DATETIME DEFAULT GETDATE()
);
GO
CREATE TABLE PhienKham_Benh (
    PhienKham_BenhID INT IDENTITY(1,1) PRIMARY KEY,
    PhienKhamID INT NOT NULL,
    LoaiBenhID INT NOT NULL,

    LoaiChanDoan NVARCHAR(50) NOT NULL
        CHECK (LoaiChanDoan IN (
            N'Chẩn đoán chính',
            N'Chẩn đoán phát sinh'
        ))
        DEFAULT N'Chẩn đoán chính',

    GhiChu NVARCHAR(MAX),

    CONSTRAINT UQ_PhienKham_Benh UNIQUE (PhienKhamID, LoaiBenhID),

    CONSTRAINT FK_PhienKhamBenh_PhienKham
        FOREIGN KEY (PhienKhamID)
        REFERENCES PhienKham(PhienKhamID)
        ON DELETE CASCADE,

    CONSTRAINT FK_PhienKhamBenh_LoaiBenh
        FOREIGN KEY (LoaiBenhID)
        REFERENCES LoaiBenh(LoaiBenhID)
);
GO

CREATE UNIQUE INDEX UX_PhienKham_ChanDoanChinh
ON PhienKham_Benh (PhienKhamID)
WHERE LoaiChanDoan = N'Chẩn đoán chính';
GO
--- TÁI KHÁM VÀ LIỆU TRÌNH---
CREATE TABLE TaiKham (
    TaiKhamID INT IDENTITY(1,1) PRIMARY KEY,
    PhienKhamID INT NOT NULL,          -- phiên khám gốc
    BenhNhanID INT NOT NULL,           -- bệnh nhân được yêu cầu tái khám
    NgayDuKien DATE NOT NULL,          -- ngày bác sĩ chỉ định
    LyDo NVARCHAR(500),                -- lý do tái khám
    TrangThai NVARCHAR(50) 
        CHECK (TrangThai IN (N'Chờ khám', N'Đã khám', N'Đã hủy')) 
        DEFAULT N'Chờ khám',
    CaKhamID INT NULL,                 -- nếu đã gán lịch tái khám vào ca khám
    NgayTao DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (PhienKhamID) REFERENCES PhienKham(PhienKhamID) ON DELETE CASCADE,
    FOREIGN KEY (BenhNhanID) REFERENCES BenhNhan(BenhNhanID),
    FOREIGN KEY (CaKhamID) REFERENCES CaKham(CaKhamID),
    CONSTRAINT UQ_TaiKham_PhienKham UNIQUE (PhienKhamID)
);
GO

-- Liệu trình điều trị sau phiên khám --
CREATE TABLE LieuTrinhDieuTri (
    LieuTrinhID INT IDENTITY(1,1) PRIMARY KEY,
    BenhNhanID INT NOT NULL,
    PhienKhamID INT NOT NULL,          -- phiên khám bắt đầu liệu trình
    TenLieuTrinh NVARCHAR(200) NOT NULL, 
    TongSoBuoi INT NOT NULL,           -- ví dụ 4 buổi / 6 buổi
    TrangThai NVARCHAR(50) CHECK (TrangThai IN (N'Đang điều trị', N'Hoàn thành', N'Đã hủy')) DEFAULT N'Đang điều trị',
    GhiChu NVARCHAR(MAX),
    NgayBatDau DATE DEFAULT GETDATE(),
    NgayKetThuc DATE NULL,

    FOREIGN KEY (BenhNhanID) REFERENCES BenhNhan(BenhNhanID),
    FOREIGN KEY (PhienKhamID) REFERENCES PhienKham(PhienKhamID)
);
GO
CREATE UNIQUE INDEX UX_LieuTrinh_DangDieuTri
ON LieuTrinhDieuTri (BenhNhanID)
WHERE TrangThai = N'Đang điều trị';

GO
-- Số buổi cụ thể của bệnh nhân đã tiến hành trong liệu trình --
CREATE TABLE LieuTrinh_BuoiDieuTri (
    BuoiDieuTriID INT IDENTITY(1,1) PRIMARY KEY,
    LieuTrinhID INT NOT NULL,
    CaKhamID INT NOT NULL,
    SoBuoi INT NOT NULL,                       -- buổi số mấy (1,2,3..)
    NgayDuKien DATE,
    NgayThucHien DATE NULL,
    NhanVienID INT NULL,                       -- bác sĩ thực hiện
    TrangThai NVARCHAR(50) CHECK (TrangThai IN (N'Chờ xử lý', N'Đang xử lý', N'Hoàn thành', N'Đã hủy')) DEFAULT N'Chờ xử lý',
    GhiChu NVARCHAR(MAX),
    HinhAnh NVARCHAR(MAX),                 -- ảnh theo dõi liệu trình
    FOREIGN KEY (CaKhamID) REFERENCES CaKham(CaKhamID),
    FOREIGN KEY (LieuTrinhID) REFERENCES LieuTrinhDieuTri(LieuTrinhID) ON DELETE CASCADE,
    FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID),
    CONSTRAINT UQ_LieuTrinh_SoBuoi UNIQUE (LieuTrinhID, SoBuoi),
    CONSTRAINT UQ_BuoiDieuTri_CaKham UNIQUE (CaKhamID)
);
GO

--- KHAC --
CREATE TABLE AI_TrainingFeedback (
    FeedbackID INT IDENTITY(1,1) PRIMARY KEY,
    PhienKhamID INT NOT NULL,

    ImagePath NVARCHAR(300),
    AIPredictionsJSON NVARCHAR(MAX),
    Doctor_FinalDiagnosis NVARCHAR(300),

    ErrorType NVARCHAR(100),
    ErrorScore DECIMAL(5,2) 
        CHECK (ErrorScore BETWEEN 0 AND 100),

    IsUsedForTraining BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

    FOREIGN KEY (PhienKhamID) 
        REFERENCES PhienKham(PhienKhamID) 
        ON DELETE CASCADE
);
GO

CREATE INDEX IX_AI_TrainingFeedback_PhienKham
ON AI_TrainingFeedback (PhienKhamID);
GO
CREATE TABLE BaiViet (
    BaiVietID INT IDENTITY(1,1) PRIMARY KEY,

    TieuDe NVARCHAR(300) NOT NULL,
    TomTat NVARCHAR(500),
    NoiDung NVARCHAR(MAX),
    HinhAnh NVARCHAR(500),

    TacGiaID INT NULL,
    LoaiBenhID INT NULL,

    LuotXem INT NOT NULL DEFAULT 0,

    NgayDang DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    NgayCapNhat DATETIME2 NULL,

    TrangThai NVARCHAR(50) 
        CHECK (TrangThai IN (
            N'Bản nháp',
            N'Đã đăng',
            N'Ẩn',
            N'Lưu trữ'
        )) 
        NOT NULL DEFAULT N'Bản nháp',

    FOREIGN KEY (TacGiaID) 
        REFERENCES TaiKhoan(TaiKhoanID) 
        ON DELETE SET NULL,

    FOREIGN KEY (LoaiBenhID) 
        REFERENCES LoaiBenh(LoaiBenhID)
);
GO
CREATE TABLE RefreshTokens (
    Id INT IDENTITY(1,1) PRIMARY KEY,

    TaiKhoanId INT NOT NULL,

    TokenHash NVARCHAR(500) NOT NULL,
    ExpiryDate DATETIME2 NOT NULL,

    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    IsRevoked BIT NOT NULL DEFAULT 0,

    CONSTRAINT FK_RefreshTokens_TaiKhoan
        FOREIGN KEY (TaiKhoanId)
        REFERENCES TaiKhoan(TaiKhoanID)
        ON DELETE CASCADE
);
--Phanquyen
CREATE TABLE Quyen (
    QuyenID INT IDENTITY(1,1) PRIMARY KEY,
    MaQuyen VARCHAR(100) NOT NULL,
    TenQuyen NVARCHAR(200) NOT NULL,
    Module NVARCHAR(100) NOT NULL
);
ALTER TABLE Quyen
ADD CONSTRAINT UQ_Quyen_MaQuyen UNIQUE (MaQuyen);


CREATE TABLE ChucVu_Quyen (
    ChucVuID INT NOT NULL,
    QuyenID INT NOT NULL,

    PRIMARY KEY (ChucVuID, QuyenID),

    CONSTRAINT FK_ChucVuQuyen_ChucVu
        FOREIGN KEY (ChucVuID)
        REFERENCES ChucVu(ChucVuID)
        ON DELETE CASCADE,

    CONSTRAINT FK_ChucVuQuyen_Quyen
        FOREIGN KEY (QuyenID)
        REFERENCES Quyen(QuyenID)
        ON DELETE CASCADE
);