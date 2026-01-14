using System;

namespace Domain.Entities
{
	public class KhungGioKham
	{
		public int KhungGioID { get; private set; }
		public TimeSpan GioBatDau { get; private set; }
		public TimeSpan GioKetThuc { get; private set; }
		public string? TenKhung { get; private set; }
		public int MaxSlot { get; private set; }

		// Tạo mới (MaxSlot cố định = 5)
		public KhungGioKham(TimeSpan gioBatDau, TimeSpan gioKetThuc, string? tenKhung) // vd: 7:30, 8:00, "Sáng 1"
		{
			if (gioBatDau >= gioKetThuc)
				throw new ArgumentException("Giờ bắt đầu phải nhỏ hơn giờ kết thúc");

			GioBatDau = gioBatDau;
			GioKetThuc = gioKetThuc;
			TenKhung = tenKhung;
			MaxSlot = 5;
		}

		// Map từ DB
		public KhungGioKham(
			int khungGioID,
			TimeSpan gioBatDau,
			TimeSpan gioKetThuc,
			string? tenKhung,
			int maxSlot)
		{
			KhungGioID = khungGioID;
			GioBatDau = gioBatDau;
			GioKetThuc = gioKetThuc;
			TenKhung = tenKhung;
			MaxSlot = maxSlot;
		}

		public void CapNhat(TimeSpan gioBatDau, TimeSpan gioKetThuc, string? tenKhung)
		{
			if (gioBatDau >= gioKetThuc)
				throw new ArgumentException("Giờ bắt đầu phải nhỏ hơn giờ kết thúc");

			GioBatDau = gioBatDau;
			GioKetThuc = gioKetThuc;
			TenKhung = tenKhung;
		}
		//Kiểm tra 2 khung giờ có trùng nhau hay không
		public bool KiemTraTrung(KhungGioKham other)
		{
			if (other == null)
				throw new ArgumentNullException(nameof(other));

			// (start1 < end2) && (end1 > start2)
			return GioBatDau < other.GioKetThuc
				&& GioKetThuc > other.GioBatDau;
		}
	}
}
