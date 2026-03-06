import 'package:ql_phongkham/core/network/api_client.dart';
import 'package:ql_phongkham/features/clinic/data/models/user_model.dart';

class AuthRepository {
  Future<UserModel> login(String email, String password) async {
    final response = await ApiClient.post('/TaiKhoan/dangnhap', {
      "email": email,
      "matKhau": password,
    });

    return UserModel.fromJson(response);
  }
}
