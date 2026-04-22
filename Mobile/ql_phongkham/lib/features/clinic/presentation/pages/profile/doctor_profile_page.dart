import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:ql_phongkham/core/utils/dialog_helper.dart';
import 'package:ql_phongkham/features/clinic/data/models/doctor_profile_model.dart';
import 'package:ql_phongkham/features/clinic/data/models/employ_model.dart';
import 'package:ql_phongkham/features/clinic/data/repository/doctor_profile_repository.dart';

class NhanVienDetailScreen extends StatefulWidget {
  final int nhanVienId;
  final int bacSiId;
  const NhanVienDetailScreen({
    super.key,
    required this.nhanVienId,
    required this.bacSiId,
  });

  @override
  State<NhanVienDetailScreen> createState() => _NhanVienDetailScreenState();
}

class _NhanVienDetailScreenState extends State<NhanVienDetailScreen> {
  NhanVienModel? nhanVien;
  BacSiProfileModel? profile;
  @override
  void initState() {
    super.initState();
    loadNhanVien();
  }

  Future<void> loadNhanVien() async {
    try {
      final data = await NhanVienRepository().getNhanVien(widget.nhanVienId);
      final data2 = await BacsiProfileRepository().geChiTiettBacSi(
        widget.nhanVienId,
      );
      setState(() {
        nhanVien = data;
        profile = data2;
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
    if (nhanVien == null) {
      return const Scaffold(body: Center(child: CircularProgressIndicator()));
    }
    return Scaffold(
      appBar: AppBar(title: const Text("Chi tiết bác sĩ")),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(16),
          child: Column(
            children: [
              imageProfile(),
              const SizedBox(height: 30),
              profileField("Họ tên", nhanVien!.hoTen),
              const SizedBox(height: 20),
              profileField(
                "Ngày sinh",
                DateFormat('dd/MM/yyyy').format(nhanVien!.ngaySinh),
              ),
              const SizedBox(height: 20),
              profileField("Giới tính", nhanVien!.gioiTinh),
              const SizedBox(height: 20),
              profileField("Số điện thoại", nhanVien!.sdt),
              const SizedBox(height: 20),
              profileField("Email liên hệ", nhanVien!.emailLienHe),
              const SizedBox(height: 20),
              profileField("Địa chỉ", nhanVien!.diaChi),
              const SizedBox(height: 20),
              profileField("Chức vụ", nhanVien!.chucVu.name),
              const SizedBox(height: 20),
              profileField("Phòng chức năng", nhanVien!.phongChucNang.name),
              const SizedBox(height: 20),
              profileField("Bằng cấp", nhanVien!.bangCap),
              const SizedBox(height: 20),
              profileField("Kinh nghiệm", nhanVien!.kinhNghiem),
              const SizedBox(height: 20),
              profileField(
                "Ngày vào làm",
                DateFormat('dd/MM/yyyy').format(nhanVien!.ngayVaoLam),
              ),
              const SizedBox(height: 20),
              profileField("Trạng thái", nhanVien!.trangThai),
              const SizedBox(height: 20),
            ],
          ),
        ),
      ),
    );
  }

  Widget imageProfile() {
    final linkAvatar = _buildAvatarUrl(profile?.hinhAnh);
    return Center(
      child: Stack(
        children: [
          CircleAvatar(
            radius: 60,
            backgroundImage: linkAvatar != null && linkAvatar.isNotEmpty
                ? NetworkImage(linkAvatar)
                : const AssetImage("assets/images/user.png") as ImageProvider,
          ),
        ],
      ),
    );
  }

  String? _buildAvatarUrl(String? avatar) {
    if (avatar == null || avatar.isEmpty) return null;
    if (avatar.startsWith('http')) return avatar;
    final path = avatar.startsWith('/') ? avatar.substring(1) : avatar;
    return "https://hoanmyclinic.s3.ap-southeast-2.amazonaws.com/$path";
  }
}
