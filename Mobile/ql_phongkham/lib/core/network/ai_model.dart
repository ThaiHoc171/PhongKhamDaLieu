// core/network/ai_model.dart
import 'dart:io';
import 'package:http/http.dart' as http;
import 'dart:convert';

class AiModel {
  static const String _baseUrl =
      'https://eddie0307-efficientnetb3-classifier.hf.space';

  static Future<Map<String, dynamic>> postFile(
    String endpoint,
    File file,
  ) async {
    final uri = Uri.parse('$_baseUrl/$endpoint');

    final request = http.MultipartRequest('POST', uri)
      ..headers['accept'] = 'application/json'
      ..files.add(await http.MultipartFile.fromPath('file', file.path));

    final streamedResponse = await request.send();
    final response = await http.Response.fromStream(streamedResponse);

    if (response.statusCode == 200) {
      return jsonDecode(response.body) as Map<String, dynamic>;
    } else {
      throw Exception('Lỗi ${response.statusCode}: ${response.body}');
    }
  }
}
