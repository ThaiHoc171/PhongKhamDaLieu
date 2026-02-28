import 'package:flutter/cupertino.dart';

import '../../domain/usecase/login_usecase.dart';

class AuthController extends ChangeNotifier {
  final LoginUseCase loginUseCase;

  bool loading = false;

  AuthController(this.loginUseCase);

  Future<void> login(String email, String password) async {
    loading = true;
    notifyListeners();

    await loginUseCase(email, password);

    loading = false;
    notifyListeners();
  }
}