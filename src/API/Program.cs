using Application.Interfaces;
using Application.Repository;
using Application.Services;
using Infrastructure.Repositories;
using Infrastructure.Repository;
using Microsoft.OpenApi.Models;
using Services;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


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
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
			)
		};
	});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("BacSiOnly", p =>
	{
		p.RequireRole("Nhân viên");
		p.RequireClaim("ChucVu", "Bác sĩ");
	});

	options.AddPolicy("LeTanOnly", p =>
	{
		p.RequireRole("Nhân viên");
		p.RequireClaim("ChucVu", "Lễ tân");
	});
});

builder.Services.AddAuthorization();

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
builder.Services.AddScoped<ILieuTrinh_BuoiDieuTriRepository, LieuTrinh_BuoiDieuTriRepository>();
builder.Services.AddScoped<LieuTrinh_BuoiDieuTriService>();
builder.Services.AddScoped<IBaiVietRepository, BaiVietRepository>();
builder.Services.AddScoped<BaiVietService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clinic API v1");
    });
}
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
