import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:ql_phongkham/core/utils/dialog_helper.dart';
import 'package:ql_phongkham/features/clinic/data/repository/profile_repository.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/auth/login_page.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/auth/auth_button.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/profile/profile_field.dart';
import 'package:shared_preferences/shared_preferences.dart';

class ProfileUpdateScreen extends StatefulWidget {
  const ProfileUpdateScreen({super.key});

  @override
  State<ProfileUpdateScreen> createState() => _ProfileUpdateScreenState();
}

class _ProfileUpdateScreenState extends State<ProfileUpdateScreen> {
  String? gioiTinh;
  bool isLoading = false;
  String? linkAvatar;
  final formKey = GlobalKey<FormState>();

  final hoTenController = TextEditingController();
  final ngaySinhController = TextEditingController();
  final sdtController = TextEditingController();
  final emailController = TextEditingController();
  final diaChiController = TextEditingController();

  @override
  void dispose() {
    hoTenController.dispose();
    ngaySinhController.dispose();
    sdtController.dispose();
    emailController.dispose();
    diaChiController.dispose();
    super.dispose();
  }

  @override
  void initState() {
    super.initState();
    loadProfile();
  }

  Future<void> loadProfile() async {
    try {
      final prefs = await SharedPreferences.getInstance();
      final token = prefs.getString('accessToken');
      final thongTinId = prefs.getInt('thongTinId');
      if (token == null || thongTinId == null) return;

      final data = await ProfileRepository().getProfile(token, thongTinId);

      setState(() {
        hoTenController.text = data.hoTen ?? "";
        ngaySinhController.text = data.ngaySinh != null
            ? DateFormat('dd/MM/yyyy').format(data.ngaySinh)
            : "";
        gioiTinh = data.gioiTinh;
        sdtController.text = data.sdt ?? "";
        emailController.text = data.emailLienHe ?? "";
        diaChiController.text = data.diaChi ?? "";
        linkAvatar = data.avatar;
      });
    } catch (e) {
      DialogHelper.showSnacFailed(context, e.toString());
    }
  }

  Future<void> addProfile() async {
    try {
      setState(() {
        isLoading = true;
      });

      final prefs = await SharedPreferences.getInstance();
      final token = prefs.getString('accessToken');
      final taiKhoanId = prefs.getInt('userId');
      if (taiKhoanId == null || taiKhoanId == 0) {
        DialogHelper.showSnacFailed(context, "Không tìm thấy tài khoản");
        return;
      }
      if (token == null || taiKhoanId == null) {
        DialogHelper.showSnacFailed(context, "Thiếu thông tin đăng nhập");
        return;
      }

      DateTime ngaySinh = DateFormat(
        'dd/MM/yyyy',
      ).parse(ngaySinhController.text);

      final benhNhanId = await ProfileRepository().addProfile(
        taiKhoanId,
        hoTenController.text,
        ngaySinh,
        gioiTinh ?? "",
        sdtController.text,
        emailController.text,
        diaChiController.text,
        linkAvatar ?? "",
        "",
        token,
      );
      print("Response: $benhNhanId");
      await prefs.setInt("benhNhanId", benhNhanId);

      DialogHelper.showSnackSuccess(context, "Tạo hồ sơ thành công");

      Navigator.pushAndRemoveUntil(
        context,
        MaterialPageRoute(builder: (_) => LoginPage()),
        (route) => false,
      );
    } catch (e) {
      DialogHelper.showSnacFailed(context, e.toString());
    } finally {
      setState(() {
        isLoading = false;
      });
    }
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
                imageProfile(),

                const SizedBox(height: 30),
                ProfileField(
                  controller: hoTenController,
                  labelText: "Họ tên",
                  hintText: "Nhập họ tên",
                ),

                const SizedBox(height: 20),
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
                ProfileField(
                  controller: sdtController,
                  labelText: "Số điện thoại",
                  hintText: "Nhập số điện thoại",
                ),

                const SizedBox(height: 20),
                ProfileField(
                  controller: emailController,
                  labelText: "Email liên hệ",
                  hintText: "Nhập email",
                ),

                const SizedBox(height: 20),
                ProfileField(
                  controller: diaChiController,
                  labelText: "Địa chỉ",
                  hintText: "Nhập địa chỉ",
                ),

                const SizedBox(height: 30),
                isLoading
                    ? const CircularProgressIndicator()
                    : AuthButton(
                        buttonText: "Lưu",
                        onPressed: () {
                          if (formKey.currentState!.validate()) {
                            addProfile();
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
          CircleAvatar(
            radius: 60,
            backgroundImage: linkAvatar != null && linkAvatar!.isNotEmpty
                ? AssetImage("assets/images/$linkAvatar")
                : const AssetImage("assets/images/user.png"),
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
                onPressed: () {},
              ),
            ),
          ),
        ],
      ),
    );
  }
}
