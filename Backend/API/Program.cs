using Domain.Repository;
using Services;
using Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITaiKhoanRepository, TaiKhoanRepository>();
builder.Services.AddScoped<TaiKhoanService>();
builder.Services.AddScoped<IChucVuRepository, ChucVuRepository>();
builder.Services.AddScoped<ChucVuService>();    
builder.Services.AddScoped<INhanVienRepository, NhanVienRepository>();
builder.Services.AddScoped<LoaiBenhService>();
builder.Services.AddScoped<ILoaiBenhRepository, LoaiBenhRepository>();
builder.Services.AddScoped<NhanVienService>();
builder.Services.AddScoped<IBenhNhanRepository, BenhNhanRepository>();
builder.Services.AddScoped<BenhNhanService>();
builder.Services.AddScoped<IThuocRepository, ThuocRepository>();
builder.Services.AddScoped<ThuocService>();
builder.Services.AddScoped<IToaThuocRepository, ToaThuocRepository>();
builder.Services.AddScoped<ToaThuocService>();
builder.Services.AddScoped<IPhongChucNangRepository, PhongChucNangRepository>();
builder.Services.AddScoped<PhongChucNangService>();
builder.Services.AddScoped<IChiTietToaThuocRepository, ChiTietToaThuocRepository>();
builder.Services.AddScoped<ChiTietToaThuocService>();
builder.Services.AddScoped<IPhongKhamRepository, PhongKhamRepository>();
builder.Services.AddScoped<PhongKhamService>();
builder.Services.AddScoped<IBacSiProfileRepository, BacSiProfileRepository>();
builder.Services.AddScoped<BacSiProfileService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
