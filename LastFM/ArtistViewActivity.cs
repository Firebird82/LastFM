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
			string query = Intent.GetStringExtra ("artist") ?? "Data not available";
			string queryId = Intent.GetStringExtra ("artistId") ?? "Data not available";

			artist = RestSharpFunctions.GetArtist (query, queryId);

			PopulateArtistView ();
		}

		void PopulateArtistView ()
		{
			if (artist.Name == null) 
			{
				ArtistNotFound ();
			}
			else 
			{
				AddDataToArtistView ();
			}
		}

		void ArtistNotFound ()
		{
			TextView artistName = FindViewById<TextView> (Resource.Id.twArtistName);
			artistName.Text = "Artist Not Found!!!";

			TextView artistFormed = FindViewById<TextView> (Resource.Id.formedYearArtist);
			artistFormed.Text = "";

			TextView publishedYear = FindViewById<TextView> (Resource.Id.publishedYearArtist);
			publishedYear.Text = "";

			TextView artistBio = FindViewById<TextView> (Resource.Id.twArtistBio);
			artistBio.Text = "";
		}

		void AddDataToArtistView ()
		{
			AddTextViewDataToArtist ();
			AddSimilarListToArtist ();
			AddArtistImage ();
			ScrollviewHack ();
		}

		void ScrollviewHack ()
		{
			ScrollView scrollArtist = FindViewById<ScrollView> (Resource.Id.scrollArtistView);
			scrollArtist.SmoothScrollTo (0, 0);
		}

		void AddArtistImage ()
		{
			var artistImages = artist.Image;
			var artistPhoto = BitmapLoader.GetImageBitmapFromUrl (artistImages.First (i => i.Size.Equals ("mega")).Value);
			ImageView artistImage = FindViewById<ImageView> (Resource.Id.ivSelectedArtistImage);
			artistImage.SetImageBitmap (artistPhoto);
		}

		List<Artist> AddSimilarListToArtist ()
		{
			var similarArtists = new List<Artist> ();
			foreach (var similar in artist.Similar) 
			{
				similarArtists.Add (ConvertSimilarArtistToArtist (similar));
			}

			AddClickEventsToSimilarList (similarArtists);

			return similarArtists;
		}

		void AddClickEventsToSimilarList (List<Artist> similarArtists)
		{
			ListView listView = FindViewById<ListView> (Resource.Id.similarList);
			listView.Adapter = new ArtistSceenAdapter (this, similarArtists);
			listView.ItemClick += OnListItemClick;
		}

		static Artist ConvertSimilarArtistToArtist (Artist similar)
		{
			return new Artist {
				Name = similar.Name,
				Image = similar.Image
			};
		}

		void AddTextViewDataToArtist ()
		{
			TextView artistName = FindViewById<TextView> (Resource.Id.twArtistName);
			artistName.Text = artist.Name;

			TextView artistFormed = FindViewById<TextView> (Resource.Id.formedYearArtist);
			artistFormed.Text = artist.Bio.YearFormed.ToString ();

			AddArtistBio ();

			AddPublishedYear ();
		}

		void AddPublishedYear ()
		{
			TextView artistPublished = FindViewById<TextView> (Resource.Id.publishedYearArtist);
			if (artist.Bio.Published != null) 
			{
				artistPublished.Text = artist.Bio.Published.ToString ("MMM/yyyy");
			}
		}

		void AddArtistBio ()
		{
			TextView artistBio = FindViewById<TextView> (Resource.Id.twArtistBio);
			artistBio.TextFormatted = Html.FromHtml (artist.Bio.Summary);
			artistBio.MovementMethod = LinkMovementMethod.Instance;
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

