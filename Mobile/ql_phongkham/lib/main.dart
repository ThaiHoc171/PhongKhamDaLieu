import 'package:http/http.dart' as http;
import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:intl/date_symbol_data_local.dart';
import 'package:ql_phongkham/core/theme/theme.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/auth/login_page.dart';
import 'package:ql_phongkham/screen/home_screen/home.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:flutter/services.dart';

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

class LoginScreen extends StatefulWidget {
  @override
  State<StatefulWidget> createState() {
    return LoginScreenState();
  }
}

class LoginScreenState extends State<LoginScreen> {
  final _formKey = GlobalKey<FormState>();

  final TextEditingController usernameController = TextEditingController();
  final TextEditingController passwordController = TextEditingController();

  bool loading = false;
  bool _submitted = false;
  bool _isHidden = true;

  String? errorMessage;

  Future<void> loginApi() async {
    final url = Uri.parse(
      'https://clinicjwt-api-bperhwd0dne7c9c0.southeastasia-01.azurewebsites.net/api/TaiKhoan/dangnhap',
    );

    try {
      final response = await http.post(
        url,
        headers: {'Content-Type': 'application/json'},
        body: jsonEncode({
          'email': usernameController.text,
          'matKhau': passwordController.text,
        }),
      );
      if (response.body.isEmpty) {
        setState(() {
          loading = false;
          errorMessage = "Server không trả dữ liệu!";
        });
        return;
      }
      if (response.statusCode == 200) {
        setState(() {
          loading = false;
        });
        final data = jsonDecode(response.body);

        final prefs = await SharedPreferences.getInstance();
        await prefs.setInt('userId', data['id'] ?? 0);
        await prefs.setString('email', data['email'] ?? '');
        await prefs.setString('vaiTro', data['vaiTro'] ?? '');
        await prefs.setString('accessToken', data['accessToken'] ?? '');
        await prefs.setString('refreshToken', data['refreshToken'] ?? '');

        await prefs.setInt(
          'nhanVienId',
          data['nhanVienId'] == null ? 0 : data['nhanVienId'] as int,
        );

        await prefs.setInt(
          'benhNhanId',
          data['benhNhanId'] == null ? 0 : data['benhNhanId'] as int,
        );

        await prefs.setString('chucVu', data['chucVu'] ?? '');
        await prefs.setString('hoTen', data['hoTen'] ?? '');

        Navigator.pushAndRemoveUntil(
          context,
          MaterialPageRoute(
            builder: (_) => HomeScreen(token: data['accessToken']),
          ),
          (route) => false,
        );
      } else {
        final data = jsonDecode(response.body);
        setState(() {
          loading = false;
          errorMessage = data['message'] ?? "Đăng nhập thất bại!";
        });
      }
    } catch (e) {
      print("Lỗi chi tiết: $e");
      setState(() {
        loading = false;
        errorMessage = "Lỗi: $e";
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () => FocusScope.of(context).unfocus(),
      child: Scaffold(
        resizeToAvoidBottomInset: true,
        body: Container(
          decoration: BoxDecoration(
            image: DecorationImage(
              image: AssetImage('assets/images/bg.jpg'),
              fit: BoxFit.cover,
            ),
          ),
          child: Center(
            child: Container(
              width: 300,
              height: 300,
              padding: EdgeInsets.all(20),
              decoration: BoxDecoration(
                border: Border.all(width: 2),
                borderRadius: BorderRadius.circular(25),
                color: Colors.white,
              ),
              child: loginForm(),
            ),
          ),
        ),
      ),
    );
  }

  @override
  void dispose() {
    usernameController.dispose();
    passwordController.dispose();
    super.dispose();
  }

  Widget loginForm() {
    return Form(
      key: _formKey,
      autovalidateMode: _submitted
          ? AutovalidateMode.onUserInteraction
          : AutovalidateMode.disabled,
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          Text(
            'Đăng nhập',
            style: TextStyle(
              color: Colors.blue,
              fontSize: 20,
              fontWeight: FontWeight.bold,
            ),
          ),
          const SizedBox(height: 20),
          SizedBox(
            width: 175,
            height: 45,
            child: TextFormField(
              controller: usernameController,
              textInputAction: TextInputAction.next,
              onFieldSubmitted: (_) {
                FocusScope.of(context).nextFocus();
              },
              decoration: const InputDecoration(
                labelText: 'Email',
                labelStyle: TextStyle(fontSize: 12),
              ),
              validator: (value) {
                if (value == null || value.isEmpty) {
                  return 'Email đang trống!';
                }
                return null;
              },
            ),
          ),
          const SizedBox(height: 16),
          SizedBox(
            width: 175,
            height: 45,
            child: TextFormField(
              controller: passwordController,
              textInputAction: TextInputAction.done,
              onFieldSubmitted: (_) async {
                submitLogin();
              },
              decoration: InputDecoration(
                labelText: 'Mật khẩu',
                labelStyle: TextStyle(fontSize: 12),
                suffix: GestureDetector(
                  onTap: () {
                    setState(() {
                      _isHidden = !_isHidden;
                    });
                  },
                  child: Icon(Icons.visibility),
                ),
              ),
              obscureText: _isHidden,
              validator: (value) {
                if (value == null || value.isEmpty) {
                  return 'Mật khẩu đang trống!';
                }
                return null;
              },
            ),
          ),
          const SizedBox(height: 16),

          if (errorMessage != null)
            Text(
              errorMessage!,
              style: TextStyle(
                color: Colors.red,
                fontSize: 13,
                fontWeight: FontWeight.bold,
              ),
              textAlign: TextAlign.center,
            ),

          loading
              ? Center(child: CircularProgressIndicator())
              : ElevatedButton(
                  onPressed: submitLogin,
                  style: ElevatedButton.styleFrom(
                    backgroundColor: Colors.yellow,
                    foregroundColor: Colors.blue,
                  ),
                  child: const Text(
                    'Đăng nhập',
                    style: TextStyle(fontSize: 15),
                  ),
                ),
        ],
      ),
    );
  }

  Future<void> submitLogin() async {
    setState(() {
      _submitted = true;
    });

    if (_formKey.currentState!.validate()) {
      setState(() {
        loading = true;
        errorMessage = null;
      });

      FocusScope.of(context).unfocus();
      await loginApi();
    }
  }
}
