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
		RestSharp RestSharpFunctions = new RestSharp ();
		List<Artist> artistList = new List<Artist> ();
		List<Album> albumList = new List<Album> ();
		List<Track> trackList = new List<Track> ();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
		
			ListView artistSearchResultListview = FindViewById<ListView> (Resource.Id.lvArtistSearchResult);
			ListView albumSearchResultListview = FindViewById<ListView> (Resource.Id.lvAlbumSearchResult);
			ListView trackSearchResultListView = FindViewById<ListView> (Resource.Id.lvTrackSearchResult);

			EditText searchQuery = FindViewById<EditText> (Resource.Id.artistSearchtext);

			ButtonClickEvents (artistSearchResultListview, albumSearchResultListview, trackSearchResultListView, searchQuery);
		}

		void ButtonClickEvents (ListView artistSearchResultListview, ListView albumSearchResultListview, ListView trackSearchResultListView, EditText searchQuery)
		{
			SearchButtonClickEvent (artistSearchResultListview, searchQuery);
			GetAlbumsClickEvent (artistSearchResultListview, albumSearchResultListview, trackSearchResultListView, searchQuery);
			GetArtistsClickEvent (artistSearchResultListview, albumSearchResultListview, trackSearchResultListView, searchQuery);
			GetTracksClickEvent (artistSearchResultListview, albumSearchResultListview, trackSearchResultListView, searchQuery);
		}

		void GetTracksClickEvent (ListView artistSearchResultListview, ListView albumSearchResultListview, ListView trackSearchResultListView, EditText searchQuery)
		{
			Button btnGetTracks = FindViewById<Button> (Resource.Id.btnSongResult);
			btnGetTracks.Click += delegate {
				artistSearchResultListview.Adapter = null;
				albumSearchResultListview.Adapter = null;
				TrackSearchResult (searchQuery.Text, trackSearchResultListView);
				HideKeyboard (searchQuery);
			};
		}

		void GetArtistsClickEvent (ListView artistSearchResultListview, ListView albumSearchResultListview, ListView trackSearchResultListView, EditText searchQuery)
		{
			Button btnGetArtists = FindViewById<Button> (Resource.Id.btnArtistResult);
			btnGetArtists.Click += delegate {
				albumSearchResultListview.Adapter = null;
				trackSearchResultListView.Adapter = null;
				ArtistSearchResult (searchQuery.Text, artistSearchResultListview);
				HideKeyboard (searchQuery);
			};
		}

		void GetAlbumsClickEvent (ListView artistSearchResultListview, ListView albumSearchResultListview, ListView trackSearchResultListView, EditText searchQuery)
		{
			Button btnGetAlbums = FindViewById<Button> (Resource.Id.btnAlbumResult);
			btnGetAlbums.Click += delegate {
				artistSearchResultListview.Adapter = null;
				trackSearchResultListView.Adapter = null;
				AlbumSearchResult (searchQuery.Text, albumSearchResultListview);
				HideKeyboard (searchQuery);
			};
		}

		void SearchButtonClickEvent (ListView artistSearchResultListview, EditText searchQuery)
		{
			Button artistSearchButton = FindViewById<Button> (Resource.Id.btnArtistSearch);
			artistSearchButton.Click += delegate 
			{
				artistList.Clear ();
				albumList.Clear ();
				HideKeyboard (searchQuery);
				ArtistSearchResult (searchQuery.Text, artistSearchResultListview);
			};
		}

		public void ArtistSearchResult (string query, ListView searchResultListview)
		{
			artistList = RestSharpFunctions.GetArtistList(query);

			var tenArtist = ifArtistListIsLowerThan11 (query);

			searchResultListview.Adapter = new ArtistSceenAdapter (this, tenArtist);
			searchResultListview.ItemClick += ArtistItemClick;
		}

		List<Artist> ifArtistListIsLowerThan11 (string query)
		{
			if (artistList.Count == 0) 
			{
				artistList = RestSharpFunctions.GetArtistList (query);
			}

			var tenArtist = new List<Artist> ();
			if (artistList.Count > 10) 
			{
				tenArtist = artistList.Where (artist => artist.Mbid != "").Take (10).ToList ();
			}
			return tenArtist;
		}
			
		public void AlbumSearchResult (string query, ListView searchResultListview)
		{
			if (albumList.Count == 0)
			{
				albumList = RestSharpFunctions.GetAlbumList (query);
			}
			var tenAlbums = albumList.Where (album => album.Mbid != "").Take (10).ToList();
			searchResultListview.Adapter = new AlbumScreenAdapter (this, tenAlbums);
			searchResultListview.ItemClick += AlbumItemClick;		
		}

		public void TrackSearchResult (string query, ListView searchResultListview)
		{
			if (trackList.Count == 0)
			{
				trackList = RestSharpFunctions.GetTrackList (query);
			}

			var tenTracks = trackList.Where (track => track.Mbid != "\"\"" || track.Image != null).Take (10).ToList();
			searchResultListview.Adapter = new TrackScreenAdapter (this, tenTracks);
			searchResultListview.ItemClick += TrackItemClick;		
		}

		public void ArtistItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			var clickedArtist = artistList [e.Position];
			var intent = new Intent (this, typeof(ArtistViewActivity));
			intent.PutExtra ("artist", clickedArtist.Name);
			intent.PutExtra ("artistId", clickedArtist.Mbid);

			StartActivity (intent);
		}

		public void AlbumItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			var clickedAlbum = albumList [e.Position];
			var intent = new Intent (this, typeof(AlbumViewActivity));
			intent.PutExtra ("album", clickedAlbum.Name);
			intent.PutExtra ("albumid", clickedAlbum.Mbid);

			StartActivity (intent);
		}

		public void TrackItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			var clickedTrack = trackList [e.Position];
			var intent = new Intent (this, typeof(TrackViewActivity));
			intent.PutExtra ("trackId", clickedTrack.Mbid);
			StartActivity (intent);
		}

		public void HideKeyboard(EditText searchQuery)
		{
			InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService); imm.HideSoftInputFromWindow(searchQuery.WindowToken, 0);
		}
	}
}






