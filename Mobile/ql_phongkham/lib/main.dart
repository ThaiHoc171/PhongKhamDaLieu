import 'package:flutter/material.dart';
import 'package:intl/date_symbol_data_local.dart';
import 'package:ql_phongkham/core/theme/theme.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/auth/login_page.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await initializeDateFormatting('vi_VN', null);
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      theme: AppTheme.lightThemeMode,
      home: const LoginPage(),
    );
  }
}
