class ProfileModel {
  final int thongTinId;
  final int taiKhoanId;
  final String hoTen;
  final DateTime ngaySinh;
  final String gioiTinh;
  final String sdt;
  final String emailLienHe;
  final String diaChi;
  final String? avatar;
  final String loai;

  ProfileModel({
    required this.thongTinId,
    required this.taiKhoanId,
    required this.hoTen,
    required this.ngaySinh,
    required this.gioiTinh,
    required this.sdt,
    required this.emailLienHe,
    required this.diaChi,
    this.avatar,
    required this.loai,
  });

  factory ProfileModel.fromJson(Map<String, dynamic> json) {
    return ProfileModel(
      thongTinId: json['thongTinID'],
      taiKhoanId: json['taiKhoanID'],
      hoTen: json['hoTen'],
      ngaySinh: DateTime.parse(json['ngaySinh']),
      gioiTinh: json['gioiTinh'],
      sdt: json['sdt'],
      emailLienHe: json['emailLienHe'],
      diaChi: json['diaChi'],
      avatar: json['avatar'],
      loai: json['loai'],
    );
  }
}
