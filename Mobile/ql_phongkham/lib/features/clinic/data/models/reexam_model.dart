class TaiKhamModel {
  final int taiKhamID;
  final int phienKhamID;
  final int benhNhanID;
  final DateTime ngayDuKien;
  final String lyDo;
  final String trangThai;
  final int? caKhamID;
  final DateTime ngayTao;

  TaiKhamModel({
    required this.taiKhamID,
    required this.phienKhamID,
    required this.benhNhanID,
    required this.ngayDuKien,
    required this.lyDo,
    required this.trangThai,
    this.caKhamID,
    required this.ngayTao,
  });

  factory TaiKhamModel.fromJson(Map<String, dynamic> json) {
    return TaiKhamModel(
      taiKhamID: json['taiKhamID'],
      phienKhamID: json['phienKhamID'],
      benhNhanID: json['benhNhanID'],
      ngayDuKien: DateTime.parse(json['ngayDuKien']),
      lyDo: json['lyDo'],
      trangThai: json['trangThai'],
      caKhamID: json['caKhamID'],
      ngayTao: DateTime.parse(json['ngayTao']),
    );
  }
}
