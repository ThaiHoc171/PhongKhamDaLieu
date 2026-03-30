import 'package:flutter/material.dart';
import 'package:ql_phongkham/features/clinic/data/models/article_model.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/article/detail_article_page.dart';

class ArticleSection extends StatefulWidget {
  final List<BaiVietModel> baiVietList;
  final bool isLoading;

  const ArticleSection({
    super.key,
    required this.baiVietList,
    required this.isLoading,
    // Bỏ isExpanded và onToggle
  });

  @override
  State<ArticleSection> createState() => _ArticleSectionState();
}

class _ArticleSectionState extends State<ArticleSection> {
  bool _isExpanded = false; // chỉ dùng cái này

  @override
  Widget build(BuildContext context) {
    final list = widget.baiVietList;

    return Container(
      padding: const EdgeInsets.all(10),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(20),
        border: Border.all(color: Colors.brown, width: 3),
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Row(
            children: [
              Icon(
                Icons.library_books_rounded,
                size: 18,
                color: Colors.blueAccent,
              ),
              SizedBox(width: 5),
              Text(
                "Bài viết",
                style: TextStyle(
                  fontSize: 15,
                  fontWeight: FontWeight.bold,
                  color: Colors.blueAccent,
                ),
              ),
            ],
          ),
          const SizedBox(height: 10),

          widget.isLoading
              ? const Center(child: CircularProgressIndicator())
              : ListView.builder(
                  shrinkWrap: true,
                  physics: const NeverScrollableScrollPhysics(),
                  itemCount: _isExpanded
                      ? list.length
                      : list.length.clamp(0, 5),
                  itemBuilder: (context, index) {
                    final bv = list[index];
                    return Card(
                      margin: const EdgeInsets.only(bottom: 10),
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(15),
                      ),
                      child: InkWell(
                        borderRadius: BorderRadius.circular(15),
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (_) =>
                                  BaiVietDetailScreen(baiVietId: bv.baiVietID),
                            ),
                          );
                        },
                        child: Padding(
                          padding: const EdgeInsets.all(10),
                          child: Text(
                            bv.tieuDe,
                            style: const TextStyle(
                              fontSize: 14,
                              color: Colors.blue,
                              fontWeight: FontWeight.w500,
                            ),
                          ),
                        ),
                      ),
                    );
                  },
                ),

          if (list.length > 5)
            Center(
              child: TextButton(
                onPressed: () => setState(() => _isExpanded = !_isExpanded),
                child: Text(_isExpanded ? "Thu gọn" : "Xem thêm"),
              ),
            ),
        ],
      ),
    );
  }
}
