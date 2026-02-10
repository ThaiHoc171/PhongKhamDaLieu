using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services;

public class TaiKhoanService
{
	private readonly ITaiKhoanRepository _repo;
	private readonly INhanVienRepository _nhanVienRepo;
	private readonly IBenhNhanRepository _benhNhanRepo;
	private readonly IChucVuRepository _chucVuRepo;
	private readonly IConfiguration _configuration;
	public TaiKhoanService(ITaiKhoanRepository repo, IConfiguration configuration, INhanVienRepository nhanVienRepo, IBenhNhanRepository benhNhanRepo, IChucVuRepository chucVuRepo)
	{
		_repo = repo;
		_configuration = configuration;
		_nhanVienRepo = nhanVienRepo;
		_benhNhanRepo = benhNhanRepo;
		_chucVuRepo = chucVuRepo;
	}
	private string TaoJwt(
	TaiKhoan tk,
	int? nhanVienId,
	int? benhNhanId,
	string? chucVu)
	{
		var claims = new List<Claim>
	{
		new Claim(ClaimTypes.NameIdentifier, tk.Id.ToString()),
		new Claim(ClaimTypes.Email, tk.Email),
		new Claim(ClaimTypes.Role, tk.VaiTro)
	};

		if (nhanVienId.HasValue)
			claims.Add(new Claim("NhanVienID", nhanVienId.Value.ToString()));

		if (benhNhanId.HasValue)
			claims.Add(new Claim("BenhNhanID", benhNhanId.Value.ToString()));

		if (!string.IsNullOrEmpty(chucVu))
			claims.Add(new Claim("ChucVu", chucVu));

		var key = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
		);

		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			expires: DateTime.UtcNow.AddHours(2),
			claims: claims,
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}


	public async Task<LoginResponseDTO?> DangNhapAsync(LoginRequestDTO dto)
	{
		var tk = await _repo.GetByEmailAsync(dto.Email);
		if (tk == null) return null;

		if (!Helper.Password.VerifyPassword(dto.MatKhau, tk.MatKhau))
			return null;

		int? nhanVienId = null;
		int? benhNhanId = null;
		string? chucVu = null;

		if (tk.VaiTro == "Nhân viên")
		{
			var nv = await _nhanVienRepo.GetForAuthAsync(tk.Id);
			if (nv != null)
			{
				nhanVienId = nv.NhanVienID;
				chucVu = await _chucVuRepo.GetNameByIdAsync(nv.ChucVuID);
			}
		}
		else if (tk.VaiTro == "Bệnh nhân")
		{
			benhNhanId = await _benhNhanRepo.GetForAuthAsync(tk.Id);
		}

		var token = TaoJwt(tk, nhanVienId, benhNhanId, chucVu);

		return new LoginResponseDTO
		{
			Id = tk.Id,
			Email = tk.Email,
			VaiTro = tk.VaiTro,
			Token = token,
			NhanVienId = nhanVienId,
			BenhNhanId = benhNhanId,
			ChucVu = chucVu
		};
	}


	public async Task DangKyAsync(ThemTaiKhoanDTO dto)
	{
		var hash = Helper.Password.PassWordHash(dto.MatKhau);
		var vaiTro = VaiTroExtensions.ToEnum(dto.VaiTro);
		var tk = new TaiKhoan(dto.Email, hash, vaiTro);
		await _repo.AddAsync(tk);
	}

	public async Task<bool> DoiMatKhauAsync(int id, DoiMatKhauDTO dto)
	{
		var tk = await _repo.GetByIdAsync(id);
		if (tk == null) return false;

		if (!Helper.Password.VerifyPassword(dto.MatKhauCu, tk.MatKhau))
			return false;

		tk.DoiMatKhau(Helper.Password.PassWordHash(dto.MatKhauMoi));
		await _repo.UpdateAsync(tk);
		return true;
	}
	public async Task<bool> ResetMatKhauAsync(int taiKhoanId)
	{
		var tk = await _repo.GetByIdAsync(taiKhoanId);
		if (tk == null)
			return false;

		var defaultPassword = _configuration["DefaultPassword"];
		if (string.IsNullOrWhiteSpace(defaultPassword))
			throw new Exception("DefaultPassword chưa được cấu hình.");

		var hash = Helper.Password.PassWordHash(defaultPassword);
		tk.DoiMatKhau(hash);

		await _repo.UpdateAsync(tk);
		return true;
	}
	public async Task<List<TaiKhoanResponseDTO>> LayTatCaAsync()
	{
		var list = await _repo.GetAllAsync();
		return list.Select(MapToResponse).ToList();
	}
	public async Task<bool> CapNhatTrangThaiAsync(int taiKhoanId, string trangThaiMoi)
	{
		var tk = await _repo.GetByIdAsync(taiKhoanId);
		if (tk == null)
			return false;

		tk.CapNhatTrangThai(trangThaiMoi);
		await _repo.UpdateAsync(tk);

		return true;
	}
	public async Task<TaiKhoanResponseDTO?> LayTaiKhoanTheoIdAsync(int id)
	{
		var tk = await _repo.GetByIdAsync(id);
		if (tk == null)
			return null;

		return MapToResponse(tk);
	}
	private static TaiKhoanResponseDTO MapToResponse(TaiKhoan tk)
		=> new()
		{
			Id = tk.Id,
			Email = tk.Email,
			VaiTro = tk.VaiTro,
			TrangThai = tk.TrangThai
		};
}
