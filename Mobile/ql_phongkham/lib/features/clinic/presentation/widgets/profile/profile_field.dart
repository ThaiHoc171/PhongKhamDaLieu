import 'package:flutter/material.dart';

class ProfileField extends StatelessWidget {
  final String hintText;
  final String labelText;
  final TextEditingController controller;
  const ProfileField({
    super.key,
    required this.hintText,
    required this.controller,
    required this.labelText,
  });

  @override
  Widget build(BuildContext context) {
    return TextFormField(
      controller: controller,
      decoration: InputDecoration(
        hintText: hintText,
        labelText: labelText,
        floatingLabelStyle: TextStyle(
          fontWeight: FontWeight.bold,
          fontSize: 18,
        ),
        floatingLabelBehavior: FloatingLabelBehavior.always,
        contentPadding: const EdgeInsets.symmetric(
          vertical: 12,
          horizontal: 12,
        ),
      ),
      validator: (value) {
        if (value == null || value.isEmpty) {
          return "$hintText đang trống!";
        }
        return null;
      },
    );
  }
}
