class LieuTrinhDieuTriModel {
  final int lieuTrinhID;
  final String tenLieuTrinh;
  final String benhNhan;
  final int tongSoBuoi;
  final String trangThai;
  final DateTime ngayBatDau;
  final DateTime? ngayKetThuc;

  LieuTrinhDieuTriModel({
    required this.lieuTrinhID,
    required this.tenLieuTrinh,
    required this.benhNhan,
    required this.tongSoBuoi,
    required this.trangThai,
    required this.ngayBatDau,
    this.ngayKetThuc,
  });

  factory LieuTrinhDieuTriModel.fromJson(Map<String, dynamic> json) {
    return LieuTrinhDieuTriModel(
      lieuTrinhID: json['lieuTrinhID'] ?? 0,
      tenLieuTrinh: json['tenLieuTrinh'] ?? '',
      benhNhan: json['benhNhan'] ?? '',

      tongSoBuoi: json['tongSoBuoi'] ?? 0,
      trangThai: json['trangThai'] ?? '',

      ngayBatDau: json['ngayBatDau'] != null
          ? DateTime.parse(json['ngayBatDau'])
          : DateTime.now(),

      ngayKetThuc: json['ngayKetThuc'] != null
          ? DateTime.parse(json['ngayKetThuc'])
          : null,
    );
  }
}

class BuoiDieuTriModel {
  final int buoiDieuTriID;
  final int lieuTrinhID;
  final int caKhamID;
  final int soBuoi;
  final DateTime ngayDuKien;
  final String trangThai;

  BuoiDieuTriModel({
    required this.buoiDieuTriID,
    required this.lieuTrinhID,
    required this.caKhamID,
    required this.soBuoi,
    required this.ngayDuKien,
    required this.trangThai,
  });

  factory BuoiDieuTriModel.fromJson(Map<String, dynamic> json) {
    return BuoiDieuTriModel(
      buoiDieuTriID: json['buoiDieuTriID'] ?? 0,
      lieuTrinhID: json['lieuTrinhID'] ?? 0,
      caKhamID: json['caKhamID'] ?? 0,
      soBuoi: json['soBuoi'] ?? 0,
      trangThai: json['trangThai'] ?? '',
      ngayDuKien: json['ngayDuKien'] != null
          ? DateTime.parse(json['ngayDuKien'])
          : DateTime.now(),
    );
  }
}
