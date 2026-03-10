import 'package:flutter/material.dart';
import 'package:ql_phongkham/core/theme/app_pallete.dart';
import 'package:ql_phongkham/core/utils/dialog_helper.dart';
import 'package:ql_phongkham/features/clinic/data/models/booking_model.dart';
import 'package:ql_phongkham/features/clinic/data/repository/booking_repository.dart';
import 'package:shared_preferences/shared_preferences.dart';

class DanhSachCaKhamPage extends StatefulWidget {
  const DanhSachCaKhamPage({super.key});

  @override
  State<DanhSachCaKhamPage> createState() => _DanhSachCaKhamPageState();
}

class _DanhSachCaKhamPageState extends State<DanhSachCaKhamPage> {
  List<CaKhamModel> caKhamList = [];
  bool isLoadingBacSi = true;

  @override
  void initState() {
    super.initState();
    loadBacSi();
  }

  Future<void> loadBacSi() async {
    try {
      final prefs = await SharedPreferences.getInstance();
      final token = prefs.getString('accessToken')!;
      final thongTinId = prefs.getInt('thongTinId')!;

      final repo = CaKhamChiTietRepository();
      final data = await repo.getCaKhamBenhNhan(token, thongTinId, 1, 10);

      setState(() {
        caKhamList = data;
        isLoadingBacSi = false;
      });
    } catch (e) {
      DialogHelper.showThongBao(
        context,
        e.toString().replaceFirst('Exception: ', ''),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text("Lịch khám"), centerTitle: true),
      body: ListView.builder(
        padding: const EdgeInsets.all(10),
        itemCount: caKhamList.length,
        itemBuilder: (context, index) {
          final item = caKhamList[index];

          Color statusColor;
          IconData statusIcon;

          if (item.trangThai == 'Hoàn thành') {
            statusColor = AppPallete.correctColor;
            statusIcon = Icons.check_circle;
          } else if (item.trangThai == 'Đã hủy') {
            statusColor = AppPallete.errorColor;
            statusIcon = Icons.cancel;
          } else {
            statusColor = Colors.orange;
            statusIcon = Icons.schedule;
          }

          return Card(
            elevation: 4,
            margin: const EdgeInsets.only(bottom: 12),
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(12),
            ),
            child: Padding(
              padding: const EdgeInsets.all(14),
              child: Row(
                children: [
                  Icon(statusIcon, color: statusColor, size: 32),

                  const SizedBox(width: 12),

                  Expanded(
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Text(
                          item.tenKhungGio,
                          style: const TextStyle(
                            fontWeight: FontWeight.bold,
                            fontSize: 16,
                          ),
                        ),

                        const SizedBox(height: 4),

                        Row(
                          children: [
                            const Icon(
                              Icons.room,
                              size: 16,
                              color: Colors.grey,
                            ),
                            const SizedBox(width: 4),
                            Text(
                              item.tenPhong,
                              style: const TextStyle(color: Colors.grey),
                            ),
                          ],
                        ),

                        const SizedBox(height: 4),

                        Row(
                          children: [
                            const Icon(
                              Icons.medical_services,
                              size: 16,
                              color: Colors.grey,
                            ),
                            const SizedBox(width: 4),
                            Text(item.lyDoKham),
                          ],
                        ),
                      ],
                    ),
                  ),

                  Container(
                    padding: const EdgeInsets.symmetric(
                      horizontal: 10,
                      vertical: 4,
                    ),
                    decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(20),
                    ),
                    child: Text(
                      item.trangThai,
                      style: TextStyle(
                        color: statusColor,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ),
                ],
              ),
            ),
          );
        },
      ),
    );
  }
}
