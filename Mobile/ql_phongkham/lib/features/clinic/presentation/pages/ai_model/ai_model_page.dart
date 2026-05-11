import 'dart:io';
import 'package:flutter/material.dart';
import 'package:image_picker/image_picker.dart';
import 'package:ql_phongkham/features/clinic/data/models/doctor_profile_model.dart';
import 'package:ql_phongkham/features/clinic/data/repository/ai_model_repository.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/auth/auth_button.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/home/choose_doctor.dart';

class AiModelChat extends StatefulWidget {
  final List<BacSiProfileModel> bacSiList;
  final bool isLoading;
  const AiModelChat({
    super.key,
    required this.bacSiList,
    required this.isLoading,
  });
  @override
  State<AiModelChat> createState() => _AiModelChatState();
}

class _AiModelChatState extends State<AiModelChat> {
  final ImagePicker _picker = ImagePicker();
  final ScrollController _scrollController = ScrollController();
  bool _isLoading = false;

  final List<Map<String, dynamic>> _messages = [];

  @override
  void initState() {
    super.initState();
    _messages.add({
      'isAi': true,
      'type': 'text',
      'text':
          'Xin chào! Hãy gửi ảnh vùng da cần kiểm tra, tôi sẽ chẩn đoán bệnh cho bạn! 🩺',
    });
  }

  void _scrollToBottom() {
    Future.delayed(const Duration(milliseconds: 150), () {
      if (_scrollController.hasClients) {
        _scrollController.animateTo(
          _scrollController.position.maxScrollExtent,
          duration: const Duration(milliseconds: 300),
          curve: Curves.easeOut,
        );
      }
    });
  }

  Future<void> _pickImage() async {
    final xFile = await _picker.pickImage(
      source: ImageSource.gallery,
      imageQuality: 85,
    );
    final AiModelRepository _aiRepo = AiModelRepository();
    if (xFile == null) return;
    final file = File(xFile.path);

    setState(() {
      _messages.add({'isAi': false, 'type': 'image', 'file': file});
      _messages.add({
        'isAi': true,
        'type': 'text',
        'text': 'Đang phân tích ảnh của bạn...',
      });
      _isLoading = true;
    });
    _scrollToBottom();

    try {
      final data = await _aiRepo.predict(file);

      setState(() {
        _messages.removeLast(); // bỏ "đang phân tích"
        _messages.add({'isAi': true, 'type': 'result', 'data': data});
      });
    } catch (e) {
      print("Load profile error: $e");
    } finally {
      setState(() => _isLoading = false);
      _scrollToBottom();
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('AI Chẩn Đoán Da'), centerTitle: true),
      body: Column(
        children: [
          Expanded(
            child: ListView.builder(
              controller: _scrollController,
              padding: const EdgeInsets.all(12),
              itemCount: _messages.length,
              itemBuilder: (context, index) => _buildBubble(_messages[index]),
            ),
          ),
          _buildBottomBar(),
        ],
      ),
    );
  }

  Widget _buildBubble(Map<String, dynamic> msg) {
    final isAi = msg['isAi'] as bool;

    Widget content;

    if (msg['type'] == 'image') {
      content = ClipRRect(
        borderRadius: BorderRadius.circular(12),
        child: Image.file(
          msg['file'] as File,
          width: 180,
          height: 180,
          fit: BoxFit.cover,
        ),
      );
    } else if (msg['type'] == 'result') {
      content = _buildResultCard(msg['data'] as Map<String, dynamic>);
    } else {
      content = Container(
        padding: const EdgeInsets.symmetric(horizontal: 14, vertical: 10),
        decoration: BoxDecoration(
          color: isAi ? Colors.blue.shade50 : Colors.blue,
          borderRadius: BorderRadius.circular(16),
        ),
        child: Text(
          msg['text'] as String,
          style: TextStyle(
            color: isAi ? Colors.black87 : Colors.white,
            fontSize: 14,
            height: 1.5,
          ),
        ),
      );
    }

    return Padding(
      padding: const EdgeInsets.only(bottom: 12),
      child: Row(
        mainAxisAlignment: isAi
            ? MainAxisAlignment.start
            : MainAxisAlignment.end,
        crossAxisAlignment: CrossAxisAlignment.end,
        children: [
          if (isAi) ...[
            CircleAvatar(
              radius: 16,
              backgroundColor: Colors.blue,
              child: const Icon(
                Icons.medical_services,
                color: Colors.white,
                size: 16,
              ),
            ),
            const SizedBox(width: 8),
          ],
          Flexible(child: content),
          if (!isAi) ...[
            const SizedBox(width: 8),
            CircleAvatar(
              radius: 16,
              backgroundColor: Colors.grey.shade300,
              child: const Icon(Icons.person, color: Colors.grey, size: 16),
            ),
          ],
        ],
      ),
    );
  }

  Widget _buildResultCard(Map<String, dynamic> data) {
    final confidence = (data['confidence'] as num).toDouble();
    final friendlyName = data['friendly_name'] ?? '';
    final aiMessage = data['ai_message'] ?? '';
    final Messagee = data['message'] ?? '';
    final predicted_class = data['predicted_class'] ?? '';
    return Container(
      padding: const EdgeInsets.all(12),
      decoration: BoxDecoration(
        color: Colors.blue.shade50,
        borderRadius: BorderRadius.circular(16),
        border: Border.all(color: Colors.blue.shade200),
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Row(
            children: [
              const Icon(Icons.health_and_safety, color: Colors.blue, size: 18),
              const SizedBox(width: 6),
              Expanded(
                child: Text(
                  friendlyName,
                  style: const TextStyle(
                    fontWeight: FontWeight.bold,
                    fontSize: 14,
                  ),
                ),
              ),
              Container(
                padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 3),
                decoration: BoxDecoration(
                  color: Colors.blue,
                  borderRadius: BorderRadius.circular(12),
                ),
                child: Text(
                  '${confidence.toStringAsFixed(1)}%',
                  style: const TextStyle(
                    color: Colors.white,
                    fontSize: 12,
                    fontWeight: FontWeight.bold,
                  ),
                ),
              ),
            ],
          ),
          const SizedBox(height: 8),
          ClipRRect(
            borderRadius: BorderRadius.circular(4),
            child: LinearProgressIndicator(
              value: confidence / 100,
              minHeight: 5,
              backgroundColor: Colors.blue.shade100,
              valueColor: const AlwaysStoppedAnimation(Colors.blue),
            ),
          ),
          const SizedBox(height: 10),
          Text(
            aiMessage.isNotEmpty ? aiMessage : Messagee,
            style: const TextStyle(fontSize: 13, height: 1.5),
          ),
          if (predicted_class != 'invalid' &&
              predicted_class != 'normal' &&
              aiMessage.isNotEmpty)
            _buildCheck(),
        ],
      ),
    );
  }

  Widget _buildBottomBar() {
    return SafeArea(
      child: Padding(
        padding: const EdgeInsets.all(12),
        child: ElevatedButton.icon(
          onPressed: _isLoading ? null : _pickImage,
          icon: Icon(
            _isLoading ? Icons.hourglass_top : Icons.add_photo_alternate,
          ),
          label: Text(
            _isLoading ? 'Đang phân tích...' : 'Gửi ảnh để chẩn đoán',
          ),
          style: ElevatedButton.styleFrom(
            minimumSize: const Size(double.infinity, 50),
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(12),
            ),
          ),
        ),
      ),
    );
  }

  Widget _buildCheck() {
    return Center(
      child: Container(
        margin: const EdgeInsets.all(20),
        padding: const EdgeInsets.all(20),
        decoration: BoxDecoration(
          color: Colors.white,
          borderRadius: BorderRadius.circular(16),
          boxShadow: [BoxShadow(blurRadius: 10)],
        ),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            Icon(Icons.calendar_today, size: 30, color: Colors.blue),
            const SizedBox(height: 16),
            AuthButton(
              buttonText: 'Đặt lịch ngay',
              onPressed: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (_) => ChooseDoctorSection(
                      bacSiList: widget.bacSiList,
                      isLoading: false,
                    ),
                  ),
                );
              },
            ),
          ],
        ),
      ),
    );
  }
}
