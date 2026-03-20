import 'package:ql_phongkham/core/network/api_client.dart';
import 'package:ql_phongkham/features/clinic/data/models/profile_model.dart';

class ProfileRepository {
  Future<ProfileModel> getProfile(int thongTinId) async {
    final response = await ApiClient.get('ThongTinCaNhan/$thongTinId');
    return ProfileModel.fromJson(response['data']);
  }

  Future<int> addProfile(
    int taiKhoanId,
    String hoTen,
    DateTime ngaySinh,
    String gioiTinh,
    String sdt,
    String emailLienhe,
    String diaChi,
    String avatar,
    String ghichu,
    String token,
  ) async {
    final response = await ApiClient.post('BenhNhan', {
      "taiKhoanID": taiKhoanId,
      "hoTen": hoTen,
      "ngaySinh": ngaySinh.toIso8601String(),
      "gioiTinh": gioiTinh,
      "sdt": sdt,
      "emailLienHe": emailLienhe,
      "diaChi": diaChi,
      "avatar": avatar,
      "ghiChu": ghichu,
    });
    return response['benhNhanID'];
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
    String token,
  ) async {
    await ApiClient.put('/ThongTinCaNhan/$thongTinId', {
      "hoTen": hoTen,
      "ngaySinh": ngaySinh.toIso8601String(),
      "gioiTinh": gioiTinh,
      "sdt": sdt,
      "emailLienHe": emailLienHe,
      "diaChi": diaChi,
      "avatar": avatar,
    });
  }
}
