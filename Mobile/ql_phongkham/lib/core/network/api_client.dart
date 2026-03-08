import 'package:http/http.dart' as http;
import 'dart:convert';

class ApiClient {
  static const String baseUrl =
      "https://clinicjwt-api-bperhwd0dne7c9c0.southeastasia-01.azurewebsites.net/api";

  static Map<String, String> _headers({String? token}) {
    return {
      "Content-Type": "application/json",
      if (token != null) "Authorization": "Bearer $token",
    };
  }

  static Future<dynamic> post(
    String endpoint,
    Map<String, dynamic> data, {
    String? token,
  }) async {
    final url = Uri.parse("$baseUrl/$endpoint");

    final response = await http.post(
      url,
      headers: _headers(token: token),
      body: jsonEncode(data),
    );

    return _handleResponse(response);
  }

  static Future<dynamic> get(String endpoint, {String? token}) async {
    final url = Uri.parse("$baseUrl/$endpoint");

    final response = await http.get(url, headers: _headers(token: token));

    return _handleResponse(response);
  }

  static Future<dynamic> put(
    String endpoint,
    Map<String, dynamic> data, {
    String? token,
  }) async {
    final url = Uri.parse("$baseUrl/$endpoint");

    final response = await http.put(
      url,
      headers: _headers(token: token),
      body: jsonEncode(data),
    );

    return _handleResponse(response);
  }

  static Future<dynamic> delete(String endpoint, {String? token}) async {
    final url = Uri.parse("$baseUrl/$endpoint");

    final response = await http.delete(url, headers: _headers(token: token));

    return _handleResponse(response);
  }

  static dynamic _handleResponse(http.Response response) {
    print("STATUS: ${response.statusCode}");
    print("BODY: ${response.body}");

    if (response.statusCode >= 200 && response.statusCode < 300) {
      if (response.body.isEmpty) {
        return {};
      }

      try {
        return jsonDecode(response.body);
      } catch (e) {
        return response.body;
      }
    } else {
      if (response.body.isEmpty) {
        throw Exception("Lỗi server ${response.statusCode}");
      }

      try {
        final json = jsonDecode(response.body);
        throw Exception(json["message"] ?? "Lỗi server ${response.statusCode}");
      } catch (e) {
        throw Exception(response.body);
      }
    }
  }
}
