import 'package:ql_phongkham/core/network/api_client.dart';
import 'package:ql_phongkham/features/clinic/data/models/medical_record_model.dart';

class HoSoBenhAnRepository {
  Future<HoSoBenhAnModel> getHoSo(int benhNhanId) async {
    final response = await ApiClient.get('hosobenhan/benhnhan/$benhNhanId');
    return HoSoBenhAnModel.fromJson(response['data']);
  }
}
