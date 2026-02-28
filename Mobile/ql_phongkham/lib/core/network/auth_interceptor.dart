import 'package:dio/dio.dart';

class AuthInterceptor extends Interceptor {
  final SecureStorage storage;

  AuthInterceptor(this.storage);

  @override
  void onRequest(
      RequestOptions options, RequestInterceptorHandler handler) async {

    final token = await storage.getToken();

    if (token != null) {
      options.headers["Authorization"] = "Bearer $token";
    }

    return handler.next(options);
  }
}