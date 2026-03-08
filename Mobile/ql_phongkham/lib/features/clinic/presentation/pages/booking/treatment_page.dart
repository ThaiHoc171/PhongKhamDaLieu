import 'package:flutter/material.dart';
import 'package:ql_phongkham/core/utils/dialog_helper.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:table_calendar/table_calendar.dart';
import 'package:ql_phongkham/features/clinic/data/repository/examination_repository.dart';

class LichDieuTriScreen extends StatefulWidget {
  final int? lieuTrinhID;
  const LichDieuTriScreen({super.key, this.lieuTrinhID});

  @override
  State<LichDieuTriScreen> createState() => _LichDieuTriScreenState();
}

class _LichDieuTriScreenState extends State<LichDieuTriScreen> {
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
  final LichKhamRepository _repository = LichKhamRepository();

  List<int> khungGioConTrong = [];
  int? selectedKhungGioId;
  bool loadingSlot = false;
  int? caKhamId;
  String? errorMessage;
  CalendarFormat _format = CalendarFormat.month;
  DateTime _focusDay = DateTime.now();
  DateTime _currentDay = DateTime.now();

  bool _dateSelected = false;

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
          'Đặt lịch khám',
          style: TextStyle(fontSize: 15, fontWeight: FontWeight.bold),
        ),
      ),
      body: SafeArea(
        child: CustomScrollView(
          slivers: <Widget>[
            SliverToBoxAdapter(
              child: Column(
                children: <Widget>[
                  _tableCalendar(),
                  const Padding(
                    padding: EdgeInsets.symmetric(horizontal: 10, vertical: 25),
                    child: Center(
                      child: Text(
                        'Chọn khung giờ khám',
                        style: TextStyle(
                          fontWeight: FontWeight.bold,
                          fontSize: 20,
                        ),
                      ),
                    ),
                  ),
                ],
              ),
            ),
            _buildKhungGioSliver(),
            SliverFillRemaining(
              hasScrollBody: false,
              child: Align(
                alignment: Alignment.bottomCenter,
                child: Padding(
                  padding: const EdgeInsets.all(16),
                  child: ElevatedButton(
                    onPressed: loadingSlot
                        ? null
                        : () async {
                            if (selectedKhungGioId == null) {
                              DialogHelper.showSnacFailed(
                                context,
                                "Vui lòng chọn khung giờ khám",
                              );
                              return;
                            }
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
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }

  Widget _tableCalendar() {
    return TableCalendar(
      locale: 'vi_VN',
      focusedDay: _focusDay,
      firstDay: DateTime.now(),
      lastDay: DateTime(2027, 12, 31),
      calendarFormat: _format,
      rowHeight: 36,

      selectedDayPredicate: (day) {
        return isSameDay(_currentDay, day);
      },

      availableCalendarFormats: const {CalendarFormat.month: 'Tháng'},
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
      setState(() {
        loadingSlot = true;
      });
      final prefs = await SharedPreferences.getInstance();
      final token = prefs.getString('accessToken');
      if (token == null) return;
      final data = await _repository.getKhungGioConTrongDieuTri(
        _currentDay,
        token,
      );

      setState(() {
        khungGioConTrong = data;
        loadingSlot = false;
      });
    } catch (e) {
      setState(() {
        loadingSlot = false;
      });
    }
  }

  Future<void> loadcaKhamId() async {
    try {
      final prefs = await SharedPreferences.getInstance();
      final token = prefs.getString('accessToken');
      if (token == null) return;
      final id = await _repository.getCaKhamIdDieuTri(
        _currentDay,
        selectedKhungGioId!,
        token,
      );

      setState(() {
        caKhamId = id;
      });
    } catch (e) {
      debugPrint(e.toString());
    }
  }

  Future<void> dangkyKham() async {
    if (selectedKhungGioId == null) {
      DialogHelper.showSnacFailed(context, "Vui lòng chọn khung giờ khám");
      return;
    }

    setState(() {
      loadingSlot = true;
    });

    try {
      final prefs = await SharedPreferences.getInstance();
      final token = prefs.getString('accessToken');
      final benhNhanId = prefs.getInt('benhNhanId');
      if (token == null || benhNhanId == null) return;

      final id = await _repository.getCaKhamIdDieuTri(
        _currentDay,
        selectedKhungGioId!,
        token,
      );

      caKhamId = id;

      final message = await _repository.dangKyKham(
        caKhamId!,
        benhNhanId,
        token,
      );

      await Future.delayed(const Duration(milliseconds: 300));

      if (widget.lieuTrinhID != null) {
        await _repository.addBuoiDieuTri(
          widget.lieuTrinhID!,
          caKhamId!,
          benhNhanId,
          token,
        );
      }
      DialogHelper.showSnackSuccess(context, message);
    } catch (e) {
      DialogHelper.showSnacFailed(context, e.toString());
    }

    setState(() {
      loadingSlot = false;
    });
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
        delegate: SliverChildBuilderDelegate((context, index) {
          final slot = danhSachKhungGio[index];
          final int khungGioId = slot["id"];
          final String gio = slot["gio"];

          final bool isAvailable =
              khungGioConTrong.contains(khungGioId) && !isPastTimeSlot(gio);
          final bool isSelected = selectedKhungGioId == khungGioId;

          return GestureDetector(
            onTap: isAvailable
                ? () {
                    setState(() {
                      selectedKhungGioId = khungGioId;
                    });
                  }
                : null,
            child: Container(
              decoration: BoxDecoration(
                color: !isAvailable
                    ? Colors.grey.shade300
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
        }, childCount: danhSachKhungGio.length),
        gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
          crossAxisCount: 3,
          crossAxisSpacing: 10,
          mainAxisSpacing: 10,
          childAspectRatio: 2.5,
        ),
      ),
    );
  }

  bool isPastTimeSlot(String gio) {
    final now = DateTime.now();

    if (!isSameDay(_currentDay, now)) return false;

    final parts = gio.split(":");
    final hour = int.parse(parts[0]);
    final minute = int.parse(parts[1]);

    final slotTime = DateTime(
      _currentDay.year,
      _currentDay.month,
      _currentDay.day,
      hour,
      minute,
    );
    return slotTime.isBefore(now);
  }
}
