import 'package:ql_phongkham/core/network/api_client.dart';
import 'package:ql_phongkham/features/clinic/data/models/detail_exam_model.dart';

class PhienKhamRepository {
  Future<PhienKhamModel> getChiTietPhienKham(String token, int caKhamId) async {
    final response = await ApiClient.get(
      "PhienKham/CaKham//$caKhamId",
      token: token,
    );
    return PhienKhamModel.fromJson(response['data']);
  }
}
