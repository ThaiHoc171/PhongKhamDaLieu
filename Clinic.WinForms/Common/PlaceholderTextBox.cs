using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Clinic.WinForms.Common
{
	public class PlaceholderTextBox : TextBox
	{
		private bool _isPlaceholder = true;
		private string _placeholder = "";

		[Category("Appearance")]
		public string PlaceholderText
		{
			get => _placeholder;
			set
			{
				_placeholder = value;
				SetPlaceholder();
			}
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			SetPlaceholder();
		}

		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			if (_isPlaceholder)
			{
				Text = "";
				ForeColor = Color.Black;
				_isPlaceholder = false;
			}
		}

		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
			if (string.IsNullOrWhiteSpace(Text))
			{
				SetPlaceholder();
			}
		}

		private void SetPlaceholder()
		{
			Text = _placeholder;
			ForeColor = Color.Gray;
			_isPlaceholder = true;
		}
	}
}