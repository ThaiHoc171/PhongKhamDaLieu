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
  final String? hoTen;

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
    this.hoTen,
  });

  factory UserModel.fromJson(Map<String, dynamic> json) {
    final hoTenObj = json['hoTen'];

    return UserModel(
      id: json['id'] ?? 0,
      email: json['email'] ?? '',
      vaiTro: json['vaiTro'] ?? '',
      accessToken: json['accessToken'] ?? '',
      refreshToken: json['refreshToken'] ?? '',
      thongTinId: hoTenObj != null ? hoTenObj['id'] : null,
      nhanVienId: json['nhanVienId'],
      benhNhanId: json['benhNhanId'],
      chucVu: json['chucVu'],
      hoTen: hoTenObj != null ? hoTenObj['name'] : null,
    );
  }
}
