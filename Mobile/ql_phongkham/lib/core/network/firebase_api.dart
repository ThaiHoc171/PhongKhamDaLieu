import 'dart:ui';

import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter_local_notifications/flutter_local_notifications.dart';
import 'package:ql_phongkham/core/services/navigator_service.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/splash_page.dart';

// Phải là top-level function
@pragma('vm:entry-point')
Future<void> handleBackgroundMessage(RemoteMessage message) async {
  print('Background: ${message.notification?.title}');
}

class FirebaseApi {
  final _firebaseMessaging = FirebaseMessaging.instance;

  final _localNotif = FlutterLocalNotificationsPlugin();

  static const _channelId = 'phongkham_channel';
  static const _channelName = 'Phòng khám thông báo';

  // Setup local notifications
  Future<void> _initLocalNotifications() async {
    const android = AndroidInitializationSettings('@mipmap/ic_launcher');
    await _localNotif.initialize(
      const InitializationSettings(android: android),
    );

    const channel = AndroidNotificationChannel(
      _channelId,
      _channelName,
      importance: Importance.high,
      playSound: true,
    );

    final androidPlugin = _localNotif
        .resolvePlatformSpecificImplementation<
          AndroidFlutterLocalNotificationsPlugin
        >();

    await androidPlugin?.createNotificationChannel(channel);
  }

  // Hiển thị thông báo khi app đang mở
  Future<void> _showNotification(RemoteMessage message) async {
    final notification = message.notification;
    if (notification == null) return;

    await _localNotif.show(
      notification.hashCode,
      notification.title,
      notification.body,
      const NotificationDetails(
        android: AndroidNotificationDetails(
          _channelId,
          _channelName,
          importance: Importance.high,
          priority: Priority.high,
          playSound: true,
          icon: 'ic_notification',
          largeIcon: DrawableResourceAndroidBitmap('ic_check_green'),
          color: Color(0xFF528FEB),
        ),
      ),
    );
  }

  Future<void> initNotifications() async {
    try {
      await _firebaseMessaging.requestPermission(
        alert: true,
        badge: true,
        sound: true,
      );
    } catch (_) {}

    try {
      final fcmToken = await _firebaseMessaging.getToken().timeout(
        const Duration(seconds: 10),
      );
      print('FCM Token: $fcmToken');
    } catch (e) {
      print('FCM Token lỗi: $e');
    }

    await _initLocalNotifications();

    FirebaseMessaging.onMessage.listen((message) {
      _showNotification(message);
    });

    FirebaseMessaging.onMessageOpenedApp.listen((message) {
      _handleNotificationTap(message);
    });

    try {
      final initialMessage = await _firebaseMessaging
          .getInitialMessage()
          .timeout(const Duration(seconds: 5));
      if (initialMessage != null) {
        await Future.delayed(const Duration(milliseconds: 500));
        _handleNotificationTap(initialMessage);
      }
    } catch (_) {}

    FirebaseMessaging.onBackgroundMessage(handleBackgroundMessage);
  }

  void _handleNotificationTap(RemoteMessage message) {
    final type = message.data['type'];

    switch (type) {
      case 'xac_nhan':
      case 'nhac_nho':
        NavigatorService.pushAndRemoveUntil(const SplashPage());
        break;
      default:
        NavigatorService.pushAndRemoveUntil(const SplashPage());
    }
  }
}
