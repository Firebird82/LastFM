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
using Android.Text;
using Android.Text.Method;

namespace LastFM
{
	[Activity (Label = "ArtistViewActivity")]			
	public class ArtistViewActivity : Activity
	{
		RestSharp RestSharpFunctions = new RestSharp();
		Artist artist = new Artist();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.ArtistView);

			GetArtist ();
		}

		public void GetArtist()
		{
			TextView artistName = FindViewById<TextView> (Resource.Id.twArtistName);
			TextView artistBio = FindViewById<TextView> (Resource.Id.twArtistBio);
			TextView artistPublished = FindViewById<TextView> (Resource.Id.publishedYearArtist);
			TextView artistFormed = FindViewById<TextView> (Resource.Id.formedYearArtist);

			string query = Intent.GetStringExtra ("artist") ?? "Data not available";
			string queryId = Intent.GetStringExtra ("artistId") ?? "Data not available";

			artist = RestSharpFunctions.GetArtist (query,queryId);
			artistName.Text = artist.Name;
			artistBio.TextFormatted = Html.FromHtml(artist.Bio.Summary);
			artistBio.MovementMethod = LinkMovementMethod.Instance;
			artistFormed.Text = artist.Bio.YearFormed.ToString();
			artistPublished.Text = artist.Bio.Published.ToString();

			var similarArtists = new List<Artist> ();
			foreach (var similar in artist.Similar) 
			{
				similarArtists.Add(new Artist{ Name = similar.Name, Image = similar.Image });
			}

			ListView listView = FindViewById<ListView> (Resource.Id.similarList);
			listView.Adapter = new ArtistSceenAdapter(this, similarArtists);
			listView.ItemClick += OnListItemClick;

			ImageView artistImage = FindViewById<ImageView> (Resource.Id.ivSelectedArtistImage);
			var artistImages = artist.Image;
			var artistPhoto =  BitmapLoader.GetImageBitmapFromUrl(artistImages.First (i => i.Size.Equals ("mega")).Value);
			artistImage.SetImageBitmap(artistPhoto);

			ScrollView scrollArtist = FindViewById<ScrollView> (Resource.Id.scrollArtistView);
			scrollArtist.SmoothScrollTo(0, 0);
		}

		public void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var clickedArtist = artist.Similar[e.Position];
			var intent = new Intent (this, typeof(ArtistViewActivity));
			intent.PutExtra ("artistId", clickedArtist.Mbid);
			intent.PutExtra ("artist", clickedArtist.Name);

			StartActivity (intent);
		}
	}
}

