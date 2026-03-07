import 'package:flutter/material.dart';
import 'package:ql_phongkham/core/theme/app_pallete.dart';
import 'package:ql_phongkham/core/utils/dialog_helper.dart';
import 'package:ql_phongkham/features/clinic/data/repository/auth_repository.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/auth/login_page.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/auth/auth_button.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/auth/auth_field.dart';

class SignupPage extends StatefulWidget {
  static route() => MaterialPageRoute(builder: (context) => SignupPage());
  const SignupPage({super.key});

  @override
  State<SignupPage> createState() => _SignupPageState();
}

class _SignupPageState extends State<SignupPage> {
  bool _submitted = false;
  bool isLoading = false;
  final formKey = GlobalKey<FormState>();

  final emailController = TextEditingController();
  final passwordController = TextEditingController();
  final checkpassController = TextEditingController();

  @override
  void dispose() {
    emailController.dispose();
    passwordController.dispose();
    checkpassController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      extendBodyBehindAppBar: true,
      appBar: AppBar(
        backgroundColor: AppPallete.transparentColor,
        elevation: 0,
      ),
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
            autovalidateMode: _submitted
                ? AutovalidateMode.onUserInteraction
                : AutovalidateMode.disabled,
            key: formKey,
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Text(
                  'Đăng ký',
                  style: TextStyle(
                    color: Colors.black,
                    fontSize: 40,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(height: 30),
                AuthField(
                  hintText: 'Email',
                  controller: emailController,
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return "Email đang trống!";
                    }
                    if (!isValidEmail(value)) {
                      return "Email không hợp lệ!";
                    }
                    return null;
                  },
                ),
                const SizedBox(height: 15),
                AuthField(
                  hintText: 'Mật khẩu',
                  controller: passwordController,
                  isObscureText: true,
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return "Mật khẩu đang trống!";
                    }
                    if (value.length < 6) {
                      return "Mật khẩu ít nhất 6 ký tự!";
                    }
                    return null;
                  },
                ),
                const SizedBox(height: 15),
                AuthField(
                  hintText: 'Nhập lại mật khẩu',
                  controller: checkpassController,
                  isObscureText: true,
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return "Vui lòng nhập lại mật khẩu!";
                    }
                    if (value != passwordController.text) {
                      return "Mật khẩu không khớp!";
                    }
                    return null;
                  },
                ),
                const SizedBox(height: 15),
                isLoading
                    ? const CircularProgressIndicator()
                    : AuthButton(
                        buttonText: 'Đăng ký',
                        onPressed: () {
                          setState(() {
                            _submitted = true;
                          });
                          if (formKey.currentState!.validate()) {
                            signup();
                          }
                        },
                      ),
                const SizedBox(height: 20),
                GestureDetector(
                  onTap: () {
                    Navigator.push(context, LoginPage.route());
                  },
                  child: RichText(
                    text: TextSpan(
                      text: 'Đã có tài khoản?',
                      style: Theme.of(context).textTheme.titleMedium,
                      children: [
                        TextSpan(
                          text: ' Đăng nhập',
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

  Future<void> signup() async {
    try {
      setState(() {
        isLoading = true;
      });

      final repo = AuthRepository();

      await repo.signup(
        emailController.text,
        passwordController.text,
        "Bệnh nhân",
      );

      Navigator.pushAndRemoveUntil(
        context,
        MaterialPageRoute(builder: (_) => LoginPage()),
        (route) => false,
      );
      DialogHelper.showSnackSuccess(context, "Đăng ký thành công!");
    } catch (e) {
      DialogHelper.showSnacFailed(context, 'Đăng ký thất bại!');
    } finally {
      setState(() {
        isLoading = false;
      });
    }
  }

  bool isValidEmail(String email) {
    final emailRegex = RegExp(r'^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.com$');
    return emailRegex.hasMatch(email);
  }
}
