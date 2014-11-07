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

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button toArtist = FindViewById<Button> (Resource.Id.btnToArtist);
			Button searchButton = FindViewById<Button> (Resource.Id.btnSearch);

			toArtist.Click += delegate {
				var intent = new Intent (this, typeof(ArtistViewActivity));
				intent.PutExtra ("artist", "Backstreet Boys");
				StartActivity (intent);
			};

			searchButton.Click += delegate {
				GetMyArtist();
			};
		}

		public void GetMyArtist ()
		{
			EditText searchQuery = FindViewById<EditText> (Resource.Id.searchtext);
			var artist = RestSharpFunctions.GetArtist(searchQuery.Text);
			TextView artistBio = FindViewById<TextView> (Resource.Id.twArtistBio);
			artistBio.TextFormatted = Html.FromHtml(artist.Bio.Summary);
		}
	}
}





