import 'dart:convert';
import 'dart:io';
import 'package:http/http.dart' as http;
import 'package:ql_phongkham/core/services/storage_service.dart';

class UploadRepository {
  static const String baseUrl =
      "https://clinicjwt-api-bperhwd0dne7c9c0.southeastasia-01.azurewebsites.net/api";

  Future<String> uploadImage(File file) async {
    final token = await StorageService.getAccessToken();

    var request = http.MultipartRequest(
      'POST',
      Uri.parse("$baseUrl/upload/image"),
    );
    request.headers['Authorization'] = "Bearer $token";
    request.files.add(await http.MultipartFile.fromPath('file', file.path));
    request.fields['folder'] = "profile";
    final response = await request.send();
    final resBody = await response.stream.bytesToString();
    if (response.statusCode == 200) {
      final json = jsonDecode(resBody);
      return json['url'];
    } else {
      throw Exception("Upload ảnh thất bại");
    }
  }
}
