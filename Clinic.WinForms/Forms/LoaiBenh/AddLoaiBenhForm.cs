using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.LoaiBenh
{
	public partial class AddLoaiBenhForm : Form
	{
		public AddLoaiBenhForm()
		{
			InitializeComponent();
			LoadCombobox();
			FormDragHelper.EnableDrag(pnlHeader, this);
			_client = new LoaiBenhClient();
		}
		private readonly LoaiBenhClient _client;
		private void LoadCombobox()
		{
			var doPhoBien = new List<string>(LookupData.DoPhoBien);
			doPhoBien.Insert(0, "-- Chọn độ phổ biến --");
			cbbDoPhoBien.DataSource = doPhoBien;

			var mucDo = new List<string>(LookupData.MucDoNghiemTrong);
			mucDo.Insert(0, "-- Chọn mức độ --");
			cbbMucDo.DataSource = mucDo;

			var nhomBenh = new List<string>(LookupData.NhomBenh);
			nhomBenh.Insert(0, "-- Chọn nhóm bệnh --");
			cbbNhomBenh.DataSource = nhomBenh;
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Close();
		}
		private bool IsValid()
		{
			if (string.IsNullOrWhiteSpace(txtTen.Text)) return false;
			if (string.IsNullOrWhiteSpace(txtTenKhoaHoc.Text)) return false;
			if (string.IsNullOrWhiteSpace(txtMoTa.Text)) return false;
			if (cbbNhomBenh.SelectedIndex == 0) return false;
			if (cbbDoPhoBien.SelectedIndex == 0) return false;
			if (cbbMucDo.SelectedIndex == 0) return false;
			return true;
		}
		private async void btnLuu_Click(object sender, EventArgs e)
		{
			try
			{
				if (!IsValid())
				{
					MessageHelper.ShowMessage("Vui lòng nhập đầy đủ thông tin!");
					return;
				}
				var dto = new LoaiBenhRequestDTO
				{
					TenBenh = txtTen.Text.Trim(),
					TenKhoaHoc = txtTenKhoaHoc.Text.Trim(),
					NhomBenh = cbbNhomBenh.SelectedItem.ToString(),
					MoTa = txtMoTa.Text.Trim(),
					DoPhoBien = cbbDoPhoBien.SelectedItem.ToString(),
					MucDoNghiemTrong = cbbMucDo.SelectedItem.ToString()
				};

				btnLuu.Enabled = false;

				var result = await _client.CreateAsync(dto);

				btnLuu.Enabled = true;

				if (result)
				{
					MessageHelper.ShowMessage("Thêm loại bệnh thành công!");
					this.DialogResult = DialogResult.OK;
					this.Close();
				}
				else
				{
					MessageHelper.ShowMessage("Thêm thất bại");
				}
			}
			catch (Exception ex)
			{
				btnLuu.Enabled = true;
				MessageHelper.ShowMessage("Lỗi: " + ex.Message);
			}
		}
	}
}
