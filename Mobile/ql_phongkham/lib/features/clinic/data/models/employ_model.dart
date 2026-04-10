class NhanVienModel {
  final int nhanVienID;
  final int thongTinID;
  final ChucVuModel chucVu;
  final PhongChucNangModel phongChucNang;
  final String hoTen;
  final DateTime ngaySinh;
  final String gioiTinh;
  final String sdt;
  final String emailLienHe;
  final String diaChi;
  final String? avatar;
  final DateTime ngayVaoLam;
  final String bangCap;
  final String kinhNghiem;
  final String trangThai;
  final DateTime ngayTao;
  final DateTime? ngayCapNhat;

  NhanVienModel({
    required this.nhanVienID,
    required this.thongTinID,
    required this.chucVu,
    required this.phongChucNang,
    required this.hoTen,
    required this.ngaySinh,
    required this.gioiTinh,
    required this.sdt,
    required this.emailLienHe,
    required this.diaChi,
    this.avatar,
    required this.ngayVaoLam,
    required this.bangCap,
    required this.kinhNghiem,
    required this.trangThai,
    required this.ngayTao,
    this.ngayCapNhat,
  });

  factory NhanVienModel.fromJson(Map<String, dynamic> json) {
    final chucVuObj = json['chucVu'];
    final phongChucNangObj = json['phongChucNang'];

    return NhanVienModel(
      nhanVienID: json['nhanVienID'] ?? 0,
      thongTinID: json['thongTinID'] ?? 0,
      chucVu: ChucVuModel(
        id: chucVuObj != null ? chucVuObj['id'] : 0,
        name: chucVuObj != null ? chucVuObj['name'] : '',
      ),
      phongChucNang: PhongChucNangModel(
        id: phongChucNangObj != null ? phongChucNangObj['id'] : 0,
        name: phongChucNangObj != null ? phongChucNangObj['name'] : '',
      ),
      hoTen: json['hoTen'] ?? '',
      ngaySinh: json['ngaySinh'] != null
          ? DateTime.parse(json['ngaySinh'])
          : DateTime(0),
      gioiTinh: json['gioiTinh'] ?? '',
      sdt: json['sdt'] ?? '',
      emailLienHe: json['emailLienHe'] ?? '',
      diaChi: json['diaChi'] ?? '',
      avatar: json['avatar'],
      ngayVaoLam: json['ngayVaoLam'] != null
          ? DateTime.parse(json['ngayVaoLam'])
          : DateTime(0),
      bangCap: json['bangCap'] ?? '',
      kinhNghiem: json['kinhNghiem'] ?? '',
      trangThai: json['trangThai'] ?? '',
      ngayTao: json['ngayTao'] != null
          ? DateTime.parse(json['ngayTao'])
          : DateTime(0),
      ngayCapNhat: json['ngayCapNhat'] != null
          ? DateTime.parse(json['ngayCapNhat'])
          : null,
    );
  }
}

class ChucVuModel {
  final int id;
  final String name;

  ChucVuModel({required this.id, required this.name});
}

class PhongChucNangModel {
  final int id;
  final String name;

  PhongChucNangModel({required this.id, required this.name});
}
