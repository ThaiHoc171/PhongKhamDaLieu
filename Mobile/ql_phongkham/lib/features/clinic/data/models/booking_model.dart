class CaKhamModel {
  final int caKhamID;
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
  });

  factory CaKhamModel.fromJson(Map<String, dynamic> json) {
    return CaKhamModel(
      caKhamID: json['caKhamID'],
      tenKhungGio: json['tenKhungGio'] ?? '',
      tenPhong: json['tenPhong'] ?? '',
      hoTen: json['hoTen'] ?? '',
      lyDoKham: json['lyDoKham'] ?? '',
      trangThai: json['trangThai'] ?? '',
    );
  }
}
