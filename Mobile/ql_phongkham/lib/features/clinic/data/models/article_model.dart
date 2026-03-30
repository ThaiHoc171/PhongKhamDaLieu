class BaiVietModel {
  final int baiVietID;
  final String tieuDe;
  final String? tomTat;
  final String? noiDung;
  final String? hinhAnh;
  final int? tacGiaID;
  final int? loaiBenhID;
  final int luotXem;
  final DateTime ngayDang;
  final DateTime? ngayCapNhat;
  final String? trangThai;

  BaiVietModel({
    required this.baiVietID,
    required this.tieuDe,
    this.tomTat,
    this.noiDung,
    this.hinhAnh,
    this.tacGiaID,
    this.loaiBenhID,
    required this.luotXem,
    required this.ngayDang,
    this.ngayCapNhat,
    this.trangThai,
  });

  factory BaiVietModel.fromJson(Map<String, dynamic> json) {
    return BaiVietModel(
      baiVietID: json['baiVietID'],
      tieuDe: json['tieuDe'],
      tomTat: json['tomTat'],
      noiDung: json['noiDung'],
      hinhAnh: json['hinhAnh'],
      tacGiaID: json['tacGiaID'],
      loaiBenhID: json['loaiBenhID'],
      luotXem: json['luotXem'] ?? 0,
      ngayDang: DateTime.parse(json['ngayDang']),
      ngayCapNhat: json['ngayCapNhat'] != null
          ? DateTime.parse(json['ngayCapNhat'])
          : null,
      trangThai: json['trangThai'],
    );
  }
}
