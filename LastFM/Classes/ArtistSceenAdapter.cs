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
	public class ArtistSceenAdapter:BaseAdapter<Artist>{
		List<Artist> items;
		Activity context;

		public ArtistSceenAdapter(Activity context, List<Artist> items):base(){
		
			this.context = context;
			this.items = items;
		}

		public override Artist this [int position] {
			get{ return items [position]; }
		}

		public override long GetItemId(int position)
		{
			return position;		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = items[position];
			View view = convertView;
			var artistImages = item.Image;
			var coverphoto = BitmapLoader.GetImageFromUrl(artistImages.First (i => i.Size.Equals ("medium")).Value);


			if (view == null) {
				view = context.LayoutInflater.Inflate (Resource.Layout.ListViewTemp, null);
			}

			view.FindViewById<TextView> (Resource.Id.lvListArtistName).Text = string.Format ("{0}", item.Name);
			var imageView = view.FindViewById<ImageView> (Resource.Id.ivArtistImage);

			return view;
		}

		public override int Count
		{
			get { return items.Count; }
		}
	}
}

