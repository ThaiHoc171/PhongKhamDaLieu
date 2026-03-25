import 'package:ql_phongkham/core/network/api_client.dart';
import 'package:ql_phongkham/features/clinic/data/models/doctor_profile_model.dart';

class BacsiProfileRepository {
  Future<List<BacSiProfileModel>> getBacSiProfile() async {
    final response = await ApiClient.get('bacsi?pageNumber=1&pageSize=10');

    final items = response['data']['items'];

    return (items as List).map((e) => BacSiProfileModel.fromJson(e)).toList();
  }
}
