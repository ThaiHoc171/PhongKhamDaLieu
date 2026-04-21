import 'package:flutter/material.dart';
import 'package:ql_phongkham/core/theme/app_pallete.dart';
import 'package:ql_phongkham/core/utils/dialog_helper.dart';
import 'package:ql_phongkham/features/clinic/data/models/booking_model.dart';
import 'package:ql_phongkham/features/clinic/data/repository/booking_repository.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/exam/detail_exam_page.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/monitoring/reexam_page.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/monitoring/treatment_page.dart';
import 'package:shared_preferences/shared_preferences.dart';

class DanhSachCaKhamHoanThanhPage extends StatefulWidget {
  const DanhSachCaKhamHoanThanhPage({super.key});

  @override
  State<DanhSachCaKhamHoanThanhPage> createState() =>
      _DanhSachCaKhamHoanThanhPageState();
}

class _DanhSachCaKhamHoanThanhPageState
    extends State<DanhSachCaKhamHoanThanhPage> {
  List<CaKhamModel> caKhamList = [];
  bool isLoadingBacSi = true;
  int? thongTinId;
  int? benhNhanId;
  @override
  void initState() {
    super.initState();
    loadCaKham();
  }

  Future<void> loadCaKham() async {
    try {
      final prefs = await SharedPreferences.getInstance();
      thongTinId = prefs.getInt('thongTinId')!;
      benhNhanId = prefs.getInt('benhNhanId');
      final repo = LichKhamRepository();
      final data = await repo.getCaKhamBenhNhan(thongTinId!, 1, 10);

      setState(() {
        caKhamList = data.where((ca) => ca.trangThai == 'Hoàn thành').toList();
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
      appBar: AppBar(title: const Text("Theo dõi điều trị"), centerTitle: true),
      body: Column(
        children: [
          Padding(
            padding: const EdgeInsets.fromLTRB(12, 12, 12, 4),
            child: Row(
              children: [
                Expanded(
                  child: _ActionButton(
                    icon: Icons.calendar_month_outlined,
                    label: 'Lịch tái khám',
                    color: const Color(0xFF2196F3),
                    onTap: () {
                      if (thongTinId == null) return;
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (_) =>
                              LichTaiKhamPage(benhNhanId: benhNhanId!),
                        ),
                      );
                    },
                  ),
                ),
                const SizedBox(width: 10),
                Expanded(
                  child: _ActionButton(
                    icon: Icons.local_hospital_outlined,
                    label: 'Liệu trình điều trị',
                    color: const Color(0xFF43A047),
                    onTap: () {
                      if (thongTinId == null) return;
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (_) =>
                              LieuTrinhDieuTriPage(benhNhanId: benhNhanId!),
                        ),
                      );
                    },
                  ),
                ),
              ],
            ),
          ),

          // ── Danh sách ca khám hoàn thành ──────────────────────────
          Expanded(
            child: ListView.builder(
              padding: const EdgeInsets.all(10),
              itemCount: caKhamList.length,
              itemBuilder: (context, index) {
                final item = caKhamList[index];

                const statusColor = AppPallete.correctColor;
                const statusIcon = Icons.check_circle;

                return InkWell(
                  onTap: () {
                    if (item.trangThai == 'Hoàn thành') {
                      Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (_) =>
                              DetailExamScreen(caKhamId: item.caKhamID),
                        ),
                      );
                    }
                  },
                  child: Card(
                    elevation: 4,
                    margin: const EdgeInsets.only(bottom: 12),
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(12),
                    ),
                    child: Padding(
                      padding: const EdgeInsets.all(14),
                      child: Row(
                        children: [
                          const Icon(statusIcon, color: statusColor, size: 32),
                          const SizedBox(width: 12),
                          Expanded(
                            child: Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: [
                                Row(
                                  crossAxisAlignment: CrossAxisAlignment.end,
                                  children: [
                                    Text(
                                      item.ngayKham.toString().split(' ')[0],
                                      style: const TextStyle(
                                        fontSize: 18,
                                        fontWeight: FontWeight.bold,
                                      ),
                                    ),
                                    const SizedBox(width: 10),
                                    Text(
                                      item.tenKhungGio,
                                      style: const TextStyle(
                                        fontSize: 12,
                                        color: Colors.grey,
                                      ),
                                    ),
                                  ],
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
                                      style: const TextStyle(
                                        color: Colors.grey,
                                      ),
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
                              style: const TextStyle(
                                color: statusColor,
                                fontWeight: FontWeight.bold,
                              ),
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                );
              },
            ),
          ),
        ],
      ),
    );
  }
}

class _ActionButton extends StatelessWidget {
  final IconData icon;
  final String label;
  final Color color;
  final VoidCallback onTap;

  const _ActionButton({
    required this.icon,
    required this.label,
    required this.color,
    required this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    return Material(
      borderRadius: BorderRadius.circular(12),
      child: InkWell(
        onTap: onTap,
        borderRadius: BorderRadius.circular(12),
        child: Container(
          padding: const EdgeInsets.symmetric(vertical: 14, horizontal: 8),
          decoration: BoxDecoration(
            borderRadius: BorderRadius.circular(12),
            border: Border.all(width: 1),
          ),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Icon(icon, color: color, size: 20),
              const SizedBox(width: 8),
              Flexible(
                child: Text(
                  label,
                  style: TextStyle(
                    color: color,
                    fontWeight: FontWeight.w600,
                    fontSize: 13,
                  ),
                  overflow: TextOverflow.ellipsis,
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
