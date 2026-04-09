import 'package:intl/intl.dart';
import 'package:ql_phongkham/core/network/api_client.dart';
import 'package:ql_phongkham/features/clinic/data/models/reexam_model.dart';
import 'package:ql_phongkham/features/clinic/data/models/treatment_model.dart';
import 'package:ql_phongkham/features/clinic/data/models/booking_model.dart';

class LichKhamRepository {
  Future<List<int>> getKhungGioConTrong(DateTime date) async {
    final formattedDate = DateFormat('yyyy-MM-dd').format(date);

    final response = await ApiClient.get(
      "CaKham/khunggio-trong?ngayKham=$formattedDate&loaiCaKham=Khám",
    );

    return List<int>.from(response['data']);
  }

  Future<List<int>> getKhungGioConTrongDieuTri(DateTime date) async {
    final formattedDate = DateFormat('yyyy-MM-dd').format(date);

    final response = await ApiClient.get(
      "CaKham/khunggio-trong?ngayKham=$formattedDate&loaiCaKham=Điều trị",
    );

    return List<int>.from(response['data']);
  }

  Future<int> getCaKhamId(DateTime date, int khungGioId) async {
    final formattedDate = DateFormat('yyyy-MM-dd').format(date);

    final response = await ApiClient.get(
      "CaKham/ca-trong?ngayKham=$formattedDate&khungGioId=$khungGioId&loaiCaKham=Khám",
    );

    return response['data'];
  }

  Future<int> getCaKhamIdDieuTri(DateTime date, int khungGioId) async {
    final formattedDate = DateFormat('yyyy-MM-dd').format(date);

    final response = await ApiClient.get(
      "CaKham/ca-trong?ngayKham=$formattedDate&khungGioId=$khungGioId&loaiCaKham=Điều trị",
    );

    return response['data'];
  }

  Future<bool> checkDangKy(
    DateTime date,
    int khungGioId,
    int thongTinId,
  ) async {
    final formattedDate = DateFormat('yyyy-MM-dd').format(date);

    final response = await ApiClient.get(
      "caKham/check-dadangky?ngay=$formattedDate&khungGioId=$khungGioId&loaiCaKham=Khám&thongTinId=$thongTinId",
    );
    return response['data'];
  }

  Future<bool> checkDangKyDieuTri(
    DateTime date,
    int khungGioId,
    int thongTinId,
  ) async {
    final formattedDate = DateFormat('yyyy-MM-dd').format(date);

    final response = await ApiClient.get(
      "CaKham/kiemtra-dadangky?ngay=$formattedDate&khungGioId=$khungGioId&loaiCaKham=Điều trị&thongTinId=$thongTinId",
    );

    return response['data'];
  }

  Future<String> dangKyKham(int caKhamId, int thongTinId) async {
    final response = await ApiClient.put("caKham/$caKhamId/register", {
      'thongTinID': thongTinId,
      'lyDoKham': 'Khám da liễu',
      'ngayDat': DateTime.now().toIso8601String(),
      'ghiChu': '',
    });
    return response["message"];
  }

  Future<TaiKhamModel?> checkTaiKhamPending(int benhNhanId) async {
    final response = await ApiClient.get("taikham/benhnhan/$benhNhanId");
    if (response == null || response["data"] == null) {
      return null;
    }
    final items = response["data"]["items"];
    if (items == null || items.isEmpty) {
      return null;
    }
    return TaiKhamModel.fromJson(items[0]);
  }

  Future<String> updateTaiKham(int taiKhamId, int caKhamId) async {
    final response = await ApiClient.put("taiKham/$taiKhamId", {
      "trangThai": "Đang xử lý",
      "caKhamID": caKhamId,
    });
    return response["message"] ?? "Cập nhật thành công";
  }

  Future<LieuTrinhDieuTriModel?> checkDieuTriPending(int benhNhanId) async {
    final response = await ApiClient.get("lieutrinh/benhnhan/$benhNhanId");

    if (response == null || response["data"] == null) {
      return null;
    }
    final items = response["data"]["items"];
    if (items == null || items.isEmpty) {
      return null;
    }
    return LieuTrinhDieuTriModel.fromJson(items[0]);
  }

  Future<String> addBuoiDieuTri(int lieuTrinhId, int caKhamId) async {
    final response = await ApiClient.post("LieuTrinh_BuoiDieuTri", {
      "lieuTrinhID": lieuTrinhId,
      "caKhamID": caKhamId,
    });

    return response["message"];
  }

  Future<List<BuoiDieuTriModel>> getBuoiDieuTri(int lieuTrinhId) async {
    final response = await ApiClient.get('buoidieutri/lieutrinh/$lieuTrinhId');

    final items = response['data'];

    return (items as List).map((e) => BuoiDieuTriModel.fromJson(e)).toList();
  }

  Future<List<CaKhamModel>> getCaKhamBenhNhan(
    int thongTinId,
    int pageNumber,
    int pageSize,
  ) async {
    final response = await ApiClient.get(
      "cakham/search/by-thongtin/$thongTinId?pageNumber=$pageNumber&pageSize=$pageSize",
    );

    final List items = response['data']['items'];

    return items.map((e) => CaKhamModel.fromJson(e)).toList();
  }
}
