using Application.Interfaces;
using Application.Repository;
using Application.Services;
using Infrastructure.Repositories;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
	});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("BacSiOnly", p =>
	{
		p.RequireAssertion(context =>
			context.User.IsInRole("Admin") ||
			(
				context.User.IsInRole("Nhân viên") && 
				(context.User.HasClaim("ChucVu", "Bác sĩ khám bệnh") ||
				 context.User.HasClaim("ChucVu", "Bác sĩ điều trị"))
			)
		);
	});

	options.AddPolicy("LeTanOnly", p =>
	{
		p.RequireAssertion(context =>
			context.User.IsInRole("Admin") ||
			(
				context.User.IsInRole("Nhân viên") &&
				context.User.HasClaim("ChucVu", "Lễ tân")
			)
		);
	});

	options.AddPolicy("KyThuatVienOnly", p =>
	{
		p.RequireAssertion(context =>
			context.User.IsInRole("Admin") ||
			(
				context.User.IsInRole("Nhân viên") &&
				context.User.HasClaim("ChucVu", "Kỹ thuật viên")
			)
		);
	});
	options.AddPolicy("BacSiOrKyThuatVien", policy =>
	{
		policy.RequireAssertion(context =>
			context.User.IsInRole("Admin") ||
			(context.User.IsInRole("Nhân viên") &&
				(context.User.HasClaim("ChucVu", "Bác sĩ khám bệnh") ||
				 context.User.HasClaim("ChucVu", "Bác sĩ điều trị") ||
				 context.User.HasClaim("ChucVu", "Kỹ thuật viên")))
		);
	});
	options.AddPolicy("BacSiOrLeTan", policy =>
	{
		policy.RequireAssertion(context =>
			context.User.IsInRole("Admin") ||
			(context.User.IsInRole("Nhân viên") &&
				(context.User.HasClaim("ChucVu", "Bác sĩ khám bệnh") ||
				 context.User.HasClaim("ChucVu", "Bác sĩ điều trị") ||
				 context.User.HasClaim("ChucVu", "Lễ tân")))
		);
	});
});

builder.Services.AddScoped<ITaiKhoanRepository, TaiKhoanRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
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
builder.Services.AddScoped<ILieuTrinh_BuoiDieuTriRepository, LieuTrinh_BuoiDieuTriRepository>();
builder.Services.AddScoped<LieuTrinh_BuoiDieuTriService>();
builder.Services.AddScoped<IBaiVietRepository, BaiVietRepository>();
builder.Services.AddScoped<BaiVietService>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<RefreshTokenService>();

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
