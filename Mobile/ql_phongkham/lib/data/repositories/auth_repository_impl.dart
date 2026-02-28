import '../../core/storage/secure_storage.dart';
import '../../domain/entities/user.dart';
import '../../domain/repositories/auth_repository.dart';
import '../datasources/remote/auth_remote_datasource.dart';

class AuthRepositoryImpl implements AuthRepository {
  final AuthRemoteDataSource remote;
  final SecureStorage storage;

  AuthRepositoryImpl(this.remote, this.storage);

  @override
  Future<User> login(String email, String password) async {
    final userModel = await remote.login(email, password);

    await storage.saveToken(userModel.accessToken);

    return userModel;
  }

  @override
  Future<User> getProfile() async {
    // call API profile
    throw UnimplementedError();
  }
}