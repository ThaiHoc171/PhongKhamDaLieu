using Application.DTOs;
namespace Application.Interfaces;
public interface IQuyenRepository
{
	Task<List<QuyenReadModel>> GetAllAsync();
}