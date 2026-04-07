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
    );
    return response;
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
