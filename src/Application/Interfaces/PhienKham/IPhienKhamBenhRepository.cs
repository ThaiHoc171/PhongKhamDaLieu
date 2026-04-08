using Application.DTOs;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Data;
namespace Application.Interfaces;
public interface IPhienKhamBenhRepository
{
	// CUD
	Task<int> AddAsync (PhienKhamBenh phienKhamBenh);
	Task<int> UpdateAsync (PhienKhamBenh phienKhamBenh);
	Task<int> DeleteAsync(int id);
	
	// Query
	Task<int> CountAsync(int phienKhamID);
	Task<bool> PrimaryExistsAsync(int phienKhamID);
	Task<PhienKhamBenh?> GetByIdAsync(int id);
	Task<List<PhienKhamBenhReadModel>> GetByPhienKhamIdAsync (int phienKhamID);
}
