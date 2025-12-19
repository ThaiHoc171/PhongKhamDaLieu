--- ChucVu ---
INSERT INTO ChucVu (TenChucVu, MoTa) VALUES
(N'Bac si tư vấn', N'Chiu trach nhiem kham'),
(N'Bac si điều trị', N'Chiu trach nhiem dieu tri'),
(N'Y tá', N'Ho tro cham soc benh nhan'),
(N'Ky thuat vien', N'Thuc hien cac ky thuat hinh anh, xet nghiem'),
(N'Le tan', N'Tiep nhan benh nhan va sap xep lich hen');
GO

--- TaiKhoan ---
INSERT INTO TaiKhoan (Email, PasswordHash, Role) VALUES
-- 11 nhân viên
('dr.lamminh@example.com','hash123','employee'),
('dr.hoangphuc@example.com','hash123','employee'),
('dr.trananhkhoa@example.com','hash123','employee'),
('dr.minhtuan@example.com','hash123','employee'),
('yt.lethuhien@example.com','hash123','employee'),
('yt.trangmy@example.com','hash123','employee'),
('yt.bichthao@example.com','hash123','employee'),
('ktv.phamquang@example.com','hash123','employee'),
('ktv.dinhphuong@example.com','hash123','employee'),
('ktv.vominhtri@example.com','hash123','employee'),
('lt.khanhlinh@example.com','hash123','employee'),
('lt.haithanh@example.com','hash123','employee'),

-- 29 bệnh nhân có tài khoản
('alice.vo@example.com','hash123','customer'),
('minhduong@example.com','hash123','customer'),
('phamhoailinh@example.com','hash123','customer'),
('lethanhdat@example.com','hash123','customer'),
('truonganhtu@example.com','hash123','customer'),
('dangthuphuong@example.com','hash123','customer'),
('hoanglonglam@example.com','hash123','customer'),
('truclan98@example.com','hash123','customer'),
('kimyen03@example.com','hash123','customer'),
('anhthanh87@example.com','hash123','customer'),
('phamhaidang@example.com','hash123','customer'),
('huuky@example.com','hash123','customer'),
('lananhpham@example.com','hash123','customer'),
('nhatnam07@example.com','hash123','customer'),
('truonggiakhang@example.com','hash123','customer'),
('thanhnhu99@example.com','hash123','customer'),
('vuongkimanh@example.com','hash123','customer'),
('quanghuy92@example.com','hash123','customer'),
('tientran85@example.com','hash123','customer'),
('dungpham97@example.com','hash123','customer'),
('hoailanpham@example.com','hash123','customer'),
('linhchi03@example.com','hash123','customer'),
('hoangphuong94@example.com','hash123','customer'),
('kienhung90@example.com','hash123','customer'),
('thanhvo88@example.com','hash123','customer'),
('huephan02@example.com','hash123','customer'),
('tuyetnhung@example.com','hash123','customer'),
('thienminh04@example.com','hash123','customer'),
('giahan99@example.com','hash123','customer'),
('admin@clinic.com', 'hash123', 'Admin');
GO
--- ThongTinCaNhan ---
-- 11 nhân viên
INSERT INTO ThongTinCaNhan (TaiKhoanID, HoTen, NgaySinh, GioiTinh, SDT, EmailLienHe, DiaChi, Loai)
VALUES
(1, N'Lâm Minh Đức', '1980-03-12', 'Nam', '0901002001', 'dr.lamminh@example.com', N'Q1, TP.HCM', 'nhanvien'),
(2, N'Hoàng Phúc Lợi', '1983-06-22', 'Nam', '0901002002', 'dr.hoangphuc@example.com', N'Q3, TP.HCM', 'nhanvien'),
(3, N'Trần Anh Khoa', '1979-11-09', 'Nam', '0901002003', 'dr.trananhkhoa@example.com', N'Q5, TP.HCM', 'nhanvien'),
(4, N'Minh Tuấn', '1984-02-14', 'Nam', '0901002004', 'dr.minhtuan@example.com', N'Q7, TP.HCM', 'nhanvien'),

(5, N'Lê Thu Hiền', '1990-01-18', 'Nu', '0902003001', 'yt.lethuhien@example.com', N'Q10, TP.HCM', 'nhanvien'),
(6, N'Trăng Mỹ', '1993-04-27', 'Nu', '0902003002', 'yt.trangmy@example.com', N'Q7, TP.HCM', 'nhanvien'),
(7, N'Bích Thảo', '1989-07-10', 'Nu', '0902003003', 'yt.bichthao@example.com', N'Q4, TP.HCM', 'nhanvien'),

(8, N'Phạm Quang Tú', '1991-08-15', 'Nam', '0903004001', 'ktv.phamquang@example.com', N'Tân Bình, TP.HCM', 'nhanvien'),
(9, N'Đinh Phương Hà', '1992-03-29', 'Nu', '0903004002', 'ktv.dinhphuong@example.com', N'Bình Thạnh, TP.HCM', 'nhanvien'),
(10, N'Võ Minh Trí', '1994-05-11', 'Nam', '0903004003', 'ktv.vominhtri@example.com', N'Phú Nhuận, TP.HCM', 'nhanvien'),

(11, N'Khánh Linh', '1995-09-17', 'Nu', '0904005001', 'lt.khanhlinh@example.com', N'Q8, TP.HCM', 'nhanvien'),
(12, N'Hải Thanh', '1996-12-23', 'Nam', '0904005002', 'lt.haithanh@example.com', N'Q2, TP.HCM', 'nhanvien');
GO


-- 30 bệnh nhân
INSERT INTO ThongTinCaNhan (TaiKhoanID, HoTen, NgaySinh, GioiTinh, SDT, EmailLienHe, DiaChi, Loai) VALUES
(13, N'Dương Minh Tùng', '1988-02-12', 'Nam', '0915001002', 'minhduong@example.com', N'Q6, TP.HCM', 'benhnhan'),
(14, N'Phạm Hoài Linh', '1999-07-22', 'Nu', '0915001003', 'phamhoailinh@example.com', N'Q7, TP.HCM', 'benhnhan'),
(15, N'Lê Thành Đạt', '1985-11-16', 'Nam', '0915001004', 'lethanhdat@example.com', N'Q8, TP.HCM', 'benhnhan'),
(16, N'Truơng An Tú', '1990-05-19', 'Nam', '0915001005', 'truonganhtu@example.com', N'Tân Phú', 'benhnhan'),
(17, N'Đặng Thu Phương', '1994-09-13', 'Nu', '0915001006', 'dangthuphuong@example.com', N'Q10', 'benhnhan'),
(18, N'Hoàng Long Lâm', '2001-03-21', 'Nam', '0915001007', 'hoanglonglam@example.com', N'Q3', 'benhnhan'),
(19, N'Trúc Lan', '1998-10-01', 'Nu', '0915001008', 'truclan98@example.com', N'Q1', 'benhnhan'),
(20, N'Kim Yến', '2003-06-25', 'Nu', '0915001009', 'kimyen03@example.com', N'Q2', 'benhnhan'),
(21, N'Anh Thành', '1987-08-12', 'Nam', '0915001010', 'anhthanh87@example.com', N'Q11', 'benhnhan'),
(22, N'Phạm Hải Đăng', '1991-01-14', 'Nam', '0915001011', 'phamhaidang@example.com', N'Q12', 'benhnhan'),
(23, N'Hữu Kỳ', '1993-03-07', 'Nam', '0915001012', 'huuky@example.com', N'Q9', 'benhnhan'),
(24, N'Lân Anh Phạm', '1995-12-04', 'Nu', '0915001013', 'lananhpham@example.com', N'Q7', 'benhnhan'),
(25, N'Nhật Nam', '2000-04-22', 'Nam', '0915001014', 'nhatnam07@example.com', N'Q2', 'benhnhan'),
(26, N'Trương Gia Khang', '2010-09-30', 'Nam', '0915001015', 'truonggiakhang@example.com', N'Q6', 'benhnhan'),
(27, N'Thanh Như', '1999-12-17', 'Nu', '0915001016', 'thanhnhu99@example.com', N'Q8', 'benhnhan'),
(28, N'Vương Kim Anh', '1996-01-25', 'Nu', '0915001017', 'vuongkimanh@example.com', N'Q9', 'benhnhan'),
(29, N'Quang Huy', '1992-06-14', 'Nam', '0915001018', 'quanghuy92@example.com', N'Q10', 'benhnhan'),
(30, N'Tiến Trần', '1985-03-20', 'Nam', '0915001019', 'tientran85@example.com', N'Q3', 'benhnhan'),
(31, N'Dũng Phạm', '1997-11-09', 'Nam', '0915001020', 'dungpham97@example.com', N'Q4', 'benhnhan'),
(32, N'Hoài Lan Phạm', '1994-04-04', 'Nu', '0915001021', 'hoailanpham@example.com', N'Q7', 'benhnhan'),
(33, N'Linh Chi', '2003-05-11', 'Nu', '0915001022', 'linhchi03@example.com', N'Q5', 'benhnhan'),
(34, N'Hoàng Phương', '1994-11-06', 'Nu', '0915001023', 'hoangphuong94@example.com', N'Q8', 'benhnhan'),
(35, N'Kiến Hưng', '1990-02-03', 'Nam', '0915001024', 'kienhung90@example.com', N'Q10', 'benhnhan'),
(36, N'Thanh Võ', '1988-07-12', 'Nam', '0915001025', 'thanhvo88@example.com', N'Q11', 'benhnhan'),
(37, N'Huệ Phan', '2002-08-30', 'Nu', '0915001026', 'huephan02@example.com', N'Q12', 'benhnhan'),
(38, N'Tuyết Nhung', '1993-09-27', 'Nu', '0915001027', 'tuyetnhung@example.com', N'Q4', 'benhnhan'),
(39, N'Thiện Minh', '2004-03-15', 'Nam', '0915001028', 'thienminh04@example.com', N'Q5', 'benhnhan'),
(40, N'Gia Hân', '1999-06-12', 'Nu', '0915001029', 'giahan99@example.com', N'Q6', 'benhnhan'),
(41,N'Quản trị hệ thống','1990-01-01','Nam','0999000000','admin@clinic.com',N'TP.HCM','nhanvien');
GO
--- BenhNhan ---
INSERT INTO BenhNhan (ThongTinID, MaSoBN) VALUES
(13,'BN0002'), (14,'BN0003'), (15,'BN0004'), (16,'BN0005'),
(17, 'BN0006'), (18,'BN0007'), (19,'BN0008'), (20,'BN0009'), (21,'BN0010'),
(22, 'BN0011'), (23,'BN0012'), (24,'BN0013'), (25,'BN0014'), (26,'BN0015'),
(27, 'BN0016'), (28,'BN0017'), (29,'BN0018'), (30,'BN0019'), (31,'BN0020'),
(32, 'BN0021'), (33,'BN0022'), (34,'BN0023'), (35,'BN0024'), (36,'BN0025'),
(37, 'BN0026'), (38,'BN0027'), (39,'BN0028'), (40,'BN0029');
GO


--- NhanVien ---
INSERT INTO NhanVien (ThongTinID, ChucVuID, Luong, NgayVaoLam, BangCap, KinhNghiem)
VALUES
-- 4 bác sĩ (2 tư vấn, 2 điều trị)
(1, 1, 30000000, '2015-02-01', N'ĐH Y Dược', N'10 năm kinh nghiệm da liễu'),
(2, 1, 29000000, '2017-04-01', N'ĐH Y Hà Nội', N'8 năm kinh nghiệm da liễu'),

(3, 2, 31000000, '2013-09-15', N'ĐH Y Huế', N'12 năm kinh nghiệm bệnh da'),
(4, 2, 29500000, '2016-08-10', N'ĐH Y Hải Phòng', N'7 năm điều trị da liễu'),

-- 3 y tá
(5, 3, 15000000, '2019-07-11', N'CĐ Điều dưỡng', N'5 năm điều dưỡng'),
(6, 3, 16000000, '2020-03-22', N'CĐ Điều dưỡng', N'4 năm điều dưỡng'),
(7, 3, 15500000, '2021-10-18', N'CĐ Điều dưỡng', N'3 năm điều dưỡng'),

-- 3 kỹ thuật viên
(8, 4, 17000000, '2018-05-01', N'KTV xét nghiệm', N'6 năm kinh nghiệm'),
(9, 4, 16500000, '2019-08-09', N'KTV hình ảnh', N'5 năm kinh nghiệm'),
(10, 4, 16800000, '2020-02-14', N'KTV da liễu', N'4 năm kinh nghiệm'),

-- 2 lễ tân
(11, 5, 12000000, '2021-11-02', N'THPT', N'2 năm lễ tân'),
(12, 5, 12500000, '2022-06-16', N'THPT', N'1 năm lễ tân');
GO


--- BacSi ---
INSERT INTO BacSiProfile (NhanVienID, GioiThieu, ChuyenMon, ThanhTuu, HinhAnh, KinhNghiem)
VALUES
(1, N'Bác sĩ chuyên khoa da liễu.', N'Da liễu tổng quát', N'Nhiều bài báo khoa học', N'bs_lamminh.jpg', N'Hơn 10 năm kinh nghiệm'),
(2, N'Bác sĩ da liễu chuyên trị mụn và sẹo.', N'Mụn - Sẹo - Laser', N'Giải thưởng bác sĩ trẻ giỏi', N'bs_hoangphuc.jpg', N'8 năm kinh nghiệm'),
(3, N'Chuyên gia điều trị bệnh da mãn tính.', N'Viêm da - Vảy nến', N'Nghiên cứu chuyên sâu', N'bs_trananhkhoa.jpg', N'12 năm kinh nghiệm'),
(4, N'Bác sĩ điều trị thẩm mỹ da.', N'Laser - Nội khoa da liễu', N'Nhiều ca điều trị thành công', N'bs_minhtuan.jpg', N'7 năm kinh nghiệm');
GO

