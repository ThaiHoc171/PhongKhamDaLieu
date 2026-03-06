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
    await prefs.setString('hoTen', user.hoTen);
    await prefs.setInt(
      'thongTinId',
      user.thongTinId == null ? 0 : user.thongTinId as int,
    );
    await prefs.setInt(
      'nhanVienId',
      user.nhanVienId == null ? 0 : user.nhanVienId as int,
    );
    await prefs.setInt(
      'benhNhanId',
      user.benhNhanId == null ? 0 : user.benhNhanId as int,
    );
  }
}
