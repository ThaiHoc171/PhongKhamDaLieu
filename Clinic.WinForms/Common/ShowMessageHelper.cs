using System.Linq;
using System.Windows.Forms;

namespace Clinic.WinForms.Common
{
	public static class MessageHelper
	{
		public static void ShowMessage(string message)
		{
			var currentForm = Form.ActiveForm
							  ?? Application.OpenForms
											.Cast<Form>()
											.FirstOrDefault(f => f.Focused);

			var dialog = new Guna.UI2.WinForms.Guna2MessageDialog
			{
				Text = message,
				Caption = "Thông báo",
				Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK,
				Icon = Guna.UI2.WinForms.MessageDialogIcon.Information,
				Style = Guna.UI2.WinForms.MessageDialogStyle.Light
			};

			if (currentForm != null && currentForm.TopLevel)
			{
				dialog.Parent = currentForm;
			}

			dialog.Show();
		}
		public static DialogResult Confirm(string message)
		{
			var currentForm = Form.ActiveForm
							  ?? Application.OpenForms
									.Cast<Form>()
									.FirstOrDefault(f => f.Focused);

			var dialog = new Guna.UI2.WinForms.Guna2MessageDialog
			{
				Text = message,
				Caption = "Xác nhận",
				Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo,
				Icon = Guna.UI2.WinForms.MessageDialogIcon.Question,
				Style = Guna.UI2.WinForms.MessageDialogStyle.Light
			};

			if (currentForm != null && currentForm.TopLevel)
				dialog.Parent = currentForm;

			return dialog.Show();
		}
	}
}