
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
using Android.Graphics;
using Android.Text;

namespace LastFM
{
	[Activity (Label = "AlbumViewActivity")]			
	public class AlbumViewActivity : Activity
	{
		RestSharp RestSharpFunctions = new RestSharp ();
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.AlbumView);

			GetAlbum ();
		}

		public void GetAlbum()
		{
			string query = Intent.GetStringExtra ("album") ?? "Data not available";
			string queryId = Intent.GetStringExtra ("albumid") ?? "Data not available";

			var album = RestSharpFunctions.GetAlbum (query,queryId);

			AddDataToAlbumView (album);
		}

		void AddDataToAlbumView (Album album)
		{
			var checkedAlbum = CheckIfAlbumPopertyIsNull (album);

			AddCoverPhoto (checkedAlbum);

			ScrollviewHack ();

			AddTextViewDataToAlbum (checkedAlbum);

			AddTracksToAlbumView (album);
		}

		void AddTextViewDataToAlbum (Album checkedAlbum)
		{
			TextView albumName = FindViewById<TextView> (Resource.Id.tvSelectedAlbumName);
			albumName.Text = checkedAlbum.Name;

			TextView artistName = FindViewById<TextView> (Resource.Id.tvSelectedArtistName);
			artistName.Text = checkedAlbum.Artist;

			TextView albumbio = FindViewById<TextView> (Resource.Id.tvalbumBio);
			albumbio.TextFormatted = Html.FromHtml( checkedAlbum.Wiki.Summary);

			TextView realesedate = FindViewById<TextView> (Resource.Id.tvRealeseDate);
			realesedate.Text += checkedAlbum.Releasedate;
		}

		void ScrollviewHack ()
		{
			ScrollView scrollArtist = FindViewById<ScrollView> (Resource.Id.scrollAlbumView);
			scrollArtist.SmoothScrollTo (0, 0);
		}

		void AddCoverPhoto (Album checkedAlbum)
		{
			var albumImages = checkedAlbum.Image;
			Bitmap coverphoto = null;

			if (albumImages != null) 
			{
				coverphoto = BitmapLoader.GetImageBitmapFromUrl (albumImages.First (i => i.Size.Equals ("mega")).Value);
			}

			ImageView albumImage = FindViewById<ImageView> (Resource.Id.ivSelectedAlbumImage);
			albumImage.SetImageBitmap (coverphoto);
		}

		void AddTracksToAlbumView (Album album)
		{
			TextView tvTracks = FindViewById<TextView> (Resource.Id.tvTracks);
			int count = 0;

			foreach (var track in album.Tracks) 
			{
				AddTrackData (track, count, tvTracks);
			}
		}

		static void AddTrackData (Track track, int count, TextView tvTracks)
		{
			TimeSpan ts = TimeSpan.FromSeconds (track.Duration);
			var duration = String.Format ("{0}:{1:D2}", ts.Minutes, ts.Seconds);
			count++;
			tvTracks.Text += "Track " + count.ToString () + ": " + track.Name + "\n";
			tvTracks.Text += "Duration: " + duration + "\n" + "\n";
		}

		public Album CheckIfAlbumPopertyIsNull(Album album)
		{
			SetDefaultBiography (album);

			SetDefaultReleseDate (album);
			return album;
		}

		static void SetDefaultReleseDate (Album album)
		{
			if (string.IsNullOrEmpty (album.Releasedate)) 
			{
				album.Releasedate = "N/A";
			}
			else 
			{
				string realeaseDate = album.Releasedate.Remove (album.Releasedate.Length - 7);
				realeaseDate = realeaseDate.Remove (0, 3);
				album.Releasedate = realeaseDate;
			}
		}

		static void SetDefaultBiography (Album album)
		{
			if (album.Wiki == null) 
			{
				album.Wiki = new Albumbio ();
				album.Wiki.Summary = "Biography not available";
			}
		}
	}
}

