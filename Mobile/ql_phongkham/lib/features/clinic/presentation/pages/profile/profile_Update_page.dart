import 'dart:io';
import 'package:flutter/material.dart';
import 'package:image_picker/image_picker.dart';
import 'package:intl/intl.dart';
import 'package:ql_phongkham/core/constants/app_config.dart';
import 'package:ql_phongkham/core/utils/dialog_helper.dart';
import 'package:ql_phongkham/features/clinic/data/repository/profile_repository.dart';
import 'package:ql_phongkham/features/clinic/data/repository/upload_repository.dart';
import 'package:ql_phongkham/features/clinic/presentation/pages/auth/login_page.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/auth/auth_button.dart';
import 'package:ql_phongkham/features/clinic/presentation/widgets/profile/profile_field.dart';
import 'package:shared_preferences/shared_preferences.dart';

class ProfileUpdateScreen extends StatefulWidget {
  const ProfileUpdateScreen({super.key});

  @override
  State<ProfileUpdateScreen> createState() => _ProfileUpdateScreenState();
}

class _ProfileUpdateScreenState extends State<ProfileUpdateScreen> {
  XFile? _imageFile;
  final ImagePicker _picker = ImagePicker();
  String? gioiTinh;
  bool isLoading = false;
  String? linkAvatar;
  final formKey = GlobalKey<FormState>();

  final hoTenController = TextEditingController();
  final ngaySinhController = TextEditingController();
  final sdtController = TextEditingController();
  final emailController = TextEditingController();
  final diaChiController = TextEditingController();

  @override
  void dispose() {
    hoTenController.dispose();
    ngaySinhController.dispose();
    sdtController.dispose();
    emailController.dispose();
    diaChiController.dispose();
    super.dispose();
  }

  @override
  void initState() {
    super.initState();
    loadProfile();
  }

  Future<void> loadProfile() async {
    try {
      final prefs = await SharedPreferences.getInstance();
      final token = prefs.getString('accessToken');
      final thongTinId = prefs.getInt('thongTinId');
      if (token == null || thongTinId == null || thongTinId == 0) {
        return;
      }

      final data = await ProfileRepository().getProfile(thongTinId);

      setState(() {
        hoTenController.text = data.hoTen;
        ngaySinhController.text = DateFormat(
          'dd/MM/yyyy',
        ).format(data.ngaySinh);
        gioiTinh = data.gioiTinh;
        sdtController.text = data.sdt;
        emailController.text = data.emailLienHe;
        diaChiController.text = data.diaChi;
        final rawPath = Uri.parse(data.avatar!).path;
        linkAvatar = rawPath.startsWith('/') ? rawPath.substring(1) : rawPath;
      });
    } catch (e) {
      print("Load profile error: $e");
    }
  }

  Future<void> addProfile() async {
    try {
      setState(() {
        isLoading = true;
      });

      final prefs = await SharedPreferences.getInstance();
      final token = prefs.getString('accessToken');
      final taiKhoanId = prefs.getInt('userId');
      if (token == null || taiKhoanId == null) {
        DialogHelper.showSnacFailed(context, "Thiếu thông tin đăng nhập");
        return;
      }

      DateTime ngaySinh = DateFormat(
        'dd/MM/yyyy',
      ).parse(ngaySinhController.text);

      final thongTinId = await ProfileRepository().addProfile(
        taiKhoanId,
        hoTenController.text,
        ngaySinh,
        gioiTinh ?? "",
        sdtController.text,
        emailController.text,
        diaChiController.text,
        _toRelativePath(linkAvatar),
        "",
        token,
      );
      await prefs.setInt("thongTinId", thongTinId);

      DialogHelper.showSnackSuccess(context, "Tạo hồ sơ thành công");

      Navigator.pushAndRemoveUntil(
        context,
        MaterialPageRoute(builder: (_) => LoginPage()),
        (route) => false,
      );
    } catch (e) {
      DialogHelper.showSnacFailed(context, e.toString());
    } finally {
      setState(() {
        isLoading = false;
      });
    }
  }

  Future<void> updateProfile() async {
    try {
      setState(() {
        isLoading = true;
      });

      final prefs = await SharedPreferences.getInstance();
      final thongTinId = prefs.getInt('thongTinId');
      final vaitro = prefs.getString('vaiTro');
      if (thongTinId == null) {
        DialogHelper.showSnacFailed(context, "Thiếu thông tin đăng nhập");
        return;
      }
      DateTime ngaySinh = DateFormat(
        'dd/MM/yyyy',
      ).parse(ngaySinhController.text);
      await ProfileRepository().putProfile(
        thongTinId,
        hoTenController.text,
        ngaySinh,
        gioiTinh ?? "",
        sdtController.text,
        emailController.text,
        diaChiController.text,
        _toRelativePath(linkAvatar),
        vaitro!,
      );

      DialogHelper.showSnackSuccess(context, "Cập nhật thành công");

      await loadProfile();
    } catch (e) {
      DialogHelper.showSnacFailed(context, e.toString());
    } finally {
      setState(() {
        isLoading = false;
      });
    }
  }

  Future<void> pickDate() async {
    DateTime? picked = await showDatePicker(
      context: context,
      initialDate: DateTime(2000),
      firstDate: DateTime(1900),
      lastDate: DateTime.now(),
    );

    if (picked != null) {
      setState(() {
        ngaySinhController.text = DateFormat('dd/MM/yyyy').format(picked);
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text("Hồ sơ cá nhân")),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(16),
          child: Form(
            key: formKey,
            child: Column(
              children: [
                imageProfile(),

                const SizedBox(height: 30),
                ProfileField(
                  controller: hoTenController,
                  labelText: "Họ tên",
                  hintText: "Nhập họ tên",
                ),

                const SizedBox(height: 20),
                GestureDetector(
                  onTap: pickDate,
                  child: AbsorbPointer(
                    child: ProfileField(
                      controller: ngaySinhController,
                      labelText: "Ngày sinh",
                      hintText: "Chọn ngày sinh",
                    ),
                  ),
                ),

                const SizedBox(height: 20),
                DropdownButtonFormField<String>(
                  initialValue: gioiTinh,
                  decoration: const InputDecoration(
                    labelText: "Giới tính",
                    border: OutlineInputBorder(),
                  ),
                  items: const [
                    DropdownMenuItem(value: "Nam", child: Text("Nam")),
                    DropdownMenuItem(value: "Nữ", child: Text("Nữ")),
                    DropdownMenuItem(value: "Khác", child: Text("Khác")),
                  ],
                  onChanged: (value) {
                    setState(() {
                      gioiTinh = value;
                    });
                  },
                ),

                const SizedBox(height: 20),
                ProfileField(
                  controller: sdtController,
                  labelText: "Số điện thoại",
                  hintText: "Nhập số điện thoại",
                ),

                const SizedBox(height: 20),
                ProfileField(
                  controller: emailController,
                  labelText: "Email liên hệ",
                  hintText: "Nhập email",
                ),

                const SizedBox(height: 20),
                ProfileField(
                  controller: diaChiController,
                  labelText: "Địa chỉ",
                  hintText: "Nhập địa chỉ",
                ),

                const SizedBox(height: 30),
                isLoading
                    ? const CircularProgressIndicator()
                    : AuthButton(
                        buttonText: "Lưu",
                        onPressed: () async {
                          if (formKey.currentState!.validate()) {
                            final prefs = await SharedPreferences.getInstance();
                            final thongTinId = prefs.getInt('thongTinId');

                            if (thongTinId == null || thongTinId == 0) {
                              addProfile();
                            } else {
                              updateProfile();
                            }
                          }
                        },
                      ),
                const SizedBox(height: 20),
              ],
            ),
          ),
        ),
      ),
    );
  }

  Widget imageProfile() {
    return Center(
      child: Stack(
        alignment: Alignment.center,
        children: [
          CircleAvatar(radius: 60, backgroundImage: _buildAvatarImage()),
          if (isLoading)
            Container(
              width: 120,
              height: 120,
              decoration: BoxDecoration(
                color: Colors.black12,
                shape: BoxShape.circle,
              ),
              child: const Center(
                child: CircularProgressIndicator(color: Colors.white),
              ),
            ),

          Positioned(
            bottom: 0,
            right: 0,
            child: Container(
              decoration: const BoxDecoration(
                color: Colors.blue,
                shape: BoxShape.circle,
              ),
              child: IconButton(
                icon: const Icon(Icons.camera_alt, color: Colors.white),
                onPressed: () {
                  if (isLoading) return;

                  showModalBottomSheet(
                    context: context,
                    builder: ((builder) => bottomSheet()),
                  );
                },
              ),
            ),
          ),
        ],
      ),
    );
  }

  Widget bottomSheet() {
    return Container(
      height: 100,
      width: MediaQuery.of(context).size.width,
      margin: EdgeInsets.symmetric(horizontal: 20, vertical: 20),
      child: Column(
        children: <Widget>[
          Text('Chọn ảnh', style: TextStyle(fontSize: 20)),
          SizedBox(height: 20),
          Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              ElevatedButton.icon(
                onPressed: isLoading
                    ? null
                    : () {
                        takePhoto(ImageSource.camera);
                      },
                icon: const Icon(Icons.camera),
                label: const Text("Camera"),
              ),
              const SizedBox(width: 20),
              ElevatedButton.icon(
                onPressed: isLoading
                    ? null
                    : () {
                        takePhoto(ImageSource.gallery);
                      },
                icon: const Icon(Icons.image),
                label: const Text("Thư viện"),
              ),
            ],
          ),
        ],
      ),
    );
  }

  void takePhoto(ImageSource source) async {
    if (isLoading) return;
    setState(() {
      isLoading = true;
    });
    try {
      final pickedFile = await _picker.pickImage(source: source);
      if (pickedFile == null) {
        setState(() => isLoading = false);
        return;
      }
      final file = File(pickedFile.path);
      final imageUrl = await UploadRepository().uploadImage(file);
      setState(() {
        _imageFile = pickedFile;
        linkAvatar = imageUrl;
      });
      DialogHelper.showSnackSuccess(context, "Upload ảnh thành công");
      Navigator.pop(context);
    } catch (e) {
      DialogHelper.showSnacFailed(context, e.toString());
    } finally {
      setState(() {
        isLoading = false;
      });
    }
  }

  String _toRelativePath(String? url) {
    if (url == null || url.isEmpty) return "";
    // Nếu là full URL thì lấy path, ngược lại giữ nguyên
    final uri = Uri.tryParse(url);
    if (uri != null && uri.isAbsolute) {
      return uri.path; // /profile/abc.jpg
    }
    return url; // đã là relative rồi
  }

  ImageProvider _buildAvatarImage() {
    if (_imageFile != null) {
      return FileImage(File(_imageFile!.path));
    }
    if (linkAvatar != null && linkAvatar!.isNotEmpty) {
      return NetworkImage(AppConfig.baseImageUrl + linkAvatar!);
    }
    return const AssetImage("assets/images/user.png");
  }
}
