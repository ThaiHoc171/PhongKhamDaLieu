import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:ql_phongkham/core/utils/dialog_helper.dart';
import 'package:ql_phongkham/features/clinic/data/models/profile_model.dart';
import 'package:ql_phongkham/features/clinic/data/repository/profile_repository.dart';
import 'package:shared_preferences/shared_preferences.dart';

class ProfileScreen extends StatefulWidget {
  const ProfileScreen({super.key});

  @override
  State<ProfileScreen> createState() => _ProfileScreenState();
}

class _ProfileScreenState extends State<ProfileScreen> {
  ProfileModel? profile;
  late final avatar = profile!.avatar;
  @override
  void initState() {
    super.initState();
    loadProfile();
  }

  Future<void> loadProfile() async {
    try {
      final prefs = await SharedPreferences.getInstance();
      final thongTinId = prefs.getInt('thongTinId');

      if (thongTinId == null) return;

      final data = await ProfileRepository().getProfile(thongTinId);
      setState(() {
        profile = data;
      });
    } catch (e) {
      DialogHelper.showSnacFailed(
        context,
        e.toString().replaceFirst('Exception: ', ''),
      );
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
              profileField("Họ tên", profile!.hoTen),
              const SizedBox(height: 20),
              profileField(
                "Ngày sinh",
                DateFormat('dd/MM/yyyy').format(profile!.ngaySinh!),
              ),
              const SizedBox(height: 20),
              profileField("Giới tính", profile!.gioiTinh),
              const SizedBox(height: 20),
              profileField("Số điện thoại", profile!.sdt),
              const SizedBox(height: 20),
              profileField("Email liên hệ", profile!.emailLienHe),
              const SizedBox(height: 20),
              profileField("Địa chỉ", profile!.diaChi),
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
            backgroundImage: avatar != null && avatar!.isNotEmpty
                ? AssetImage("assets/images/$avatar")
                : const AssetImage("assets/images/user.png"),
          ),
        ],
      ),
    );
  }
}
