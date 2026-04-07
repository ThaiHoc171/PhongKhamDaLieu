import 'package:flutter/material.dart';
import 'package:ql_phongkham/core/theme/app_pallete.dart';
import 'package:ql_phongkham/core/utils/dialog_helper.dart';
import 'package:ql_phongkham/features/clinic/data/repository/auth_repository.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/auth/login_page.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/auth/auth_button.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/auth/auth_field.dart';
import 'package:shared_preferences/shared_preferences.dart';

class ResetPassPage extends StatefulWidget {
  static route() => MaterialPageRoute(builder: (context) => ResetPassPage());
  const ResetPassPage({super.key});

  @override
  State<ResetPassPage> createState() => _ResetPassPageState();
}

class _ResetPassPageState extends State<ResetPassPage> {
  bool _submitted = false;
  bool isLoading = false;
  final formKey = GlobalKey<FormState>();

  final passwordController = TextEditingController();
  final newpassController = TextEditingController();

  @override
  void dispose() {
    passwordController.dispose();
    newpassController.dispose();
    super.dispose();
  }

  Future<void> resetpass() async {
    try {
      final prefs = await SharedPreferences.getInstance();
      final taikhoanId = prefs.getInt('userId')!;
      setState(() => isLoading = true);

      await AuthRepository().resetpassword(
        passwordController.text.trim(),
        newpassController.text.trim(),
        taikhoanId,
      );
      DialogHelper.showSnackSuccess(
        context,
        'Đổi mật khẩu thành công, vui lòng đăng nhập lại',
      );
      if (!mounted) return;
      Navigator.pushAndRemoveUntil(
        context,
        MaterialPageRoute(builder: (_) => LoginPage()),
        (route) => false,
      );
    } catch (e) {
      if (!mounted) return;
      DialogHelper.showSnacFailed(
        context,
        e.toString().replaceFirst('Exception: ', ''),
      );
    } finally {
      if (mounted) setState(() => isLoading = false);
    }
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
                  'Đổi mật khẩu',
                  style: TextStyle(
                    color: Colors.black,
                    fontSize: 40,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(height: 30),
                AuthField(
                  hintText: 'Mật khẩu cũ',
                  controller: passwordController,
                  isObscureText: true,
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return "Mật khẩu cũ đang trống!";
                    }
                    return null;
                  },
                ),
                const SizedBox(height: 15),
                AuthField(
                  hintText: 'Mật khẩu mới',
                  controller: newpassController,
                  isObscureText: true,
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return "Mật khẩu mới đang trống!";
                    }
                    return null;
                  },
                ),
                const SizedBox(height: 15),
                isLoading
                    ? const CircularProgressIndicator()
                    : AuthButton(
                        buttonText: 'Đổi mật khẩu',
                        onPressed: () {
                          setState(() {
                            _submitted = true;
                          });
                          if (formKey.currentState!.validate()) {
                            resetpass();
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
}
