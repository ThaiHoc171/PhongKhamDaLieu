using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.Common
{
	public static class helper
	{
		public static Image ResizeImage(Image img, int width, int height)
		{
			return new Bitmap(img, new Size(width, height));
		}
	}
}
