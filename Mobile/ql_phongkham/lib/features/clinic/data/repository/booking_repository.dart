import 'dart:convert';
import 'package:ql_phongkham/core/network/api_client.dart';

class ReBookingRepository {
  Future<bool> checkTaiKham(String token, int benhNhanId) async {
    final response = await ApiClient.get(
      '/TaiKham/Benhnhan/$benhNhanId',
      token: token,
    );
    if (response is List) {
      return response.isNotEmpty;
    }
    return false;
  }
}
