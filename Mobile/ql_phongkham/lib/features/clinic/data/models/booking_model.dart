class CaKhamModel {
  final int caKhamID;
  final DateTime ngayKham;
  final String tenKhungGio;
  final String tenPhong;
  final String hoTen;
  final String lyDoKham;
  final String trangThai;

  CaKhamModel({
    required this.caKhamID,
    required this.tenKhungGio,
    required this.tenPhong,
    required this.hoTen,
    required this.lyDoKham,
    required this.trangThai,
    required this.ngayKham,
  });

  factory CaKhamModel.fromJson(Map<String, dynamic> json) {
    return CaKhamModel(
      caKhamID: json['caKhamID'],
      ngayKham: json['ngayKham'] != null
          ? DateTime.parse(json['ngayKham'])
          : DateTime.now(),
      tenKhungGio: json['tenKhungGio'] ?? '',
      tenPhong: json['tenPhong'] ?? '',
      hoTen: json['hoTen'] ?? '',
      lyDoKham: json['lyDoKham'] ?? '',
      trangThai: json['trangThai'] ?? '',
    );
  }
}
