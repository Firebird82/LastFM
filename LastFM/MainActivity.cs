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
		RestSharp RestSharpFunctions = new RestSharp ();
		GhostObjects ghost = new GhostObjects ();
		List<Artist> artistList = new List<Artist> ();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
		
			Button toArtist = FindViewById<Button> (Resource.Id.btnToArtist);
			Button searchButton = FindViewById<Button> (Resource.Id.btnSearch);
			EditText searchQuery = FindViewById<EditText> (Resource.Id.searchtext);

			toArtist.Click += delegate {
				RestSharpFunctions.GetArtist("cher");
			};

			searchButton.Click += delegate {
				SearchResult (searchQuery.Text);
			};
		}

		public void SearchResult (string searchQuery)
		{
			artistList =  RestSharpFunctions.GetArtistList(searchQuery);
			ListView lView = FindViewById<ListView> (Resource.Id.listView1);
			lView.Adapter = new ArtistSceenAdapter (this, artistList);
			lView.ItemClick += OnListItemClick;
		}

		public void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var clickedArtist = artistList[e.Position];
			var intent = new Intent (this, typeof(ArtistViewActivity));
			intent.PutExtra ("artist", clickedArtist.Name);
			StartActivity (intent);
		}
	}
}






