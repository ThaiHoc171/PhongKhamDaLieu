using Amazon.S3;
using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Infrastructure.Repositories;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OfficeOpenXml;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
ExcelPackage.License.SetNonCommercialPersonal("ClinicApp");
var builder = WebApplication.CreateBuilder(args);
var firebaseJson = Environment.GetEnvironmentVariable("FIREBASE_CREDENTIAL");

if (!string.IsNullOrEmpty(firebaseJson))
{
	var credential = CredentialFactory
		.FromJson<ServiceAccountCredential>(firebaseJson)
		.ToGoogleCredential();

	FirebaseApp.Create(new AppOptions
	{
		Credential = credential
	});
}
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
				ApiResponse<object>.Fail("Bạn chưa đăng nhập")
			);
		},
		OnForbidden = async context =>
		{
			context.Response.StatusCode = StatusCodes.Status403Forbidden;
			context.Response.ContentType = "application/json";
			await context.Response.WriteAsJsonAsync(
				ApiResponse<object>.Fail("Bạn không có quyền thực hiện hành động này")
			);
		}
	};
});
builder.Services.AddAuthorization(options =>
{
	var permissions = new[]
	{
		"PUBLIC_WRITE","PUBLIC_READ",
		"BENHNHAN_WRITE","BENHNHAN_READ",
		"LICH_WRITE","LICH_READ",
		"CSVC_READ","CSVC_WRIITE",//cơ sở vật chất
		"NHANSU_WRITE","NHANSU_READ",
		"HETHONG_WRITE","HETHONG_READ",
		"KHAMBENH_READ","KHAMBENH_WRITE",
		"USER_WRITE","USER_READ",
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
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddScoped<FileStorage>();
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
builder.Services.AddSingleton<IFcmService, FcmService>();
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IOtpRepository, OtpRepository>();
builder.Services.AddScoped<OtpService>();
builder.Services.AddHostedService<ReminderBackgroundService>(); var app = builder.Build();

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
