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
		public RestSharp ()
		{
		}

		public T Execute<T> (string query,string method) where T : new()
		{
			T argType = new T ();
			string type = argType.GetType ().ToString ();

			var request = ConfigureRequest (method, query, type);

			var client = new RestClient();
			client.BaseUrl = "http://ws.audioscrobbler.com/";

			var response = client.Execute<T>(request);
			return response.Data;
		}

		RestRequest ConfigureRequest (string methodValue, string searchString,string type)
		{
			var parameterKey = (methodValue.Split ('.')) [0];
			var request = new RestRequest("/2.0/", Method.GET);

			SetupRequest (methodValue, request); 
		
			isArtistAlbumTrackOrSearch (searchString, type, parameterKey, request);

			return request;
		}

		static void isArtistAlbumTrackOrSearch (string searchString, string type, string parameterKey, RestRequest request)
		{
			if (isArtistAlbumOrTrack (type)) 
			{
				request.AddParameter ("mbid", searchString);
			} 
			else 
			{
				//If its a search
				request.AddParameter (parameterKey, searchString);
			}
		}

		static bool isArtistAlbumOrTrack (string type)
		{
			return type == "LastFM.Album" || type == "LastFM.Artist" || type == "LastFM.Track";
		}

		static void SetupRequest (string methodValue, RestRequest request)
		{
			request.AddParameter ("method", methodValue);
			request.AddParameter ("api_key", "e527758dd1063dd021d7b8bb180ffd44");

			request.RequestFormat = DataFormat.Json;
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

