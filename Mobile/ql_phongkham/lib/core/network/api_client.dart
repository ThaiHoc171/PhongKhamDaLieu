import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:ql_phongkham/core/services/storage_service.dart';

class ApiClient {
  static const String baseUrl =
      "https://clinicjwt-api-bperhwd0dne7c9c0.southeastasia-01.azurewebsites.net/api";
  static const _timeout = Duration(seconds: 15);

  static Map<String, String> _headers({String? token}) => {
    "Content-Type": "application/json",
    if (token != null) "Authorization": "Bearer $token",
  };

  static Future<bool> _refreshToken() async {
    final refreshToken = await StorageService.getRefreshToken();
    if (refreshToken == null) return false;
    try {
      final response = await http
          .post(
            Uri.parse("$baseUrl/auth/refresh"),
            headers: {"Content-Type": "application/json"},
            body: jsonEncode({"refreshToken": refreshToken}),
          )
          .timeout(_timeout);
      if (response.statusCode == 200) {
        final data = jsonDecode(response.body)['data'];
        await StorageService.saveTokens(
          data['accessToken'],
          data['refreshToken'],
        );
        return true;
      }
    } catch (_) {}
    return false;
  }

  // ✅ Thêm tham số requiresAuth
  static Future<dynamic> _request(
    Future<http.Response> Function(String? token) apiCall, {
    bool requiresAuth = true,
  }) async {
    print("[ApiClient] requiresAuth = $requiresAuth");
    if (!requiresAuth) {
      final response = await apiCall(null).timeout(_timeout);
      return _handleResponse(response);
    }

    String? token = await StorageService.getAccessToken();
    var response = await apiCall(token).timeout(_timeout);

    if (response.statusCode == 401) {
      final refreshed = await _refreshToken();
      if (refreshed) {
        token = await StorageService.getAccessToken();
        response = await apiCall(token).timeout(_timeout);
      } else {
        await StorageService.clear();
        throw Exception("Phiên đăng nhập hết hạn");
      }
    }

    return _handleResponse(response);
  }

  static Future<dynamic> get(String endpoint, {bool requiresAuth = true}) =>
      _request(
        (token) => http.get(
          Uri.parse("$baseUrl/$endpoint"),
          headers: _headers(token: token),
        ),
        requiresAuth: requiresAuth,
      );

  static Future<dynamic> post(
    String endpoint,
    Map<String, dynamic> data, {
    bool requiresAuth = true,
  }) => _request(
    (token) => http.post(
      Uri.parse("$baseUrl/$endpoint"),
      headers: _headers(token: token),
      body: jsonEncode(data),
    ),
    requiresAuth: requiresAuth,
  );

  static Future<dynamic> put(
    String endpoint,
    Map<String, dynamic> data, {
    bool requiresAuth = true,
  }) => _request(
    (token) => http.put(
      Uri.parse("$baseUrl/$endpoint"),
      headers: _headers(token: token),
      body: jsonEncode(data),
    ),
    requiresAuth: requiresAuth,
  );

  static Future<dynamic> delete(String endpoint, {bool requiresAuth = true}) =>
      _request(
        (token) => http.delete(
          Uri.parse("$baseUrl/$endpoint"),
          headers: _headers(token: token),
        ),
        requiresAuth: requiresAuth,
      );

  static dynamic _handleResponse(http.Response response) {
    assert(() {
      print("STATUS: ${response.statusCode}");
      return true;
    }());
    if (response.statusCode >= 200 && response.statusCode < 300) {
      if (response.body.isEmpty) return {};
      return jsonDecode(response.body);
    }
    final message = response.body.isNotEmpty
        ? (jsonDecode(response.body)["message"] ?? "Lỗi ${response.statusCode}")
        : "Lỗi server ${response.statusCode}";
    throw Exception(message);
  }
}
