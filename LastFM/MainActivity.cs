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

namespace LastFM
{
	[Activity (Label = "LastFM", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		RestSharp RestSharpFunctions;
		List<Artist> artistList;
		List<Album> albumList;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			RestSharpFunctions = new RestSharp ();
			artistList = new List<Artist> ();
			albumList = new List<Album> ();
		
			Button artistSearchButton = FindViewById<Button> (Resource.Id.btnArtistSearch);
			Button btnGetAlbums = FindViewById<Button> (Resource.Id.btnAlbumResult);
			Button btnGetArtists = FindViewById<Button> (Resource.Id.btnArtistResult);
			ListView searchResultListview = FindViewById<ListView> (Resource.Id.lvArtistSearchResult);

			EditText artistSearchQery = FindViewById<EditText> (Resource.Id.artistSearchtext);
			//Button albumSearchButton = FindViewById<Button> (Resource.Id.btnAlbumSearch);
			//EditText albumSearchQery = FindViewById<EditText> (Resource.Id.albumSearchtext);

			artistSearchButton.Click += delegate {

				artistList.Clear();
				albumList.Clear();

				ArtistSearchResult (artistSearchQery.Text, searchResultListview);
			};
		
			btnGetAlbums.Click += delegate {
				AlbumSearchResult (artistSearchQery.Text, searchResultListview);
			};

			btnGetArtists.Click += delegate {
				ArtistSearchResult (artistSearchQery.Text, searchResultListview);
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
			if (artistList.Count > 10) {
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

		public void artistItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			var clickedArtist = artistList [e.Position];
			var intent = new Intent (this, typeof(ArtistViewActivity));
			intent.PutExtra ("artist", clickedArtist.Name);
			StartActivity (intent);
		}

		public void albumItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			var clickedAlbum = artistList [e.Position];
			var intent = new Intent (this, typeof(AlbumViewActivity));
			intent.PutExtra ("album", clickedAlbum.Name);
			StartActivity (intent);
		}
	}
}






