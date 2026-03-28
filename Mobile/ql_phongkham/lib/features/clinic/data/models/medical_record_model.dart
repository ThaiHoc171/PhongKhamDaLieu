class HoSoBenhAnModel {
  final int hoSoBenhAnID;
  final int benhNhanID;
  final String benhNen;
  final String diUng;
  final String tienSuBenh;
  final String tienSuGiaDinh;
  final String thoiQuenSong;
  final String thongTinKhac;
  final DateTime ngayTao;
  final DateTime ngayCapNhat;

  HoSoBenhAnModel({
    required this.hoSoBenhAnID,
    required this.benhNhanID,
    required this.benhNen,
    required this.diUng,
    required this.tienSuBenh,
    required this.tienSuGiaDinh,
    required this.thoiQuenSong,
    required this.thongTinKhac,
    required this.ngayTao,
    required this.ngayCapNhat,
  });

  factory HoSoBenhAnModel.fromJson(Map<String, dynamic> json) {
    return HoSoBenhAnModel(
      hoSoBenhAnID: json['hoSoBenhAnID'],
      benhNhanID: json['benhNhanID'],
      benhNen: json['benhNen'] ?? '',
      diUng: json['diUng'] ?? '',
      tienSuBenh: json['tienSuBenh'] ?? '',
      tienSuGiaDinh: json['tienSuGiaDinh'] ?? '',
      thoiQuenSong: json['thoiQuenSong'] ?? '',
      thongTinKhac: json['thongTinKhac'] ?? '',
      ngayTao: DateTime.parse(json['ngayTao']),
      ngayCapNhat: DateTime.parse(json['ngayCapNhat']),
    );
  }
}
