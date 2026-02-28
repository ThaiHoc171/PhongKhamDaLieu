import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class SecureStorage {
  final _storage = const FlutterSecureStorage();

  Future<void> saveToken(String token) async {
    await _storage.write(key: "ACCESS_TOKEN", value: token);
  }

  Future<String?> getToken() async {
    return await _storage.read(key: "ACCESS_TOKEN");
  }

  Future<void> clear() async {
    await _storage.deleteAll();
  }
}