using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Text;


namespace LastFM
{
	[Activity (Label = "LastFM", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		RestSharp RestSharpFunctions = new RestSharp ();
		GhostObjects ghost = new GhostObjects ();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
		
			Button toArtist = FindViewById<Button> (Resource.Id.btnToArtist);
			Button searchButton = FindViewById<Button> (Resource.Id.btnSearch);
			EditText searchQuery = FindViewById<EditText> (Resource.Id.searchtext);


			string query = Intent.GetStringExtra ("searchquery") ?? "Data not available";



			toArtist.Click += delegate {
				var intent = new Intent (this, typeof(ArtistViewActivity));
				intent.PutExtra ("artist", "Backstreet Boys");
				StartActivity (intent);
			};

			searchButton.Click += delegate {
				SearchResult (query);

			};


		}

		public void SearchResult (string query)
		{
			var data = ghost.Artistmatches;
			var items = new string[] { "Vegetables", "Fruits", "Flower Buds", "Legumes", "Bulbs", "Tubers" };
			EditText searchQuery = FindViewById<EditText> (Resource.Id.searchtext);
			ListView lView = FindViewById<ListView> (Resource.Id.listView1);
			lView.Adapter = new ArtistSceenAdapter (this, data);

		
		}

		public void GetMyArtist (string selectedArtist)
		{
			EditText searchQuery = FindViewById<EditText> (Resource.Id.searchtext);

			var artist = RestSharpFunctions.GetArtist(searchQuery.Text);
		//TextView artistBio = FindViewById<TextView> (Resource.Id.twArtistBio);
			//artistBio.TextFormatted = Html.FromHtml(artist.Bio.Summary);

		}


	}

	
}






