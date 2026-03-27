class PhienKhamModel {
  final int phienKhamID;
  final int caKhamID;
  final Map<String, dynamic>? benhNhan;
  final Map<String, dynamic>? nhanVien;
  final int phongChucNangID;
  final String trieuChung;
  final String ghiChu;
  final String hinhAnh;
  final String chanDoanCuoi;
  final DateTime? ngayKham;
  final String trangThai;

  PhienKhamModel({
    required this.phienKhamID,
    required this.caKhamID,
    this.benhNhan,
    this.nhanVien,
    required this.phongChucNangID,
    required this.trieuChung,
    required this.ghiChu,
    required this.hinhAnh,
    required this.chanDoanCuoi,
    this.ngayKham,
    required this.trangThai,
  });

  factory PhienKhamModel.fromJson(Map<String, dynamic> json) {
    return PhienKhamModel(
      phienKhamID: json['phienKhamID'] ?? 0,
      caKhamID: json['caKhamID'] ?? 0,
      benhNhan: json['benhNhan'],
      nhanVien: json['nhanVien'],
      phongChucNangID: json['phongChucNangID'] ?? 0,
      trieuChung: json['trieuChung'] ?? '',
      ghiChu: json['ghiChu'] ?? '',
      hinhAnh: json['hinhAnh'] ?? '',
      chanDoanCuoi: json['chanDoanCuoi'] ?? '',
      ngayKham: json['ngayKham'] != null
          ? DateTime.parse(json['ngayKham'])
          : null,
      trangThai: json['trangThai'] ?? '',
    );
  }
}
