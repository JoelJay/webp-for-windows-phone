using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

using Microsoft.Phone.Controls;

namespace WebpForWinPhone
{
	public partial class MainPage : PhoneApplicationPage
	{
		// Constructor
		public MainPage()
		{
			InitializeComponent();

			// Sample code to localize the ApplicationBar
			//BuildLocalizedApplicationBar();
		}


		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Uri fileUri = new Uri(string.Format("Assets/{0}.webp", "unnamed"), UriKind.Relative);
			StreamResourceInfo res = Application.GetResourceStream(fileUri);
			if (res != null && res.Stream != null)
			{
				using (MemoryStream stream = new MemoryStream())
				{
					CopyStream(res.Stream, stream);

                    WebpComponent.WebpRuntimeComponent com = new WebpComponent.WebpRuntimeComponent();
					byte[] arr = stream.GetBuffer();

					var result = com.DecodeRGBA(arr, arr.Length, 128, 128);

					WriteableBitmap bitmap = new WriteableBitmap(128, 128);
					for (int i = 0; i < result.Length; i+=4)
					{
						// make the transform from color to pixel.
						int tranformedPixel = (result[i + 3] << 24 | result[i] << 16 | result[i + 1] << 8 | result[i + 2]);

						bitmap.Pixels[i/4] = tranformedPixel;
					}
					bitmap.Invalidate();
					image.Source = bitmap;
				}
			}

		}

		public void CopyStream(Stream input, Stream output)
		{
			byte[] buffer = new byte[16 * 1024];
			int read;
			while (((read = input.Read(buffer, 0, buffer.Length)) > 0))
			{
				output.Write(buffer, 0, read);
			}
		}

		public static BitmapImage ByteArray2Bitmap(byte[] bitData, BitmapCreateOptions option = BitmapCreateOptions.BackgroundCreation)
		{
			BitmapImage bitmap = new BitmapImage();
			using (MemoryStream ms = new MemoryStream(bitData))
			{
				bitmap.CreateOptions = option;
				bitmap.SetSource(ms);
				return bitmap;
			}
		}

	}
}