import 'package:flutter/material.dart';
import 'package:ql_phongkham/core/utils/dialog_helper.dart';
import 'package:ql_phongkham/features/clinic/data/repository/auth_repository.dart';
import 'package:ql_phongkham/features/clinic/data/repository/profile_repository.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/auth/login_page.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/auth/reset_pass_page.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/medical_record/medical_record_page.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/monitoring/exam_page.dart';
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
  bool _isNotificationEnabled = true;
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
            leading: Icon(Icons.person, color: Colors.blue),
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
            leading: Icon(Icons.article, color: Colors.blue),
            title: Text('Hồ sơ bệnh án', style: TextStyle(fontSize: 14)),
            onTap: () {
              if (benhnhanid == null) {
                DialogHelper.showSnacFailed(
                  context,
                  'Không tìm thấy bệnh nhân',
                );
                return;
              }

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
            leading: Icon(Icons.health_and_safety, color: Colors.blue),
            title: Text('Theo dõi điều trị', style: TextStyle(fontSize: 14)),
            onTap: () {
              Navigator.push(
                context,
                MaterialPageRoute(
                  builder: (_) => DanhSachCaKhamHoanThanhPage(),
                ),
              );
            },
          ),
          Divider(),
          ListTile(
            leading: Icon(Icons.password, color: Colors.blue),
            title: Text('Thay đổi mật khẩu', style: TextStyle(fontSize: 14)),
            onTap: () {
              Navigator.push(
                context,
                MaterialPageRoute(builder: (_) => ResetPassPage()),
              );
            },
          ),
          Divider(),
          ListTile(
            leading: Icon(
              _isNotificationEnabled
                  ? Icons.notifications
                  : Icons.notifications_off,
              color: _isNotificationEnabled ? Colors.blue : Colors.grey,
            ),
            title: Text('Thông báo', style: TextStyle(fontSize: 14)),
            trailing: Icon(
              Icons.arrow_forward_ios,
              size: 14,
              color: Colors.grey,
            ),
            onTap: _showNotificationDialog,
          ),
          Divider(),
          ListTile(
            leading: Icon(Icons.logout, color: Colors.blue),
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

  Future<void> _toggleNotification(bool value) async {
    try {
      final prefs = await SharedPreferences.getInstance();
      final thongTinId = prefs.getInt('thongTinId');
      if (thongTinId == null) return;

      if (!value) {
        await AuthRepository().updateFCM('', thongTinId);
      } else {
        final fcmToken = prefs.getString('fcmToken');
        if (fcmToken != null) {
          await AuthRepository().updateFCM(fcmToken, thongTinId);
        }
      }

      setState(() {
        _isNotificationEnabled = value;
      });

      DialogHelper.showSnackSuccess(
        context,
        value ? 'Đã bật thông báo' : 'Đã tắt thông báo',
      );
    } catch (e) {
      DialogHelper.showSnacFailed(context, e.toString());
    }
  }

  void _showNotificationDialog() {
    showDialog(
      context: context,
      builder: (_) => StatefulBuilder(
        builder: (context, setStateDialog) => AlertDialog(
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(16),
          ),
          title: const Row(
            children: [
              Icon(Icons.notifications, color: Colors.blue),
              SizedBox(width: 8),
              Text('Thông báo', style: TextStyle(fontSize: 16)),
            ],
          ),
          content: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              const Divider(),
              Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: [
                  const Text('Nhận thông báo', style: TextStyle(fontSize: 14)),
                  Switch(
                    value: _isNotificationEnabled,
                    onChanged: (value) async {
                      setStateDialog(() => _isNotificationEnabled = value);
                      await _toggleNotification(value);
                    },
                  ),
                ],
              ),
              const SizedBox(height: 4),
              Text(
                _isNotificationEnabled
                    ? 'Bạn đang nhận thông báo từ ứng dụng'
                    : 'Bạn đã tắt thông báo từ ứng dụng',
                style: TextStyle(fontSize: 12, color: Colors.grey.shade600),
              ),
            ],
          ),
          actions: [
            TextButton(
              onPressed: () => Navigator.pop(context),
              child: const Text('Đóng'),
            ),
          ],
        ),
      ),
    );
  }
}
