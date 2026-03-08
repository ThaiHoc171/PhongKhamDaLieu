using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.WinForms.Clients
{
	public class PhienKham_ThietBiClient : ApiClientBase
	{
		public async Task<List<PhienKham_ThietBiReadModel>> GetByPhienKhamAsync(int phienKhamId)
		{
			var result = await GetAsync<ApiResponse<List<PhienKham_ThietBiReadModel>>>($"api/PhienKhamThietBi/phienkham/{phienKhamId}");

			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);

			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);

			return result.Data.Data;
		}

		public async Task<PhienKham_ThietBiReadModel> GetByIdAsync(int id)
		{
			var result = await GetAsync<ApiResponse<PhienKham_ThietBiReadModel>>($"api/PhienKhamThietBi/{id}");

			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);

			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);

			return result.Data.Data;
		}

		public async Task<bool> CreateAsync(PhienKham_ThietBiRequestDTO dto)
		{
			var result = await PostAsync<ApiResponse<object>>("api/PhienKhamThietBi", dto);

			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);

			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);

			return true;
		}

		public async Task<bool> UpdateAsync(int id, string ghiChu)
		{
			var result = await PutAsync<ApiResponse<object>>($"api/PhienKhamThietBi/{id}", ghiChu);

			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);

			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);

			return true;
		}
	}
}