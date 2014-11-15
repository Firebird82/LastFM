using System;
using System.Collections.Generic;

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

	public class AlbumImageCollection : List<Image>{}

	public class Album
	{
		public string Name{ get; set; }
		public string Artist{ get; set; }
		public string Id{ get; set;}
		public string Url{ get; set; }
		public AlbumImageCollection Image{ get; set;}
		public string Mbid{ get; set; }
		public string Releasedate{ get; set; }
		public TrackCollection Tracks { get; set; }
		public Albumbio Wiki{ get; set;}
	}

	public class Albumbio
	{
		public string Summary { get; set; }
		public string Content { get; set; }
	}

	public class TrackBio
	{
		public string Summary { get; set; }
		public string Content { get; set; }
	}

	public class AlbumCollection : List<Album>{}

	public class Albummatches
	{
		public AlbumCollection albumCollection{ get; set; }
	}

	public class TackImageCollection: List<Image>{}

	public class Track
	{
		public string Name{ get; set; }
		public string Artist{ get; set; }
		public Album Album { get; set; }
		public TrackBio Wiki { get; set; }
		public string Url{ get; set; }
		public string Id { get; set; }
		public string Mbid{ get; set; }
		public double Duration{ get; set; }
		public TackImageCollection Image{ get; set; }
	}

	public class TrackCollection : List<Track>{}

	public class Trackmatches
	{
		public TrackCollection trackCollection{ get; set; }
	}
}

