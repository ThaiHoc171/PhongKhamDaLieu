import 'package:ql_phongkham/core/network/api_client.dart';
import 'package:ql_phongkham/features/clinic/data/models/doctor_profile_model.dart';

class BacsiProfileRepository {
  Future<List<BacSiProfileModel>> getBacSiProfile(String token) async {
    final response = await ApiClient.get('BacSiProfile', token: token);
    return (response as List)
        .map((e) => BacSiProfileModel.fromJson(e))
        .toList();
  }
}
