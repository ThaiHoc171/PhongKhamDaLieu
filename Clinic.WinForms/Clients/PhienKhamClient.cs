using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.WinForms.Clients
{
	public class PhienKhamClient : ApiClientBase
	{
		public async Task<int?> TaoMoiAsync(int id)
		{
			var result = await PostAsync<ApiResponse<int>>($"api/PhienKham?caKhamID={id}", new { });

			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);

			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);

			return result.Data.Data;
		}

		public async Task<bool> CapNhatAsync(int id, PhienKhamRequestDTO dto)
		{
			var result = await PutAsync<ApiResponse<object>>($"api/PhienKham/{id}", dto);

			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);

			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);

			return true;
		}

		public async Task<bool> KetThucAsync(int id, string chanDoanCuoi)
		{
			var result = await PutAsync<ApiResponse<object>>(
				$"api/PhienKham/{id}/ket-thuc", chanDoanCuoi);

			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);

			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);

			return true;
		}

		public async Task<PagedResult<PhienKhamListReadModel>> GetPagedAsync(int pageNumber, int pageSize, int? nhanVienId = null, string trangThai = null)
		{
			string url = $"api/PhienKham?pageNumber={pageNumber}&pageSize={pageSize}";
			
			if (nhanVienId.HasValue && nhanVienId != 0)
				url += $"&nhanVienID={nhanVienId}";

			if (!string.IsNullOrEmpty(trangThai))
				url += $"&trangThai={trangThai}";
			var result = await GetAsync<ApiResponse<PagedResult<PhienKhamListReadModel>>>(url);

			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);

			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);

			return result.Data.Data;
		}

		public async Task<PagedResult<PhienKhamListReadModel>> SearchAsync(string keyword, int pageNumber, int pageSize, int? nhanVienId)
		{
			string url = $"api/PhienKham/timkiem?keyword={keyword}&pageNumber={pageNumber}&pageSize={pageSize}";

			if (nhanVienId.HasValue || nhanVienId != 0)
				url += $"&nhanVienID={nhanVienId}";

			var result = await GetAsync<ApiResponse<PagedResult<PhienKhamListReadModel>>>(url);

			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);

			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);

			return result.Data.Data;
		}

		public async Task<PhienKhamReadModel> GetByIdAsync(int id)
		{
			var result = await GetAsync<ApiResponse<PhienKhamReadModel>>($"api/PhienKham/{id}");

			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);

			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);

			return result.Data.Data;
		}

		public async Task<PagedResult<PhienKhamListReadModel>> GetByBenhNhanAsync( int benhNhanId, int pageNumber, int pageSize)
		{
			var result = await GetAsync<ApiResponse<PagedResult<PhienKhamListReadModel>>>(
				$"api/PhienKham/benhnhan/{benhNhanId}?pageNumber={pageNumber}&pageSize={pageSize}");

			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);

			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);

			return result.Data.Data;
		}
	}
}