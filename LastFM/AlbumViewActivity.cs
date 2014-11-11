
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LastFM
{
	[Activity (Label = "AlbumViewActivity")]			
	public class AlbumViewActivity : Activity
	{
		RestSharp RestSharpFunctions;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.AlbumView);
			RestSharpFunctions = new RestSharp ();
			GetAlbum ();
		}

		public void GetAlbum(){

			string query = Intent.GetStringExtra ("album") ?? "Data not available";
			string queryId = Intent.GetStringExtra ("albumid") ?? "Data not available";

			TextView albumName = FindViewById<TextView> (Resource.Id.twSelectedAlbumName);
			TextView artistName = FindViewById<TextView> (Resource.Id.twSelectedAlbumName);
			ImageView albumImage = FindViewById<ImageView> (Resource.Id.ivSelectedAlbumImage);
			var album = RestSharpFunctions.GetAlbum (query,queryId);
			var albumImages = album.Image;
			var coverphoto =  BitmapLoader.GetImageBitmapFromUrl(albumImages.First (i => i.Size.Equals ("medium")).Value);

			albumName.Text = album.Name;
			artistName.Text = album.Artist;
			albumImage.SetImageBitmap(coverphoto);



		}
	}
}

