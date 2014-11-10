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

		public T Execute<T> (string artistName,string method) where T : new()
		{
			var client = new RestClient();
			client.BaseUrl = "http://ws.audioscrobbler.com/";

			var request = GetRequest (method, artistName);
			var response = client.Execute<T>(request);
			return response.Data;
		}

		 RestRequest GetRequest (string methodValue, string searchString)
		{
			var parameterKey = (methodValue.Split ('.')) [0];
			var request = new RestRequest("/2.0/", Method.GET);
			request.AddParameter("method", methodValue); // album.search
			request.AddParameter(parameterKey, searchString); // album, albumName
			request.AddParameter("api_key", "e527758dd1063dd021d7b8bb180ffd44");
			request.RequestFormat = DataFormat.Json;
			return request;
		}

		public Artist GetArtist(string query)
		{
			string method = "artist.getinfo";
			var artist = Execute<Artist> (query, method);
			artist.Similar.RemoveAt(0);
			return artist;
		}

		public ArtistsCollection GetArtistList(string query)
		{
			string method = "artist.search";
			return Execute<ArtistsCollection> (query, method);
		}
	}
}

