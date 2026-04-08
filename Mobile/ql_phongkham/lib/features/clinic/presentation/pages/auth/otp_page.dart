// otp_page.dart
import 'dart:async';
import 'package:flutter/material.dart';
import 'package:ql_phongkham/core/theme/app_pallete.dart';
import 'package:ql_phongkham/core/utils/dialog_helper.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/auth/login_page.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/auth/auth_button.dart';
import 'package:ql_phongkham/features/clinic/data/repository/auth_repository.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/auth/auth_field.dart';

class OtpVerifyPage extends StatefulWidget {
  final String email;
  final String? password;
  final String? loai;

  const OtpVerifyPage({
    super.key,
    required this.email,
    this.password,
    this.loai,
  });

  // Từ đăng ký: truyền email + password + loai
  static routeRegister({
    required String email,
    required String password,
    required String loai,
  }) => MaterialPageRoute(
    builder: (_) => OtpVerifyPage(email: email, password: password, loai: loai),
  );

  static routeForgotPassword({required String email}) =>
      MaterialPageRoute(builder: (_) => OtpVerifyPage(email: email));

  @override
  State<OtpVerifyPage> createState() => _OtpVerifyPageState();
}

class _OtpVerifyPageState extends State<OtpVerifyPage> {
  bool isLoading = false;
  bool isResending = false;
  int _countdown = 60;
  Timer? _timer;

  final otpControllers = TextEditingController();

  @override
  void initState() {
    super.initState();
    _startCountdown();
  }

  @override
  void dispose() {
    _timer?.cancel();
    super.dispose();
  }

  void _startCountdown() {
    _countdown = 120;
    _timer?.cancel();
    _timer = Timer.periodic(const Duration(seconds: 1), (t) {
      if (_countdown == 0) {
        t.cancel();
      } else {
        setState(() => _countdown--);
      }
    });
  }

  Future<void> verifyOtp() async {
    final code = otpControllers.text.trim();
    if (code.length < 6) {
      DialogHelper.showSnacFailed(context, 'Vui lòng nhập đủ 6 chữ số');
      return;
    }

    try {
      setState(() => isLoading = true);

      await AuthRepository().verifyOtp(widget.email, code);

      if (!mounted) return;

      if (widget.password != null && widget.loai != null) {
        await AuthRepository().signup(
          widget.email,
          widget.password!,
          widget.loai!,
        );

        if (!mounted) return;
        DialogHelper.showSnackSuccess(context, 'Đăng ký thành công');
        Navigator.pushAndRemoveUntil(
          context,
          MaterialPageRoute(builder: (_) => LoginPage()),
          (route) => false,
        );
      } else {
        final taikhoanid = await AuthRepository().getIdByEmail(widget.email);
        await AuthRepository().forgetpassword(taikhoanid);
        print("Email: ${widget.email}");
        print("TaiKhoanID: $taikhoanid");
        if (!mounted) return;
        DialogHelper.showSnackSuccess(
          context,
          'Mật khẩu đã đươc reset, Mật khẩu hiện tại là: 123456, vui lòng đăng nhập và đổi mật khẩu mới',
        );
        Navigator.pushAndRemoveUntil(
          context,
          MaterialPageRoute(builder: (_) => LoginPage()),
          (route) => false,
        );
      }
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

  Future<void> resendOtp() async {
    try {
      setState(() => isResending = true);
      await AuthRepository().sendOtp(widget.email);
      _startCountdown();
      if (!mounted) return;
      DialogHelper.showSnackSuccess(context, 'Đã gửi lại mã OTP');
    } catch (e) {
      if (!mounted) return;
      DialogHelper.showSnacFailed(
        context,
        e.toString().replaceFirst('Exception: ', ''),
      );
    } finally {
      if (mounted) setState(() => isResending = false);
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
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              const Text(
                'Xác nhận OTP',
                style: TextStyle(
                  color: Colors.black,
                  fontSize: 40,
                  fontWeight: FontWeight.bold,
                ),
              ),
              const SizedBox(height: 10),
              Text(
                'Mã OTP đã được gửi đến\n${widget.email}',
                textAlign: TextAlign.center,
                style: const TextStyle(color: Colors.black54, fontSize: 14),
              ),
              const SizedBox(height: 30),
              AuthField(
                hintText: 'Mã OTP',
                controller: otpControllers,
                isObscureText: false,
                validator: (value) {
                  if (value == null || value.isEmpty) {
                    return "Mã OTP đang trống!";
                  }
                  if (value.length != 6) {
                    return "Mã OTP phải có 6 ký tự!";
                  }
                  return null;
                },
              ),
              const SizedBox(height: 20),
              _countdown > 0
                  ? Text(
                      'Gửi lại mã sau $_countdown giây',
                      style: const TextStyle(color: Colors.black54),
                    )
                  : isResending
                  ? const CircularProgressIndicator()
                  : GestureDetector(
                      onTap: resendOtp,
                      child: RichText(
                        text: TextSpan(
                          text: 'Không nhận được mã? ',
                          style: Theme.of(context).textTheme.titleMedium,
                          children: [
                            TextSpan(
                              text: 'Gửi lại',
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
              const SizedBox(height: 20),
              isLoading
                  ? const CircularProgressIndicator()
                  : AuthButton(buttonText: 'Xác nhận', onPressed: verifyOtp),
            ],
          ),
        ),
      ),
    );
  }
}
