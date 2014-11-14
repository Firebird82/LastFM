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

namespace LastFM
{
	public class AlbumScreenAdapter:BaseAdapter<Album>{
		List<Album> items;
		Activity context;

		public AlbumScreenAdapter(Activity context, List<Album> items):base()
		{
			this.context = context;
			this.items = items;
		}

		public override Album this [int position] 
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
			var coverphoto =  BitmapLoader.GetImageBitmapFromUrl(artistImages.First (i => i.Size.Equals ("small")).Value);

			View view = convertView;

			if (view == null) 
			{
				view = context.LayoutInflater.Inflate (Resource.Layout.albumListTemplate, null);
			}
			view.FindViewById<TextView> (Resource.Id.tvAlbumName).Text = item.Name;
			var imageView = view.FindViewById<ImageView> (Resource.Id.ivAlbumImage);
			imageView.SetImageBitmap (coverphoto);

			return view;
		}

		public override int Count
		{
			get { return items.Count; }
		}
	}
}

