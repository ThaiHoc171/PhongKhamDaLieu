import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:ql_phongkham/core/services/storage_service.dart';

class ApiClient {
  static const String baseUrl =
      "https://clinicjwt-api-bperhwd0dne7c9c0.southeastasia-01.azurewebsites.net/api";

  static Map<String, String> _headers({String? token}) {
    return {
      "Content-Type": "application/json",
      if (token != null) "Authorization": "Bearer $token",
    };
  }

  static Future<bool> _refreshToken() async {
    final refreshToken = await StorageService.getRefreshToken();

    if (refreshToken == null) return false;

    final url = Uri.parse("$baseUrl/auth/refresh");

    final response = await http.post(
      url,
      headers: {"Content-Type": "application/json"},
      body: jsonEncode({"refreshToken": refreshToken}),
    );

    if (response.statusCode == 200) {
      final json = jsonDecode(response.body);
      final data = json['data'];

      await StorageService.saveTokens(
        data['accessToken'],
        data['refreshToken'],
      );

      return true;
    }

    return false;
  }

  static Future<dynamic> _request(
    Future<http.Response> Function(String? token) apiCall,
  ) async {
    String? token = await StorageService.getAccessToken();

    var response = await apiCall(token);

    if (response.statusCode == 401) {
      final refreshed = await _refreshToken();

      if (refreshed) {
        token = await StorageService.getAccessToken();
        response = await apiCall(token);
      } else {
        await StorageService.clear();
        throw Exception("Phiên đăng nhập hết hạn");
      }
    }
    return _handleResponse(response);
  }

  static Future<dynamic> get(String endpoint) async {
    final url = Uri.parse("$baseUrl/$endpoint");

    return _request((token) {
      return http.get(url, headers: _headers(token: token));
    });
  }

  static Future<dynamic> post(
    String endpoint,
    Map<String, dynamic> data,
  ) async {
    final url = Uri.parse("$baseUrl/$endpoint");

    return _request((token) {
      return http.post(
        url,
        headers: _headers(token: token),
        body: jsonEncode(data),
      );
    });
  }

  static Future<dynamic> put(String endpoint, Map<String, dynamic> data) async {
    final url = Uri.parse("$baseUrl/$endpoint");

    return _request((token) {
      return http.put(
        url,
        headers: _headers(token: token),
        body: jsonEncode(data),
      );
    });
  }

  static Future<dynamic> delete(String endpoint) async {
    final url = Uri.parse("$baseUrl/$endpoint");

    return _request((token) {
      return http.delete(url, headers: _headers(token: token));
    });
  }

  static dynamic _handleResponse(http.Response response) {
    print("STATUS: ${response.statusCode}");
    print("BODY: ${response.body}");

    if (response.statusCode >= 200 && response.statusCode < 300) {
      if (response.body.isEmpty) return {};
      return jsonDecode(response.body);
    } else {
      if (response.body.isEmpty) {
        throw Exception("Lỗi server ${response.statusCode}");
      }

      final json = jsonDecode(response.body);
      throw Exception(json["message"] ?? "Lỗi server ${response.statusCode}");
    }
  }
}
