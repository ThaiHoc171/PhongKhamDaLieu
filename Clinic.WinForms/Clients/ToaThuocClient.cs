using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Clinic.WinForms.Clients
{
	public class ToaThuocClient : ApiClientBase
	{
		public async Task<int?> TaoToaThuocAsync(ToaThuocRequestDTO dto)
		{
			var result = await PostAsync<ApiResponse<int>>("api/ToaThuoc", dto);
			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);
			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);
			return result.Data.Data;
		}
		public async Task<ToaThuocResponseDTO> GetByPhienKhamAsync(int phienKhamID)
		{
			var result = await GetAsync<ApiResponse<ToaThuocResponseDTO>>(
				$"api/ToaThuoc/phien-kham/{phienKhamID}");
			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);
			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);
			return result.Data.Data;
		}
		public async Task<List<ChiTietToaThuocResponseDTO>> GetChiTietAsync(int toaThuocID)
		{
			var result = await GetAsync<ApiResponse<List<ChiTietToaThuocResponseDTO>>>(
				$"api/ToaThuoc/chi-tiet/{toaThuocID}");
			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);
			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);
			return result.Data.Data;
		}
		public async Task<PagedResult<ToaThuocResponseDTO>> GetPagedAsync(int pageNumber, int pageSize)
		{
			var result = await GetAsync<ApiResponse<PagedResult<ToaThuocResponseDTO>>>(
				$"api/ToaThuoc/paged?pageNumber={pageNumber}&pageSize={pageSize}");
			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);
			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);
			return result.Data.Data;
		}
		public async Task<bool> KiemTraTonTaiAsync(int phienKhamID)
		{
			var result = await GetAsync<bool>($"api/ToaThuoc/exists/{phienKhamID}");
			return result.Data;
		}
	}
}