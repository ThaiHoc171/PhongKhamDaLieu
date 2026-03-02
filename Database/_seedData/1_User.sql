
--- Chức Vụ ---
INSERT INTO ChucVu (TenChucVu, MoTa) VALUES
(N'Bác sĩ khám bệnh', N'Khám và chẩn đoán ban đầu'),
(N'Bác sĩ điều trị', N'Thực hiện điều trị theo phác đồ'),
(N'Y tá', N'Hỗ trợ bác sĩ trong điều trị'),
(N'Kỹ thuật viên', N'Xét nghiệm, chẩn đoán cận lâm sàng'),
(N'Lễ tân', N'Tiếp nhận và sắp xếp lịch hẹn');

INSERT INTO TaiKhoan (Email, MatKhau, VaiTro)
VALUES
-- Admin
(N'admin@clinic.com', 'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Admin'),

-- Nhân viên (13)
(N'duc.lamminh@clinic.com',        'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Nhân viên'),
(N'loi.hoangphuc@clinic.com',      'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Nhân viên'),
(N'tran.nguyenthibao@clinic.com',  'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Nhân viên'),
(N'khanh.tranquoc@clinic.com',     'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Nhân viên'),
(N'hien.lethu@clinic.com',         'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Nhân viên'),
(N'my.phamthitrang@clinic.com',    'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Nhân viên'),
(N'thao.nguyenthibich@clinic.com', 'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Nhân viên'),
(N'tam.vominh@clinic.com',         'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Nhân viên'),
(N'tu.phamquang@clinic.com',       'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Nhân viên'),
(N'ha.dinhphuong@clinic.com',      'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Nhân viên'),
(N'thanh.ngoduc@clinic.com',       'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Nhân viên'),
(N'linh.nguyenkhanh@clinic.com',   'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Nhân viên'),
(N'thanh.phamhai@clinic.com',      'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Nhân viên'),

-- Bệnh nhân (30)
(N'minhduong@example.com',         'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'phamhoailinh@example.com',      'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'lethanhdat@example.com',        'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'truonganhtu@example.com',       'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'dangthuphuong@example.com',     'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'hoanglonglam@example.com',      'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'truclan98@example.com',         'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'kimyen03@example.com',          'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'anhthanh87@example.com',        'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'phamhaidang@example.com',       'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'huuky@example.com',             'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'lananhpham@example.com',        'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'nhatnam07@example.com',         'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'truonggiakhang@example.com',    'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'thanhnhu99@example.com',        'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'vuongkimanh@example.com',       'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'quanghuy92@example.com',        'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'tientran85@example.com',        'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'dungpham97@example.com',        'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'hoailanpham@example.com',       'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'linhchi03@example.com',         'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'hoangphuong94@example.com',     'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'kienhung90@example.com',        'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'thanhvo88@example.com',         'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'huephan02@example.com',         'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'tuyetnhung@example.com',        'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'thienminh04@example.com',       'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'giahan99@example.com',          'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'ngocanh91@example.com',         'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân'),
(N'hoangson86@example.com',        'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', N'Bệnh nhân');

-- Admin
INSERT INTO ThongTinCaNhan (TaiKhoanID, HoTen, NgaySinh, GioiTinh, SDT, EmailLienHe, DiaChi, Loai)
VALUES
(1, N'Admin', '2000-01-01', N'Nam', '0909000001', 'admin@clinic.com', N'TP.HCM', N'Nhân viên'),

-- Nhân viên
(2, N'Lâm Minh Đức', '1980-03-12', N'Nam', '0901002001', 'duc.lamminh@clinic.com', N'Quận 1, TP.HCM', N'Nhân viên'),
(3, N'Hoàng Phúc Lợi', '1983-06-22', N'Nam', '0901002002', 'loi.hoangphuc@clinic.com', N'Quận 3, TP.HCM', N'Nhân viên'),

-- Bác sĩ điều trị
(4, N'Nguyễn Thị Bảo Trân', '1979-11-08', N'Nữ', '0901002003', 'tran.nguyenthibao@clinic.com', N'Quận 7, TP.HCM', N'Nhân viên'),
(5, N'Trần Quốc Khánh', '1982-04-19', N'Nam', '0901002004', 'khanh.tranquoc@clinic.com', N'Quận 5, TP.HCM', N'Nhân viên'),

-- Y tá
(6, N'Lê Thu Hiền', '1990-01-18', N'Nữ', '0902003001', 'hien.lethu@clinic.com', N'Quận 10, TP.HCM', N'Nhân viên'),
(7, N'Phạm Thị Trang My', '1993-04-27', N'Nữ', '0902003002', 'my.phamthitrang@clinic.com', N'Quận 7, TP.HCM', N'Nhân viên'),
(8, N'Nguyễn Thị Bích Thảo', '1989-07-10', N'Nữ', '0902003003', 'thao.nguyenthibich@clinic.com', N'Quận 4, TP.HCM', N'Nhân viên'),
(9, N'Võ Minh Tâm', '1995-09-05', N'Nữ', '0902003004', 'tam.vominh@clinic.com', N'Quận 8, TP.HCM', N'Nhân viên'),

-- Kỹ thuật viên
(10, N'Phạm Quang Tú', '1991-08-15', N'Nam', '0903004001', 'tu.phamquang@clinic.com', N'Tân Bình, TP.HCM', N'Nhân viên'),
(11, N'Đinh Phương Hà', '1992-03-29', N'Nữ', '0903004002', 'ha.dinhphuong@clinic.com', N'Bình Thạnh, TP.HCM', N'Nhân viên'),
(12, N'Ngô Đức Thành', '1988-12-02', N'Nam', '0903004003', 'thanh.ngoduc@clinic.com', N'Quận 6, TP.HCM', N'Nhân viên'),

-- Lễ tân
(13, N'Nguyễn Khánh Linh', '1996-09-17', N'Nữ', '0904005001', 'linh.nguyenkhanh@clinic.com', N'Quận 8, TP.HCM', N'Nhân viên'),
(14, N'Phạm Hải Thanh', '1997-12-23', N'Nam', '0904005002', 'thanh.phamhai@clinic.com', N'Quận 2, TP.HCM', N'Nhân viên');


INSERT INTO ThongTinCaNhan
(TaiKhoanID, HoTen, NgaySinh, GioiTinh, SDT, EmailLienHe, DiaChi, Loai)
VALUES
(15, N'Dương Minh Tùng', '1988-02-12', N'Nam', '0915001002', 'minhduong@example.com', N'Quận 6, TP.HCM', N'Bệnh nhân'),
(16, N'Phạm Hoài Linh', '1999-07-22', N'Nữ', '0915001003', 'phamhoailinh@example.com', N'Quận 7, TP.HCM', N'Bệnh nhân'),
(17, N'Lê Thành Đạt', '1985-11-16', N'Nam', '0915001004', 'lethanhdat@example.com', N'Quận 8, TP.HCM', N'Bệnh nhân'),
(18, N'Trương An Tú', '1990-05-19', N'Nam', '0915001005', 'truonganhtu@example.com', N'Quận Tân Phú, TP.HCM', N'Bệnh nhân'),
(19, N'Đặng Thu Phương', '1994-09-13', N'Nữ', '0915001006', 'dangthuphuong@example.com', N'Quận 10, TP.HCM', N'Bệnh nhân'),
(20, N'Hoàng Long Lâm', '2001-03-21', N'Nam', '0915001007', 'hoanglonglam@example.com', N'Quận 3, TP.HCM', N'Bệnh nhân'),
(21, N'Trúc Lan', '1998-10-01', N'Nữ', '0915001008', 'truclan98@example.com', N'Quận 1, TP.HCM', N'Bệnh nhân'),
(22, N'Kim Yến', '2003-06-25', N'Nữ', '0915001009', 'kimyen03@example.com', N'Quận 2, TP.HCM', N'Bệnh nhân'),
(23, N'Anh Thành', '1987-08-12', N'Nam', '0915001010', 'anhthanh87@example.com', N'Quận 11, TP.HCM', N'Bệnh nhân'),
(24, N'Phạm Hải Đăng', '1991-01-14', N'Nam', '0915001011', 'phamhaidang@example.com', N'Quận 12, TP.HCM', N'Bệnh nhân'),
(25, N'Hữu Kỳ', '1993-03-07', N'Nam', '0915001012', 'huuky@example.com', N'Quận 9, TP.HCM', N'Bệnh nhân'),
(26, N'Lân Anh Phạm', '1995-12-04', N'Nữ', '0915001013', 'lananhpham@example.com', N'Quận 7, TP.HCM', N'Bệnh nhân'),
(27, N'Nhật Nam', '2000-04-22', N'Nam', '0915001014', 'nhatnam07@example.com', N'Quận 2, TP.HCM', N'Bệnh nhân'),
(28, N'Trương Gia Khang', '2010-09-30', N'Nam', '0915001015', 'truonggiakhang@example.com', N'Quận 6, TP.HCM', N'Bệnh nhân'),
(29, N'Thanh Như', '1999-12-17', N'Nữ', '0915001016', 'thanhnhu99@example.com', N'Quận 8, TP.HCM', N'Bệnh nhân'),
(30, N'Vương Kim Anh', '1996-01-25', N'Nữ', '0915001017', 'vuongkimanh@example.com', N'Quận 9, TP.HCM', N'Bệnh nhân'),
(31, N'Quang Huy', '1992-06-14', N'Nam', '0915001018', 'quanghuy92@example.com', N'Quận 10, TP.HCM', N'Bệnh nhân'),
(32, N'Tiến Trần', '1985-03-20', N'Nam', '0915001019', 'tientran85@example.com', N'Quận 3, TP.HCM', N'Bệnh nhân'),
(33, N'Dũng Phạm', '1997-11-09', N'Nam', '0915001020', 'dungpham97@example.com', N'Quận 4, TP.HCM', N'Bệnh nhân'),
(34, N'Hoài Lan Phạm', '1994-04-04', N'Nữ', '0915001021', 'hoailanpham@example.com', N'Quận 7, TP.HCM', N'Bệnh nhân'),
(35, N'Linh Chi', '2003-05-11', N'Nữ', '0915001022', 'linhchi03@example.com', N'Quận 5, TP.HCM', N'Bệnh nhân'),
(36, N'Hoàng Phương', '1994-11-06', N'Nữ', '0915001023', 'hoangphuong94@example.com', N'Quận 8, TP.HCM', N'Bệnh nhân'),
(37, N'Kiến Hưng', '1990-02-03', N'Nam', '0915001024', 'kienhung90@example.com', N'Quận 10, TP.HCM', N'Bệnh nhân'),
(38, N'Thanh Võ', '1988-07-12', N'Nam', '0915001025', 'thanhvo88@example.com', N'Quận 11, TP.HCM', N'Bệnh nhân'),
(39, N'Huệ Phan', '2002-08-30', N'Nữ', '0915001026', 'huephan02@example.com', N'Quận 12, TP.HCM', N'Bệnh nhân'),
(40, N'Tuyết Nhung', '1993-09-27', N'Nữ', '0915001027', 'tuyetnhung@example.com', N'Quận 4, TP.HCM', N'Bệnh nhân'),
(41, N'Thiện Minh', '2004-03-15', N'Nam', '0915001028', 'thienminh04@example.com', N'Quận 5, TP.HCM', N'Bệnh nhân'),
(42, N'Gia Hân', '1999-06-12', N'Nữ', '0915001029', 'giahan99@example.com', N'Quận 6, TP.HCM', N'Bệnh nhân'),
(43, N'Ngọc Anh', '1991-02-18', N'Nữ', '0915001030', 'ngocanh91@example.com', N'Quận 1, TP.HCM', N'Bệnh nhân'),
(44, N'Nguyễn Hoàng Sơn', '1986-1-1', N'Nam', '0999777555', 'hoangson86@example.com', N'Quận 1, TP.HCM', N'Bệnh nhân');
GO

-- BenhNhan--
INSERT INTO BenhNhan (ThongTinID, GhiChu)
SELECT
    ThongTinID,
    N'Dữ liệu seed ban đầu'
FROM ThongTinCaNhan
WHERE Loai = N'Bệnh nhân';

--- Phòng Chức Năng ---
INSERT INTO PhongChucNang (TenPhong, LoaiPhong, MoTa)
VALUES
(N'Phòng khám bệnh', N'Khám lâm sàng', N'Phòng khám da liễu tổng quát, đánh giá tình trạng da, chẩn đoán ban đầu.'),
(N'Phòng điều trị', N'Điều trị', N'Không gian thực hiện các liệu trình điều trị theo chỉ định bác sĩ.'),
(N'Phòng xét nghiệm', N'Xét nghiệm nhanh', N'Thực hiện các xét nghiệm cơ bản như đường huyết, CRP, HbA1c, test nhanh.'),
(N'Phòng chẩn đoán da liễu', N'Chẩn đoán cận lâm sàng', N'Soi da, soi nấm, kiểm tra sắc tố, tổn thương da.'),
(N'Phòng thủ thuật', N'Thủ thuật da liễu', N'Thực hiện thủ thuật nhỏ: đốt điện, lạnh nitơ, lăn kim, RF.'),
(N'Phòng laser', N'Điều trị laser', N'Sử dụng công nghệ laser CO2, YAG, IPL để điều trị chuyên sâu.');

--- Nhân Viên ---
INSERT INTO NhanVien
(ThongTinID, ChucVuID, NgayVaoLam, BangCap, KinhNghiem, PhongChucNangID)
VALUES
-- BÁC SĨ KHÁM 
(2, 1, '2012-02-01', N'Bác sĩ Đa khoa – ĐH Y Dược TP.HCM', N'13 năm khám và chẩn đoán da liễu', 1),
(3, 1, '2014-04-10', N'Bác sĩ Đa khoa – ĐH Y Hà Nội',       N'11 năm kinh nghiệm khám da liễu', 1),
--  BÁC SĨ ĐIỀU TRỊ 
(4, 2, '2010-08-15', N'CKI Da liễu – ĐH Y Dược Huế',       N'15 năm điều trị chuyên sâu da liễu', 2),
(5, 2, '2011-06-20', N'CKI Da liễu – ĐH Y Dược TP.HCM',   N'14 năm điều trị mụn, sẹo, nám',      2),
--  Y TÁ 
(6, 3, '2016-09-01', N'Cử nhân Điều dưỡng',                N'8 năm hỗ trợ điều trị da liễu',      2),
(7, 3, '2017-03-12', N'Cử nhân Điều dưỡng',                N'7 năm chăm sóc bệnh nhân',           2),
(8, 3, '2015-05-18', N'Cao đẳng Điều dưỡng',               N'9 năm hỗ trợ thủ thuật da liễu',      2),
(9, 3, '2018-10-05', N'Cao đẳng Điều dưỡng',               N'6 năm điều dưỡng phòng điều trị',     2),
--  KỸ THUẬT VIÊN 
(10, 4, '2015-01-20', N'Cử nhân Xét nghiệm y học',         N'9 năm xét nghiệm và chẩn đoán',       3),
(11, 4, '2016-07-11', N'Cử nhân Kỹ thuật hình ảnh',        N'8 năm soi da, phân tích da',           3),
(12, 4, '2014-11-02', N'Cử nhân Xét nghiệm',               N'10 năm xét nghiệm cận lâm sàng',       3),
--  LỄ TÂN 
(13, 5, '2019-06-01', N'Cao đẳng Quản trị văn phòng',       N'5 năm tiếp nhận và điều phối lịch hẹn',1),
(14, 5, '2020-02-15', N'Trung cấp Hành chính',             N'4 năm lễ tân phòng khám',              1);
GO


INSERT INTO BacSiProfile
(NhanVienID, GioiThieu, ChuyenMon, ThanhTuu, HinhAnh, KinhNghiem)
VALUES
-- ===== BÁC SĨ KHÁM =====
(1,
 N'Bác sĩ có nhiều năm kinh nghiệm trong khám và chẩn đoán các bệnh da liễu.',
 N'Da liễu tổng quát',
 N'Hơn 5.000 ca khám da liễu thành công',
 N'bs_lamminhduc.jpg',
 N'13 năm kinh nghiệm khám và chẩn đoán bệnh da'),

(2,
 N'Bác sĩ tận tâm, chuyên khám và tư vấn điều trị các bệnh da thường gặp.',
 N'Da liễu tổng quát',
 N'Thành viên Hội Da liễu Việt Nam',
 N'bs_hoangphucloi.jpg',
 N'11 năm kinh nghiệm khám da liễu'),

-- ===== BÁC SĨ ĐIỀU TRỊ =====
(3,
 N'Bác sĩ chuyên điều trị các bệnh da liễu phức tạp và liệu trình chuyên sâu.',
 N'Điều trị mụn – sẹo – nám',
 N'Nhiều năm nghiên cứu và áp dụng phác đồ điều trị cá nhân hóa',
 N'bs_nguyenthibaotran.jpg',
 N'15 năm kinh nghiệm điều trị da liễu'),

(4,
 N'Bác sĩ chuyên sâu điều trị công nghệ cao trong da liễu.',
 N'Laser – điều trị da chuyên sâu',
 N'Chứng chỉ Laser CO2, IPL, YAG',
 N'bs_tranquockhanh.jpg',
 N'14 năm kinh nghiệm điều trị bằng công nghệ cao');
GO



select * from ChucVu;
select * from TaiKhoan;
select * from ThongTinCaNhan;
select * from NhanVien;
select * from BenhNhan;
select * from BacSiProfile;
select * from PhongChucNang;