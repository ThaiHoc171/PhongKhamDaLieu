import 'package:ql_phongkham/core/network/api_client.dart';
import 'package:ql_phongkham/features/clinic/data/models/profile_model.dart';

class ProfileRepository {
  Future<ProfileModel> getProfile(int thongTinId) async {
    final response = await ApiClient.get('ThongTinCaNhan/$thongTinId');
    return ProfileModel.fromJson(response['data']);
  }

  Future<void> addProfile(
    int taiKhoanId,
    String hoTen,
    DateTime ngaySinh,
    String gioiTinh,
    String sdt,
    String emailLienhe,
    String diaChi,
    String avatar,
  ) async {
    await ApiClient.post('thongtincanhan', {
      "taiKhoanID": taiKhoanId,
      "hoTen": hoTen,
      "ngaySinh": ngaySinh.toIso8601String(),
      "gioiTinh": gioiTinh,
      "sdt": sdt,
      "emailLienHe": emailLienhe,
      "diaChi": diaChi,
      "avatar": avatar,
    });
  }

  Future<void> putProfile(
    int thongTinId,
    String hoTen,
    DateTime ngaySinh,
    String gioiTinh,
    String sdt,
    String emailLienHe,
    String diaChi,
    String avatar,
    String loai,
  ) async {
    await ApiClient.put('/ThongTinCaNhan/$thongTinId', {
      "hoTen": hoTen,
      "ngaySinh": ngaySinh.toIso8601String(),
      "gioiTinh": gioiTinh,
      "sdt": sdt,
      "emailLienHe": emailLienHe,
      "diaChi": diaChi,
      "avatar": avatar,
      "loai": loai,
    });
  }
}
