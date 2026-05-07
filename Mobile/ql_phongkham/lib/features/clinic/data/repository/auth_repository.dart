import 'package:ql_phongkham/core/network/api_client.dart';
import 'package:ql_phongkham/core/services/storage_service.dart';
import 'package:ql_phongkham/features/clinic/data/models/user_model.dart';

class AuthRepository {
  Future<UserModel> login(
    String email,
    String password,
    bool rememberMe,
  ) async {
    final response = await ApiClient.post('auth/login', {
      "email": email,
      "matKhau": password,
    });

    final user = UserModel.fromJson(response['data']);
    await StorageService.saveUser(user, rememberMe: rememberMe);
    return user;
  }

  Future<void> logout() async {
    final refreshToken = await StorageService.getRefreshToken();
    if (refreshToken != null) {
      try {
        await ApiClient.post('auth/logout', {'refreshToken': refreshToken});
      } catch (_) {
        //
      }
    }
    await StorageService.clear();
  }

  Future<void> signup(String email, String password, String vaitro) async {
    final response = await ApiClient.post('taikhoan', {
      "email": email,
      "matKhau": password,
      "vaiTro": vaitro,
    });
    return response;
  }

  Future<void> resetpassword(
    String password,
    String newpassword,
    int taiKhoanId,
  ) async {
    final response = await ApiClient.put('taikhoan/$taiKhoanId/password', {
      "matKhauCu": password,
      "matKhauMoi": newpassword,
    });
    return response;
  }

  Future<void> forgetpassword(int taiKhoanId) async {
    final response = await ApiClient.put(
      'taikhoan/$taiKhoanId/reset-password',
      {},
      requiresAuth: false,
    );
    return response;
  }

  Future<int> getIdByEmail(String email) async {
    final encodedEmail = Uri.encodeComponent(email);
    final response = await ApiClient.get(
      'taikhoan/getIdByEmail/$encodedEmail',
      requiresAuth: false,
    );
    return response['data'];
  }

  Future<void> updateFCM(String fcmToken, int taiKhoanId) async {
    final response = await ApiClient.put('taikhoan/$taiKhoanId/fcm-token', {
      "fcmToken": fcmToken,
    });
    return response;
  }

  Future<void> sendOtp(String email) async {
    final response = await ApiClient.post('otp/tao', {"email": email});
    return response;
  }

  Future<void> verifyOtp(String email, String otp) async {
    final response = await ApiClient.post("otp/xac-thuc", {
      "email": email,
      "maOTP": otp,
    });
    return response;
  }
}
