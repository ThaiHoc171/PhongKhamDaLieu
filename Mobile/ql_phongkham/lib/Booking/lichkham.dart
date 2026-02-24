import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:table_calendar/table_calendar.dart';
class LichKhamScreen extends StatefulWidget {

  const LichKhamScreen({super.key});

  @override
  State<LichKhamScreen> createState() => _LichKhamScreenState();
}

class _LichKhamScreenState extends State<LichKhamScreen> {
  CalendarFormat _format = CalendarFormat.month;
  DateTime _focusDay = DateTime.now();
  DateTime _currentDay = DateTime.now();
  int? _currentIndex;

  bool _dateSelected = false;
  bool _timeSelected = false;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        centerTitle: true,
          title: const Text(
            'Đặt lịch khám', style: TextStyle(fontSize: 15, fontWeight: FontWeight.bold,),),
      ) ,
      body: CustomScrollView(
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
          )
        ],
      )
    );
  }
  Widget _tableCalendar() {
    return TableCalendar(
        focusedDay: _focusDay,
        firstDay: DateTime.now(),
        lastDay: DateTime(2027, 12, 31),
        calendarFormat: _format,
        rowHeight: 48,

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
        onDaySelected: ((selectedDay, focusDay) {
          setState(() {
            _currentDay = selectedDay;
            _focusDay = focusDay;
            _dateSelected = true;
          });
        })
    );
  }
}