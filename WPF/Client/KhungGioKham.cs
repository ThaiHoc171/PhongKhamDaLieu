using System;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Client;

public class KhungGioKhamClient : AppClientBase
{
	private readonly string BASE = "api/khunggiokham";

	public Task<ApiResult<bool>> Create(KhungGioKhamRequest req)
		=> PostAsync<bool>(BASE, req);

	public Task<ApiResult<bool>> Update(int id, KhungGioKhamRequest req)
		=> PutAsync<bool>($"{BASE}/{id}", req);

	public Task<ApiResult<KhungGioKhamReadModel>> GetById(int id)
		=> GetAsync<KhungGioKhamReadModel>($"{BASE}/{id}");

	public Task<ApiResult<List<KhungGioKhamReadModel>>> GetList()
		=> GetAsync<List<KhungGioKhamReadModel>>(BASE);

	public Task<ApiResult<List<NameHelper>>> GetCombobox()
		=> GetAsync<List<NameHelper>>($"{BASE}/combobox");
	public Task<ApiResult<int>> Count()
		=> GetAsync<int>($"{BASE}/count");

	public Task<ApiResult<List<int>>> GetByCaLamViec(int caLamViec)
		=> GetAsync<List<int>>($"{BASE}/calamviec/{caLamViec}");
}