import 'package:flutter/material.dart';
import 'package:ql_phongkham/features/clinic/data/models/treatment_model.dart';
import 'package:ql_phongkham/features/clinic/data/repository/booking_repository.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/exam/detail_exam_page.dart';

class LieuTrinhDieuTriPage extends StatefulWidget {
  final int benhNhanId;

  const LieuTrinhDieuTriPage({super.key, required this.benhNhanId});

  @override
  State<LieuTrinhDieuTriPage> createState() => _LieuTrinhDieuTriPageState();
}

class _LieuTrinhDieuTriPageState extends State<LieuTrinhDieuTriPage> {
  List<BuoiDieuTriModel> caKhamList = [];
  int? lieuTrinhId;
  int? soBuoi;
  @override
  void initState() {
    super.initState();
    loadData();
  }

  Future<void> loadData() async {
    try {
      final repo = LichKhamRepository();

      final dieuTri = await repo.checkDieuTriPending(widget.benhNhanId);

      if (dieuTri == null) {
        setState(() {});
        return;
      }

      final buoidieutri = await repo.getBuoiDieuTri(dieuTri.lieuTrinhID);

      setState(() {
        caKhamList = buoidieutri;
        soBuoi = dieuTri.tongSoBuoi;
        lieuTrinhId = dieuTri.lieuTrinhID;
      });
    } catch (e) {
      setState(() {});
    }
  }

  @override
  Widget build(BuildContext context) {
    final hoanThanh = caKhamList
        .where((c) => c.trangThai == 'Hoàn thành')
        .toList();
    final chuaHoanThanh = caKhamList
        .where((c) => c.trangThai != 'Hoàn thành')
        .toList();

    final total = caKhamList.length;
    final doneCount = hoanThanh.length;
    final progress = total == 0 ? 0.0 : doneCount / total;

    return Scaffold(
      backgroundColor: Colors.grey.shade50,
      appBar: AppBar(
        title: const Text("Liệu trình điều trị"),
        centerTitle: true,
        backgroundColor: Colors.white,
        foregroundColor: Colors.black87,
        elevation: 0,
      ),
      body: total == 0
          ? _buildEmpty()
          : SingleChildScrollView(
              padding: const EdgeInsets.all(16),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  _buildProgressCard(doneCount, total, progress),

                  if (chuaHoanThanh.isNotEmpty) ...[
                    _sectionHeader(
                      'Đang điều trị',
                      chuaHoanThanh.length,
                      const Color(0xFFFFA000),
                    ),
                    ...chuaHoanThanh.map(
                      (ca) => _BuoiDieuTriCard(caKham: ca, onTap: null),
                    ),
                  ],

                  if (hoanThanh.isNotEmpty) ...[
                    _sectionHeader(
                      'Đã hoàn thành',
                      hoanThanh.length,
                      const Color(0xFF43A047),
                    ),
                    ...hoanThanh.map(
                      (ca) => _BuoiDieuTriCard(
                        caKham: ca,
                        onTap: () => Navigator.push(
                          context,
                          MaterialPageRoute(
                            builder: (_) =>
                                DetailExamScreen(caKhamId: ca.caKhamID),
                          ),
                        ),
                      ),
                    ),
                  ],
                ],
              ),
            ),
    );
  }

  Widget _buildProgressCard(int done, int total, double progress) {
    return Container(
      width: double.infinity,
      margin: const EdgeInsets.only(bottom: 20),
      padding: const EdgeInsets.all(20),
      decoration: BoxDecoration(
        gradient: const LinearGradient(
          colors: [Color(0xFF43A047), Color(0xFF66BB6A)],
          begin: Alignment.topLeft,
          end: Alignment.bottomRight,
        ),
        borderRadius: BorderRadius.circular(16),
        boxShadow: [
          BoxShadow(
            color: const Color(0xFF43A047),
            blurRadius: 12,
            offset: const Offset(0, 4),
          ),
        ],
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Text(
            'Tiến độ điều trị',
            style: TextStyle(
              color: Colors.white70,
              fontSize: 13,
              letterSpacing: 0.5,
            ),
          ),
          const SizedBox(height: 6),
          Text(
            '$done / $soBuoi buổi hoàn thành',
            style: const TextStyle(
              color: Colors.white,
              fontSize: 22,
              fontWeight: FontWeight.bold,
            ),
          ),
          const SizedBox(height: 14),
          ClipRRect(
            borderRadius: BorderRadius.circular(8),
            child: LinearProgressIndicator(
              value: progress,
              minHeight: 10,
              backgroundColor: Colors.white30,
              valueColor: const AlwaysStoppedAnimation<Color>(Colors.white),
            ),
          ),
          const SizedBox(height: 8),
          Text(
            '${(progress * 100).toStringAsFixed(0)}% hoàn thành',
            style: const TextStyle(color: Colors.white70, fontSize: 12),
          ),
        ],
      ),
    );
  }

  Widget _sectionHeader(String title, int count, Color color) {
    return Padding(
      padding: const EdgeInsets.only(top: 4, bottom: 10),
      child: Row(
        children: [
          Container(
            width: 4,
            height: 18,
            decoration: BoxDecoration(
              color: color,
              borderRadius: BorderRadius.circular(2),
            ),
          ),
          const SizedBox(width: 8),
          Text(
            title,
            style: TextStyle(
              fontSize: 14,
              fontWeight: FontWeight.w600,
              color: color,
              letterSpacing: 0.3,
            ),
          ),
          const SizedBox(width: 6),
          Container(
            padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 2),
            decoration: BoxDecoration(borderRadius: BorderRadius.circular(12)),
            child: Text(
              '$count',
              style: TextStyle(
                color: color,
                fontSize: 12,
                fontWeight: FontWeight.bold,
              ),
            ),
          ),
          const SizedBox(width: 8),
          Expanded(child: Divider(color: Colors.grey.shade200)),
        ],
      ),
    );
  }

  Widget _buildEmpty() {
    return Center(
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          Icon(
            Icons.local_hospital_outlined,
            size: 72,
            color: Colors.grey.shade300,
          ),
          const SizedBox(height: 16),
          Text(
            'Chưa có dữ liệu liệu trình',
            style: TextStyle(
              fontSize: 16,
              color: Colors.grey.shade500,
              fontWeight: FontWeight.w500,
            ),
          ),
        ],
      ),
    );
  }
}

class _BuoiDieuTriCard extends StatelessWidget {
  final BuoiDieuTriModel caKham;
  final VoidCallback? onTap;

  const _BuoiDieuTriCard({required this.caKham, required this.onTap});

  @override
  Widget build(BuildContext context) {
    final isHoanThanh = caKham.trangThai == 'Hoàn thành';
    final color = isHoanThanh
        ? const Color(0xFF43A047)
        : const Color(0xFFFFA000);

    return Card(
      elevation: 2,
      margin: const EdgeInsets.only(bottom: 10),
      shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
      child: InkWell(
        onTap: onTap,
        borderRadius: BorderRadius.circular(12),
        child: Padding(
          padding: const EdgeInsets.all(14),
          child: Row(
            children: [
              Container(
                width: 40,
                height: 40,
                decoration: BoxDecoration(
                  shape: BoxShape.circle,
                  border: Border.all(width: 1),
                ),
                child: Icon(
                  isHoanThanh ? Icons.check : Icons.access_time,
                  color: color,
                  size: 20,
                ),
              ),
              const SizedBox(width: 12),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      'Buổi ${caKham.soBuoi}',
                      style: const TextStyle(
                        fontWeight: FontWeight.bold,
                        fontSize: 15,
                      ),
                    ),
                    const SizedBox(height: 2),
                    Text(
                      caKham.ngayDuKien.toString().split(' ')[0],
                      style: const TextStyle(color: Colors.grey, fontSize: 12),
                    ),
                    const SizedBox(height: 2),
                    Text(
                      caKham.trangThai,
                      style: TextStyle(fontSize: 13, color: color),
                    ),
                  ],
                ),
              ),
              if (isHoanThanh)
                Icon(Icons.chevron_right, color: Colors.grey.shade400),
            ],
          ),
        ),
      ),
    );
  }
}
