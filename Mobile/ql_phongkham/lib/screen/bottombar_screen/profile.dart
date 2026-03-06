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
                  hintText: 'Email',
                  controller: emailController,
                  labelText: 'Email',
                ),
                const SizedBox(height: 15),
                ProfileField(
                  hintText: 'Mật khẩu',
                  controller: passwordController,
                  labelText: 'Mật khẩu',
                ),
                const SizedBox(height: 20),
                isLoading
                    ? const CircularProgressIndicator()
                    : AuthButton(
                        buttonText: 'Lưu',
                        onPressed: () {
                          print("Button clicked");
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
    return Stack(
      children: <Widget>[
        CircleAvatar(
          radius: 80,
          //backgroundColor: ,
        ),
      ],
    );
  }
}
