class TaiKhamModel {
  final int taiKhamID;
  final int benhNhanID;
  final String benhNhanName;
  final DateTime ngayDuKien;
  final String lyDo;
  final String trangThai;

  TaiKhamModel({
    required this.taiKhamID,
    required this.benhNhanID,
    required this.benhNhanName,
    required this.ngayDuKien,
    required this.lyDo,
    required this.trangThai,
  });

  factory TaiKhamModel.fromJson(Map<String, dynamic> json) {
    return TaiKhamModel(
      taiKhamID: json['taiKhamID'] ?? 0,

      benhNhanID: json['benhNhan']?['id'] ?? 0,
      benhNhanName: json['benhNhan']?['name'] ?? '',

      ngayDuKien: json['ngayDuKien'] != null
          ? DateTime.parse(json['ngayDuKien'])
          : DateTime.now(),

      lyDo: json['lyDo'] ?? '',
      trangThai: json['trangThai'] ?? '',
    );
  }
}
