
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
	[Activity (Label = "SearchActivity")]			
	public class SearchActivity :ListActivity
	{
		RestSharp RestSharpFunctions = new RestSharp ();
		GhostObjects ghost = new GhostObjects ();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Create your application here
		}
	}
}

