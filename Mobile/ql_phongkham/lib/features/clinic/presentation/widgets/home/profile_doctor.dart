import 'package:flutter/material.dart';
import 'package:ql_phongkham/features/clinic/data/models/doctor_profile_model.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/profile/doctor_profile_page.dart';

class DoctorSection extends StatelessWidget {
  final List<BacSiProfileModel> bacSiList;
  final bool isLoading;

  const DoctorSection({
    super.key,
    required this.bacSiList,
    required this.isLoading,
  });

  @override
  Widget build(BuildContext context) {
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
              Icon(Icons.medical_services, size: 18, color: Colors.blueAccent),
              SizedBox(width: 5),
              Text(
                "Bác sĩ",
                style: TextStyle(
                  fontSize: 15,
                  fontWeight: FontWeight.bold,
                  color: Colors.blueAccent,
                ),
              ),
            ],
          ),
          const SizedBox(height: 10),
          isLoading
              ? const Center(child: CircularProgressIndicator())
              : SizedBox(
                  height: 205,
                  child: ListView.builder(
                    scrollDirection: Axis.horizontal,
                    itemCount: bacSiList.length,
                    itemBuilder: (context, index) {
                      final bacSi = bacSiList[index];

                      return GestureDetector(
                        onTap: () {
                          Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (_) => NhanVienDetailScreen(
                                nhanVienId: bacSi.nhanVienID,
                                bacSiId: bacSi.bacSiProfileID,
                              ),
                            ),
                          );
                        },
                        child: Container(
                          width: 160,
                          margin: const EdgeInsets.only(right: 10),
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
                    },
                  ),
                ),
        ],
      ),
    );
  }

  Widget imageProfile(String Avatar) {
    final linkAvatar = _buildAvatarUrl(Avatar);
    return Center(
      child: Stack(
        children: [
          CircleAvatar(
            radius: 60,
            backgroundImage: linkAvatar != null && linkAvatar.isNotEmpty
                ? NetworkImage(linkAvatar)
                : const AssetImage("assets/images/user.png") as ImageProvider,
          ),
        ],
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
