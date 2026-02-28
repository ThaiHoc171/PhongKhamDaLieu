import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:table_calendar/table_calendar.dart';
import 'package:intl/intl.dart';
class LichKhamScreen extends StatefulWidget {

  const LichKhamScreen({super.key});

  @override
  State<LichKhamScreen> createState() => _LichKhamScreenState();
}

class _LichKhamScreenState extends State<LichKhamScreen> {
  // 12 khung giờ cố định
  final List<Map<String, dynamic>> danhSachKhungGio = [
    {"id": 1, "gio": "07:00"},
    {"id": 2, "gio": "07:30"},
    {"id": 3, "gio": "08:00"},
    {"id": 4, "gio": "08:30"},
    {"id": 5, "gio": "09:00"},
    {"id": 6, "gio": "09:30"},
    {"id": 7, "gio": "13:00"},
    {"id": 8, "gio": "13:30"},
    {"id": 9, "gio": "14:00"},
    {"id": 10, "gio": "14:30"},
    {"id": 11, "gio": "15:00"},
    {"id": 12, "gio": "15:30"},
  ];

  String URL = 'https://clinicjwt-api-bperhwd0dne7c9c0.southeastasia-01.azurewebsites.net/api';
  List<int> khungGioConTrong = [];
  int? selectedKhungGioId;
  bool loadingSlot = false;
  int? CaKhamId;
  String? errorMessage;
  bool Dangkykham = false;
  CalendarFormat _format = CalendarFormat.month;
  DateTime _focusDay = DateTime.now();
  DateTime _currentDay = DateTime.now();

  bool _dateSelected = false;
  bool _timeSelected = false;

  @override
  void initState() {
    super.initState();
    _dateSelected = true;
    loadKhungGioConTrong();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
          title: const Text(
            'Đặt lịch khám', style: TextStyle(fontSize: 15, fontWeight: FontWeight.bold,),),
      ) ,
      body: SafeArea(
        child: CustomScrollView(
          slivers: <Widget>[
            SliverToBoxAdapter(
              child: Column(
                children: <Widget>[
                  _tableCalendar(),
                  const Padding(
                      padding: EdgeInsets.symmetric(horizontal: 10,vertical: 25),
                      child: Center(
                        child: Text(
                            'Chọn khung giờ khám',
                              style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20,
                                ),
                              ),
                      )
                  ),
                ],
              )
            ),
            _buildKhungGioSliver(),
            SliverFillRemaining(
              hasScrollBody: false,
              child: Align(
                alignment: Alignment.bottomCenter,
                child: Padding(
                  padding: const EdgeInsets.all(16),
                  child: ElevatedButton(
                    onPressed: loadingSlot ? null : () async {
                      bool daDangKy = await checkDangKy();
                      if (daDangKy) return;
                      await dangkyKham();
                    },
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Colors.blueAccent,
                      foregroundColor: Colors.white,
                    ),
                    child: const Text(
                      'Đăng ký khám',
                      style: TextStyle(
                          fontSize: 15,
                          fontWeight: FontWeight.bold),
                    ),
                  ),
                ),
              ),
            ),
          ],
        ),
      )
    );
  }

  Widget _tableCalendar() {
    return TableCalendar(
        focusedDay: _focusDay,
        firstDay: DateTime.now(),
        lastDay: DateTime(2027, 12, 31),
        calendarFormat: _format,
        rowHeight: 36,

        selectedDayPredicate: (day) {
          return isSameDay(_currentDay, day);
        },

        calendarStyle: const CalendarStyle(
            todayDecoration: BoxDecoration(
                color: Colors.blueAccent, shape: BoxShape.circle
            )
        ),
        availableCalendarFormats: const{
          CalendarFormat.month: 'Month',
        },
        onFormatChanged: (format) {
          setState(() {
            _format = format;
          });
        },
      onDaySelected: ((selectedDay, focusDay) async {
        setState(() {
          _currentDay = selectedDay;
          _focusDay = focusDay;
          _dateSelected = true;
          loadingSlot = true;
          selectedKhungGioId = null;
        });

        await loadKhungGioConTrong();
      }),
    );
  }

  Future<void> loadKhungGioConTrong() async {
    try {
      final prefs = await SharedPreferences.getInstance();
      final token = prefs.getString('accessToken');

      final formattedDate = DateFormat('yyyy-MM-dd').format(_currentDay);

      final url =
          '$URL/CaKham/khunggio-trong?ngayKham=$formattedDate&loaiCaKham=Khám';


      final response = await http.get(
        Uri.parse(url),
        headers: {
          'Authorization': 'Bearer $token',
          'accept': '*/*',
        },
      );

      if (response.statusCode == 200) {
        final data = json.decode(response.body);

        setState(() {
          khungGioConTrong = List<int>.from(data);
          loadingSlot = false;
        });
      } else {
        setState(() {
          khungGioConTrong = []; // quan trọng
          loadingSlot = false;
        });
      }
    } catch (e) {
      setState(() {
        loadingSlot = false;
      });
    }
  }
  Future<void> loadCaKhamID() async{
    try{
      final prefs = await SharedPreferences.getInstance();
      final token = prefs.getString('accessToken');

      final formattedDate = DateFormat('yyyy-MM-dd').format(_currentDay);

      final url = '$URL/CaKham/trong?ngayKham=$formattedDate&khungGioId=$selectedKhungGioId&loaiCaKham=Khám';

      final response = await http.get(
        Uri.parse(url),
        headers: {
          'Authorization': 'Bearer $token',
          'accept': '*/*',
        },
      );

      if(response.statusCode == 200){
        final data = json.decode(response.body);

        setState(() {
          CaKhamId = data;
        });
      }
    }catch(e){
      setState(() {
        loadingSlot = false;
      });
    }
  }
  Future<bool> checkDangKy() async {
    try {
      final prefs = await SharedPreferences.getInstance();
      final token = prefs.getString('accessToken');
      final benhNhanID = prefs.getInt('benhNhanId');
      final formattedDate = DateFormat('yyyy-MM-dd').format(_currentDay);

      final url =
          '$URL/CaKham/Kiemtradadangky?ngay=$formattedDate&khungGioId=$selectedKhungGioId&loaiCaKham=Khám&benhNhanId=$benhNhanID';

      final response = await http.get(
        Uri.parse(url),
        headers: {
          'Authorization': 'Bearer $token',
          'accept': '*/*',
        },
      );

      if (response.statusCode == 200) {
        final data = json.decode(response.body);

        if (data == true) {
          showThongBao(context,
              "Bệnh nhân đã đăng ký ca khám vào khung giờ này!");
          return true; // ĐÃ đăng ký
        }
      }
    } catch (e) {}

    return false; // CHƯA đăng ký
  }
  Future<void> dangkyKham() async {
    if (CaKhamId == null) {
      showThongBao(context, "Vui lòng chọn khung giờ khám");
      return;
    }

    setState(() {
      loadingSlot = true;
    });

    try {
      final prefs = await SharedPreferences.getInstance();
      final token = prefs.getString('accessToken');
      final benhNhanID = prefs.getInt('benhNhanId');

      final url = '$URL/CaKham/$CaKhamId/dangky';

      final response = await http.put(
        Uri.parse(url),
        headers: {
          'Authorization': 'Bearer $token',
          'Content-Type': 'application/json',
          'accept': '*/*',
        },
        body: jsonEncode({
          'benhNhanID': benhNhanID,
          'lyDoKham': 'Khám da liễu',
          'ngayDat': DateTime.now().toIso8601String(),
          'ghiChu': ''
        }),
      );

      setState(() {
        loadingSlot = false;
      });

      if (response.statusCode == 200) {
        final data = jsonDecode(response.body);
        showThongBao(context, data['message']);
      } else {
        showThongBao(context,
            "Đăng ký thất bại (${response.statusCode})");
      }
    } catch (e) {
      setState(() {
        loadingSlot = false;
      });

      showThongBao(context, "Có lỗi xảy ra");
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
              Navigator.of(context).pop(); // đóng dialog
            },
            child: const Text("OK"),
          ),
        ],
      ),
    );
  }
  Widget _buildKhungGioSliver() {
    if (!_dateSelected) {
      return const SliverToBoxAdapter(
        child: Padding(
          padding: EdgeInsets.all(16),
          child: Text("Vui lòng chọn ngày khám"),
        ),
      );
    }

    if (loadingSlot) {
      return const SliverToBoxAdapter(
        child: Center(
          child: Padding(
            padding: EdgeInsets.all(20),
            child: CircularProgressIndicator(),
          ),
        ),
      );
    }

    return SliverPadding(
          padding: const EdgeInsets.symmetric(horizontal: 12),
          sliver: SliverGrid(
            delegate: SliverChildBuilderDelegate(
                  (context, index) {
                final slot = danhSachKhungGio[index];
                final int khungGioId = slot["id"];
                final String gio = slot["gio"];

                final bool isAvailable = khungGioConTrong.contains(khungGioId);
                final bool isSelected = selectedKhungGioId == khungGioId;

                return GestureDetector(
                  onTap: isAvailable
                      ? () async{
                    setState(() {
                      selectedKhungGioId = khungGioId;
                      _timeSelected = true;
                    });
                    await loadCaKhamID();
                    print(CaKhamId);
                  }
                      : null,
                  child: Container(
                    decoration: BoxDecoration(
                      color: !isAvailable
                          ? Colors.grey.shade300 // full
                          : isSelected
                          ? Colors.blue
                          : Colors.white,
                      borderRadius: BorderRadius.circular(12),
                      border: Border.all(
                        color: isSelected ? Colors.blue : Colors.grey,
                        width: 1.5,
                      ),
                    ),
                    alignment: Alignment.center,
                    child: Text(
                      gio,
                      style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.bold,
                        color: !isAvailable ? Colors.grey : Colors.black,
                      ),
                    ),
                  ),
                );
              },
              childCount: danhSachKhungGio.length,
            ),
            gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
              crossAxisCount: 3,
              crossAxisSpacing: 10,
              mainAxisSpacing: 10,
              childAspectRatio: 2.5,
            ),
          ),
    );
  }
}