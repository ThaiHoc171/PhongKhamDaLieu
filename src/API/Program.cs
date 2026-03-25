using Application.Common;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Repositories;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OfficeOpenXml;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
ExcelPackage.License.SetNonCommercialPersonal("ClinicApp");
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Clinic Management API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập: Bearer {token}"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddControllers()
.AddJsonOptions(opt =>
{
	opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services
.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidateAudience = false,
		ValidateLifetime = true,	
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
		),
		RoleClaimType = ClaimTypes.Role
	};
	options.Events = new JwtBearerEvents
	{
		OnChallenge = async context =>
		{
			context.HandleResponse();
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			context.Response.ContentType = "application/json";
			await context.Response.WriteAsJsonAsync(
				ApiResponse<string>.Fail("Bạn chưa đăng nhập")
			);
		},
		OnForbidden = async context =>
		{
			context.Response.StatusCode = StatusCodes.Status403Forbidden;
			context.Response.ContentType = "application/json";
			await context.Response.WriteAsJsonAsync(
				ApiResponse<string>.Fail("Bạn không có quyền truy cập")
			);
		}
	};
});
builder.Services.AddAuthorization(options =>
{
	var permissions = new[]
	{
		"BACSI_DETAIL","BACSI_LIST","BACSI_CREATE","BACSI_UPDATE",
		"BAIVIET_VIEW","BAIVIET_CREATE","BAIVIET_UPDATE",
		"BENHNHAN_DETAIL","BENHNHAN_LIST","BENHNHAN_CREATE","BENHNHAN_UPDATE",
		"LICHKHAM_VIEW","LICHKHAM_CREATE","LICHKHAM_UPDATE",
		"CSVC_VIEW","CSVC_CREATE","CSVC_UPDATE","CSVC_DELETE", //cơ sở vật chất
		"ROLE_VIEW","ROLE_CREATE","ROLE_UPDATE",
		"HOSO_LIST","HOSO_DETAIL","HOSO_CREATE","HOSO_UPDATE",
		"HETHONG_VIEW","HETHONG_CREATE","HETHONG_UPDATE","HETHONG_DELETE",
		"LICHLAMVIEC_VIEW","LICHLAMVIEC_CREATE","LICHLAMVIEC_UPDATE",
		"BENH_VIEW","BENH_CREATE","BENH_UPDATE",
		"NHANVIEN_VIEW","NHANVIEN_CREATE","NHANVIEN_UPDATE",
		"PHIENKHAM_VIEW","PHIENKHAM_CREATE","PHIENKHAM_UPDATE",
		"USER_VIEW","USER_CREATE","USER_UPDATE",
		"KHACH_VIEW", "KHACH_CREATE",
		"LIEUTRINH_VIEW","LIEUTRINH_CREATE","LIEUTRINH_UPDATE",
        "THUOC_VIEW","THUOC_CREATE","THUOC_UPDATE",
	};
	foreach (var permission in permissions)
	{
		var p = permission;
		options.AddPolicy(permission, policy =>
		{
			policy.RequireAssertion(context =>
				context.User.IsInRole("Admin") ||
				context.User.HasClaim("Permission", p)
			);
		});
	}
});
builder.Services.AddScoped<ITaiKhoanRepository, TaiKhoanRepository>();
builder.Services.AddScoped<TaiKhoanService>();
builder.Services.AddScoped<IChucVuRepository, ChucVuRepository>();
builder.Services.AddScoped<ChucVuService>();
builder.Services.AddScoped<IThongTinCaNhanRepository, ThongTinCaNhanRepository>();
builder.Services.AddScoped<ThongTinCaNhanService>();
builder.Services.AddScoped<INhanVienRepository, NhanVienRepository>();
builder.Services.AddScoped<NhanVienService>();
builder.Services.AddScoped<IBenhNhanRepository, BenhNhanRepository>();
builder.Services.AddScoped<BenhNhanService>();
builder.Services.AddScoped<IPhongChucNangRepository, PhongChucNangRepository>();
builder.Services.AddScoped<PhongChucNangService>();
builder.Services.AddScoped<IThietBiRepository, ThietBiRepository>();
builder.Services.AddScoped<ThietBiService>();
builder.Services.AddScoped<IPCNThietBiRepository, PCNThietBiRepository>();
builder.Services.AddScoped<PCNThietBiService>();
builder.Services.AddScoped<IChiTietPCNThietBiRepository, ChiTietPCNThietBiRepository>();
builder.Services.AddScoped<ChiTietPCNThietBiService>();
builder.Services.AddScoped<IKhungGioKhamRepository, KhungGioKhamRepository>();
builder.Services.AddScoped<KhungGioKhamService>();
builder.Services.AddScoped<ICanLamSangRepository, CanLamSangRepository>();
builder.Services.AddScoped<CanLamSangService>();
builder.Services.AddScoped<IThuocRepository, ThuocRepository>();
builder.Services.AddScoped<ThuocService>();
builder.Services.AddScoped<ILoaiBenhRepository, LoaiBenhRepository>();
builder.Services.AddScoped<LoaiBenhService>();
builder.Services.AddScoped<IBacSiProfileRepository, BacSiProfileRepository>();
builder.Services.AddScoped<BacSiProfileService>();
builder.Services.AddScoped<IToaThuocRepository, ToaThuocRepository>();
builder.Services.AddScoped<ToaThuocService>();
builder.Services.AddScoped<IChiTietToaThuocRepository, ChiTietToaThuocRepository>();
builder.Services.AddScoped<IPhienKhamRepository, PhienKhamRepository>();
builder.Services.AddScoped<PhienKhamService>();
builder.Services.AddScoped<IPhienKhamBenhRepository, PhienKhamBenhRepository>();
builder.Services.AddScoped<PhienKhamBenhService>();
builder.Services.AddScoped<ILichLamViecRepository, LichLamViecRepository>();
builder.Services.AddScoped<LichLamViecService>();
builder.Services.AddScoped<ICaKhamRepository, CaKhamRepository>();
builder.Services.AddScoped<CaKhamService>();
builder.Services.AddScoped<IPhienKhamCLSRepository, PhienKhamCLSRepository>();
builder.Services.AddScoped<PhienKhamCLSService>();
builder.Services.AddScoped<IPhienKhamThietBiRepository, PhienKhamThietBiRepository>();
builder.Services.AddScoped<PhienKhamThietBiService>();	
builder.Services.AddScoped<IHoSoBenhAnRepository, HoSoBenhAnRepository>();
builder.Services.AddScoped<HoSoBenhAnService>();
builder.Services.AddScoped<INgayNghiNhanVienRepository, NgayNghiNhanVienRepository>();
builder.Services.AddScoped<NgayNghiNhanVienService>();
builder.Services.AddScoped<ITaiKhamRepository, TaiKhamRepository>();
builder.Services.AddScoped<TaiKhamService>();
builder.Services.AddScoped<ILieuTrinhDieuTriRepository, LieuTrinhDieuTriRepository>();
builder.Services.AddScoped<LieuTrinhDieuTriService>();
builder.Services.AddScoped<IBuoiDieuTriRepository, BuoiDieuTriRepository>();
builder.Services.AddScoped<BuoiDieuTriService>();
builder.Services.AddScoped<IBaiVietRepository, BaiVietRepository>();
builder.Services.AddScoped<BaiVietService>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<RefreshTokenService>();
builder.Services.AddScoped<IQuyenRepository, QuyenRepository>();
builder.Services.AddScoped<IChucVuQuyenRepository, ChucVuQuyenRepository>();
builder.Services.AddScoped<ChucVuQuyenService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IPhongKhamRepository, PhongKhamRepository>();
builder.Services.AddScoped<PhongKhamService>();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clinic API v1");
    c.RoutePrefix = "swagger"; // truy cập /swagger
});
app.MapGet("/", () => Results.Redirect("/swagger"));
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
