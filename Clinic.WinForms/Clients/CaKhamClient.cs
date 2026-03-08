using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Clinic.WinForms.Clients
{
	public class CaKhamClient : ApiClientBase
	{
		public async Task<int?> TaoMoiAsync(TaoCaKhamDTO dto)
		{
			var result = await PostAsync<ApiResponse<int>>("api/CaKham", dto);
			if (!result.IsSuccess)
				throw new Exception(result.ErrorMessage);
			if (!result.Data.Success)
				throw new Exception(result.Data.Message);
			return result.Data.Data;
		}
		public async Task<bool> DangKyAsync(int id, DangKyCaKhamDTO dto)
		{
			var result = await PutAsync<ApiResponse<object>>(
				$"api/CaKham/{id}/dangky", dto);
			if (!result.IsSuccess)
				throw new Exception(result.ErrorMessage);
			if (!result.Data.Success)
				throw new Exception(result.Data.Message);
			return true;
		}
		public async Task<bool> CapNhatTrangThaiAsync(int id, string trangThai)
		{
			var result = await PutAsync<ApiResponse<object>>(
				$"api/CaKham/{id}/trangthai?TrangThai={trangThai}", null);
			if (!result.IsSuccess)
				throw new Exception(result.ErrorMessage);
			if (!result.Data.Success)
				throw new Exception(result.Data.Message);
			return true;
		}
		public async Task<CaKhamReadModel> GetByIdAsync(int id)
		{
			var result = await GetAsync<ApiResponse<CaKhamReadModel>>(
				$"api/CaKham/{id}");
			if (!result.IsSuccess)
				throw new Exception(result.ErrorMessage);
			if (!result.Data.Success)
				throw new Exception(result.Data.Message);
			return result.Data.Data;
		}
		public async Task<PagedResult<CaKhamListReadModel>> GetPagedAsync(
			DateTime ngayKham, string trangThai, string loaiCaKham, int pageNumber, int pageSize)
		{
			string url =
				$"api/CaKham?ngayKham={ngayKham:yyyy-MM-dd}&trangThai={trangThai}&loaiCaKham={loaiCaKham}&pageNumber={pageNumber}&pageSize={pageSize}";
			var result = await GetAsync<ApiResponse<PagedResult<CaKhamListReadModel>>>(url);

			if (!result.IsSuccess)
				throw new Exception(result.ErrorMessage);
			if (!result.Data.Success)
				throw new Exception(result.Data.Message);
			return result.Data.Data;
		}
		public async Task<PagedResult<CaKhamListReadModel>> GetByBenhNhanAsync(	int thongTinId,	int pageNumber,	int pageSize)
		{
			var result = await GetAsync<ApiResponse<PagedResult<CaKhamListReadModel>>>(
				$"api/CaKham/benhnhan/{thongTinId}?pageNumber={pageNumber}&pageSize={pageSize}");
			if (!result.IsSuccess)
				throw new Exception(result.ErrorMessage);
			if (!result.Data.Success)
				throw new Exception(result.Data.Message);
			return result.Data.Data;
		}
		public async Task<bool> KiemTraDaDangKyAsync( DateTime ngay, int khungGioId, string loaiCaKham, int benhNhanId)
		{
			string url =
				$"api/CaKham/kiemtra-dadangky?ngay={ngay:yyyy-MM-dd}&khungGioId={khungGioId}&loaiCaKham={loaiCaKham}&benhNhanId={benhNhanId}";
			var result = await GetAsync<ApiResponse<bool>>(url);
			if (!result.IsSuccess)
				throw new Exception(result.ErrorMessage);
			if (!result.Data.Success)
				throw new Exception(result.Data.Message);
			return result.Data.Data;
		}
		public async Task<List<int>> GetKhungGioConTrongAsync( DateTime ngayKham,string loaiCaKham)
		{
			string url =$"api/CaKham/khunggio-trong?ngayKham={ngayKham:yyyy-MM-dd}&loaiCaKham={loaiCaKham}";
			var result = await GetAsync<ApiResponse<List<int>>>(url);
			if (!result.IsSuccess)
				throw new Exception(result.ErrorMessage);
			if (!result.Data.Success)
				throw new Exception(result.Data.Message);
			return result.Data.Data;
		}
		public async Task<int> GetCaTrongAsync(	DateTime ngayKham, int khungGioId, string loaiCaKham)
		{
			string url =$"api/CaKham/ca-trong?ngayKham={ngayKham:yyyy-MM-dd}&khungGioId={khungGioId}&loaiCaKham={loaiCaKham}";
			var result = await GetAsync<ApiResponse<int>>(url);
			if (!result.IsSuccess)
				throw new Exception(result.ErrorMessage);
			if (!result.Data.Success)
				throw new Exception(result.Data.Message);
			return result.Data.Data;
		}
		public async Task<List<NameHelper>> GetComboboxAsync(
			DateTime ngayKham,
			string trangThai)
		{
			string url =
				$"api/CaKham/combobox?" +
				$"ngayKham={ngayKham:yyyy-MM-dd}" +
				$"&trangThai={trangThai}";
			var result = await GetAsync<ApiResponse<List<NameHelper>>>(url);
			if (!result.IsSuccess)
				throw new Exception(result.ErrorMessage);
			if (!result.Data.Success)
				throw new Exception(result.Data.Message);
			return result.Data.Data;
		}
	}
}