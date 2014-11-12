
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


			TextView albumName = FindViewById<TextView> (Resource.Id.tvSelectedAlbumName);
			TextView artistName = FindViewById<TextView> (Resource.Id.tvSelectedArtistName);
			TextView realesedate = FindViewById<TextView> (Resource.Id.tvRealeseDate);
			TextView albumbio = FindViewById<TextView> (Resource.Id.tvalbumBio);
			TextView tvTracks = FindViewById<TextView> (Resource.Id.tvTracks);

			ImageView albumImage = FindViewById<ImageView> (Resource.Id.ivSelectedAlbumImage);

			//ListView tracks = FindViewById<ListView> (Resource.Id.lvTracks);
		
			var album = RestSharpFunctions.GetAlbum (query,queryId);
			var checkedAlbum = CheckIfAlbumPopertyIsNull (album);
			var albumImages = checkedAlbum.Image;
			var coverphoto =  BitmapLoader.GetImageBitmapFromUrl(albumImages.First (i => i.Size.Equals ("mega")).Value);

			ScrollView scrollArtist = FindViewById<ScrollView> (Resource.Id.scrollAlbumView);
			scrollArtist.SmoothScrollTo(0, 0);

			albumName.Text = checkedAlbum.Name;
			artistName.Text = checkedAlbum.Artist;
			albumbio.Text = checkedAlbum.Wiki.Summary;
			albumImage.SetImageBitmap(coverphoto);
			realesedate.Text += checkedAlbum.Releasedate;

			int count = 0;

			foreach (var track in album.Tracks) {
			
				TimeSpan ts = TimeSpan.FromSeconds(track.Duration);
				var duration = String.Format("{0}:{1:D2}", ts.Minutes, ts.Seconds);

				count++;
				tvTracks.Text += "Track " + count.ToString() + ": " + track.Name + "\n";
				tvTracks.Text += "Duration: " + duration + "\n"+"\n";
			
			}

			//tracks.Adapter = new AlbumTrackScreenAdapter (this, album.Tracks);
		}

		public Album CheckIfAlbumPopertyIsNull(Album album){
		
			if (album.Wiki == null) {
				album.Wiki = new Albumbio ();
				album.Wiki.Summary = "Biography not available";
				}

			if (string.IsNullOrEmpty(album.Releasedate)) {
				album.Releasedate = "N/A";			
			} 
			else{


				string realeaseDate = album.Releasedate.Remove (album.Releasedate.Length -7);
				realeaseDate = realeaseDate.Remove (0, 3);
				album.Releasedate = realeaseDate;			
		
			}
			return album;
		
		}
	}
}

