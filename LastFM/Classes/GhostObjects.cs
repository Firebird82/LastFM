using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Drawing;	
using System.Text;

namespace LastFM
{
	public class GhostObjects
	{
		public GhostObjects ()
		{
			Artistmatches = FillList ();
		}

		public List<GhostArtist> Artistmatches{ get; set; }

		public List<GhostArtist> FillList()
		{
			RestSharp restSharp = new RestSharp();

			return Artistmatches;
		}
	}

	public class GhostArtist
	{
		public string Name{ get; set; }
		public Image Image { get; set; }

		public GhostArtist ()
		{

		}
	}
}


