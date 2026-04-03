import 'package:flutter/material.dart';
import 'package:ql_phongkham/core/utils/dialog_helper.dart';
import 'package:ql_phongkham/features/clinic/data/models/article_model.dart';
import 'package:ql_phongkham/features/clinic/data/models/doctor_profile_model.dart';
import 'package:ql_phongkham/features/clinic/data/models/reexam_model.dart';
import 'package:ql_phongkham/features/clinic/data/models/treatment_model.dart';
import 'package:ql_phongkham/features/clinic/data/repository/article_repository.dart';
import 'package:ql_phongkham/features/clinic/data/repository/doctor_profile_repository.dart';
import 'package:ql_phongkham/features/clinic/data/repository/booking_repository.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/booking/list_booking_page.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/booking/examination_page.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/booking/treatment_page.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/profile/proflie_page.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/home/article.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/home/profile_doctor.dart';
import 'package:ql_phongkham/screen/home_screen/menubar.dart';
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
  List<BacSiProfileModel> bacSiList = [];
  List<BaiVietModel> baiVietList = [];
  bool isLoadingBacSi = true;
  bool isLoadingBaiViet = true;
  bool isExpandArticle = false;
  bool hasTaiKham = false;
  bool hasDieuTri = false;

  int? taiKhamId;
  int? lieuTrinhId;
  int _selectedIndex = 0;
  final items = ['assets/images/banner1.jpg', 'assets/images/banner2.png'];
  int myCurrentIndex = 0;

  @override
  void initState() {
    super.initState();
    loadData();
  }

  Future<void> loadData() async {
    try {
      final prefs = await SharedPreferences.getInstance();
      final benhNhanId = prefs.getInt('benhNhanId')!;

      final repo = LichKhamRepository();

      final results = await Future.wait([
        BacsiProfileRepository().getBacSiProfile(),
        BaiVietRepository().getListBaiViet(),
        repo.checkTaiKhamPending(benhNhanId),
        repo.checkDieuTriPending(benhNhanId),
      ]);

      final taiKham = results[2] as TaiKhamModel?;
      final dieuTri = results[3] as LieuTrinhDieuTriModel?;

      setState(() {
        bacSiList = results[0] as List<BacSiProfileModel>;
        baiVietList = results[1] as List<BaiVietModel>;

        if (taiKham?.trangThai == 'Chờ khám') {
          hasTaiKham = taiKham != null;
          taiKhamId = taiKham?.taiKhamID;
        }
        if (dieuTri?.trangThai == 'Đang điều trị') {
          hasDieuTri = dieuTri != null;
          lieuTrinhId = dieuTri?.lieuTrinhID;
        }

        isLoadingBacSi = false;
        isLoadingBaiViet = false;
      });
    } catch (e) {
      DialogHelper.showThongBao(context, e.toString());
    }
  }

  bool duocDangKyTaiKham(DateTime ngayDuKien) {
    final today = DateTime.now();

    final todayOnly = DateTime(today.year, today.month, today.day);
    final ngayDuKienOnly = DateTime(
      ngayDuKien.year,
      ngayDuKien.month,
      ngayDuKien.day,
    );

    return todayOnly.isAfter(ngayDuKienOnly) ||
        todayOnly.isAtSameMomentAs(ngayDuKienOnly);
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
          flexibleSpace: Container(
            decoration: BoxDecoration(
              gradient: LinearGradient(
                colors: [Color(0xFF6EC6A8), Color(0xFF4F9FEF)],
                begin: Alignment.topLeft,
                end: Alignment.bottomRight,
              ),
            ),
          ),
          centerTitle: true,
          title: Image.asset('assets/images/logo.png', height: 35),
        ),
        body: SafeArea(
          child: _selectedIndex == 0
              ? homeScreen()
              : _selectedIndex == 1
              ? ProfileScreen()
              : DanhSachCaKhamPage(),
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
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 10),
        child: Column(
          children: [
            //Gioi thieu
            SizedBox(height: 10),
            Container(
              padding: const EdgeInsets.all(10),
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
              padding: const EdgeInsets.all(10),
              height: 165,
              decoration: BoxDecoration(
                color: Colors.white,
                borderRadius: BorderRadius.all(Radius.circular(20)),
                border: BoxBorder.all(color: Colors.brown, width: 3),
              ),
              child: Column(
                children: [
                  Row(
                    children: [
                      SizedBox(width: 10),
                      Icon(
                        Icons.date_range,
                        size: 18,
                        color: Colors.blueAccent,
                      ),
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
                        onPressed: hasDieuTri
                            ? () {
                                Navigator.push(
                                  context,
                                  MaterialPageRoute(
                                    builder: (_) => LichDieuTriScreen(
                                      lieuTrinhID: lieuTrinhId!,
                                    ),
                                  ),
                                );
                              }
                            : null,
                        child: const Text('Đặt lịch điều trị'),
                      ),
                    ],
                  ),
                  SizedBox(height: 10),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceAround,
                    children: [
                      ElevatedButton(
                        onPressed: hasTaiKham
                            ? () {
                                Navigator.push(
                                  context,
                                  MaterialPageRoute(
                                    builder: (_) =>
                                        LichKhamScreen(taiKhamId: taiKhamId!),
                                  ),
                                );
                              }
                            : null,
                        child: const Text('Đặt lịch tái khám'),
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

            SizedBox(height: 10),
            DoctorSection(bacSiList: bacSiList, isLoading: isLoadingBacSi),

            SizedBox(height: 10),

            ArticleSection(
              baiVietList: baiVietList,
              isLoading: isLoadingBaiViet,
            ),
          ],
        ),
      ),
    );
  }
}
