import 'package:flutter/material.dart';
import 'package:ql_phongkham/features/clinic/data/models/reexam_model.dart';
import 'package:ql_phongkham/features/clinic/data/repository/booking_repository.dart';

/// Hiển thị lịch tái khám (các ca khám trạng thái != 'Hoàn thành') của bệnh nhân.
class LichTaiKhamPage extends StatefulWidget {
  final int benhNhanId;
  const LichTaiKhamPage({super.key, required this.benhNhanId});

  @override
  State<LichTaiKhamPage> createState() => _LichTaiKhamPageState();
}

class _LichTaiKhamPageState extends State<LichTaiKhamPage> {
  List<TaiKhamModel> lichTaiKham = [];
  bool isLoading = true;

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
        lichTaiKham = data as List<TaiKhamModel>;
        isLoading = false;
      });
    } catch (e) {
      setState(() => isLoading = false);
      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(content: Text(e.toString().replaceFirst('Exception: ', ''))),
        );
      }
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
    if (lichTaiKham.isEmpty) {
      return _buildEmpty();
    }

    final item = lichTaiKham.first; // lấy lịch gần nhất

    final ngay = item.ngayDuKien.toString().split(' ')[0];

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
}
