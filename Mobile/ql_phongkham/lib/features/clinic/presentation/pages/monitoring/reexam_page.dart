import 'package:flutter/material.dart';
import 'package:ql_phongkham/features/clinic/data/models/reexam_model.dart';
import 'package:ql_phongkham/features/clinic/data/repository/booking_repository.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/booking/examination_page.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/auth/auth_button.dart';

/// Hiển thị lịch tái khám (các ca khám trạng thái != 'Hoàn thành') của bệnh nhân.
class LichTaiKhamPage extends StatefulWidget {
  final int benhNhanId;
  const LichTaiKhamPage({super.key, required this.benhNhanId});

  @override
  State<LichTaiKhamPage> createState() => _LichTaiKhamPageState();
}

class _LichTaiKhamPageState extends State<LichTaiKhamPage> {
  bool isLoading = true;
  TaiKhamModel? taiKham;
  @override
  void initState() {
    super.initState();
    _load();
  }

  Future<void> _load() async {
    try {
      final repo = LichKhamRepository();
      final data = await repo.checkTaiKhamPending(widget.benhNhanId);
      setState(() {
        taiKham = data;
        isLoading = false;
      });
    } catch (e) {
      setState(() {
        taiKham = null;
        isLoading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.grey.shade50,
      appBar: AppBar(
        title: const Text("Lịch tái khám"),
        centerTitle: true,
        backgroundColor: Colors.white,
        foregroundColor: Colors.black87,
        elevation: 0,
      ),
      body: isLoading
          ? const Center(child: CircularProgressIndicator())
          : _buildContent(),
    );
  }

  Widget _buildContent() {
    if (taiKham == null || taiKham!.taiKhamID == 0) {
      return _buildEmpty();
    }
    final ngay = taiKham!.ngayDuKien.toString().split(' ')[0];
    final checkNgay = kiemTraLich(taiKham!.ngayDuKien);
    if (taiKham!.taiKhamID == 0) {
      return _buildEmpty();
    }
    if (!checkNgay && taiKham!.trangThai == 'Chờ khám') {
      return _buildCheck(ngay, taiKham!.taiKhamID);
    }
    return Center(
      child: Container(
        margin: const EdgeInsets.all(20),
        padding: const EdgeInsets.all(20),
        decoration: BoxDecoration(
          color: Colors.white,
          borderRadius: BorderRadius.circular(16),
          boxShadow: [BoxShadow(blurRadius: 10)],
        ),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            Icon(Icons.calendar_today, size: 40, color: Colors.blue),
            const SizedBox(height: 16),

            Text(
              "Bạn có lịch tái khám dự kiến vào ngày:",
              textAlign: TextAlign.center,
              style: TextStyle(fontSize: 14, color: Colors.grey.shade600),
            ),

            const SizedBox(height: 8),

            Text(
              ngay,
              style: const TextStyle(
                fontSize: 20,
                fontWeight: FontWeight.bold,
                color: Colors.blue,
              ),
            ),

            const SizedBox(height: 16),

            Text(
              "Nếu chưa đăng ký lịch khám, hãy đăng ký lịch ngay để được theo dõi sức khỏe thường xuyên!",
              textAlign: TextAlign.center,
              style: TextStyle(fontSize: 13, color: Colors.grey.shade600),
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildEmpty() {
    return Center(
      child: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          Icon(
            Icons.calendar_month_outlined,
            size: 72,
            color: Colors.grey.shade300,
          ),
          const SizedBox(height: 16),
          Text(
            'Bạn chưa có lịch tái khám',
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

  Widget _buildCheck(String ngay, int id) {
    return Center(
      child: Container(
        margin: const EdgeInsets.all(20),
        padding: const EdgeInsets.all(20),
        decoration: BoxDecoration(
          color: Colors.white,
          borderRadius: BorderRadius.circular(16),
          boxShadow: [BoxShadow(blurRadius: 10)],
        ),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            Icon(Icons.calendar_today, size: 40, color: Colors.blue),
            const SizedBox(height: 16),
            Text(
              "Bạn đã trễ lịch khám dự kiến!\nLịch khám dự kiến là: ",
              textAlign: TextAlign.center,
              style: TextStyle(fontSize: 14, color: Colors.grey.shade600),
            ),
            const SizedBox(height: 8),
            Text(
              ngay,
              style: const TextStyle(
                fontSize: 20,
                fontWeight: FontWeight.bold,
                color: Colors.blue,
              ),
            ),

            const SizedBox(height: 16),

            AuthButton(
              buttonText: 'Đặt lịch ngay',
              onPressed: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(builder: (_) => LichKhamScreen()),
                );
              },
            ),
          ],
        ),
      ),
    );
  }

  bool kiemTraLich(DateTime ngayHen) {
    final now = DateTime.now();

    if (ngayHen.isBefore(now)) {
      return false;
    }
    return true;
  }
}
