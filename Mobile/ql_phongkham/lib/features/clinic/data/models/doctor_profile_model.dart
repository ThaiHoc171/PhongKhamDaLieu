class BacSiProfileModel {
  final int bacSiProfileID;
  final int nhanVienID;
  final String chuyenMon;
  final String hinhAnh;
  final DateTime ngayCapNhat;

  BacSiProfileModel({
    required this.bacSiProfileID,
    required this.nhanVienID,
    required this.chuyenMon,
    required this.hinhAnh,
    required this.ngayCapNhat,
  });

  factory BacSiProfileModel.fromJson(Map<String, dynamic> json) {
    return BacSiProfileModel(
      bacSiProfileID: json['bacSiProfileID'],
      nhanVienID: json['nhanVienID'],
      chuyenMon: json['chuyenMon'] ?? '',
      hinhAnh: json['hinhAnh'] ?? '',
      ngayCapNhat: DateTime.parse(json['ngayCapNhat']),
    );
  }
}
