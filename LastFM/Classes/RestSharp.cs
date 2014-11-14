using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Net;

namespace LastFM
{
	public class RestSharp
	{
		private const string apiKey = "e527758dd1063dd021d7b8bb180ffd44";
		private const string appsecret = "ec2f518ee3c70baa607756ab81fa066e";
		private const string address = "http://ws.audioscrobbler.com/2.0/";
		private const string methodArtistSearch =  "?method=artist.search&artist=";
		private const string medthodGetArtistInfo =  "?method=artist.getinfo&artist=";

		public RestSharp ()
		{

		}

		public T Execute<T> (string query,string method) where T : new()
		{
			var client = new RestClient();
			client.BaseUrl = "http://ws.audioscrobbler.com/";

			T argType = new T ();
			string type = argType.GetType ().ToString ();

			var request = GetRequest (method, query,type);
			var response = client.Execute<T>(request);
			return response.Data;
		}

		RestRequest GetRequest (string methodValue, string searchString,string type)
		{
			var parameterKey = (methodValue.Split ('.')) [0];
			var request = new RestRequest("/2.0/", Method.GET);
			request.AddParameter("method", methodValue); // album.search
			request.AddParameter("api_key", "e527758dd1063dd021d7b8bb180ffd44");
			request.RequestFormat = DataFormat.Json;
		
			if (type == "LastFM.Album" || type == "LastFM.Artist" || type=="LastFM.Track") {
			
					request.AddParameter ("mbid", searchString);
				}

			else {
					request.AddParameter(parameterKey, searchString); // album, albumName
			}

			return request;
		}




		public Artist GetArtist(string query, string queryId)
		{
			string method = "artist.getinfo";
			var artist = Execute<Artist> (queryId, method);

			if (artist.Similar.Count > 0) 
			{
				artist.Similar.RemoveAt(0);
			}

			return artist;
		}

		public ArtistsCollection GetArtistList(string query)
		{
			string method = "artist.search";
			return Execute<ArtistsCollection> (query, method);
		}

		public Album GetAlbum(string query, string queryId)
		{
			string method = "album.getinfo";
			var album = Execute<Album> (queryId, method);
			return album;
		}

		public AlbumCollection GetAlbumList(string query)
		{
			string method = "album.search";
			return Execute<AlbumCollection> (query, method);
		}


		public Track GetTrack(string queryId)
		{
			string method = "track.getinfo";
			var track = Execute<Track> (queryId, method);
			return track;
		}

		public TrackCollection GetTrackList(string query)
		{
			string method = "track.search";
			return Execute<TrackCollection> (query, method);
		}
	}
}

