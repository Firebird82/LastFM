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

		public static async Task<Bitmap> GetImageFromUrl(string url)
		{
			using (var client = new HttpClient())
			{
				var msg = await client.GetAsync(url);
				if (!msg.IsSuccessStatusCode) return null;
				using (var stream = await msg.Content.ReadAsStreamAsync())
				{
					var bitmap = await BitmapFactory.DecodeStreamAsync(stream);
					return bitmap;
				}
			}
		}

		public static Bitmap GetImageBitmapFromUrl(string url)
		{
			Bitmap imageBitmap = null;

			using (var webClient = new WebClient())
			{
				var imageBytes = webClient.DownloadData(url);
				if (imageBytes != null && imageBytes.Length > 0)
				{
					imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
				}
			}

			return imageBitmap;
		}

	}
}

