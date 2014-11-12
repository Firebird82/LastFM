using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Text;
using Android;
using System.Collections.Generic;
using Android.Views.InputMethods;

namespace LastFM
{
	[Activity (Label = "LastFM", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		RestSharp RestSharpFunctions;
		List<Artist> artistList;
		List<Album> albumList;
		List<Track> trackList;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			RestSharpFunctions = new RestSharp ();
			artistList = new List<Artist> ();
			albumList = new List<Album> ();
			trackList = new List<Track> ();
		
			Button artistSearchButton = FindViewById<Button> (Resource.Id.btnArtistSearch);
			Button btnGetAlbums = FindViewById<Button> (Resource.Id.btnAlbumResult);
			Button btnGetArtists = FindViewById<Button> (Resource.Id.btnArtistResult);
			Button btnGetTracks = FindViewById<Button> (Resource.Id.btnSongResult);

			ListView searchResultListview = FindViewById<ListView> (Resource.Id.lvArtistSearchResult);

			EditText artistSearchQuery = FindViewById<EditText> (Resource.Id.artistSearchtext);
			//Button albumSearchButton = FindViewById<Button> (Resource.Id.btnAlbumSearch);
			//EditText albumSearchQery = FindViewById<EditText> (Resource.Id.albumSearchtext);

			artistSearchButton.Click += delegate 
			{
				InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService); imm.HideSoftInputFromWindow(artistSearchQuery.WindowToken, 0);
				artistList.Clear();
				albumList.Clear();
				ArtistSearchResult (artistSearchQuery.Text, searchResultListview);
			};
		
			btnGetAlbums.Click += delegate 
			{
				AlbumSearchResult (artistSearchQuery.Text, searchResultListview);
			};

			btnGetArtists.Click += delegate 
			{
				ArtistSearchResult (artistSearchQuery.Text, searchResultListview);
			};

			btnGetTracks.Click += delegate 
			{
				TrackSearchResult (artistSearchQuery.Text, searchResultListview);
			};
		}

		public void ArtistSearchResult (string query, ListView searchResultListview)
		{
			artistList =  RestSharpFunctions.GetArtistList(query);

			if (artistList.Count == 0) 
			{
				artistList = RestSharpFunctions.GetArtistList (query);
			}

			var tenArtist = new List<Artist>();
			if (artistList.Count > 10) 
			{
				tenArtist = artistList.GetRange (0,10);
			}	

			searchResultListview.Adapter = new ArtistSceenAdapter (this, tenArtist);
			searchResultListview.ItemClick += artistItemClick;
		}
			
		public void AlbumSearchResult (string query, ListView searchResultListview)
		{
			if (albumList.Count == 0)
			{
				albumList = RestSharpFunctions.GetAlbumList (query);
			}

			var tenAlbums = albumList.GetRange (0, 10);
			searchResultListview.Adapter = new AlbumScreenAdapter (this, tenAlbums);
			searchResultListview.ItemClick += albumItemClick;		
		}

		public void TrackSearchResult (string query, ListView searchResultListview)
		{
			if (trackList.Count == 0)
			{
				trackList = RestSharpFunctions.GetTrackList (query);
			}

			var tenTracks = trackList.GetRange (0, 10);
			searchResultListview.Adapter = new TrackScreenAdapter (this, tenTracks);
			searchResultListview.ItemClick += trackItemClick;		
		}

		public void artistItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			var clickedArtist = artistList [e.Position];
			var intent = new Intent (this, typeof(ArtistViewActivity));
			intent.PutExtra ("artist", clickedArtist.Name);
			StartActivity (intent);
		}

		public void albumItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			var clickedAlbum = albumList [e.Position];
			var intent = new Intent (this, typeof(AlbumViewActivity));
			intent.PutExtra ("album", clickedAlbum.Name);
			intent.PutExtra ("albumid", clickedAlbum.Mbid);

			StartActivity (intent);
		}

		public void trackItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			var clickedTrack = trackList [e.Position];
			var intent = new Intent (this, typeof(AlbumViewActivity));
			intent.PutExtra ("track", clickedTrack.Name);
			StartActivity (intent);
		}
	}
}






