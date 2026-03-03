import 'package:flutter/material.dart';
import 'package:ql_phongkham/main.dart';
import 'package:shared_preferences/shared_preferences.dart';

class MenuBarScreen extends StatefulWidget {
  const MenuBarScreen({super.key});

  @override
  State<StatefulWidget> createState() => _MenuBarScreenState();
}

class _MenuBarScreenState extends State<MenuBarScreen> {
  String hoTen = '';
  String email = '';
  String chucVu = '';

  Future<void> loadUserData() async {
    final prefs = await SharedPreferences.getInstance();

    setState(() {
      hoTen = prefs.getString('hoTen') ?? '';
      email = prefs.getString('email') ?? '';
      chucVu = prefs.getString('chucVu') ?? '';
    });
  }

  @override
  void initState() {
    super.initState();
    loadUserData();
  }

  void showThongBao(BuildContext context, String message) {
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text("Thông báo"),
        content: Text(message),
        actions: [
          TextButton(
            onPressed: () {
              Navigator.of(context).pop();
            },
            child: const Text("OK"),
          ),
        ],
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: ListView(
        padding: EdgeInsets.zero,
        children: [
          UserAccountsDrawerHeader(
            accountName: Text(hoTen),
            accountEmail: Text(email),
            currentAccountPicture: CircleAvatar(
              child: ClipOval(
                child: Image.asset(
                  'assets/images/user.png',
                  width: 90,
                  height: 90,
                  fit: BoxFit.cover,
                ),
              ),
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
            onTap: () {},
          ),
          Divider(),
          ListTile(
            leading: Icon(Icons.date_range),
            title: Text('Lịch khám', style: TextStyle(fontSize: 14)),
            onTap: () {},
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
                        MaterialPageRoute(builder: (_) => LoginScreen()),
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
}
