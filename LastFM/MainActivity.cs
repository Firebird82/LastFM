using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Text;
using Android 
using System.Collections.Generic;

namespace LastFM
{
	[Activity (Label = "LastFM", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		RestSharp RestSharpFunctions = new RestSharp ();
		GhostObjects ghost = new GhostObjects ();
		List<GhostArtist> data = new List<GhostArtist> ();
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
		
			Button toArtist = FindViewById<Button> (Resource.Id.btnToArtist);
			Button searchButton = FindViewById<Button> (Resource.Id.btnSearch);
			EditText searchQuery = FindViewById<EditText> (Resource.Id.searchtext);

			toArtist.Click += delegate {
				var intent = new Intent (this, typeof(ArtistViewActivity));
				intent.PutExtra ("artist", "Cher");
				StartActivity (intent);
			};

			searchButton.Click += delegate {
				SearchResult (searchQuery.Text);
			};
		}

		public void SearchResult (string query)
		{
			RestSharp restSharp = new RestSharp();

			data =  restSharp.GetSearchResult(query);
			var items = new string[] { "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers" };
			EditText searchQuery = FindViewById<EditText> (Resource.Id.searchtext);
			ListView lView = FindViewById<ListView> (Resource.Id.listView1);
			lView.Adapter = new ArtistSceenAdapter (this, data);
			lView.ItemClick += OnListItemClick;
		}

		public void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var listView = sender as ListView;
			var t = data[e.Position];

			var balle = t.Name;

			var intent = new Intent (this, typeof(ArtistViewActivity));
			intent.PutExtra ("artist", t.Name);
			StartActivity (intent);
		

		
		}




	}
}






