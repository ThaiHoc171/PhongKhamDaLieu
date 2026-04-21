import 'package:ql_phongkham/core/network/api_client.dart';
import 'package:ql_phongkham/features/clinic/data/models/doctor_profile_model.dart';
import 'package:ql_phongkham/features/clinic/data/models/employ_model.dart';

class BacsiProfileRepository {
  Future<List<BacSiProfileModel>> getBacSiProfile() async {
    final response = await ApiClient.get('bacsi?pageNumber=1&pageSize=10');

    final items = response['data']['items'];

    return (items as List).map((e) => BacSiProfileModel.fromJson(e)).toList();
  }

  Future<BacSiProfileModel> geChiTiettBacSi(int bacSiId) async {
    final response = await ApiClient.get('bacsi/$bacSiId');
    return BacSiProfileModel.fromJson(response['data']);
  }
}

class NhanVienRepository {
  Future<NhanVienModel> getNhanVien(int nhanVienId) async {
    final response = await ApiClient.get('nhanvien/$nhanVienId');
    return NhanVienModel.fromJson(response['data']);
  }

  Future<List<int>> getIdBacSiKham() async {
    final response = await ApiClient.get("nhanvien/combobox?chucVuId=1");
    return response['data']['id'];
  }
}
