class UserModel {
  final int id;
  final String email;
  final String vaiTro;
  final String accessToken;
  final String refreshToken;
  final int? thongTinId;
  final int? nhanVienId;
  final int? benhNhanId;
  final String? chucVu;
  final String hoTen;

  UserModel({
    required this.id,
    required this.email,
    required this.vaiTro,
    required this.accessToken,
    required this.refreshToken,
    this.thongTinId,
    this.nhanVienId,
    this.benhNhanId,
    this.chucVu,
    required this.hoTen,
  });

  factory UserModel.fromJson(Map<String, dynamic> json) {
    return UserModel(
      id: json['id'],
      email: json['email'],
      vaiTro: json['vaiTro'],
      accessToken: json['accessToken'],
      refreshToken: json['refreshToken'],
      thongTinId: json['thongTinId'],
      nhanVienId: json['nhanVienId'],
      benhNhanId: json['benhNhanId'],
      chucVu: json['chucVu'],
      hoTen: json['hoTen'],
    );
  }
}
