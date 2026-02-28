import '../../../core/network/api_client.dart';
import '../../../core/network/api_constaint.dart';
import '../../models/user_model.dart';

class AuthRemoteDataSource {
  final ApiClient apiClient;

  AuthRemoteDataSource(this.apiClient);

  Future<UserModel> login(String email, String password) async {
    final response = await apiClient.post(
      ApiConstants.login,
      {
        "email": email,
        "password": password,
      },
    );

    return UserModel.fromJson(response.data);
  }
}