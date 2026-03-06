import 'package:flutter/material.dart';
import 'package:ql_phongkham/core/theme/app_pallete.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/auth/signup_page.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/auth/auth_button.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/auth/auth_field.dart';
import 'package:ql_phongkham/features/clinic/data/repository/auth_repository.dart';
import 'package:ql_phongkham/core/services/storage_service.dart';
import 'package:ql_phongkham/screen/home_screen/home.dart';

class LoginPage extends StatefulWidget {
  static route() => MaterialPageRoute(builder: (context) => LoginPage());
  const LoginPage({super.key});

  @override
  State<LoginPage> createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  bool isLoading = false;
  final formKey = GlobalKey<FormState>();

  final emailController = TextEditingController();
  final passwordController = TextEditingController();

  @override
  void dispose() {
    emailController.dispose();
    passwordController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
        decoration: BoxDecoration(
          image: DecorationImage(
            image: AssetImage('assets/images/bg.jpg'),
            fit: BoxFit.cover,
          ),
        ),
        child: Padding(
          padding: const EdgeInsets.all(15.0),
          child: Form(
            key: formKey,
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Text(
                  'Đăng nhập',
                  style: TextStyle(
                    color: Colors.black,
                    fontSize: 40,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(height: 30),
                AuthField(hintText: 'Email', controller: emailController),
                const SizedBox(height: 15),
                AuthField(
                  hintText: 'Mật khẩu',
                  controller: passwordController,
                  isObscureText: true,
                ),
                const SizedBox(height: 20),
                isLoading
                    ? const CircularProgressIndicator()
                    : AuthButton(
                        buttonText: 'Đăng nhập',
                        onPressed: () {
                          print("Button clicked");
                          if (formKey.currentState!.validate()) {
                            login();
                          }
                        },
                      ),
                const SizedBox(height: 20),
                GestureDetector(
                  onTap: () {
                    Navigator.push(context, SignupPage.route());
                  },
                  child: RichText(
                    text: TextSpan(
                      text: 'Chưa có tài khoản?',
                      style: Theme.of(context).textTheme.titleMedium,
                      children: [
                        TextSpan(
                          text: ' Đăng ký',
                          style: Theme.of(context).textTheme.titleMedium
                              ?.copyWith(
                                color: AppPallete.gradient3,
                                fontWeight: FontWeight.bold,
                              ),
                        ),
                      ],
                    ),
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }

  Future<void> login() async {
    try {
      setState(() {
        isLoading = true;
      });

      final repo = AuthRepository();

      final user = await repo.login(
        emailController.text,
        passwordController.text,
      );

      await StorageService.saveUser(user);

      Navigator.pushAndRemoveUntil(
        context,
        MaterialPageRoute(builder: (_) => HomeScreen(token: user.accessToken)),
        (route) => false,
      );
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text(e.toString().replaceFirst('Exception: ', '')),
          backgroundColor: AppPallete.errorColor,
        ),
      );
    } finally {
      setState(() {
        isLoading = false;
      });
    }
  }
}
