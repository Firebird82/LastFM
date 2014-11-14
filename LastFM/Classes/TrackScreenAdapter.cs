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
	public class TrackScreenAdapter:BaseAdapter<Track>
	{
		List<Track> items;
		Activity context;

		public TrackScreenAdapter(Activity context, List<Track> items):base()
		{
			this.context = context;
			this.items = items;
		}

		public override Track this [int position] 
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

			var artistImages = item.Image;
			Bitmap coverphoto = null;
			if (artistImages != null) 
			{
				coverphoto =  BitmapLoader.GetImageBitmapFromUrl(artistImages.First (i => i.Size.Equals ("small")).Value);
			}

			View view = convertView;

			if (view == null) {
				view = context.LayoutInflater.Inflate (Resource.Layout.trackListTempelate, null);
			}

			view.FindViewById<TextView> (Resource.Id.lvTrackName).Text = item.Name;

			if (item.Artist != null) {
				view.FindViewById<TextView> (Resource.Id.lvTrackArtistName).Text = item.Artist.Name;
			}

			var imageView = view.FindViewById<ImageView> (Resource.Id.ivTrackImage);
			imageView.SetImageBitmap (coverphoto);

			return view;
		}

		public override int Count
		{
			get { return items.Count; }
		}
	}
}

