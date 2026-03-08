using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.WinForms.Clients
{
	public class PhienKham_ClsClient : ApiClientBase
	{
		public async Task<List<PhienKham_ClsListReadModel>> GetByPhienKhamAsync(int phienKhamId)
		{
			var result = await GetAsync<ApiResponse<List<PhienKham_ClsListReadModel>>>($"api/phienkham-cls/phienkham/{phienKhamId}");

			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);

			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);

			return result.Data.Data;
		}
		public async Task<PhienKham_ClsReadModel> GetDetailAsync(int phienKhamBenhID)
		{
			var result = await GetAsync<ApiResponse<PhienKham_ClsReadModel>>($"api/phienkham-cls/chitiet/{phienKhamBenhID}");

			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);

			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);

			return result.Data.Data;
		}
		public async Task<bool> CreateAsync(PhienKham_clsRequestDTO dto)
		{
			var result = await PostAsync<ApiResponse<object>>("api/phienkham-cls", dto);

			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);

			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);

			return true;
		}

		public async Task<bool> NhanThucHienAsync(int id, NhanThucHienCLSDTO dto)
		{
			var result = await PutAsync<ApiResponse<object>>($"api/phienkham-cls/{id}/nhan", dto);

			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);

			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);

			return true;
		}

		public async Task<bool> CapNhatKetQuaAsync(int id, CapNhatKetQuaCLSDTO dto)
		{
			var result = await PutAsync<ApiResponse<object>>($"api/phienkham-cls/{id}/ketqua", dto);

			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);

			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);

			return true;
		}

		public async Task<bool> HuyAsync(int id)
		{
			var result = await PutAsync<ApiResponse<object>>($"api/phienkham-cls/{id}/huy", null);

			if (!result.IsSuccess)
				throw new System.Exception(result.ErrorMessage);

			if (!result.Data.Success)
				throw new System.Exception(result.Data.Message);

			return true;
		}
	}
}