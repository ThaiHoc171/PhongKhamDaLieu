import 'package:ql_phongkham/core/network/api_client.dart';
import 'package:ql_phongkham/features/clinic/data/models/user_model.dart';

class AuthRepository {
  Future<UserModel> login(String email, String password) async {
    final response = await ApiClient.post('auth/login', {
      "email": email,
      "matKhau": password,
    });

    return UserModel.fromJson(response['data']);
  }

  Future<void> signup(String email, String password, String vaitro) async {
    final response = await ApiClient.post('taiKhoan', {
      "email": email,
      "matKhau": password,
      "vaiTro": vaitro,
    });
    return response;
  }
}
