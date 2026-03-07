import 'package:ql_phongkham/core/network/api_client.dart';
import 'package:ql_phongkham/features/clinic/data/models/profile_model.dart';

class ProfileRepository {
  Future<ProfileModel> getProfile(String token, int thongTinId) async {
    final response = await ApiClient.get(
      '/ThongTinCaNhan/$thongTinId',
      token: token,
    );
    return ProfileModel.fromJson(response['data']);
  }

  Future<ProfileModel> addProfile(String token, int thongTinId) async {
    final response = await ApiClient.get(
      '/ThongTinCaNhan/$thongTinId',
      token: token,
    );
    return ProfileModel.fromJson(response['data']);
  }
}
