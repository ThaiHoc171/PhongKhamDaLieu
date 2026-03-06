import 'dart:convert';
import 'package:http/http.dart' as http;

class ReBookingRepository {
  Future<bool> checkTaiKham(String token, int benhNhanId) async {
    final url =
        "https://clinicjwt-api-bperhwd0dne7c9c0.southeastasia-01.azurewebsites.net/api";

    final response = await http.get(
      Uri.parse(url),
      headers: {'Authorization': 'Bearer $token', 'accept': '*/*'},
    );

    if (response.statusCode == 200) {
      final data = jsonDecode(response.body);

      if (data is List && data.isEmpty) {
        return false;
      }
      return true;
    }

    if (response.statusCode == 404) {
      return false;
    }

    throw Exception("Server error ${response.statusCode}");
  }
}
