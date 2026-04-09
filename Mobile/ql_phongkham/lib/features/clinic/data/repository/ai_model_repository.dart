import 'dart:io';
import 'package:ql_phongkham/core/network/ai_model.dart';

class AiModelRepository {
  Future<Map<String, dynamic>> predict(File imageFile) async {
    final response = await AiModel.postFile('ai-predict', imageFile);
    return response;
  }
}
