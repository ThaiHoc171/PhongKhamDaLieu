import 'package:flutter/material.dart';
import 'package:ql_phongkham/features/clinic/data/models/doctor_profile_model.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/booking/examination_page.dart';

class ChooseDoctorSection extends StatelessWidget {
  final List<BacSiProfileModel> bacSiList;
  final bool isLoading;

  const ChooseDoctorSection({
    super.key,
    required this.bacSiList,
    required this.isLoading,
  });

  List<BacSiProfileModel> get _filteredBacSiList {
    return bacSiList
        .where((bs) => bs.nhanVienID == 1 || bs.nhanVienID == 2)
        .toList();
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      color: Colors.white,
      child: Center(
        child: Container(
          margin: const EdgeInsets.all(16),
          padding: const EdgeInsets.all(10),
          decoration: BoxDecoration(
            color: Colors.white,
            borderRadius: BorderRadius.circular(20),
            border: Border.all(color: Colors.brown, width: 3),
          ),
          child: Column(
            mainAxisSize: MainAxisSize.min,
            crossAxisAlignment: CrossAxisAlignment.center,
            children: [
              const Row(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Icon(
                    Icons.medical_services,
                    size: 18,
                    color: Colors.blueAccent,
                  ),
                  SizedBox(width: 5),
                  Text(
                    "Bạn có muốn chọn bác sĩ để khám không?",
                    style: TextStyle(
                      fontSize: 13,
                      fontWeight: FontWeight.bold,
                      color: Colors.blueAccent,
                    ),
                  ),
                ],
              ),
              const SizedBox(height: 10),
              isLoading
                  ? const Center(child: CircularProgressIndicator())
                  : Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: _filteredBacSiList.map((bacSi) {
                        return GestureDetector(
                          onTap: () {
                            Navigator.push(
                              context,
                              MaterialPageRoute(
                                builder: (_) => LichKhamScreen(
                                  nhanVienId: bacSi.nhanVienID,
                                ),
                              ),
                            );
                          },
                          child: Container(
                            width: 140,
                            margin: const EdgeInsets.symmetric(horizontal: 5),
                            child: Card(
                              shape: RoundedRectangleBorder(
                                borderRadius: BorderRadius.circular(15),
                              ),
                              child: Padding(
                                padding: const EdgeInsets.all(5),
                                child: Column(
                                  children: [
                                    imageProfile(bacSi.hinhAnh),
                                    const SizedBox(height: 5),
                                    Text(
                                      bacSi.chuyenMon,
                                      textAlign: TextAlign.center,
                                      style: const TextStyle(
                                        fontWeight: FontWeight.bold,
                                        fontSize: 13,
                                      ),
                                    ),
                                    const SizedBox(height: 5),
                                    Text(
                                      bacSi.hoTen,
                                      textAlign: TextAlign.center,
                                      style: const TextStyle(
                                        fontSize: 12,
                                        color: Colors.grey,
                                      ),
                                    ),
                                  ],
                                ),
                              ),
                            ),
                          ),
                        );
                      }).toList(),
                    ),
              const SizedBox(height: 10),
              SizedBox(
                width: double.infinity,
                child: OutlinedButton(
                  onPressed: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (_) => LichKhamScreen(nhanVienId: null),
                      ),
                    );
                  },
                  style: OutlinedButton.styleFrom(
                    foregroundColor: Colors.grey[600],
                    side: BorderSide(color: Colors.grey.shade300),
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(12),
                    ),
                    padding: const EdgeInsets.symmetric(vertical: 12),
                  ),
                  child: const Text(
                    "Không chọn",
                    style: TextStyle(fontSize: 14),
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget imageProfile(String Avatar) {
    final linkAvatar = _buildAvatarUrl(Avatar);
    return Center(
      child: CircleAvatar(
        radius: 60,
        backgroundImage: linkAvatar != null && linkAvatar.isNotEmpty
            ? NetworkImage(linkAvatar)
            : const AssetImage("assets/images/user.png") as ImageProvider,
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
