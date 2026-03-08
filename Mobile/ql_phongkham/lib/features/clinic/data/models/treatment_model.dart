class LieuTrinhDieuTriModel {
  final int lieuTrinhID;
  final int benhNhanID;
  final int phienKhamID;
  final String tenLieuTrinh;
  final int tongSoBuoi;
  final String trangThai;
  final String? ghiChu;
  final DateTime ngayBatDau;
  final DateTime? ngayKetThuc;

  LieuTrinhDieuTriModel({
    required this.lieuTrinhID,
    required this.benhNhanID,
    required this.phienKhamID,
    required this.tenLieuTrinh,
    required this.tongSoBuoi,
    required this.trangThai,
    this.ghiChu,
    required this.ngayBatDau,
    this.ngayKetThuc,
  });

  factory LieuTrinhDieuTriModel.fromJson(Map<String, dynamic> json) {
    return LieuTrinhDieuTriModel(
      lieuTrinhID: json['lieuTrinhID'],
      benhNhanID: json['benhNhanID'],
      phienKhamID: json['phienKhamID'],
      tenLieuTrinh: json['tenLieuTrinh'],
      tongSoBuoi: json['tongSoBuoi'],
      trangThai: json['trangThai'],
      ghiChu: json['ghiChu'],
      ngayBatDau: DateTime.parse(json['ngayBatDau']),
      ngayKetThuc: json['ngayKetThuc'] != null
          ? DateTime.parse(json['ngayKetThuc'])
          : null,
    );
  }
}
