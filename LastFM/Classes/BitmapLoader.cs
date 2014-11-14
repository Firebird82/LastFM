using System;
using System.Threading.Tasks;
using Android.Graphics;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Drawing;

namespace LastFM
{
	public class BitmapLoader
	{
		public BitmapLoader ()
		{
		}

		public static Bitmap GetImageBitmapFromUrl(string url)
		{
			Bitmap imageBitmap = null;

			imageBitmap = DownloadImage (url, imageBitmap);

			return imageBitmap;
		}

		static Bitmap DownloadImage(string url, Bitmap imageBitmap)
		{
			using (var webClient = new WebClient())
			{
				if (url != "")
				{
					imageBitmap = DownloadImageData (url, webClient);
				}
			}
			return imageBitmap;
		}

		static Bitmap DownloadImageData (string url, WebClient webClient)
		{
			Bitmap imageBitmap = null;
			var imageBytes = webClient.DownloadData (url);
			if (isNotNullorZero (imageBytes)) 
			{
				imageBitmap = BitmapFactory.DecodeByteArray (imageBytes, 0, imageBytes.Length);
				return imageBitmap;
			}
			return imageBitmap;
		}

		static bool isNotNullorZero (byte[] imageBytes)
		{
			return imageBytes != null && imageBytes.Length > 0;
		}
	}
}

