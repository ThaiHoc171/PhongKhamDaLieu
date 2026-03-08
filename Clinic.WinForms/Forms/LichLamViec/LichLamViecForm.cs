using Clinic.WinForms.Clients;
using Clinic.WinForms.Common;
using Clinic.WinForms.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Util;
using System.Windows.Forms;

namespace Clinic.WinForms.Forms.LichLamViec
{
	public partial class LichLamViecForm : Form
	{
		public LichLamViecForm()	
		{
			InitializeComponent();
		}
		private Dictionary<(int dayIndex, int ca), Panel> _calendarPanels;
		private List<LichLamViecResponseDTO> _allData = new List<LichLamViecResponseDTO>();
		private LichLamViecCaNhanResponseDTO _personalResponse;
		private LichLamViecClient _lichClient = new LichLamViecClient();

		private int _currentPage = 0;
		private int _currentNhanVienId;

		// Hàm thiết lập các panel cho lịch cá nhân
		private void SetupCalendarPanels()
		{
			_calendarPanels = new Dictionary<(int, int), Panel>
			{
				{(0,1), pnlMon1},
				{(0,2), pnlMon2},

				{(1,1), pnlTue1},
				{(1,2), pnlTue2},
					
				{(2,1), pnlWed1},
				{(2,2), pnlWed2},

				{(3,1), pnlThu1},
				{(3,2), pnlThu2},

				{(4,1), pnlFri1},
				{(4,2), pnlFri2},

				{(5,1), pnlSat1},
				{(5,2), pnlSat2},

				{(6,1), pnlSun1},
				{(6,2), pnlSun2}
			};
		}

		private Control CreateShiftCard(LichLamViecItemDTO item)
		{
			var card = new Panel
			{
				Height = 100,
				Dock = DockStyle.Top,
				Margin = new Padding(4),
				Padding = new Padding(6),
				BackColor = item.CaLamViec == 1
					   ? Color.FromArgb(200, 230, 255)
					   : Color.FromArgb(200, 255, 200)
			};
			string caText =	item.CaLamViec == 1 ? "Ca sáng" + Environment.NewLine + "(07:00 - 10:00)" : 
							item.CaLamViec == 2 ? "Ca chiều" + Environment.NewLine + "(13:00 - 16:00)" : "Ca không" + Environment.NewLine + "xác định";
			var lbl = new Label
			{
				Dock = DockStyle.Fill,
				TextAlign = ContentAlignment.MiddleLeft,
				Text = caText
			};

			card.Controls.Add(lbl);

			return card;
		}
		private void RenderPersonalCalendar()
		{
			if (_personalResponse?.LichLamViecs == null)
				return;

			foreach (var pnl in _calendarPanels.Values)
				pnl.Controls.Clear();

			var monday = _personalResponse.TuanBatDau.Date;

			foreach (var item in _personalResponse.LichLamViecs)
			{
				int dayIndex = (item.Ngay.Date - monday).Days;

				if (dayIndex < 0 || dayIndex > 6)
					continue;

				var key = (dayIndex, item.CaLamViec);

				if (_calendarPanels.TryGetValue(key, out var panel))
				{
					var card = CreateShiftCard(item);
					panel.Controls.Add(card);
				}
			}

			foreach (var kvp in _calendarPanels)
			{
				var panel = kvp.Value;

				if (panel.Controls.Count == 0)
				{
					var emptyLabel = new Label
					{
						Dock = DockStyle.Fill,
						TextAlign = ContentAlignment.MiddleCenter,
						Text = "— Trống —",
						ForeColor = Color.Gray,
						AutoSize = false
					};

					panel.Controls.Add(emptyLabel);
				}
			}
		}

		// Hàm xử lý cbbWeek 
		private void SetupWeekComboBox()
		{
			var weekList = new List<WeekItem>();

			for (int i = -4; i <= 4; i++)
			{
				weekList.Add(new WeekItem
				{
					Page = i,
					Display = GetWeekDisplay(i)
				});
			}

			cbbWeek.DataSource = weekList;
			cbbWeek.DisplayMember = "Display";
			cbbWeek.ValueMember = "Page";

			cbbWeek.SelectedValue = 0;
		}
		private string GetWeekDisplay(int page)
		{
			var today = DateTime.Today;

			int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7; // tính thứ 2
			var monday = today.AddDays(-diff).Date;

			var targetMonday = monday.AddDays(page * 7);
			var sunday = targetMonday.AddDays(6);

			return $"Tuần {targetMonday:dd/MM} - {sunday:dd/MM}";
		}
		private async Task LoadWeekAsync()
		{
			var mode = cbbViewMode.SelectedValue as string;

			if (string.IsNullOrEmpty(mode))
				return;

			if (mode == "PERSONAL")
			{
				_personalResponse = await _lichClient
					.GetByNhanVienIdAsync(_currentNhanVienId, _currentPage);

				RenderPersonalCalendar();
			}
			else
			{
				_allData = await _lichClient.GetByWeekAsync(_currentPage);
				BindCommonGrid();
			}
		}
		private async void cbbWeek_SelectedIndexChanged(object sender, EventArgs e)
		{
			var selectedWeek = cbbWeek.SelectedItem as WeekItem;

			if (selectedWeek == null)
				return;

			_currentPage = selectedWeek.Page;

			await LoadWeekAsync();
		}

		// Hàm load data cho cbbViewMode
		private void SetupModeComboBox()
		{
			var modes = new List<ModeItem>
			{
				new ModeItem
				{
					Text = "TKB chung",
					Value = "COMMON"
				},
				new ModeItem
				{
					Text = "TKB cá nhân",
					Value = "PERSONAL"
				}
			};
			cbbViewMode.DataSource = modes;
			cbbViewMode.DisplayMember = "Text";
			cbbViewMode.ValueMember = "Value";
			cbbViewMode.SelectedValue = "PERSONAL";
		}
		private async void cbbViewMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cbbViewMode.SelectedValue is string mode)
			{
				bool isPersonal = mode == "PERSONAL";

				pnlCommonView.Visible = !isPersonal;
				pnlPersonalView.Visible = isPersonal;

				await LoadWeekAsync();
			}
		}


		//Xem lịch làm việc chung
		//xử lý dgv 
		private void SetupDataGridView()
		{
			SetupDatagridview.ApplyGridStyle(dgvLichLamViec);


			dgvLichLamViec.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "Ngay",
				HeaderText = "Ngày",
				FillWeight = 15
			});

			dgvLichLamViec.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "CaLamViec",
				HeaderText = "Ca",
				FillWeight = 15
			});

			dgvLichLamViec.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "NhanVien",
				HeaderText = "Nhân viên",
				FillWeight = 20
			});

			dgvLichLamViec.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "TenChucVu",
				HeaderText = "Chức vụ",
				FillWeight = 20
			});

			dgvLichLamViec.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "Phong",
				HeaderText = "Phòng",
				FillWeight = 10
			});

			dgvLichLamViec.Columns.Add(new DataGridViewTextBoxColumn
			{
				Name = "GhiChu",
				HeaderText = "Ghi chú",
				FillWeight = 20
			});
		}
		private void BindCommonGrid()
		{
			dgvLichLamViec.Rows.Clear();

			if (_allData == null || !_allData.Any())
				return;
			var sortedData = _allData
				.OrderBy(x => x.Ngay.Date)
				.ThenBy(x => x.CaLamViec)
				.ToList();
			foreach (var item in sortedData)
			{
				string caText =
					item.CaLamViec == 1 ? "Ca sáng" :
					item.CaLamViec == 2 ? "Ca chiều" :
					"Không xác định";

				int rowIndex = dgvLichLamViec.Rows.Add(
					item.Ngay.ToString("dd/MM/yyyy"),
					caText,
					item.NhanVien?.Name,
					item.TenChucVu,
					item.PhongChucNangID,
					item.GhiChu
				);

				// Optional: tô màu theo ca
				if (item.CaLamViec == 1)
				{
					dgvLichLamViec.Rows[rowIndex].DefaultCellStyle.BackColor =
						Color.FromArgb(219, 234, 254);
				}
				else if (item.CaLamViec == 2)
				{
					dgvLichLamViec.Rows[rowIndex].DefaultCellStyle.BackColor =
						Color.FromArgb(220, 252, 231);
				}
			}
		}
		private async void LichLamViecForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetupCalendarPanels();

				_currentNhanVienId = Session.NhanVienId ?? 0;

				SetupWeekComboBox();
				SetupModeComboBox();
				SetupDataGridView();
				await LoadWeekAsync();


			}
			catch (Exception ex)
			{
				MessageHelper.ShowMessage("Lỗi load lịch: " + ex.Message);
			}
		}

		private void pnlTop_Paint(object sender, PaintEventArgs e)
		{

		}

		private void dgvLichLamViec_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}
	}
}
