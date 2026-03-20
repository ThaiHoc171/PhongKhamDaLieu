import 'package:ql_phongkham/features/clinic/data/models/user_model.dart';
import 'package:shared_preferences/shared_preferences.dart';

class StorageService {
  static Future<void> saveUser(UserModel user) async {
    final prefs = await SharedPreferences.getInstance();

    await prefs.setInt('userId', user.id);
    await prefs.setString('email', user.email);
    await prefs.setString('vaiTro', user.vaiTro);
    await prefs.setString('accessToken', user.accessToken);
    await prefs.setString('refreshToken', user.refreshToken);
    await prefs.setString('chucVu', user.chucVu ?? '');
    await prefs.setString('hoTen', user.hoTen ?? '');
    await prefs.setInt('thongTinId', user.thongTinId ?? 0);
    await prefs.setInt('nhanVienId', user.nhanVienId ?? 0);
    await prefs.setInt('benhNhanId', user.benhNhanId ?? 0);
  }

  static Future<String?> getAccessToken() async {
    final prefs = await SharedPreferences.getInstance();
    return prefs.getString('accessToken');
  }

  static Future<String?> getRefreshToken() async {
    final prefs = await SharedPreferences.getInstance();
    return prefs.getString('refreshToken');
  }

  static Future<void> saveTokens(String access, String refresh) async {
    final prefs = await SharedPreferences.getInstance();
    await prefs.setString('accessToken', access);
    await prefs.setString('refreshToken', refresh);
  }

  static Future<void> clear() async {
    final prefs = await SharedPreferences.getInstance();
    await prefs.clear();
  }
}
