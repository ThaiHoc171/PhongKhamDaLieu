class User {
  final int id;
  final String email;
  final String vaitro;
  final int? nhanVienId;
  final int? benhNhanId;
  final String chucvu;
  final String hoten;

  User({
    required this.id,
    required this.email,
    required this.vaitro,
    this.nhanVienId,
    this.benhNhanId,
    required this.chucvu,
    required this.hoten,
  });
}