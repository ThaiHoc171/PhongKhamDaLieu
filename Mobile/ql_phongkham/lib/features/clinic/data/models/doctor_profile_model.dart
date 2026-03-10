class BacSiProfileModel {
  final int bacSiProfileID;
  final int nhanVienID;
  final String gioiThieu;
  final String chuyenMon;
  final String thanhTuu;
  final String hinhAnh;
  final String kinhNghiem;
  final DateTime ngayCapNhat;

  BacSiProfileModel({
    required this.bacSiProfileID,
    required this.nhanVienID,
    required this.gioiThieu,
    required this.chuyenMon,
    required this.thanhTuu,
    required this.hinhAnh,
    required this.kinhNghiem,
    required this.ngayCapNhat,
  });

  factory BacSiProfileModel.fromJson(Map<String, dynamic> json) {
    return BacSiProfileModel(
      bacSiProfileID: json['bacSiProfileID'],
      nhanVienID: json['nhanVienID'],
      gioiThieu: json['gioiThieu'],
      chuyenMon: json['chuyenMon'],
      thanhTuu: json['thanhTuu'],
      hinhAnh: json['hinhAnh'],
      kinhNghiem: json['kinhNghiem'],
      ngayCapNhat: DateTime.parse(json['ngayCapNhat']),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'bacSiProfileID': bacSiProfileID,
      'nhanVienID': nhanVienID,
      'gioiThieu': gioiThieu,
      'chuyenMon': chuyenMon,
      'thanhTuu': thanhTuu,
      'hinhAnh': hinhAnh,
      'kinhNghiem': kinhNghiem,
      'ngayCapNhat': ngayCapNhat.toIso8601String(),
    };
  }
}
