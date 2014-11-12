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
	public class AlbumTrackScreenAdapter:BaseAdapter<Track>{
		List<Track> items;
		Activity context;

		public AlbumTrackScreenAdapter(Activity context, List<Track> items):base(){

			this.context = context;
			this.items = items;
		}

		public override Track this [int position] {
			get{ return items [position]; }
		}

		public override long GetItemId(int position)
		{
			return position;		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = items[position];
			View view = convertView;
			TimeSpan ts = TimeSpan.FromSeconds(item.Duration);
			var duration = String.Format("{0}:{1:D2}", ts.Minutes, ts.Seconds);



			if (view == null) {
				view = context.LayoutInflater.Inflate (Resource.Layout.albumTrackListTempelate, null);
			}

			view.FindViewById<TextView> (Resource.Id.tvAlbumTrackName).Text = "Track " + (position + 1).ToString() + ": " + item.Name;
			view.FindViewById<TextView> (Resource.Id.tvDuration).Text = "Duration: " + duration;


			return view;
		}

		public override int Count
		{
			get { return items.Count; }
		}
	}
}

