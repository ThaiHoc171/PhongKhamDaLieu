using Application.DTOs;
using Domain.Entities;
using Microsoft.Data.SqlClient;

namespace Application.Interfaces;

public interface ICanLamSangRepository
{
    //-- CUD
    Task<int> AddAsync(CanLamSang entity);
    Task UpdateAsync(CanLamSang entity);

    //-- R
    Task<CanLamSang?> GetByIdAsync(int id);
    Task<(List<CanLamSangListReadModel>, int)>GetPagedAsync(int page, int size, string? loaiXetNghiem, string? trangThai);
    Task<(List<CanLamSangListReadModel>, int)>SearchPagedAsync(string keyword, int page, int size);
    Task<List<CanLamSangListReadModel>>GetByLoaiXetNghiemAsync(string loaiXetNghiem);
    Task<CanLamSangReadModel?>GetDetailAsync(int id);
    Task<List<(int Id, string Ten)>> GetIdAndNameAsync();
  
}