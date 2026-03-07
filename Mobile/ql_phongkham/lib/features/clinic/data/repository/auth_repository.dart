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

  Future<void> signup(String email, String password, String vaitro) async {
    final response = await ApiClient.post('/TaiKhoan/dangky', {
      "email": email,
      "matKhau": password,
      "vaiTro": vaitro,
    });
    return response;
  }
}
