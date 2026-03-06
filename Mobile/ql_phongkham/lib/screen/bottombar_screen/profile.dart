import 'package:flutter/material.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/auth/auth_button.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/profile/profile_field.dart';

class ProfileScreen extends StatefulWidget {
  const ProfileScreen({super.key});

  @override
  State<ProfileScreen> createState() => _ProfileScreenState();
}

class _ProfileScreenState extends State<ProfileScreen> {
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
      appBar: AppBar(),
      body: Container(
        child: Padding(
          padding: const EdgeInsets.all(15.0),
          child: Form(
            key: formKey,
            child: Column(
              mainAxisAlignment: MainAxisAlignment.start,
              children: [
                Text(
                  'Hồ sơ',
                  style: TextStyle(
                    color: Colors.black,
                    fontSize: 40,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(height: 30),

                const SizedBox(height: 30),
                ProfileField(
                  hintText: 'Họ tên',
                  controller: emailController,
                  labelText: 'Họ tên',
                ),
                const SizedBox(height: 15),
                ProfileField(
                  hintText: 'Ngày sinh',
                  controller: passwordController,
                  labelText: 'Ngày sinh',
                ),
                const SizedBox(height: 15),
                ProfileField(
                  hintText: 'Giới tính',
                  controller: passwordController,
                  labelText: 'Giới tính',
                ),
                ProfileField(
                  hintText: 'Số điện thoại',
                  controller: passwordController,
                  labelText: 'Số điện thoại',
                ),
                ProfileField(
                  hintText: 'Email liên hệ',
                  controller: passwordController,
                  labelText: 'Email liên hệ',
                ),
                ProfileField(
                  hintText: 'Địa chỉ',
                  controller: passwordController,
                  labelText: 'Địa chỉ',
                ),
                isLoading
                    ? const CircularProgressIndicator()
                    : AuthButton(
                        buttonText: 'Lưu',
                        onPressed: () {
                          if (formKey.currentState!.validate()) {
                            //login();
                          }
                        },
                      ),
                const SizedBox(height: 20),
              ],
            ),
          ),
        ),
      ),
    );
  }

  Widget imageProfile() {
    return Stack(children: <Widget>[CircleAvatar(radius: 80)]);
  }
}
