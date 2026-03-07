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
    }, token: token);
    return response['benhNhanID'];
  }
}
