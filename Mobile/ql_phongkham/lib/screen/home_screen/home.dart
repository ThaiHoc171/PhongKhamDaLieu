import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:ql_phongkham/screen/Booking_screen/lichkham.dart';
import 'package:ql_phongkham/screen/home_screen/menubar.dart';
import 'package:ql_phongkham/screen/bottombar_screen/profile.dart';
import 'dart:convert';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:carousel_slider/carousel_slider.dart';
import 'package:flutter/services.dart';

class HomeScreen extends StatefulWidget {
  final String token;

  const HomeScreen({super.key, required this.token});

  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  int _selectedIndex = 0;
  final items = ['assets/images/banner1.jpg', 'assets/images/banner2.png'];
  int myCurrentIndex = 0;

  @override
  void initState() {
    super.initState();
  }

  Future<void> checkTaiKham() async {
    try {
      final prefs = await SharedPreferences.getInstance();
      final token = prefs.getString('accessToken');
      final benhNhanId = prefs.getInt('benhNhanId');

      if (token == null || benhNhanId == null) {
        showThongBao(context, 'Thiếu thông tin đăng nhập');
        return;
      }

      final url =
          'https://clinicjwt-api-bperhwd0dne7c9c0.southeastasia-01.azurewebsites.net/api/TaiKham/Benhnhan/$benhNhanId';

      final response = await http.get(
        Uri.parse(url),
        headers: {'Authorization': 'Bearer $token', 'accept': '*/*'},
      );

      if (response.statusCode == 200) {
        final data = jsonDecode(response.body);

        if (data is List && data.isEmpty) {
          showThongBao(context, 'Không có lịch tái khám cho bạn');
        } else {
          // Có dữ liệu
          print("Có lịch tái khám: $data");
        }
      } else if (response.statusCode == 404) {
        showThongBao(context, 'Không có lịch tái khám cho bạn');
      } else if (response.statusCode == 401) {
        showThongBao(context, 'Phiên đăng nhập hết hạn');
      } else {
        showThongBao(context, 'Lỗi: ${response.statusCode}');
      }
    } catch (e) {
      showThongBao(context, 'Lỗi kết nối: $e');
    }
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
    return PopScope(
      canPop: false,
      onPopInvokedWithResult: (didPop, result) async {
        if (didPop) return;

        final shouldExit = await showDialog<bool>(
          context: context,
          builder: (context) => AlertDialog(
            title: const Text("Xác nhận"),
            content: const Text("Bạn có muốn thoát ứng dụng không?"),
            actions: [
              TextButton(
                onPressed: () => Navigator.of(context).pop(false),
                child: const Text("Không"),
              ),
              TextButton(
                onPressed: () => Navigator.of(context).pop(true),
                child: const Text("Có"),
              ),
            ],
          ),
        );

        if (shouldExit == true) {
          SystemNavigator.pop();
        }
      },

      child: Scaffold(
        drawer: MenuBarScreen(),
        appBar: AppBar(
          backgroundColor: Colors.blueAccent,
          elevation: 0,
          centerTitle: true,
          titleTextStyle: TextStyle(fontFamily: 'Time New Roman', fontSize: 18),
          title: Text('Phòng khám'),
        ),
        body: SafeArea(
          child: _selectedIndex == 0 ? homeScreen() : ProfileScreen(),
        ),
        bottomNavigationBar: BottomNavigationBar(
          currentIndex: _selectedIndex,
          type: BottomNavigationBarType.fixed,
          onTap: (index) {
            setState(() {
              _selectedIndex = index;
            });
          },
          items: [
            BottomNavigationBarItem(icon: Icon(Icons.home), label: 'Trang chủ'),
            BottomNavigationBarItem(icon: Icon(Icons.person), label: 'Hồ sơ'),
            BottomNavigationBarItem(
              icon: Icon(Icons.date_range),
              label: 'Lịch khám',
            ),
          ],
          backgroundColor: Colors.black,
          selectedItemColor: Colors.white,
          unselectedItemColor: Colors.grey,
          selectedLabelStyle: TextStyle(
            fontWeight: FontWeight.w600,
            fontSize: 14,
          ),
        ),
      ),
    );
  }

  Widget homeScreen() {
    return SingleChildScrollView(
      child: Column(
        children: [
          //Gioi thieu
          SizedBox(height: 10),
          Container(
            width: MediaQuery.of(context).size.width - 15,
            height: 150,
            decoration: BoxDecoration(
              color: Colors.white,
              borderRadius: BorderRadius.all(Radius.circular(20)),
            ),
            clipBehavior: Clip.antiAlias,
            child: Stack(
              children: [
                CarouselSlider(
                  items: items.map((path) {
                    return Image.asset(
                      path,
                      width: double.infinity,
                      height: double.infinity,
                      fit: BoxFit.cover,
                    );
                  }).toList(),
                  options: CarouselOptions(
                    height: 150,
                    autoPlay: true,
                    viewportFraction: 1.0,
                    autoPlayInterval: Duration(seconds: 3),
                    autoPlayAnimationDuration: Duration(milliseconds: 800),
                    enlargeCenterPage: false,
                    onPageChanged: (index, reason) {
                      setState(() {
                        myCurrentIndex = index;
                      });
                    },
                  ),
                ),

                Positioned(
                  bottom: 8,
                  left: 0,
                  right: 0,
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: items.asMap().entries.map((entry) {
                      return AnimatedContainer(
                        duration: Duration(milliseconds: 300),
                        width: myCurrentIndex == entry.key ? 12 : 8,
                        height: 8,
                        margin: EdgeInsets.symmetric(horizontal: 4),
                        decoration: BoxDecoration(
                          borderRadius: BorderRadius.circular(10),
                          color: myCurrentIndex == entry.key
                              ? Colors.white
                              : Colors.white54,
                        ),
                      );
                    }).toList(),
                  ),
                ),
              ],
            ),
          ),

          //Đặt lịch khám
          SizedBox(height: 10),
          Container(
            alignment: Alignment.topLeft,
            width: MediaQuery.of(context).size.width - 20,
            height: 150,
            decoration: BoxDecoration(
              color: Colors.white,
              borderRadius: BorderRadius.all(Radius.circular(20)),
            ),
            child: Column(
              children: [
                Row(
                  children: [
                    SizedBox(width: 10),
                    Icon(Icons.date_range, size: 18, color: Colors.blueAccent),
                    SizedBox(width: 5),
                    Text(
                      "Đặt lịch khám",
                      style: TextStyle(
                        fontSize: 15,
                        fontWeight: FontWeight.bold,
                        color: Colors.blueAccent,
                      ),
                    ),
                  ],
                ),
                SizedBox(height: 10),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceAround,
                  children: [
                    ElevatedButton(
                      onPressed: () {
                        Navigator.push(
                          context,
                          MaterialPageRoute(builder: (_) => LichKhamScreen()),
                        );
                      },
                      child: const Text(
                        'Đặt lịch khám',
                        style: TextStyle(fontSize: 15),
                      ),
                    ),
                    ElevatedButton(
                      onPressed: () {},
                      child: const Text(
                        'Đặt lịch điều trị',
                        style: TextStyle(fontSize: 15),
                      ),
                    ),
                  ],
                ),
                SizedBox(height: 10),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceAround,
                  children: [
                    ElevatedButton(
                      onPressed: () {
                        checkTaiKham();
                      },

                      child: const Text(
                        'Đặt lịch tái khám',
                        style: TextStyle(fontSize: 15),
                      ),
                    ),
                    ElevatedButton(
                      onPressed: () {},
                      child: const Text(
                        'Tư vấn hỗ trợ',
                        style: TextStyle(fontSize: 15),
                      ),
                    ),
                  ],
                ),
              ],
            ),
          ),

          //Bác sĩ profile
          SizedBox(height: 10),
          Container(
            alignment: Alignment.topLeft,
            width: MediaQuery.of(context).size.width - 20,
            height: 100,
            decoration: BoxDecoration(
              color: Colors.white,
              border: Border.all(width: 2),
              borderRadius: BorderRadius.all(Radius.circular(20)),
            ),
            child: Row(
              children: [
                SizedBox(width: 10),
                Icon(
                  Icons.medical_services,
                  size: 18,
                  color: Colors.blueAccent,
                ),
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
          ),

          //Bài viết liên quan
          SizedBox(height: 10),
          Container(
            alignment: Alignment.topLeft,
            width: MediaQuery.of(context).size.width - 20,
            height: 200,
            decoration: BoxDecoration(
              color: Colors.white,
              border: Border.all(width: 2),
              borderRadius: BorderRadius.all(Radius.circular(20)),
            ),
            child: Row(
              children: [
                SizedBox(width: 10),
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
          ),
        ],
      ),
    );
  }
}
