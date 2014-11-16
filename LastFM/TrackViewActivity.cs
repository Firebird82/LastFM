
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
using Android.Text;

namespace LastFM
{
	[Activity (Label = "TrackViewActivity")]			
	public class TrackViewActivity : Activity
	{
		RestSharp RestSharpFunctions;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.TrackView);
			RestSharpFunctions = new RestSharp();
			GetTrack ();
		}

		public void GetTrack()
		{
			string queryId = Intent.GetStringExtra ("trackId") ?? "Data not available";
			string sArtistName = Intent.GetStringExtra ("artistName") ?? "Data not available";

			var track = RestSharpFunctions.GetTrack (queryId);
			var checkTrack = CheckIfTrackPopertyIsNull (track);

			PopulateTrackVIew (sArtistName, checkTrack);
		}

		void PopulateTrackVIew (string sArtistName, Track checkTrack)
		{
			ScrollViewHack ();

			PopulateTextViews (sArtistName, checkTrack);

			AddCoverPhoto (checkTrack);
		}

		void ScrollViewHack ()
		{
			ScrollView scrollTrack = FindViewById<ScrollView> (Resource.Id.scrollTrackView);
			scrollTrack.SmoothScrollTo (0, 0);
		}

		void PopulateTextViews (string sArtistName, Track checkTrack)
		{
			TextView trackName = FindViewById<TextView> (Resource.Id.tvSelectedTrackName);
			trackName.Text = checkTrack.Name;

			TextView artistName = FindViewById<TextView> (Resource.Id.tvSelectedTrackArtistName);
			artistName.Text = sArtistName;

			TextView trackBio = FindViewById<TextView> (Resource.Id.tvTrackBio);
			trackBio.TextFormatted = Html.FromHtml (checkTrack.Wiki.Summary);

			TextView duration = FindViewById<TextView> (Resource.Id.tvTrackDuration);
			TimeSpan ts = TimeSpan.FromSeconds (checkTrack.Duration);
			duration.Text += String.Format ("{0}:{1:D2}", ts.Minutes, ts.Seconds);
		}

		void AddCoverPhoto (Track checkTrack)
		{
			ImageView albumImage = FindViewById<ImageView> (Resource.Id.ivSelectedTrackImage);
			var trackImage = checkTrack.Album.Image;
			var coverphoto = BitmapLoader.GetImageBitmapFromUrl (trackImage.First (i => i.Size.Equals ("extralarge")).Value);
			albumImage.SetImageBitmap (coverphoto);
		}

		public Track CheckIfTrackPopertyIsNull(Track track)
		{
			if (track.Wiki == null) 
			{
				track.Wiki = new TrackBio ();
				track.Wiki.Summary = "Biography not available";
			}

			if (string.IsNullOrEmpty(track.Album.Releasedate)) 
			{
				track.Album.Releasedate = "N/A";			
			} 
			else
			{
				string realeaseDate = track.Album.Releasedate.Remove (track.Album.Releasedate.Length -7);
				realeaseDate = realeaseDate.Remove (0, 3);
				track.Album.Releasedate = realeaseDate;
			}
			return track;
		}
	}
}