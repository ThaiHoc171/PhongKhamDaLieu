import 'dart:convert';
import 'dart:io';
import 'package:http/http.dart' as http;
import 'package:ql_phongkham/core/services/storage_service.dart';
import 'package:ql_phongkham/core/constants/app_config.dart';

class UploadRepository {
  Future<String> uploadImage(File file) async {
    final token = await StorageService.getAccessToken();

    var request = http.MultipartRequest(
      'POST',
      Uri.parse("${AppConfig.baseUrl}/upload/image"),
    );

    request.headers['Authorization'] = "Bearer $token";
    request.files.add(await http.MultipartFile.fromPath('file', file.path));
    request.fields['folder'] = "profile";

    final response = await request.send();
    final resBody = await response.stream.bytesToString();

    if (response.statusCode == 200) {
      final json = jsonDecode(resBody);
      print(">>> upload response: $json"); // xem full response
      final fullUrl = json['url'];
      print(">>> full URL từ server: $fullUrl"); // xem URL gốc trước khi cắt
      final path = Uri.parse(fullUrl).path;
      return path.startsWith('/') ? path.substring(1) : path;
    } else {
      throw Exception("Upload ảnh thất bại");
    }
  }
}
