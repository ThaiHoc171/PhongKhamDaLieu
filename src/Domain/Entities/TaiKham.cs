using System;

namespace Domain.Entities;

public class TaiKham
{
    public int TaiKhamId { get; private set; }
    public int PhienKhamId { get; private set; }
    public int BenhNhanId { get; private set; }
    public DateTime NgayDuKien { get; private set; }
    public string LyDo { get; private set; }
    public string TrangThai { get; private set; }
    public int? CaKhamId { get; private set; }
    public DateTime NgayTao { get; private set; }

    // Constructor dùng khi tạo mới (từ DTO)
    public TaiKham(
        int phienKhamId,
        int benhNhanId,
        DateTime ngayDuKien,
        string lyDo,
        int? caKhamId = null,
        string trangThai = "Chờ tái khám"
    )
    {
        if (phienKhamId <= 0)
            throw new ArgumentException("PhienKhamId không hợp lệ");

        if (benhNhanId <= 0)
            throw new ArgumentException("BenhNhanId không hợp lệ");

        if (ngayDuKien.Date < DateTime.Today)
            throw new ArgumentException("Ngày dự kiến không được nhỏ hơn ngày hiện tại");

        PhienKhamId = phienKhamId;
        BenhNhanId = benhNhanId;
        NgayDuKien = ngayDuKien;
        LyDo = lyDo ?? "";
        TrangThai = trangThai;
        CaKhamId = caKhamId;
        NgayTao = DateTime.Now;
    }

    // Constructor dùng khi map từ DB
    public TaiKham(
        int taiKhamId,
        int phienKhamId,
        int benhNhanId,
        DateTime ngayDuKien,
        string lyDo,
        string trangThai,
        int? caKhamId,
        DateTime ngayTao
    )
    {
        TaiKhamId = taiKhamId;
        PhienKhamId = phienKhamId;
        BenhNhanId = benhNhanId;
        NgayDuKien = ngayDuKien;
        LyDo = lyDo ?? "";
        TrangThai = trangThai;
        CaKhamId = caKhamId;
        NgayTao = ngayTao;
    }

    // ===== Business Methods =====

    public void CapNhatNgayTaiKham(DateTime ngayDuKienMoi, int? caKhamId = null)
    {
        if (ngayDuKienMoi.Date < DateTime.Today)
            throw new ArgumentException("Ngày tái khám không hợp lệ");

        NgayDuKien = ngayDuKienMoi;
        CaKhamId = caKhamId;
    }

    public void CapNhatTrangThai(string trangThaiMoi)
    {
        if (string.IsNullOrWhiteSpace(trangThaiMoi))
            throw new ArgumentException("Trạng thái không hợp lệ");

        TrangThai = trangThaiMoi;
    }

    public void CapNhatLyDo(string lyDo)
    {
        LyDo = lyDo ?? "";
    }
}
