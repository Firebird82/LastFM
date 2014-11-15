using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace LastFM
{
	public class ArtistSceenAdapter:BaseAdapter<Artist>
	{
		List<Artist> items;
		Activity context;

		public ArtistSceenAdapter(Activity context, List<Artist> items):base()
		{
			this.context = context;
			this.items = items;
		}

		public override Artist this [int position]
		{
			get{ return items [position]; }
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = items[position];

			var coverphoto = SetCoverPhoto (item);

			var view = SetView (convertView, item, coverphoto);

			return view;
		}

		static Bitmap SetCoverPhoto (Artist item)
		{
			var artistImages = item.Image;
			Bitmap coverphoto = null;
			if (artistImages != null) 
			{
				coverphoto = BitmapLoader.GetImageBitmapFromUrl (artistImages.First (i => i.Size.Equals ("small")).Value);
			}
			return coverphoto;
		}

		View SetView (View convertView, Artist item, Bitmap coverphoto)
		{
			View view = convertView;
			if (view == null) 
			{
				view = context.LayoutInflater.Inflate (Resource.Layout.artistListTemplate, null);
			}

			view.FindViewById<TextView> (Resource.Id.lvListArtistName).Text = item.Name;
			var imageView = view.FindViewById<ImageView> (Resource.Id.ivArtistImage);
			imageView.SetImageBitmap (coverphoto);

			return view;
		}

		public override int Count
		{
			get { return items.Count; }
		}
	}
}

