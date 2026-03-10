import 'package:ql_phongkham/core/network/api_client.dart';
import 'package:ql_phongkham/features/clinic/data/models/booking_model.dart';

class CaKhamChiTietRepository {
  Future<List<CaKhamModel>> getCaKhamBenhNhan(
    String token,
    int thongTinId,
    int pageNumber,
    int pageSize,
  ) async {
    final response = await ApiClient.get(
      "/CaKham/benhnhan/$thongTinId?pageNumber=$pageNumber&pageSize=$pageSize",
      token: token,
    );

    final List items = response['data']['items'];

    return items.map((e) => CaKhamModel.fromJson(e)).toList();
  }
}
