import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:ql_phongkham/core/utils/dialog_helper.dart';
import 'package:ql_phongkham/features/clinic/data/repository/profile_repository.dart';
import 'package:shared_preferences/shared_preferences.dart';

class ProfileScreen extends StatefulWidget {
  const ProfileScreen({super.key});

  @override
  State<ProfileScreen> createState() => _ProfileScreenState();
}

class _ProfileScreenState extends State<ProfileScreen> {
  String? gioiTinh;
  String? linkAvatar;

  String hoTen = "";
  String ngaySinh = "";
  String sdt = "";
  String email = "";
  String diaChi = "";

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
        hoTen = data.hoTen ?? "";
        ngaySinh = data.ngaySinh != null
            ? DateFormat('dd/MM/yyyy').format(data.ngaySinh)
            : "";
        gioiTinh = data.gioiTinh;
        sdt = data.sdt ?? "";
        email = data.emailLienHe ?? "";
        diaChi = data.diaChi ?? "";
        linkAvatar = data.avatar;
      });
    } catch (e) {
      DialogHelper.showSnacFailed(context, e.toString());
    }
  }

  Widget profileItem(String label, String value) {
    return Container(
      width: double.infinity,
      padding: const EdgeInsets.symmetric(vertical: 14, horizontal: 12),
      decoration: BoxDecoration(
        border: Border.all(color: Colors.grey.shade400),
        borderRadius: BorderRadius.circular(8),
      ),
      child: Text(
        value.isEmpty ? "Chưa cập nhật" : value,
        style: const TextStyle(fontSize: 16),
      ),
    );
  }

  Widget profileField(String label, String value) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(label, style: const TextStyle(fontWeight: FontWeight.bold)),
        const SizedBox(height: 6),
        profileItem(label, value),
      ],
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text("Hồ sơ cá nhân")),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(16),
          child: Column(
            children: [
              imageProfile(),
              const SizedBox(height: 30),
              profileField("Họ tên", hoTen),
              const SizedBox(height: 20),
              profileField("Ngày sinh", ngaySinh),
              const SizedBox(height: 20),
              profileField("Giới tính", gioiTinh ?? ""),
              const SizedBox(height: 20),
              profileField("Số điện thoại", sdt),
              const SizedBox(height: 20),
              profileField("Email liên hệ", email),
              const SizedBox(height: 20),
              profileField("Địa chỉ", diaChi),
              const SizedBox(height: 20),
            ],
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
        ],
      ),
    );
  }
}
