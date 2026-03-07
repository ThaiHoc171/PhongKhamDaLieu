import 'package:flutter/material.dart';
import 'package:ql_phongkham/core/theme/app_pallete.dart';

class DialogHelper {
  static void showThongBao(BuildContext context, String message) {
    showDialog(
      context: context,
      builder: (_) => AlertDialog(
        title: const Text("Thông báo"),
        content: Text(message),
        actions: [
          TextButton(
            onPressed: () {
              Navigator.pop(context);
            },
            child: const Text("OK"),
          ),
        ],
      ),
    );
  }

  static void showSnackSuccess(BuildContext context, String message) {
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        content: Text(
          message,
          style: TextStyle(
            color: Colors.black,
            fontWeight: FontWeight.bold,
            shadows: [
              Shadow(
                blurRadius: 6,
                color: const Color.fromARGB(255, 255, 255, 255),
                offset: Offset(2, 2),
              ),
            ],
          ),
        ),
        backgroundColor: AppPallete.correctColor,
      ),
    );
  }

  static void showSnacFailed(BuildContext context, String message) {
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        content: Text(
          message.replaceFirst('Exception: ', ''),
          style: TextStyle(
            fontWeight: FontWeight.bold,
            shadows: [
              Shadow(blurRadius: 6, color: Colors.black, offset: Offset(2, 2)),
            ],
          ),
        ),
        backgroundColor: AppPallete.errorColor,
      ),
    );
  }
}
