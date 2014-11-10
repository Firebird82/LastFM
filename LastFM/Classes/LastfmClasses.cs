using System;
using System.Collections.Generic;
//using System.Xml.Serialization;

namespace LastFM
{
	public class Image
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

	public class ArtistImageCollection : List<Image> { }
	public class ArtistSimilarCollection : List<Artist> { }

	public class Artist
	{
		public string Name { get; set; }
		public string Mbid { get; set; }
		public string Url { get; set; }
		public Biography Bio { get; set; }
		public ArtistImageCollection Image { get; set; }
		public ArtistSimilarCollection Similar { get; set; }
	}

	public class Similar
	{
		public string Name { get; set; }
		public string Url { get; set; }
	}

	public class ArtistsCollection : List<Artist> { }

	public class Artistmatches
	{
		public ArtistsCollection artistsCollection { get; set; }
	}
}

