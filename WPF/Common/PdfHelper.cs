using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.Win32;
using System.IO;
using HoanMyClinic.Models;

namespace HoanMyClinic.Common;

public class PdfHelper
{
	private static readonly DeviceRgb PrimaryColor = new(0x1A, 0x73, 0xE8);
	private static readonly DeviceRgb HeaderBg = new(0xF0, 0xF4, 0xFF);
	private static readonly DeviceRgb RowAltBg = new(0xF9, 0xFA, 0xFF);
	private static readonly DeviceRgb DividerColor = new(0xCC, 0xD8, 0xF0);

	// ─── Helpers ────────────────────────────────────────────────────────────────
	private static Cell HeaderCell(string text, PdfFont boldFont) =>
		new Cell()
			.Add(new Paragraph(text).SetFont(boldFont).SetFontSize(9).SetFontColor(ColorConstants.WHITE))
			.SetBackgroundColor(PrimaryColor)
			.SetPadding(5)
			.SetBorder(Border.NO_BORDER);

	private static Cell DataCell(string text, PdfFont font, bool alt = false) =>
		new Cell()
			.Add(new Paragraph(text ?? "").SetFont(font).SetFontSize(9))
			.SetBackgroundColor(alt ? RowAltBg : ColorConstants.WHITE)
			.SetPadding(4)
			.SetBorder(new SolidBorder(DividerColor, 0.5f));

	private static Paragraph SectionTitle(string text, PdfFont boldFont) =>
		new Paragraph(text)
			.SetFont(boldFont)
			.SetFontSize(11)
			.SetFontColor(PrimaryColor)
			.SetMarginTop(14)
			.SetMarginBottom(4);

	private static LineSeparator Divider() =>
		new LineSeparator(new iText.Kernel.Pdf.Canvas.Draw.SolidLine(0.5f))
		{
		};

	public string? ExportPdf(PhienKhamPdfDto vm)
	{
		if (vm == null) throw new ArgumentNullException(nameof(vm));

		// ── Chọn nơi lưu ──────────────────────────────────────────────────────
		var dialog = new SaveFileDialog
		{
			Title = "Lưu phiếu khám bệnh",
			Filter = "PDF Files (*.pdf)|*.pdf",
			FileName = $"PhieuKham_{vm.BenhNhan?.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmm}.pdf",
			DefaultExt = ".pdf"
		};

		if (dialog.ShowDialog() != true) return null;
		var savePath = dialog.FileName;

		// ── Fonts ──────────────────────────────────────────────────────────────
		var baseDir = AppDomain.CurrentDomain.BaseDirectory;
		var fontPath = Path.Combine(baseDir, "Assets", "fonts", "ARIAL.TTF");
		var boldPath = Path.Combine(baseDir, "Assets", "fonts", "ARIALBD.TTF");

		if (!File.Exists(fontPath)) throw new FileNotFoundException("Không tìm thấy font", fontPath);
		if (!File.Exists(boldPath)) throw new FileNotFoundException("Không tìm thấy bold font", boldPath);

		var font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);
		var boldFont = PdfFontFactory.CreateFont(boldPath, PdfEncodings.IDENTITY_H);

		// ── Tạo document ───────────────────────────────────────────────────────
		using var stream = new MemoryStream();
		var writer = new PdfWriter(stream);
		writer.SetCloseStream(false);
		var pdf = new PdfDocument(writer);
		var doc = new Document(pdf);
		doc.SetFont(font).SetFontSize(10);
		doc.SetMargins(36, 40, 36, 40);

		// ── Header: logo + tên phòng khám ─────────────────────────────────────
		var headerTable = new Table(UnitValue.CreatePercentArray(new[] { 1f, 3f }))
			.UseAllAvailableWidth()
			.SetMarginBottom(6);

		var logoPath = Path.Combine(baseDir, "Assets", "Images", "logo.png");
		if (File.Exists(logoPath))
		{
			var logo = new Image(ImageDataFactory.Create(logoPath)).ScaleToFit(70, 70);
			headerTable.AddCell(new Cell().Add(logo)
				.SetBorder(Border.NO_BORDER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
		}
		else
		{
			headerTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
		}

		headerTable.AddCell(
			new Cell()
				.Add(new Paragraph("PHÒNG KHÁM DA LIỄU HOÀN MỸ")
						.SetFont(boldFont).SetFontSize(13).SetFontColor(PrimaryColor))
				.Add(new Paragraph("Địa chỉ: 123 Đường ABC, Quận 1, TP.HCM").SetFontSize(9))
				.Add(new Paragraph("Điện thoại: (028) 1234 5678  |  Email: info@hoanmy.vn").SetFontSize(9))
				.SetBorder(Border.NO_BORDER)
				.SetVerticalAlignment(VerticalAlignment.MIDDLE)
		);
		doc.Add(headerTable);

		// Đường kẻ ngang dưới header
		doc.Add(new LineSeparator(new iText.Kernel.Pdf.Canvas.Draw.SolidLine(1.5f)));

		// ── Tiêu đề phiếu ─────────────────────────────────────────────────────
		doc.Add(new Paragraph("PHIẾU KHÁM BỆNH")
			.SetFont(boldFont).SetFontSize(17)
			.SetFontColor(PrimaryColor)
			.SetTextAlignment(TextAlignment.CENTER)
			.SetMarginTop(10).SetMarginBottom(2));

		doc.Add(new Paragraph($"Ngày khám: {vm.NgayKham?.ToString("dd/MM/yyyy") ?? "—"}")
			.SetFontSize(9).SetTextAlignment(TextAlignment.CENTER)
			.SetFontColor(new DeviceRgb(0x60, 0x60, 0x60))
			.SetMarginBottom(10));

		// ── Thông tin chung (2 cột) ────────────────────────────────────────────
		doc.Add(SectionTitle("THÔNG TIN CHUNG", boldFont));
		doc.Add(Divider());

		var infoTable = new Table(UnitValue.CreatePercentArray(new[] { 1f, 1f }))
			.UseAllAvailableWidth().SetMarginTop(6).SetMarginBottom(4);

		infoTable.AddCell(InfoRow("Bệnh nhân", vm.BenhNhan, font, boldFont));
		infoTable.AddCell(InfoRow("Bác sĩ", vm.BacSi, font, boldFont));
		infoTable.AddCell(InfoRow("Trạng thái", vm.TrangThai, font, boldFont));
		infoTable.AddCell(InfoRow("Ngày khám", vm.NgayKham?.ToString("dd/MM/yyyy"), font, boldFont));
		doc.Add(infoTable);

		// ── Triệu chứng & Chẩn đoán (2 cột) ──────────────────────────────────
		doc.Add(SectionTitle("LÂM SÀNG", boldFont));
		doc.Add(Divider());

		var clinicTable = new Table(UnitValue.CreatePercentArray(new[] { 1f, 1f }))
			.UseAllAvailableWidth().SetMarginTop(6).SetMarginBottom(4);

		clinicTable.AddCell(TextBlock("Triệu chứng", vm.TrieuChung, font, boldFont));
		clinicTable.AddCell(TextBlock("Chẩn đoán", vm.ChanDoan, font, boldFont));
		doc.Add(clinicTable);

		// ── Danh sách bệnh ────────────────────────────────────────────────────
		doc.Add(SectionTitle("DANH SÁCH BỆNH", boldFont));
		doc.Add(Divider());

		var benhTable = new Table(UnitValue.CreatePercentArray(new[] { 2f, 2f, 3f }))
			.UseAllAvailableWidth().SetMarginTop(6).SetMarginBottom(4);
		benhTable.AddHeaderCell(HeaderCell("Loại bệnh", boldFont));
		benhTable.AddHeaderCell(HeaderCell("Loại chẩn đoán", boldFont));
		benhTable.AddHeaderCell(HeaderCell("Ghi chú", boldFont));

		if (vm.BenhList != null)
		{
			int r = 0;
			foreach (var b in vm.BenhList)
			{
				bool alt = r++ % 2 == 1;
				benhTable.AddCell(DataCell(b?.LoaiBenh?.Name, font, alt));
				benhTable.AddCell(DataCell(b?.LoaiChanDoan, font, alt));
				benhTable.AddCell(DataCell(b?.GhiChu, font, alt));
			}
		}
		doc.Add(benhTable);

		// ── Cận lâm sàng ──────────────────────────────────────────────────────
		doc.Add(SectionTitle("CẬN LÂM SÀNG", boldFont));
		doc.Add(Divider());

		var clsTable = new Table(UnitValue.CreatePercentArray(new[] { 3f, 2f, 3f, 2f }))
			.UseAllAvailableWidth().SetMarginTop(6).SetMarginBottom(4);
		clsTable.AddHeaderCell(HeaderCell("Tên CLS", boldFont));
		clsTable.AddHeaderCell(HeaderCell("Trạng thái", boldFont));
		clsTable.AddHeaderCell(HeaderCell("Kết quả", boldFont));
		clsTable.AddHeaderCell(HeaderCell("Ngày thực hiện", boldFont));

		if (vm.CLSList != null)
		{
			int r = 0;
			foreach (var c in vm.CLSList)
			{
				bool alt = r++ % 2 == 1;
				clsTable.AddCell(DataCell(c?.TenCLS, font, alt));
				clsTable.AddCell(DataCell(c?.TrangThai, font, alt));
				clsTable.AddCell(DataCell(c?.KetQua, font, alt));
				clsTable.AddCell(DataCell(c?.NgayThucHien?.ToString("dd/MM/yyyy"), font, alt));
			}
		}
		doc.Add(clsTable);

		// ── Thiết bị ──────────────────────────────────────────────────────────
		doc.Add(SectionTitle("THIẾT BỊ SỬ DỤNG", boldFont));
		doc.Add(Divider());

		var tbTable = new Table(UnitValue.CreatePercentArray(new[] { 3f, 2f, 3f }))
			.UseAllAvailableWidth().SetMarginTop(6).SetMarginBottom(4);
		tbTable.AddHeaderCell(HeaderCell("Thiết bị", boldFont));
		tbTable.AddHeaderCell(HeaderCell("Phòng", boldFont));
		tbTable.AddHeaderCell(HeaderCell("Ghi chú", boldFont));

		if (vm.ThietBiList != null)
		{
			int r = 0;
			foreach (var t in vm.ThietBiList)
			{
				bool alt = r++ % 2 == 1;
				tbTable.AddCell(DataCell(t?.TenThietBi, font, alt));
				tbTable.AddCell(DataCell(t?.TenPhong, font, alt));
				tbTable.AddCell(DataCell(t?.GhiChu, font, alt));
			}
		}
		doc.Add(tbTable);

		// ── Ghi chú chung ─────────────────────────────────────────────────────
		if (!string.IsNullOrWhiteSpace(vm.GhiChu))
		{
			doc.Add(SectionTitle("GHI CHÚ", boldFont));
			doc.Add(Divider());
			doc.Add(new Paragraph(vm.GhiChu).SetFontSize(9).SetMarginTop(4).SetMarginBottom(4));
		}

		// ── Footer / Chữ ký ───────────────────────────────────────────────────
		doc.Add(new Paragraph($"TP.HCM, ngày {DateTime.Now:dd} tháng {DateTime.Now:MM} năm {DateTime.Now:yyyy}")
			.SetTextAlignment(TextAlignment.RIGHT)
			.SetFontSize(9).SetMarginTop(20));

		var signTable = new Table(UnitValue.CreatePercentArray(new[] { 1f, 1f }))
			.UseAllAvailableWidth().SetMarginTop(6);

		signTable.AddCell(
			new Cell()
				.Add(new Paragraph("BỆNH NHÂN").SetFont(boldFont).SetFontSize(10)
						.SetTextAlignment(TextAlignment.CENTER))
				.Add(new Paragraph("(Ký, ghi rõ họ tên)").SetFontSize(8)
						.SetTextAlignment(TextAlignment.CENTER).SetFontColor(new DeviceRgb(0x80, 0x80, 0x80)))
				.Add(new Paragraph("\n\n\n").SetFontSize(10))
				.Add(new Paragraph(vm.BenhNhan ?? "").SetFont(boldFont).SetFontSize(10)
						.SetTextAlignment(TextAlignment.CENTER))
				.SetBorder(Border.NO_BORDER)
		);

		signTable.AddCell(
			new Cell()
				.Add(new Paragraph("BÁC SĨ ĐIỀU TRỊ").SetFont(boldFont).SetFontSize(10)
						.SetTextAlignment(TextAlignment.CENTER))
				.Add(new Paragraph("(Ký, ghi rõ họ tên)").SetFontSize(8)
						.SetTextAlignment(TextAlignment.CENTER).SetFontColor(new DeviceRgb(0x80, 0x80, 0x80)))
				.Add(new Paragraph("\n\n\n").SetFontSize(10))
				.Add(new Paragraph(vm.BacSi ?? "").SetFont(boldFont).SetFontSize(10)
						.SetTextAlignment(TextAlignment.CENTER))
				.SetBorder(Border.NO_BORDER)
		);
		doc.Add(signTable);

		// ── Đóng và lưu ───────────────────────────────────────────────────────
		doc.Close();
		File.WriteAllBytes(savePath, stream.ToArray());

		return savePath;
	}

	// ─── Helper cells ───────────────────────────────────────────────────────────
	private static Cell InfoRow(string label, string? value, PdfFont font, PdfFont boldFont) =>
		new Cell()
			.Add(new Paragraph(label + ":").SetFont(boldFont).SetFontSize(9)
					.SetFontColor(new DeviceRgb(0x44, 0x44, 0x44)))
			.Add(new Paragraph(value ?? "—").SetFont(font).SetFontSize(10))
			.SetBorder(Border.NO_BORDER)
			.SetPaddingBottom(6);

	private static Cell TextBlock(string label, string? value, PdfFont font, PdfFont boldFont) =>
		new Cell()
			.Add(new Paragraph(label + ":").SetFont(boldFont).SetFontSize(9)
					.SetFontColor(new DeviceRgb(0x44, 0x44, 0x44)))
			.Add(new Paragraph(value ?? "—").SetFont(font).SetFontSize(10))
			.SetBorder(new SolidBorder(DividerColor, 0.5f))
			.SetPadding(6);
}