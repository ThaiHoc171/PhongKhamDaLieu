using Application.Interfaces;
using Application.Services;
using Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using Services;


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

//builder.Services.AddScoped<IThuocRepository, ThuocRepository>();
//builder.Services.AddScoped<ThuocService>();
//builder.Services.AddScoped<IToaThuocRepository, ToaThuocRepository>();
//builder.Services.AddScoped<ToaThuocService>();

//builder.Services.AddScoped<IChiTietToaThuocRepository, ChiTietToaThuocRepository>();
//builder.Services.AddScoped<ChiTietToaThuocService>();
//builder.Services.AddScoped<IPhongKhamRepository, PhongKhamRepository>();
//builder.Services.AddScoped<PhongKhamService>();
//builder.Services.AddScoped<IBacSiProfileRepository, BacSiProfileRepository>();
//builder.Services.AddScoped<BacSiProfileService>();

//builder.Services.AddScoped<IPhongChucNang_ThietBiRepository, PhongChucNang_ThietBiRepository>();
//builder.Services.AddScoped<PhongChucNang_ThietBiService>();
//builder.Services.AddScoped<ILichLamViecNhanVienRepository, LichLamViecNhanVienRepository>();
//builder.Services.AddScoped<LichLamViecNhanVienService>();
//builder.Services.AddScoped<INgayNghiNhanVienRepository, NgayNghiNhanVienRepository>();
//builder.Services.AddScoped<NgayNghiNhanVienService>();
//builder.Services.AddScoped<IKhungGioKhamRepository, KhungGioKhamRepository>();
//builder.Services.AddScoped<KhungGioKhamService>();
//builder.Services.AddScoped<ICaKhamRepository, CaKhamRepository>();
//builder.Services.AddScoped<CaKhamService>();
//builder.Services.AddScoped<IPhienKhamRepositories, PhienKhamRepository>();
//builder.Services.AddScoped<PhienKhamService>();
//builder.Services.AddScoped<ICanLamSangRepositories, CanLamSangRepository>();
//builder.Services.AddScoped<CanLamSangService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
