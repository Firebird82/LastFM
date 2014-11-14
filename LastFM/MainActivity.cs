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
using System.Linq;

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

			ListView artistSearchResultListview = FindViewById<ListView> (Resource.Id.lvArtistSearchResult);
			ListView albumSearchResultListview = FindViewById<ListView> (Resource.Id.lvAlbumSearchResult);
			ListView trackSearchResultListView = FindViewById<ListView> (Resource.Id.lvTrackSearchResult);




			EditText searchQuery = FindViewById<EditText> (Resource.Id.artistSearchtext);
			//Button albumSearchButton = FindViewById<Button> (Resource.Id.btnAlbumSearch);
			//EditText albumSearchQery = FindViewById<EditText> (Resource.Id.albumSearchtext);

			artistSearchButton.Click += delegate 
			{
				artistList.Clear();
				albumList.Clear();
				HideKeyboard(searchQuery);
				ArtistSearchResult (searchQuery.Text, artistSearchResultListview);
			};
		
			btnGetAlbums.Click += delegate {
				artistSearchResultListview.Adapter = null;
				trackSearchResultListView.Adapter = null;

				AlbumSearchResult (searchQuery.Text, albumSearchResultListview);
				HideKeyboard(searchQuery);
			};

			btnGetArtists.Click += delegate {
				albumSearchResultListview.Adapter = null;
				trackSearchResultListView.Adapter = null;
				ArtistSearchResult (searchQuery.Text, artistSearchResultListview);
				HideKeyboard(searchQuery);
			};

			btnGetTracks.Click += delegate {
				artistSearchResultListview.Adapter = null;
				albumSearchResultListview.Adapter = null;

				TrackSearchResult (searchQuery.Text, trackSearchResultListView);
				HideKeyboard(searchQuery);
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
				tenArtist = artistList.Where (artist => artist.Mbid != "").Take (10).ToList();
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
			var tenAlbums = albumList.Where (album => album.Mbid != "").Take (10).ToList();
			searchResultListview.Adapter = new AlbumScreenAdapter (this, tenAlbums);
			searchResultListview.ItemClick += albumItemClick;		
		}

		public void TrackSearchResult (string query, ListView searchResultListview)
		{
			if (trackList.Count == 0)
			{
				trackList = RestSharpFunctions.GetTrackList (query);
			}

			var tenTracks = trackList.Where (track => track.Mbid != "\"\"" || track.Image != null).Take (10).ToList();
			searchResultListview.Adapter = new TrackScreenAdapter (this, tenTracks);
			searchResultListview.ItemClick += trackItemClick;		
		}

		public void artistItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			var clickedArtist = artistList [e.Position];
			var intent = new Intent (this, typeof(ArtistViewActivity));
			intent.PutExtra ("artist", clickedArtist.Name);
			intent.PutExtra ("artistId", clickedArtist.Mbid);

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
			var intent = new Intent (this, typeof(TrackViewActivity));
			intent.PutExtra ("trackId", clickedTrack.Mbid);
			StartActivity (intent);
		}

		public void HideKeyboard(EditText searchQuery){
		
		
			InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService); imm.HideSoftInputFromWindow(searchQuery.WindowToken, 0);

		}
	}
}






