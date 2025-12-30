select * from ChucVu
select * from TaiKhoan
select * from ThongTinCaNhan
select * from BenhNhan
select * from NhanVien
--- ChucVu ---
INSERT INTO ChucVu (TenChucVu, MoTa) VALUES
(N'Bac si', N'Chiu trach nhiem kham benh'),
(N'Y tá', N'Ho tro cham soc benh nhan'),
(N'Ky thuat vien', N'Thuc hien cac ky thuat hinh anh, xet nghiem'),
(N'Le tan', N'Tiep nhan benh nhan va sap xep lich hen');
GO
--- TaiKhoan ---
INSERT INTO TaiKhoan (Email, MatKhau, VaiTro) VALUES
('admin@clinic.com', 'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', 'Admin'), --1 admin
('dr.lamminh@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Nhân viên'),
('dr.hoangphuc@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Nhân viên'),
('yt.lethuhien@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Nhân viên'),
('yt.trangmy@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Nhân viên'),
('yt.bichthao@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Nhân viên'),
('ktv.phamquang@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Nhân viên'),
('ktv.dinhphuong@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Nhân viên'),
('lt.khanhlinh@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Nhân viên'),
('lt.haithanh@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Nhân viên'),

-- 40 bệnh nhân có tài khoản
('alice.vo@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('minhduong@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('phamhoailinh@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('lethanhdat@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('truonganhtu@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('dangthuphuong@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('hoanglonglam@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('truclan98@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('kimyen03@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('anhthanh87@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('phamhaidang@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('huuky@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('lananhpham@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('nhatnam07@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('truonggiakhang@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('thanhnhu99@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('vuongkimanh@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('quanghuy92@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('tientran85@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('dungpham97@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('hoailanpham@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('linhchi03@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('hoangphuong94@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('kienhung90@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('thanhvo88@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('huephan02@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('tuyetnhung@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('thienminh04@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('giahan99@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('ngocanh91@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('hoangson86@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('myduyen95@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('quynhtrang89@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('ducthang93@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('anhkhoa90@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('phuonglinh96@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('minhquan88@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('thanhtruc92@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('hoainam84@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân'),
('nguyetminh97@example.com','jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=',N'Bệnh nhân');
GO
--- ThongTinCaNhan ---
-- 11 nhân viên
INSERT INTO ThongTinCaNhan (TaiKhoanID, HoTen, NgaySinh, GioiTinh, SDT, EmailLienHe, DiaChi, Loai)
VALUES
(1, N'Quản trị hệ thống', '1990-01-01', N'Nam', '0999000000', 'admin@clinic.com', N'TP.HCM', N'Nhân viên'),
(2, N'Lâm Minh Đức', '1980-03-12', N'Nam', '0901002001', 'dr.lamminh@example.com', N'Q1, TP.HCM', N'Nhân viên'),
(3, N'Hoàng Phúc Lợi', '1983-06-22', N'Nam', '0901002002', 'dr.hoangphuc@example.com', N'Q3, TP.HCM', N'Nhân viên'),
(4, N'Lê Thu Hiền', '1990-01-18', N'Nữ', '0902003001', 'yt.lethuhien@example.com', N'Q10, TP.HCM', N'Nhân viên'),
(5, N'Trăng Mỹ', '1993-04-27', N'Nữ', '0902003002', 'yt.trangmy@example.com', N'Q7, TP.HCM', N'Nhân viên'),
(6, N'Bích Thảo', '1989-07-10', N'Nữ', '0902003003', 'yt.bichthao@example.com', N'Q4, TP.HCM', N'Nhân viên'),
(7, N'Phạm Quang Tú', '1991-08-15', N'Nam', '0903004001', 'ktv.phamquang@example.com', N'Tân Bình, TP.HCM', N'Nhân viên'),
(8, N'Đinh Phương Hà', '1992-03-29', N'Nữ', '0903004002', 'ktv.dinhphuong@example.com', N'Bình Thạnh, TP.HCM', N'Nhân viên'),
(9, N'Khánh Linh', '1995-09-17', N'Nữ', '0904005001', 'lt.khanhlinh@example.com', N'Q8, TP.HCM', N'Nhân viên'),
(10, N'Hải Thanh', '1996-12-23', N'Nam', '0904005002', 'lt.haithanh@example.com', N'Q2, TP.HCM', N'Nhân viên');
GO


-- 30 bệnh nhân
INSERT INTO ThongTinCaNhan
(TaiKhoanID, HoTen, NgaySinh, GioiTinh, SDT, EmailLienHe, DiaChi, Loai)
VALUES
(11, N'Dương Minh Tùng', '1988-02-12', N'Nam', '0915001002', 'minhduong@example.com', N'Q6, TP.HCM', N'Bệnh nhân'),
(12, N'Phạm Hoài Linh', '1999-07-22', N'Nữ', '0915001003', 'phamhoailinh@example.com', N'Q7, TP.HCM', N'Bệnh nhân'),
(13, N'Lê Thành Đạt', '1985-11-16', N'Nam', '0915001004', 'lethanhdat@example.com', N'Q8, TP.HCM', N'Bệnh nhân'),
(14, N'Truơng An Tú', '1990-05-19', N'Nam', '0915001005', 'truonganhtu@example.com', N'Tân Phú', N'Bệnh nhân'),
(15, N'Đặng Thu Phương', '1994-09-13', N'Nữ', '0915001006', 'dangthuphuong@example.com', N'Q10', N'Bệnh nhân'),
(16, N'Hoàng Long Lâm', '2001-03-21', N'Nam', '0915001007', 'hoanglonglam@example.com', N'Q3', N'Bệnh nhân'),
(17, N'Trúc Lan', '1998-10-01', N'Nữ', '0915001008', 'truclan98@example.com', N'Q1', N'Bệnh nhân'),
(18, N'Kim Yến', '2003-06-25', N'Nữ', '0915001009', 'kimyen03@example.com', N'Q2', N'Bệnh nhân'),
(19, N'Anh Thành', '1987-08-12', N'Nam', '0915001010', 'anhthanh87@example.com', N'Q11', N'Bệnh nhân'),
(20, N'Phạm Hải Đăng', '1991-01-14', N'Nam', '0915001011', 'phamhaidang@example.com', N'Q12', N'Bệnh nhân'),
(21, N'Hữu Kỳ', '1993-03-07', N'Nam', '0915001012', 'huuky@example.com', N'Q9', N'Bệnh nhân'),
(22, N'Lân Anh Phạm', '1995-12-04', N'Nữ', '0915001013', 'lananhpham@example.com', N'Q7', N'Bệnh nhân'),
(23, N'Nhật Nam', '2000-04-22', N'Nam', '0915001014', 'nhatnam07@example.com', N'Q2', N'Bệnh nhân'),
(24, N'Trương Gia Khang', '2010-09-30', N'Nam', '0915001015', 'truonggiakhang@example.com', N'Q6', N'Bệnh nhân'),
(25, N'Thanh Như', '1999-12-17', N'Nữ', '0915001016', 'thanhnhu99@example.com', N'Q8', N'Bệnh nhân'),
(26, N'Vương Kim Anh', '1996-01-25', N'Nữ', '0915001017', 'vuongkimanh@example.com', N'Q9', N'Bệnh nhân'),
(27, N'Quang Huy', '1992-06-14', N'Nam', '0915001018', 'quanghuy92@example.com', N'Q10', N'Bệnh nhân'),
(28, N'Tiến Trần', '1985-03-20', N'Nam', '0915001019', 'tientran85@example.com', N'Q3', N'Bệnh nhân'),
(29, N'Dũng Phạm', '1997-11-09', N'Nam', '0915001020', 'dungpham97@example.com', N'Q4', N'Bệnh nhân'),
(30, N'Hoài Lan Phạm', '1994-04-04', N'Nữ', '0915001021', 'hoailanpham@example.com', N'Q7', N'Bệnh nhân'),
(31, N'Linh Chi', '2003-05-11', N'Nữ', '0915001022', 'linhchi03@example.com', N'Q5', N'Bệnh nhân'),
(32, N'Hoàng Phương', '1994-11-06', N'Nữ', '0915001023', 'hoangphuong94@example.com', N'Q8', N'Bệnh nhân'),
(33, N'Kiến Hưng', '1990-02-03', N'Nam', '0915001024', 'kienhung90@example.com', N'Q10', N'Bệnh nhân'),
(34, N'Thanh Võ', '1988-07-12', N'Nam', '0915001025', 'thanhvo88@example.com', N'Q11', N'Bệnh nhân'),
(35, N'Huệ Phan', '2002-08-30', N'Nữ', '0915001026', 'huephan02@example.com', N'Q12', N'Bệnh nhân'),
(36, N'Tuyết Nhung', '1993-09-27', N'Nữ', '0915001027', 'tuyetnhung@example.com', N'Q4', N'Bệnh nhân'),
(37, N'Thiện Minh', '2004-03-15', N'Nam', '0915001028', 'thienminh04@example.com', N'Q5', N'Bệnh nhân'),
(38, N'Gia Hân', '1999-06-12', N'Nữ', '0915001029', 'giahan99@example.com', N'Q6', N'Bệnh nhân'),
(39, N'Ngọc Anh', '1991-02-18', N'Nữ', '0915001030', 'ngocanh91@example.com', N'Q1, TP.HCM', N'Bệnh nhân'),
(40, N'Hoàng Sơn', '1986-07-09', N'Nam', '0915001031', 'hoangson86@example.com', N'Q3, TP.HCM', N'Bệnh nhân'),
(41, N'Mỹ Duyên', '1995-10-21', N'Nữ', '0915001032', 'myduyen95@example.com', N'Q7, TP.HCM', N'Bệnh nhân'),
(42, N'Quỳnh Trang', '1989-12-05', N'Nữ', '0915001033', 'quynhtrang89@example.com', N'Q10, TP.HCM', N'Bệnh nhân'),
(43, N'Đức Thắng', '1993-04-14', N'Nam', '0915001034', 'ducthang93@example.com', N'Q11, TP.HCM', N'Bệnh nhân'),
(44, N'Anh Khoa', '1990-09-02', N'Nam', '0915001035', 'anhkhoa90@example.com', N'Q5, TP.HCM', N'Bệnh nhân'),
(45, N'Phương Linh', '1996-06-26', N'Nữ', '0915001036', 'phuonglinh96@example.com', N'Q8, TP.HCM', N'Bệnh nhân'),
(46, N'Minh Quân', '1988-03-11', N'Nam', '0915001037', 'minhquan88@example.com', N'Q6, TP.HCM', N'Bệnh nhân'),
(47, N'Thanh Trúc', '1992-11-19', N'Nữ', '0915001038', 'thanhtruc92@example.com', N'Q4, TP.HCM', N'Bệnh nhân'),
(48, N'Hoài Nam', '1984-01-28', N'Nam', '0915001039', 'hoainam84@example.com', N'Q9, TP.HCM', N'Bệnh nhân'),
(49, N'Nguyệt Minh', '1997-08-07', N'Nữ', '0915001040', 'nguyetminh97@example.com', N'Q12, TP.HCM', N'Bệnh nhân');
GO

--BenhNhan--
INSERT INTO BenhNhan (ThongTinID, LoaiDa, GhiChu) VALUES
(11, N'Da dầu',        NULL), (12, N'Da khô',        NULL),
(13, N'Da hỗn hợp',    NULL), (14, N'Da dầu',        NULL),
(15, N'Da nhạy cảm',   NULL), (16, N'Da dầu',        NULL),
(17, N'Da khô',        NULL), (18, N'Da hỗn hợp',    NULL),
(19, N'Da dầu',        NULL), (20, N'Da nhạy cảm',   NULL),
(21, N'Da dầu',        NULL), (22, N'Da khô',        NULL),
(23, N'Da hỗn hợp',    NULL), (24, N'Da dầu',        NULL),
(25, N'Da nhạy cảm',   NULL), (26, N'Da khô',        NULL),
(27, N'Da dầu',        NULL), (28, N'Da hỗn hợp',    NULL),
(29, N'Da dầu',        NULL), (30, N'Da nhạy cảm',   NULL),
(31, N'Da khô',        NULL), (32, N'Da dầu',        NULL),
(33, N'Da hỗn hợp',    NULL), (34, N'Da dầu',        NULL),
(35, N'Da nhạy cảm',   NULL), (36, N'Da khô',        NULL),
(37, N'Da dầu',        NULL), (38, N'Da hỗn hợp',    NULL),
(39, N'Da dầu',        NULL), (40, N'Da nhạy cảm',   NULL),
(41, N'Da khô',        NULL), (42, N'Da dầu',        NULL),
(43, N'Da hỗn hợp',    NULL), (44, N'Da dầu',        NULL),
(45, N'Da nhạy cảm',   NULL), (46, N'Da khô',        NULL),
(47, N'Da dầu',        NULL), (48, N'Da hỗn hợp',    NULL),
(49, N'Da dầu',        NULL);
GO



--- NhanVien ---
INSERT INTO NhanVien (ThongTinID, ChucVuID, Luong, NgayVaoLam, BangCap, KinhNghiem)
VALUES
-- 2 bác sĩ 
(2, 1, 30000000, '2015-02-01', N'ĐH Y Dược', N'10 năm kinh nghiệm da liễu'),
(3, 1, 29000000, '2017-04-01', N'ĐH Y Hà Nội', N'8 năm kinh nghiệm da liễu'),
-- 3 y tá
(4, 2, 15000000, '2019-07-11', N'CĐ Điều dưỡng', N'5 năm điều dưỡng'),
(5, 2, 16000000, '2020-03-22', N'CĐ Điều dưỡng', N'4 năm điều dưỡng'),
(6, 2, 15500000, '2021-10-18', N'CĐ Điều dưỡng', N'3 năm điều dưỡng'),

-- 3 kỹ thuật viên
(7, 3, 17000000, '2018-05-01', N'KTV xét nghiệm', N'6 năm kinh nghiệm'),
(8, 3, 16500000, '2019-08-09', N'KTV hình ảnh', N'5 năm kinh nghiệm'),

-- 2 lễ tân
(9, 4, 12000000, '2021-11-02', N'THPT', N'2 năm lễ tân'),
(10, 4, 12500000, '2022-06-16', N'THPT', N'1 năm lễ tân');
GO


--- BacSi ---
INSERT INTO BacSiProfile (NhanVienID, GioiThieu, ChuyenMon, ThanhTuu, HinhAnh, KinhNghiem)
VALUES
(1, N'Bác sĩ chuyên khoa da liễu.', N'Da liễu tổng quát', N'Nhiều bài báo khoa học', N'bs_lamminh.jpg', N'Hơn 10 năm kinh nghiệm'),
(2, N'Bác sĩ da liễu chuyên trị mụn và sẹo.', N'Mụn - Sẹo - Laser', N'Giải thưởng bác sĩ trẻ giỏi', N'bs_hoangphuc.jpg', N'8 năm kinh nghiệm');
GO
