
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

			TextView trackName = FindViewById<TextView> (Resource.Id.tvSelectedTrackName);
			TextView artistName = FindViewById<TextView> (Resource.Id.tvSelectedTrackArtistName);
			TextView duration = FindViewById<TextView> (Resource.Id.tvTrackDuration);
			TextView trackBio = FindViewById<TextView> (Resource.Id.tvTrackBio);
			TextView tvTracks = FindViewById<TextView> (Resource.Id.tvTracks);

			ImageView albumImage = FindViewById<ImageView> (Resource.Id.ivSelectedTrackImage);

			//ListView tracks = FindViewById<ListView> (Resource.Id.lvTracks);

			var track = RestSharpFunctions.GetTrack (queryId);
			var checkTrack = CheckIfTrackPopertyIsNull (track);
			var trackImage = checkTrack.Album.Image;
			var coverphoto =  BitmapLoader.GetImageBitmapFromUrl(trackImage.First (i => i.Size.Equals ("extralarge")).Value);
			TimeSpan ts = TimeSpan.FromSeconds(checkTrack.Duration);

			ScrollView scrollTrack = FindViewById<ScrollView> (Resource.Id.scrollTrackView);
			scrollTrack.SmoothScrollTo(0, 0);

			trackName.Text = checkTrack.Name;
			artistName.Text = checkTrack.Artist.Name;
			trackBio.Text = checkTrack.Wiki.Summary;
			albumImage.SetImageBitmap(coverphoto);
			duration.Text += String.Format("{0}:{1:D2}", ts.Minutes, ts.Seconds);


	

		



		

			//tracks.Adapter = new AlbumTrackScreenAdapter (this, album.Tracks);
		}

		public Track CheckIfTrackPopertyIsNull(Track track){

			if (track.Wiki == null) {
				track.Wiki = new TrackBio ();
				track.Wiki.Summary = "Biography not available";
			}

			if (string.IsNullOrEmpty(track.Album.Releasedate)) {
				track.Album.Releasedate = "N/A";			
			} 
			else{


				string realeaseDate = track.Album.Releasedate.Remove (track.Album.Releasedate.Length -7);
				realeaseDate = realeaseDate.Remove (0, 3);
				track.Album.Releasedate = realeaseDate;			

			}
			return track;

		}
	}
}