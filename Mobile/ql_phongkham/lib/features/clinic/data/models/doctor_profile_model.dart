class BacSiProfileModel {
  final int bacSiProfileID;
  final int nhanVienID;
  final String hoTen;
  final String chuyenMon;
  final String hinhAnh;
  final DateTime ngayCapNhat;

  BacSiProfileModel({
    required this.bacSiProfileID,
    required this.nhanVienID,
    required this.hoTen,
    required this.chuyenMon,
    required this.hinhAnh,
    required this.ngayCapNhat,
  });

  factory BacSiProfileModel.fromJson(Map<String, dynamic> json) {
    return BacSiProfileModel(
      bacSiProfileID: json['bacSiProfileID'],
      nhanVienID: json['nhanVienID'],
      hoTen: json['hoTen'],
      chuyenMon: json['chuyenMon'] ?? '',
      hinhAnh: json['hinhAnh'] ?? '',
      ngayCapNhat: DateTime.parse(json['ngayCapNhat']),
    );
  }
}
