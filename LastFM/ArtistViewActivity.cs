
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
	[Activity (Label = "ArtistViewActivity")]			
	public class ArtistViewActivity : Activity
	{
		RestSharp RestSharpFunctions = new RestSharp();


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			GetArtist ();


		}

		public void GetArtist(){

			string query = Intent.GetStringExtra ("artist") ?? "Data not available";

			var artist = RestSharpFunctions.GetArtist (query);

			var name = artist.Name;
			var summary = artist.Bio.Summary;
			var yearformed = artist.Bio.YearFormed;
			var published = artist.Bio.Published;
			//var image = ?????;

		}
	}
}

