using System;
using System.Collections.Generic;

namespace LastFM
{
	public class image
	{
		public string Size { get; set; }
		public string Value { get; set; }
	}

	public class Biography
	{
		public string Summary { get; set; }
		public string Content { get; set; }
		public int YearFormed { get; set; }
		public DateTime Published { get; set; }
	}

	public class ArtistImageCollection : List<image> { }

	public class Artist
	{
		public string Name { get; set; }
		public string Mbid { get; set; }
		public string Url { get; set; }
		public Biography Bio { get; set; }
		public ArtistImageCollection Image { get; set; }

	}



	public class Results   
	{
		public ArtistCollection Artistmatches{ get; set; }
	}

	public class ArtistCollection : List<Artist>{}





}

