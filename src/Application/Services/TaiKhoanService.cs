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
    private readonly IRefreshTokenRepository _refreshRepo;
	private readonly IChucVuQuyenRepository _chucVuQuyenRepo;

	public TaiKhoanService(ITaiKhoanRepository repo, IConfiguration configuration, INhanVienRepository nhanVienRepo, 
		IBenhNhanRepository benhNhanRepo, IChucVuRepository chucVuRepo, IRefreshTokenRepository refreshRepo,
		IChucVuQuyenRepository chucVuQuyenRepo)
	{
		_repo = repo;
		_configuration = configuration;
		_nhanVienRepo = nhanVienRepo;
		_benhNhanRepo = benhNhanRepo;
		_chucVuRepo = chucVuRepo;
		_refreshRepo = refreshRepo;
		_chucVuQuyenRepo = chucVuQuyenRepo;
	}
    private string TaoAccessToken(TaiKhoan tk, int? thongTinId, int? nhanVienId,
		int? benhNhanId, string? chucVu, List<string>? quyen)
	{
		var claims = new List<Claim>
	{
		new Claim(ClaimTypes.NameIdentifier, tk.Id.ToString()),
		new Claim(ClaimTypes.Email, tk.Email),
		new Claim(ClaimTypes.Role, tk.VaiTro)
	};
        if (thongTinId.HasValue)
            claims.Add(new Claim("ThongTinID", thongTinId.Value.ToString()));

        if (nhanVienId.HasValue)
			claims.Add(new Claim("NhanVienID", nhanVienId.Value.ToString()));

		if (benhNhanId.HasValue)
			claims.Add(new Claim("BenhNhanID", benhNhanId.Value.ToString()));

		if (quyen != null)
		{
			foreach (var p in quyen)
				claims.Add(new Claim("Permission", p));
		}
		if (!string.IsNullOrEmpty(chucVu))
			claims.Add(new Claim("ChucVu", chucVu));

		var key = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
		);

		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            expires: DateTime.UtcNow.AddMinutes(30),
			claims: claims,
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
    private string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        return Convert.ToBase64String(randomBytes);
    }

    public async Task<LoginResponseDTO?> DangNhapAsync(LoginRequestDTO dto)
	{
		var tk = await _repo.GetByEmailAsync(dto.Email);
		if (tk == null) return null;

		if (!Helper.Password.VerifyPassword(dto.MatKhau, tk.MatKhau))
			return null;
		int? thongTinId = null;
		int? nhanVienId = null;
		int? benhNhanId = null;
		string? chucVu = null;
		string? hoTen = null;
		List<string> quyen = new();
		if (tk.VaiTro == "Admin")
		{
			hoTen = "Admin";
		}
		else if (tk.VaiTro == "Nhân viên")
		{
			int nvId = await _nhanVienRepo.GetIdAsync(tk.Id);
			var nv = await _nhanVienRepo.GetDetailAsync(nvId);
			if (nv != null && nv.ChucVu != null)
			{
				nhanVienId = nv.NhanVienID;
				thongTinId = nv.ThongTinID;
				chucVu = nv.ChucVu.Name;
				hoTen = nv.HoTen;
				quyen = await _chucVuQuyenRepo.GetNameByChucVuAsync(nv.ChucVu.Id);
			}
		}
		else if (tk.VaiTro == "Bệnh nhân")
		{
			var bn = await _benhNhanRepo.GetDetailAsync(tk.Id);
			if(bn != null)
			{
				benhNhanId = bn.BenhNhanID;
				thongTinId = bn.ThongTinID;
                hoTen = bn.HoTen;
            }
			quyen.Add("DatLichKham");
			quyen.Add("XemHoSo");
		}

		var accessToken = TaoAccessToken(tk, thongTinId, nhanVienId, benhNhanId, chucVu, quyen);
        var refreshToken = GenerateRefreshToken();
		var token = new RefreshToken(tk.Id, refreshToken, DateTime.UtcNow.AddDays(7));
        await _refreshRepo.SaveAsync(token);	

        return new LoginResponseDTO
		{
            Id = tk.Id,
            Email = tk.Email,
            VaiTro = tk.VaiTro,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
			ThongTinID = thongTinId,
            NhanVienId = nhanVienId,
            BenhNhanId = benhNhanId,
            ChucVu = chucVu,
			HoTen = hoTen
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
    public async Task<LoginResponseDTO?> RefreshTokenAsync(string refreshToken)	
    {	

        var storedToken = await _refreshRepo.GetAsync(refreshToken);
		List<string> quyen = new();
		if (storedToken == null ||
            storedToken.IsRevoked ||
            storedToken.ExpiryDate < DateTime.UtcNow)
            return null;

        var taiKhoan = await _repo.GetByIdAsync(storedToken.TaiKhoanId);
        if (taiKhoan == null) return null;
        int? thongTinId = null;
        int? nhanVienId = null;
        int? benhNhanId = null;
        string? chucVu = null;
        string? hoTen = null;

        if (taiKhoan.VaiTro == "Nhân viên")
        {
			int nvId = await _nhanVienRepo.GetIdAsync(taiKhoan.Id);
			var nv = await _nhanVienRepo.GetDetailAsync(nvId);
			if (nv != null && nv.ChucVu != null )
			{
				nhanVienId = nv.NhanVienID;
				thongTinId = nv.ThongTinID;
				chucVu = nv.ChucVu.Name;
				hoTen = nv.HoTen;
				quyen = await _chucVuQuyenRepo.GetNameByChucVuAsync(nv.ChucVu.Id);
			}
		}
        else if (taiKhoan.VaiTro == "Bệnh nhân")
        {
            var bn = await _benhNhanRepo.GetDetailAsync(taiKhoan.Id);
            if (bn != null)
            {
                benhNhanId = bn.BenhNhanID;
                thongTinId = bn.ThongTinID;
                hoTen = bn.HoTen;
            }
        }
        else if (taiKhoan.VaiTro == "Admin")
        {
            hoTen = "Admin";
        }

        await _refreshRepo.RevokeAsync(refreshToken);
        
        var newRefreshToken = GenerateRefreshToken();
        var token = new RefreshToken(taiKhoan.Id, newRefreshToken, DateTime.UtcNow.AddDays(7));
        await _refreshRepo.SaveAsync(token);
       
        var accessToken = TaoAccessToken(taiKhoan, thongTinId, nhanVienId, benhNhanId, chucVu,quyen);

        return new LoginResponseDTO
        {
            Id = taiKhoan.Id,
            Email = taiKhoan.Email,
            VaiTro = taiKhoan.VaiTro,
            AccessToken = accessToken,
            RefreshToken = newRefreshToken,
			ThongTinID = thongTinId,
            NhanVienId = nhanVienId,
            BenhNhanId = benhNhanId,
            ChucVu = chucVu,
            HoTen = hoTen
        };
    }
    public async Task LogoutAsync(string refreshToken)
    {
        await _refreshRepo.RevokeAsync(refreshToken);
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
