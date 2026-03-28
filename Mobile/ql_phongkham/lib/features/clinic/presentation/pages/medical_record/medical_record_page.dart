import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:ql_phongkham/features/clinic/data/models/medical_record_model.dart';
import 'package:ql_phongkham/features/clinic/data/repository/medical_record_repository.dart';

class HoSoBenhAnPage extends StatefulWidget {
  final int benhNhanId;
  const HoSoBenhAnPage({super.key, required this.benhNhanId});

  @override
  State<HoSoBenhAnPage> createState() => _HoSoBenhAnPageState();
}

class _HoSoBenhAnPageState extends State<HoSoBenhAnPage> {
  final _repo = HoSoBenhAnRepository();
  HoSoBenhAnModel? hoSo;
  bool isLoading = true;
  String? error;

  @override
  void initState() {
    super.initState();
    fetchData();
  }

  Future<void> fetchData() async {
    try {
      final result = await _repo.getHoSo(widget.benhNhanId);
      setState(() {
        hoSo = result;
        isLoading = false;
      });
    } catch (e) {
      setState(() {
        error = e.toString();
        isLoading = false;
      });
    }
  }

  String formatDate(DateTime date) =>
      DateFormat('dd/MM/yyyy HH:mm').format(date);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.grey.shade50,
      appBar: AppBar(
        title: const Text("Hồ sơ bệnh án"),
        elevation: 0,
        backgroundColor: Colors.white,
        foregroundColor: Colors.black87,
      ),
      body: _buildBody(),
    );
  }

  Widget _buildBody() {
    if (isLoading) return const Center(child: CircularProgressIndicator());
    if (hoSo == null) return const Center(child: Text("Không có dữ liệu"));

    return SingleChildScrollView(
      padding: const EdgeInsets.all(16),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          _buildSection("Bệnh lý", [
            _buildFieldCard("Bệnh nền", hoSo!.benhNen),
            _buildFieldCard("Dị ứng", hoSo!.diUng),
          ]),
          _buildSection("Tiền sử", [
            _buildFieldCard("Tiền sử bệnh", hoSo!.tienSuBenh),
            _buildFieldCard("Tiền sử gia đình", hoSo!.tienSuGiaDinh),
          ]),
          _buildSection("Sinh hoạt", [
            _buildFieldCard("Thói quen sống", hoSo!.thoiQuenSong),
            _buildFieldCard("Thông tin khác", hoSo!.thongTinKhac),
          ]),
          _buildSection("Thời gian", [
            Row(
              children: [
                Expanded(
                  child: _buildFieldCard("Ngày tạo", formatDate(hoSo!.ngayTao)),
                ),
                const SizedBox(width: 8),
                Expanded(
                  child: _buildFieldCard(
                    "Cập nhật",
                    formatDate(hoSo!.ngayCapNhat),
                  ),
                ),
              ],
            ),
          ]),
        ],
      ),
    );
  }

  Widget _buildSection(String title, List<Widget> children) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Padding(
          padding: const EdgeInsets.only(top: 20, bottom: 10),
          child: Row(
            children: [
              Text(
                title.toUpperCase(),
                style: TextStyle(
                  fontSize: 11,
                  fontWeight: FontWeight.w600,
                  color: Colors.grey.shade500,
                  letterSpacing: 0.8,
                ),
              ),
              const SizedBox(width: 8),
              Expanded(child: Divider(color: Colors.grey.shade200, height: 1)),
            ],
          ),
        ),
        ...children,
      ],
    );
  }

  Widget _buildFieldCard(String label, String value) {
    final isEmpty = value.trim().isEmpty;
    return Container(
      width: double.infinity,
      margin: const EdgeInsets.only(bottom: 8),
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(12),
        border: Border.all(color: Colors.grey.shade100),
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(
            label,
            style: TextStyle(fontSize: 12, color: Colors.grey.shade500),
          ),
          const SizedBox(height: 4),
          Text(
            isEmpty ? "Chưa có thông tin" : value,
            style: TextStyle(
              fontSize: 14,
              color: isEmpty ? Colors.grey.shade400 : Colors.black87,
              fontStyle: isEmpty ? FontStyle.italic : FontStyle.normal,
              height: 1.5,
            ),
          ),
        ],
      ),
    );
  }
}
