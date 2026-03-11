import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:ql_phongkham/core/utils/dialog_helper.dart';
import 'package:ql_phongkham/features/clinic/data/models/detail_exam_model.dart';
import 'package:ql_phongkham/features/clinic/data/repository/examination_repository.dart';
import 'package:shared_preferences/shared_preferences.dart';

class DetailExamScreen extends StatefulWidget {
  final int caKhamId;
  const DetailExamScreen({super.key, required this.caKhamId});

  @override
  State<DetailExamScreen> createState() => _DetailExamScreenState();
}

class _DetailExamScreenState extends State<DetailExamScreen> {
  PhienKhamModel? phienKham;

  @override
  void initState() {
    super.initState();
    loadPhienKham();
  }

  Future<void> loadPhienKham() async {
    try {
      final prefs = await SharedPreferences.getInstance();
      final token = prefs.getString('accessToken');

      if (token == null || widget.caKhamId == null) return;

      final data = await PhienKhamRepository().getChiTietPhienKham(
        token,
        widget.caKhamId,
      );
      setState(() {
        phienKham = data;
      });
    } catch (e) {
      DialogHelper.showSnacFailed(context, e.toString());
    }
  }

  List<Widget> parseImages() {
    try {
      final List images = jsonDecode(phienKham!.hinhAnhJSON);

      return images.map((img) {
        return ClipRRect(
          borderRadius: BorderRadius.circular(10),
          child: Image.network(img, fit: BoxFit.cover),
        );
      }).toList();
    } catch (e) {
      return [];
    }
  }

  Widget phienKhamItem(String value) {
    return Container(
      width: double.infinity,
      padding: const EdgeInsets.symmetric(vertical: 14, horizontal: 12),
      decoration: BoxDecoration(
        color: Colors.grey.shade50,
        borderRadius: BorderRadius.circular(10),
        border: Border.all(color: Colors.grey.shade300),
      ),
      child: Text(
        value.isEmpty ? "Chưa cập nhật" : value,
        style: const TextStyle(fontSize: 15),
      ),
    );
  }

  Widget phienKhamField(IconData icon, String label, String value) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Row(
          children: [
            Icon(icon, size: 18, color: Colors.blue),
            const SizedBox(width: 6),
            Text(label, style: const TextStyle(fontWeight: FontWeight.bold)),
          ],
        ),
        const SizedBox(height: 6),
        phienKhamItem(value),
      ],
    );
  }

  @override
  Widget build(BuildContext context) {
    if (phienKham == null) {
      return const Scaffold(body: Center(child: CircularProgressIndicator()));
    }

    return Scaffold(
      appBar: AppBar(
        title: const Text("Chi tiết phiên khám"),
        centerTitle: true,
      ),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Card(
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(12),
              ),
              elevation: 2,
              child: Padding(
                padding: const EdgeInsets.all(16),
                child: Column(
                  children: [
                    phienKhamField(
                      Icons.person,
                      "Bệnh nhân",
                      phienKham!.benhNhan?['name'] ?? "",
                    ),
                    const SizedBox(height: 15),
                    phienKhamField(
                      Icons.medical_services,
                      "Nhân viên khám",
                      phienKham!.nhanVien?['name'] ?? "",
                    ),
                    const SizedBox(height: 15),
                    phienKhamField(
                      Icons.calendar_today,
                      "Ngày khám",
                      phienKham!.ngayKham != null
                          ? DateFormat(
                              'dd/MM/yyyy HH:mm',
                            ).format(phienKham!.ngayKham!)
                          : "",
                    ),
                    const SizedBox(height: 15),
                    phienKhamField(
                      Icons.info_outline,
                      "Trạng thái",
                      phienKham!.trangThai,
                    ),
                  ],
                ),
              ),
            ),

            const SizedBox(height: 20),
            Card(
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(12),
              ),
              elevation: 2,
              child: Padding(
                padding: const EdgeInsets.all(16),
                child: Column(
                  children: [
                    phienKhamField(
                      Icons.sick,
                      "Triệu chứng",
                      phienKham!.trieuChung,
                    ),
                    const SizedBox(height: 15),
                    phienKhamField(
                      Icons.assignment,
                      "Chẩn đoán cuối",
                      phienKham!.chanDoanCuoi,
                    ),
                    const SizedBox(height: 15),
                    phienKhamField(Icons.notes, "Ghi chú", phienKham!.ghiChu),
                  ],
                ),
              ),
            ),
            const SizedBox(height: 20),
            if (phienKham!.hinhAnhJSON.isNotEmpty) ...[
              const Text(
                "Hình ảnh bệnh",
                style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 10),
              GridView.count(
                crossAxisCount: 3,
                shrinkWrap: true,
                physics: const NeverScrollableScrollPhysics(),
                crossAxisSpacing: 10,
                mainAxisSpacing: 10,
                children: parseImages(),
              ),
            ],
          ],
        ),
      ),
    );
  }
}
