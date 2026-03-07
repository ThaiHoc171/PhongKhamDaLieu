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

  Future<ProfileModel> addProfile(
    int taiKhoanId,
    String hoTen,
    DateTime ngaySinh,
    String gioiTinh,
    String sdt,
    String emailLienhe,
    String diaChi,
    String avatar,
    String ghichu,
  ) async {
    final response = await ApiClient.post('/BenhNhan', {
      "thongTinID": 0,
      "taiKhoanID": taiKhoanId,
      "hoTen": hoTen,
      "ngaySinh": ngaySinh,
      "gioiTinh": gioiTinh,
      "sdt": sdt,
      "emailLienHe": emailLienhe,
      "diaChi": diaChi,
      "avatar": avatar,
      "ghiChu": ghichu,
    });
    return response;
  }
}
