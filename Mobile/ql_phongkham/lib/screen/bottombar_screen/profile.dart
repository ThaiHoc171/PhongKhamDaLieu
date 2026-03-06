import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/auth/auth_button.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/profile/profile_field.dart';

class ProfileScreen extends StatefulWidget {
  const ProfileScreen({super.key});

  @override
  State<ProfileScreen> createState() => _ProfileScreenState();
}

class _ProfileScreenState extends State<ProfileScreen> {
  final formKey = GlobalKey<FormState>();

  final hoTenController = TextEditingController();
  final ngaySinhController = TextEditingController();
  final sdtController = TextEditingController();
  final emailController = TextEditingController();
  final diaChiController = TextEditingController();

  String? gioiTinh;

  bool isLoading = false;

  @override
  void dispose() {
    hoTenController.dispose();
    ngaySinhController.dispose();
    sdtController.dispose();
    emailController.dispose();
    diaChiController.dispose();
    super.dispose();
  }

  Future<void> pickDate() async {
    DateTime? picked = await showDatePicker(
      context: context,
      initialDate: DateTime(2000),
      firstDate: DateTime(1900),
      lastDate: DateTime.now(),
    );

    if (picked != null) {
      setState(() {
        ngaySinhController.text = DateFormat('dd/MM/yyyy').format(picked);
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text("Hồ sơ cá nhân")),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(16),
          child: Form(
            key: formKey,
            child: Column(
              children: [
                /// Avatar
                imageProfile(),

                const SizedBox(height: 30),

                /// Họ tên
                ProfileField(
                  controller: hoTenController,
                  labelText: "Họ tên",
                  hintText: "Nhập họ tên",
                ),

                const SizedBox(height: 20),

                /// Ngày sinh
                GestureDetector(
                  onTap: pickDate,
                  child: AbsorbPointer(
                    child: ProfileField(
                      controller: ngaySinhController,
                      labelText: "Ngày sinh",
                      hintText: "Chọn ngày sinh",
                    ),
                  ),
                ),

                const SizedBox(height: 20),

                /// Giới tính
                DropdownButtonFormField<String>(
                  value: gioiTinh,
                  decoration: const InputDecoration(
                    labelText: "Giới tính",
                    border: OutlineInputBorder(),
                  ),
                  items: const [
                    DropdownMenuItem(value: "Nam", child: Text("Nam")),
                    DropdownMenuItem(value: "Nữ", child: Text("Nữ")),
                    DropdownMenuItem(value: "Khác", child: Text("Khác")),
                  ],
                  onChanged: (value) {
                    setState(() {
                      gioiTinh = value;
                    });
                  },
                ),

                const SizedBox(height: 20),

                /// SĐT
                ProfileField(
                  controller: sdtController,
                  labelText: "Số điện thoại",
                  hintText: "Nhập số điện thoại",
                ),

                const SizedBox(height: 20),

                /// Email
                ProfileField(
                  controller: emailController,
                  labelText: "Email liên hệ",
                  hintText: "Nhập email",
                ),

                const SizedBox(height: 20),

                /// Địa chỉ
                ProfileField(
                  controller: diaChiController,
                  labelText: "Địa chỉ",
                  hintText: "Nhập địa chỉ",
                ),

                const SizedBox(height: 30),

                /// Button lưu
                isLoading
                    ? const CircularProgressIndicator()
                    : AuthButton(
                        buttonText: "Lưu",
                        onPressed: () {
                          if (formKey.currentState!.validate()) {
                            final data = {
                              "hoTen": hoTenController.text,
                              "ngaySinh": ngaySinhController.text,
                              "gioiTinh": gioiTinh,
                              "sdt": sdtController.text,
                              "email": emailController.text,
                              "diaChi": diaChiController.text,
                            };

                            print(data);
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
    return Center(
      child: Stack(
        children: [
          const CircleAvatar(
            radius: 60,
            backgroundImage: AssetImage(
              "assets/images/360_F_501018486_SQE0vK8bwMaFAbsHbp5JV2r1rnE1hT9z.jpg",
            ),
          ),

          Positioned(
            bottom: 0,
            right: 0,
            child: Container(
              decoration: const BoxDecoration(
                color: Colors.blue,
                shape: BoxShape.circle,
              ),
              child: IconButton(
                icon: const Icon(Icons.camera_alt, color: Colors.white),
                onPressed: () {
                  // chọn ảnh
                },
              ),
            ),
          ),
        ],
      ),
    );
  }
}
