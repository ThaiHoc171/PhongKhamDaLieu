// lib/features/clinic/presentation/pages/splash_page.dart

import 'package:flutter/material.dart';
import 'package:ql_phongkham/core/network/api_client.dart';
import 'package:ql_phongkham/core/services/storage_service.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/auth/login_page.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/profile/profile_Update_page.dart';
import 'package:ql_phongkham/screen/home_screen/home.dart';

class SplashPage extends StatefulWidget {
  const SplashPage({super.key});

  @override
  State<SplashPage> createState() => _SplashPageState();
}

class _SplashPageState extends State<SplashPage> {
  @override
  void initState() {
    super.initState();
    _checkSession();
  }

  Future<void> _checkSession() async {
    await Future.delayed(const Duration(seconds: 1));

    final rememberMe = await StorageService.getRememberMe();
    final refreshToken = await StorageService.getRefreshToken();

    if (!rememberMe || refreshToken == null) {
      await StorageService.clear();
      _go(const LoginPage());
      return;
    }

    final ok = await _tryRefresh(refreshToken);
    if (!ok) {
      await StorageService.clear();
      _go(const LoginPage());
      return;
    }

    final thongTinId = await StorageService.getThongTinId();
    final accessToken = await StorageService.getAccessToken();

    if (thongTinId == 0) {
      _go(ProfileUpdateScreen());
    } else {
      _go(HomeScreen(token: accessToken ?? ''));
    }
  }

  Future<bool> _tryRefresh(String refreshToken) async {
    try {
      final response = await ApiClient.post('auth/refresh', {
        'refreshToken': refreshToken,
      });
      final data = response['data'];
      await StorageService.saveTokens(
        data['accessToken'],
        data['refreshToken'],
      );
      return true;
    } catch (_) {
      return false;
    }
  }

  void _go(Widget page) {
    if (!mounted) return;
    Navigator.pushReplacement(context, MaterialPageRoute(builder: (_) => page));
  }

  @override
  Widget build(BuildContext context) {
    return const Scaffold(body: Center(child: CircularProgressIndicator()));
  }
}
