import 'package:flutter/material.dart';
import 'package:ql_phongkham/core/utils/dialog_helper.dart';
import 'package:ql_phongkham/features/clinic/data/repository/profile_repository.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/auth/login_page.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/medical_record/medical_record_page.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/profile/profile_Update_page.dart';
import 'package:shared_preferences/shared_preferences.dart';

class MenuBarScreen extends StatefulWidget {
  const MenuBarScreen({super.key});

  @override
  State<StatefulWidget> createState() => _MenuBarScreenState();
}

class _MenuBarScreenState extends State<MenuBarScreen> {
  String? linkAvatar;
  String hoTen = "";
  String email = "";
  int? benhnhanid;
  @override
  void initState() {
    super.initState();
    loadProfile();
  }

  Future<void> loadProfile() async {
    try {
      final prefs = await SharedPreferences.getInstance();
      final token = prefs.getString('accessToken');
      final thongTinId = prefs.getInt('thongTinId');
      final benhNhanId = prefs.getInt('benhNhanId');
      if (token == null || thongTinId == null) return;

      final data = await ProfileRepository().getProfile(thongTinId);
      setState(() {
        hoTen = data.hoTen;
        email = data.emailLienHe;
        linkAvatar = _buildAvatarUrl(data.avatar);
        benhnhanid = benhNhanId;
      });
    } catch (e) {
      DialogHelper.showSnacFailed(context, e.toString());
    }
  }

  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: ListView(
        padding: EdgeInsets.zero,
        children: [
          UserAccountsDrawerHeader(
            accountName: Text(
              hoTen,
              style: TextStyle(
                fontWeight: FontWeight.bold,
                shadows: [
                  Shadow(
                    blurRadius: 6,
                    color: Colors.black,
                    offset: Offset(2, 2),
                  ),
                ],
              ),
            ),
            accountEmail: Text(
              email,
              style: TextStyle(
                fontWeight: FontWeight.bold,
                shadows: [
                  Shadow(
                    blurRadius: 6,
                    color: Colors.black,
                    offset: Offset(2, 2),
                  ),
                ],
              ),
            ),
            currentAccountPicture: CircleAvatar(
              radius: 60,
              backgroundImage: linkAvatar != null && linkAvatar!.isNotEmpty
                  ? NetworkImage(linkAvatar!)
                  : const AssetImage("assets/images/user.png") as ImageProvider,
            ),
            decoration: BoxDecoration(
              color: Colors.blueAccent,
              image: DecorationImage(
                image: AssetImage('assets/images/Background.webp'),
                fit: BoxFit.cover,
              ),
            ),
          ),
          ListTile(
            leading: Icon(Icons.person),
            title: Text('Cập nhật thông tin', style: TextStyle(fontSize: 14)),
            onTap: () {
              Navigator.push(
                context,
                MaterialPageRoute(builder: (_) => ProfileUpdateScreen()),
              );
            },
          ),
          Divider(),
          ListTile(
            leading: Icon(Icons.article),
            title: Text('Hồ sơ bệnh án', style: TextStyle(fontSize: 14)),
            onTap: () {
              Navigator.push(
                context,
                MaterialPageRoute(
                  builder: (_) => HoSoBenhAnPage(benhNhanId: benhnhanid!),
                ),
              );
            },
          ),
          Divider(),
          ListTile(
            leading: Icon(Icons.password),
            title: Text('Thay đổi mật khẩu', style: TextStyle(fontSize: 14)),
            onTap: () {},
          ),
          Divider(),
          ListTile(
            leading: Icon(Icons.notifications),
            title: Text('Thông báo', style: TextStyle(fontSize: 14)),
            onTap: () {},
          ),
          Divider(),
          ListTile(
            leading: Icon(Icons.settings),
            title: Text('Cài đặt', style: TextStyle(fontSize: 14)),
            onTap: () {},
          ),
          Divider(),
          ListTile(
            leading: Icon(Icons.logout),
            title: Text('Đăng xuất', style: TextStyle(fontSize: 14)),
            onTap: () {
              showDialog<bool>(
                context: context,
                builder: (context) => AlertDialog(
                  title: const Text("Xác nhận"),
                  content: const Text("Bạn có muốn đăng xuất ứng dụng không?"),
                  actions: [
                    TextButton(
                      onPressed: () => Navigator.of(context).pop(false),
                      child: const Text("Không"),
                    ),
                    TextButton(
                      onPressed: () => Navigator.pushAndRemoveUntil(
                        context,
                        MaterialPageRoute(builder: (_) => LoginPage()),
                        (route) => false,
                      ),
                      child: const Text("Có"),
                    ),
                  ],
                ),
              );
            },
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
