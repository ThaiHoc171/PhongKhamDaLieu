import 'package:intl/intl.dart';
import 'package:ql_phongkham/core/network/api_client.dart';

class LichKhamRepository {
  Future<List<int>> getKhungGioConTrong(DateTime date, String token) async {
    final formattedDate = DateFormat('yyyy-MM-dd').format(date);

    final response = await ApiClient.get(
      "CaKham/khunggio-trong?ngayKham=$formattedDate&loaiCaKham=Khám",
      token: token,
    );

    return List<int>.from(response['data']);
  }

  Future<int> getCaKhamId(DateTime date, int khungGioId, String token) async {
    final formattedDate = DateFormat('yyyy-MM-dd').format(date);

    final response = await ApiClient.get(
      "CaKham/ca-trong?ngayKham=$formattedDate&khungGioId=$khungGioId&loaiCaKham=Khám",
      token: token,
    );

    return response['data'];
  }

  Future<bool> checkDangKy(
    DateTime date,
    int khungGioId,
    int benhNhanId,
    String token,
  ) async {
    final formattedDate = DateFormat('yyyy-MM-dd').format(date);

    final response = await ApiClient.get(
      "CaKham/kiemtra-dadangky?ngay=$formattedDate&khungGioId=$khungGioId&loaiCaKham=Khám&benhNhanId=$benhNhanId",
      token: token,
    );

    return response['data'];
  }

  Future<String> dangKyKham(int caKhamId, int benhNhanId, String token) async {
    final response = await ApiClient.put("CaKham/$caKhamId/dangky", {
      'benhNhanID': benhNhanId,
      'lyDoKham': 'Khám da liễu',
      'ngayDat': DateTime.now().toIso8601String(),
      'ghiChu': '',
    }, token: token);

    return response["message"];
  }
}
