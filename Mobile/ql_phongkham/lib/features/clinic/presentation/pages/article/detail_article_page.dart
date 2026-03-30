import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:flutter_markdown/flutter_markdown.dart';
import 'package:ql_phongkham/core/utils/dialog_helper.dart';
import 'package:ql_phongkham/features/clinic/data/models/article_model.dart';
import 'package:ql_phongkham/features/clinic/data/repository/article_repository.dart';

class BaiVietDetailScreen extends StatefulWidget {
  final baiVietId;

  const BaiVietDetailScreen({super.key, required this.baiVietId});

  @override
  State<BaiVietDetailScreen> createState() => _BaiVietDetailScreenState();
}

class _BaiVietDetailScreenState extends State<BaiVietDetailScreen> {
  BaiVietModel? baiviet;
  @override
  void initState() {
    super.initState();
    loadBaiViet();
  }

  Widget fieldItem(String value) {
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

  Widget field(IconData icon, String label, String value) {
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
        fieldItem(value),
      ],
    );
  }

  Future<void> loadBaiViet() async {
    try {
      final baiVietId = widget.baiVietId;
      if (baiVietId == null) return;
      final data = await BaiVietRepository().getBaiViet(baiVietId);
      setState(() {
        baiviet = data;
      });
    } catch (e) {
      DialogHelper.showSnacFailed(
        context,
        e.toString().replaceFirst('Exception: ', ''),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    if (baiviet == null) {
      return const Scaffold(body: Center(child: CircularProgressIndicator()));
    }

    final bv = baiviet!;

    return Scaffold(
      appBar: AppBar(title: const Text("Chi tiết bài viết"), centerTitle: true),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            if (bv.hinhAnh != null && bv.hinhAnh!.isNotEmpty)
              ClipRRect(
                borderRadius: BorderRadius.circular(12),
                child: Image.network(
                  bv.hinhAnh!,
                  width: double.infinity,
                  height: 180,
                  fit: BoxFit.cover,
                ),
              ),

            const SizedBox(height: 15),
            Text(
              bv.tieuDe,
              style: const TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 10),
            Text(
              bv.tomTat ?? "",
              style: const TextStyle(fontSize: 14, color: Colors.grey),
            ),
            const SizedBox(height: 15),
            Card(
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(12),
              ),
              elevation: 2,
              child: Padding(
                padding: const EdgeInsets.all(16),
                child: Column(
                  children: [
                    field(
                      Icons.remove_red_eye,
                      "Lượt xem",
                      bv.luotXem.toString(),
                    ),
                    const SizedBox(height: 15),
                    field(
                      Icons.calendar_today,
                      "Ngày đăng",
                      DateFormat('dd/MM/yyyy HH:mm').format(bv.ngayDang),
                    ),
                    const SizedBox(height: 15),
                    field(
                      Icons.update,
                      "Cập nhật",
                      bv.ngayCapNhat != null
                          ? DateFormat(
                              'dd/MM/yyyy HH:mm',
                            ).format(bv.ngayCapNhat!)
                          : "Chưa cập nhật",
                    ),
                    const SizedBox(height: 15),
                    field(
                      Icons.info,
                      "Trạng thái",
                      bv.trangThai ?? "Chưa cập nhật",
                    ),
                  ],
                ),
              ),
            ),

            const SizedBox(height: 20),
            const Text(
              "Nội dung bài viết",
              style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 10),
            Card(
              shape: RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(12),
              ),
              elevation: 2,
              child: Padding(
                padding: const EdgeInsets.all(16),
                child: Markdown(
                  data: bv.noiDung?.toString() ?? "Không có nội dung",
                  shrinkWrap: true,
                  physics: const NeverScrollableScrollPhysics(),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
