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
	public class SimilarCollection : List<Similar> { }

	public class Artist
	{
		public string Name { get; set; }
		public string Mbid { get; set; }
		public string Url { get; set; }
		public Biography Bio { get; set; }
		public ArtistImageCollection Image { get; set; }
		public SimilarCollection Similar { get; set; }
	}

	public class Similar
	{
		public string Name { get; set; }
		public string Url { get; set; }
	}
//
//	public class ArtistCollection : List<Artist>{}
//
//	public class Artistmatches
//	{
//		[XmlElement("artistmatches")]
//		public ArtistCollection artist { get; set; }
//	}
//
//	public class ArtistMatchesCollection : List<Artistmatches>{}
//
//	public class Results
//	{
//		[XmlElement("artistmatches")]
//		public ArtistMatchesCollection artistmatches { get; set; }
//	}
}

