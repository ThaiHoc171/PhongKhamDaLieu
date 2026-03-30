import 'package:ql_phongkham/core/network/api_client.dart';
import 'package:ql_phongkham/features/clinic/data/models/article_model.dart';

class BaiVietRepository {
  Future<List<BaiVietModel>> getListBaiViet() async {
    final response = await ApiClient.get('baiviet?page=1&size=10');
    final items = response['data']['items'];
    return (items as List).map((e) => BaiVietModel.fromJson(e)).toList();
  }

  Future<BaiVietModel> getBaiViet(int baiVietId) async {
    final response = await ApiClient.get('baiviet/$baiVietId');
    return BaiVietModel.fromJson(response['data']);
  }
}
