namespace Application.DTOs
{
    public class KhungGioKhamRequestDTO
    {
        public int CaLamViec { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
    }
    public class KhungGioKhamUpdateDTO
    {
        public int CaLamViec { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
    }
    public class KhungGioKhamReadModel
    {
        public int KhungGioID { get; set; }
        public int CaLamViec { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
        public string? TenKhung { get; set; }
    }
    public class KhungGioKhamListReadModel
    {
        public int KhungGioID { get; set; }
        public int CaLamViec { get; set; }
        public string? TenKhung { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
    }
}
