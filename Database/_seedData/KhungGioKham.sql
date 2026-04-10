INSERT INTO KhungGioKham (CaLamViec,GioBatDau,GioKetThuc)
VALUES
(1,'07:00','07:30'),
(1,'07:30','08:00'),
(1,'08:00','08:30'),
(1,'08:30','09:00'),
(1,'09:00','09:30'),
(1,'09:30','10:00'),
(2,'13:00','13:30'),
(2,'13:30','14:00'),
(2,'14:00','14:30'),
(2,'14:30','15:00'),
(2,'15:00','15:30'),
(2,'15:30','16:00');

ALTER TABLE KhungGioKham
DROP COLUMN TenKhung;

ALTER TABLE KhungGioKham
ADD TenKhung AS (
    CONVERT(NVARCHAR(5), GioBatDau,108)
    + N' - ' +
    CONVERT(NVARCHAR(5), GioKetThuc,108)
) PERSISTED;

select * from KhungGioKham;