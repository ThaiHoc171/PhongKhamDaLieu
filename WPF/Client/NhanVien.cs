using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPF.Models;
using WPF.Common;

namespace WPF.Client;

public class NhanVienClient : AppClientBase
{
	private const string BASE = "api/nhanvien";
	public Task<ApiResult<int>> Create(NhanVienRequestDTO req)
		=> PostAsync<int>(BASE, req);
	public Task<ApiResult<bool>> Update(int id, NhanVienRequestUpdateDTO req)
		=> PutAsync<bool>($@"{BASE}/{id}", req);
	public Task<ApiResult<bool>> Status(int id, string trangThai)
		=> PutAsync<bool>($@"{BASE}/status/{id}?trangThai={trangThai}", null);
	public Task<ApiResult<NhanVienReadModel>> Detail(int id)
		=> GetAsync<NhanVienReadModel>($@"{BASE}/{id}");
	public Task<ApiResult<PagedResult<NhanVienReadListModel>>> GetPaged(int page = 1, int size = 10)
	{
		var url = $@"{BASE}?page={page}&size={size}";
		return GetAsync<PagedResult<NhanVienReadListModel>>(url);
	}
	public Task<ApiResult<PagedResult<NhanVienReadListModel>>> Search(string keyword, int page = 1, int size = 10)
	{
		var url = $@"{BASE}/search?keyword={keyword}&page={page}&size={size}";
		return GetAsync<PagedResult<NhanVienReadListModel>>(url);
	}
	public Task<ApiResult<List<NameHelper>>> GetCombobox(int chucVuId)
		=> GetAsync<List<NameHelper>>($@"{BASE}/combobox?chucVuId={chucVuId}");
	public Task<ApiResult<List<NameHelper>>> GetComboboxDoctor()
		=> GetAsync<List<NameHelper>>($@"{BASE}/combobox/doctor");


	
}
