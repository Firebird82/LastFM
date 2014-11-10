using System;
using System.Threading.Tasks;
using Android.Graphics;
using System.Net.Http;

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
	}
}

