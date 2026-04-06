import 'package:ql_phongkham/core/network/api_client.dart';
import 'package:ql_phongkham/features/clinic/data/models/detail_exam_model.dart';

class PhienKhamRepository {
  Future<PhienKhamModel> getChiTietPhienKham(int caKhamId) async {
    final response = await ApiClient.get("phienkham/cakham/$caKhamId");

    if (response['success'] == false) {
      throw Exception(response['message'] ?? 'Có lỗi xảy ra');
    }

    return PhienKhamModel.fromJson(response['data']);
  }
}
