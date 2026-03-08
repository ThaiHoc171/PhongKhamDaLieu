using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.WinForms.Clients
{
	public class PhienKham_BenhClient : ApiClientBase
	{
		public async Task<List<PhienKham_BenhReadModel>> GetByPhienKhamAsync(int phienKhamId)
		{
			var result = await GetAsync<ApiResponse<List<PhienKham_BenhReadModel>>>(
				$"api/PhienKhamBenh/phien-kham/{phienKhamId}");

			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);

			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);

			return result.Data.Data;
		}

		public async Task<bool> CreateAsync(PhienKham_BenhRequestDTO dto)
		{
			var result = await PostAsync<ApiResponse<object>>(
				"api/PhienKhamBenh", dto);

			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);

			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);

			return true;
		}

		public async Task<bool> UpdateAsync(int id, PhienKham_BenhRequestDTO dto)
		{
			var result = await PutAsync<ApiResponse<object>>(
				$"api/PhienKhamBenh/{id}", dto);

			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);

			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);

			return true;
		}
	}
}