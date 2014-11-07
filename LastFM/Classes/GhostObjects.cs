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
			Artistmatches = FillList ();
		}

		public List<GhostArtist> Artistmatches{ get; set; }

		public List<GhostArtist> FillList()
		{
			RestSharp restSharp = new RestSharp();
			Artistmatches = restSharp.GetSearchResult("Abba");


//			Artistmatches = new List<GhostArtist> ();
//			Artistmatches.Add (new GhostArtist{ Name = "Cher" });
//			Artistmatches.Add (new GhostArtist{ Name = "Eagle-Eye Cherry" });
//			Artistmatches.Add (new GhostArtist{ Name = "Cheryl Cole" });
//			Artistmatches.Add (new GhostArtist{ Name = "Cher Lloyd" });
//			Artistmatches.Add (new GhostArtist{ Name = "Cherish" });
//			Artistmatches.Add (new GhostArtist{ Name = "Wild Cherry" });
//			Artistmatches.Add (new GhostArtist{ Name = "Black Stone Cherry" });
//			Artistmatches.Add (new GhostArtist{ Name = "Sonny & Cher" });
//			Artistmatches.Add (new GhostArtist{ Name = "Neneh Cherry" });
//			Artistmatches.Add (new GhostArtist{ Name = "Cheryl Lynn" });
//			Artistmatches.Add (new GhostArtist{ Name = "Cherry Poppin' Daddies" });
//			Artistmatches.Add (new GhostArtist{ Name = "Cherry Ghost" });
//			Artistmatches.Add (new GhostArtist{ Name = "Cherub" });
//			Artistmatches.Add (new GhostArtist{ Name = "Cherish The Ladies" });
//			Artistmatches.Add (new GhostArtist{ Name = "Youssou N'Dour & Neneh Cherry" });
//			Artistmatches.Add (new GhostArtist{ Name = "Don Cherry" });
//			Artistmatches.Add (new GhostArtist{ Name = "Cheryl" });
//			Artistmatches.Add (new GhostArtist{ Name = "Cheri Dennis" });
//			Artistmatches.Add (new GhostArtist{ Name = "Cherrelle" });
//			Artistmatches.Add (new GhostArtist{ Name = "Cherbourg" });

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


