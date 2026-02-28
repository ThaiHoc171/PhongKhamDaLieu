import '../../domain/entities/user.dart';

class UserModel extends User {
  final String accessToken;

  UserModel({
    required int id,
    required String email,
    required String vaitro,
    required int? nhanVienId,
    required int? benhNhanId,
    required String chucvu,
    required String hoten,
    required this.accessToken,
  }) : super(id: id, email: email, vaitro: vaitro, nhanVienId: nhanVienId, benhNhanId: benhNhanId, chucvu: chucvu, hoten: hoten);

  factory UserModel.fromJson(Map<String, dynamic> json) {
    return UserModel(
      id: json["id"],
      email: json["email"],
      vaitro: json["vaiTro"],
      nhanVienId: json["nhanVienId"],
      benhNhanId: json["benhNhanId"],
      chucvu: json["chucVu"],
      hoten: json["hoTen"],
      accessToken: json["accessToken"],
    );
  }
}