using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace LastFM
{
	public class GhostObjects
	{
		public GhostObjects ()
		{
	
		}

		public List<GhostArtist> Artistmatches{ get; set; }


		public List<GhostArtist> FillList ()
		{
			Artistmatches.Add (new GhostArtist{ Name = "Cher" });
			Artistmatches.Add (new GhostArtist{ Name = "Eagle-Eye Cherry" });




			return Artistmatches;

		}


	}

	public class GhostArtist
	{
		public string Name{ get; set; }

		public GhostArtist ()
		{

		}


	}
}


