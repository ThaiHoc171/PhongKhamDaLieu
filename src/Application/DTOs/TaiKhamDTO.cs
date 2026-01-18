namespace Application.DTOs
{
    public class ThemTaiKhamDTO
    {
        public int PhienKhamId { get; set; }
        public int BenhNhanId { get; set; }
        public DateTime NgayDuKien { get; set; }
        public string LyDo { get; set; }
        public int? CaKhamId { get; set; }
    }
    public class CapNhatTaiKhamDTO
    {
        public DateTime NgayDuKien { get; set; }
        public int? CaKhamId { get; set; }
        public string LyDo { get; set; }
    }
    public class TaiKhamResponseDTO
    {
        public int TaiKhamId { get; set; }
        public int PhienKhamId { get; set; }
        public int BenhNhanId { get; set; }
        public DateTime NgayDuKien { get; set; }
        public string LyDo { get; set; }
        public string TrangThai { get; set; }
        public int? CaKhamId { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
