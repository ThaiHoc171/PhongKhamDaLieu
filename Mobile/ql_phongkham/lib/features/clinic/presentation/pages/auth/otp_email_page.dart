// otp_email_page.dart
import 'package:flutter/material.dart';
import 'package:ql_phongkham/core/theme/app_pallete.dart';
import 'package:ql_phongkham/core/utils/dialog_helper.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/auth/otp_page.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/auth/auth_button.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/auth/auth_field.dart';
import 'package:ql_phongkham/features/clinic/data/repository/auth_repository.dart';

class OtpEmailPage extends StatefulWidget {
  const OtpEmailPage({super.key});

  static route() => MaterialPageRoute(builder: (_) => const OtpEmailPage());

  @override
  State<OtpEmailPage> createState() => _OtpEmailPageState();
}

class _OtpEmailPageState extends State<OtpEmailPage> {
  bool _submitted = false;
  bool isLoading = false;
  final formKey = GlobalKey<FormState>();
  final emailController = TextEditingController();

  @override
  void dispose() {
    emailController.dispose();
    super.dispose();
  }

  bool isValidEmail(String email) {
    final emailRegex = RegExp(r'^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.com$');
    return emailRegex.hasMatch(email);
  }

  Future<void> sendOtp() async {
    try {
      setState(() => isLoading = true);

      await AuthRepository().sendOtp(emailController.text.trim());

      if (!mounted) return;
      Navigator.push(
        context,
        OtpVerifyPage.routeForgotPassword(email: emailController.text.trim()),
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
      body: Container(
        decoration: const BoxDecoration(
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
                const Text(
                  'Xác nhận',
                  style: TextStyle(
                    color: Colors.black,
                    fontSize: 40,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(height: 10),
                const Text(
                  'Nhập email để nhận mã xác nhận',
                  textAlign: TextAlign.center,
                  style: TextStyle(color: Colors.black54, fontSize: 14),
                ),
                const SizedBox(height: 30),
                AuthField(
                  hintText: 'Email',
                  controller: emailController,
                  validator: (value) {
                    if (value == null || value.isEmpty)
                      return "Email đang trống!";
                    if (!isValidEmail(value)) return "Email không hợp lệ!";
                    return null;
                  },
                ),
                const SizedBox(height: 20),
                isLoading
                    ? const CircularProgressIndicator()
                    : AuthButton(
                        buttonText: 'Gửi mã OTP',
                        onPressed: () {
                          setState(() => _submitted = true);
                          if (formKey.currentState!.validate()) sendOtp();
                        },
                      ),
                const SizedBox(height: 20),
                GestureDetector(
                  onTap: () => Navigator.pop(context),
                  child: RichText(
                    text: TextSpan(
                      text: 'Đã có tài khoản? ',
                      style: Theme.of(context).textTheme.titleMedium,
                      children: [
                        TextSpan(
                          text: 'Đăng nhập',
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
