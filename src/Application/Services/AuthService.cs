using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace Application.Services;
public class AuthService
{
	private readonly ITaiKhoanRepository _repo;
	private readonly INhanVienRepository _nhanVienRepo;
	private readonly IBenhNhanRepository _benhNhanRepo;
	private readonly IRefreshTokenRepository _refreshRepo;
	private readonly IChucVuQuyenRepository _chucVuQuyenRepo;
	private readonly IConfiguration _configuration;
	private readonly IThongTinCaNhanRepository _thongTinCaNhanRepo;
	public AuthService(
		ITaiKhoanRepository repo,
		INhanVienRepository nhanVienRepo,
		IBenhNhanRepository benhNhanRepo,
		IRefreshTokenRepository refreshRepo,
		IChucVuQuyenRepository chucVuQuyenRepo,
		IConfiguration configuration,
		IThongTinCaNhanRepository thongTinCaNhanRepo)
	{
		_repo = repo;
		_nhanVienRepo = nhanVienRepo;
		_benhNhanRepo = benhNhanRepo;
		_refreshRepo = refreshRepo;
		_chucVuQuyenRepo = chucVuQuyenRepo;
		_configuration = configuration;
        _thongTinCaNhanRepo = thongTinCaNhanRepo;
    }
	public async Task<ApiResponse<LoginResponseDTO>> DangNhapAsync(LoginRequestDTO dto)
	{
		if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.MatKhau))
			return ApiResponse<LoginResponseDTO>.Fail("Email hoặc mật khẩu không hợp lệ");
		var tk = await _repo.GetByEmailAsync(dto.Email);
		if (tk == null)
			return ApiResponse<LoginResponseDTO>.Fail("Tài khoản không tồn tại");
		if (tk.TrangThai == "Bị khóa")
            return ApiResponse<LoginResponseDTO>.Fail("Tài khoản đã bị khóa");
        if (!Helper.Password.VerifyPassword(dto.MatKhau, tk.MatKhau))
			return ApiResponse<LoginResponseDTO>.Fail("Sai mật khẩu");
		var info = await BuildUserInfoAsync(tk);
		List<string> quyen = _chucVuQuyenRepo.GetNameByChucVuAsync(info.ChucVu != null ? int.Parse(info.ChucVu) : 0).Result;
		var accessToken = GenerateAccessToken(tk, info);
		var refreshToken = GenerateRefreshToken();
		await _refreshRepo.SaveAsync(
			new RefreshToken(tk.TaiKhoanID, refreshToken, DateTime.UtcNow.AddDays(7)));
		return ApiResponse<LoginResponseDTO>.SuccessResponse(
			new LoginResponseDTO
			{
				Id = tk.TaiKhoanID,
				Email = tk.Email,
				VaiTro = VaiTroExtensions.ToDbValue(tk.VaiTro),
				AccessToken = accessToken,
				RefreshToken = refreshToken,
				HoTen = new NameResponseDTO
				{
					Id = info.ThongTinID ?? 0,
					Name = info.HoTen ?? ""
				},
				NhanVienId = info.NhanVienID,
				BenhNhanId = info.BenhNhanID,
				ChucVu = info.ChucVu,
				Quyen = quyen
			});
	}
	public async Task<ApiResponse<LoginResponseDTO>> RefreshTokenAsync(string refreshToken)
	{
		var storedToken = await _refreshRepo.GetAsync(refreshToken);
		if (storedToken == null ||
			storedToken.IsRevoked ||
			storedToken.ExpiryDate < DateTime.UtcNow)
			return ApiResponse<LoginResponseDTO>.Fail("RefreshToken không hợp lệ");
		var taiKhoan = await _repo.GetByIdAsync(storedToken.TaiKhoanId);
		if (taiKhoan == null)
			return ApiResponse<LoginResponseDTO>.Fail("Tài khoản không tồn tại");
		var info = await BuildUserInfoAsync(taiKhoan);
		await _refreshRepo.RevokeAsync(refreshToken);
		var newRefreshToken = GenerateRefreshToken();
		await _refreshRepo.SaveAsync(
			new RefreshToken(taiKhoan.TaiKhoanID, newRefreshToken, DateTime.UtcNow.AddDays(7)));
		var accessToken = GenerateAccessToken(taiKhoan, info);
		List<string> quyen = _chucVuQuyenRepo.GetNameByChucVuAsync(info.ChucVu != null ? int.Parse(info.ChucVu) : 0).Result;
		return ApiResponse<LoginResponseDTO>.SuccessResponse(
			new LoginResponseDTO
			{
				Id = taiKhoan.TaiKhoanID,
				Email = taiKhoan.Email,
				VaiTro = VaiTroExtensions.ToDbValue(taiKhoan.VaiTro),
				AccessToken = accessToken,
				RefreshToken = newRefreshToken,
				HoTen = new NameResponseDTO
				{
					Id = info.ThongTinID ?? 0,
					Name = info.HoTen ?? ""
				},
				NhanVienId = info.NhanVienID,
				BenhNhanId = info.BenhNhanID,
				ChucVu = info.ChucVu,
				Quyen = quyen
			});
	}
	public async Task<ApiResponse<bool>> LogoutAsync(string refreshToken)
	{
		await _refreshRepo.RevokeAsync(refreshToken);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	private async Task<UserInfo> BuildUserInfoAsync(TaiKhoan tk)
	{
		var info = new UserInfo();
		if (tk.VaiTro == VaiTroEnum.Admin)
		{
			info.HoTen = "Admin";
			return info;
		}
		if (tk.VaiTro == VaiTroEnum.NhanVien)
		{
			int nvId = await _nhanVienRepo.GetIdAsync(tk.TaiKhoanID);
			var nv = await _nhanVienRepo.GetDetailAsync(nvId);
			if (nv != null)
			{
				info.NhanVienID = nv.NhanVienID;
				info.ThongTinID = nv.ThongTinID;
				info.HoTen = nv.HoTen;
				if (nv.ChucVu != null)
				{
					info.ChucVu = nv.ChucVu.Name;
					info.Quyen = await _chucVuQuyenRepo.GetNameByChucVuAsync(nv.ChucVu.Id);
				}
			}
		}
		if (tk.VaiTro == VaiTroEnum.BenhNhan)
		{
			var thongTinId = await _thongTinCaNhanRepo.GetIdByTaiKhoanId(tk.TaiKhoanID);
			var bn = await _benhNhanRepo.GetByThongTinIDAsync(thongTinId);
			if (bn != null)
			{
				info.BenhNhanID = bn.BenhNhanID;
				info.ThongTinID = bn.ThongTinID;
				info.HoTen = bn.HoTen;
				info.Quyen.AddRange(new[]
				{
					"USER_VIEW","USER_CREATE","USER_UPDATE", "NHANVIEN_VIEW",
					"LIEUTRINH_CREATE","LIEUTRINH_UPDATE",
					"THONGTINCANHAN_LIST","THONGTINCANHAN_CREATE","THONGTINCANHAN_UPDATE",
					"BENHNHAN_DETAIL","BENHNHAN_UPDATE",
					"BACSI_DETAIL","BACSI_LIST",
					"LICHLAMVIEC_VIEW",
					"LICHKHAM_VIEW","LICHKHAM_UPDATE",
					"HOSO_DETAIL","PHIENKHAM_VIEW","PHIENKHAM_UPDATE",
					"THUOC_LIST","LIEUTRINH_VIEW","THUOC_VIEW",
					"BAIVIET_VIEW"
				});
			}
		}
		if (tk.VaiTro == VaiTroEnum.Khach)
		{
			info.Quyen.AddRange(new[]
			{
					"USER_VIEW","USER_CREATE","USER_UPDATE", "NHANVIEN_VIEW",
					"THONGTINCANHAN_LIST","THONGTINCANHAN_CREATE","THONGTINCANHAN_UPDATE",
					"BENHNHAN_DETAIL","BENHNHAN_UPDATE",
					"BACSI_DETAIL","BACSI_LIST",
					"LICHLAMVIEC_VIEW",
					"LICHKHAM_VIEW","LICHKHAM_UPDATE",
					"HOSO_DETAIL","PHIENKHAM_VIEW","PHIENKHAM_UPDATE",
					"THUOC_LIST","LIEUTRINH_VIEW","THUOC_VIEW",
					"BAIVIET_VIEW"
			});
		}
		return info;
	}
	private string GenerateAccessToken(TaiKhoan tk, UserInfo info)
	{
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, tk.TaiKhoanID.ToString()),
			new Claim(ClaimTypes.Email, tk.Email),
			new Claim(ClaimTypes.Role, VaiTroExtensions.ToDbValue(tk.VaiTro))
		};
		if (info.ThongTinID.HasValue)
			claims.Add(new Claim("ThongTinID", info.ThongTinID.Value.ToString()));
		if (info.NhanVienID.HasValue)
			claims.Add(new Claim("NhanVienID", info.NhanVienID.Value.ToString()));
		if (info.BenhNhanID.HasValue)
			claims.Add(new Claim("BenhNhanID", info.BenhNhanID.Value.ToString()));
		if (!string.IsNullOrEmpty(info.ChucVu))
			claims.Add(new Claim("ChucVu", info.ChucVu));
		foreach (var p in info.Quyen)
			claims.Add(new Claim("Permission", p));
		var key = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		var token = new JwtSecurityToken(
			issuer: _configuration["Jwt:Issuer"],
			expires: DateTime.UtcNow.AddMinutes(30),
			claims: claims,
			signingCredentials: creds);
		return new JwtSecurityTokenHandler().WriteToken(token);
	}
	private string GenerateRefreshToken()
	{
		var bytes = new byte[64];
		using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
		rng.GetBytes(bytes);
		return Convert.ToBase64String(bytes);
	}
	private class UserInfo
	{
		public int? ThongTinID;
		public int? NhanVienID;
		public int? BenhNhanID;
		public string? HoTen;
		public string? ChucVu;
		public List<string> Quyen = new();
	}
}