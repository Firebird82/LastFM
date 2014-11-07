using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Text.RegularExpressions;

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

		public T Execute<T>(RestRequest request, string url) where T : new()
		{
			var client = new RestClient();
			client.BaseUrl = url;

			var response = client.Execute<T>(request);

			if (response.ErrorException != null)
			{
				const string message = "Error retrieving response.  Check inner details for more info.";
				var twilioException = new ApplicationException(message, response.ErrorException);
				throw twilioException;
			}

			return response.Data;
		}

		public Artist GetArtist(string arg) 
		{
			string url = address + medthodGetArtistInfo + arg + "&api_key=" + apiKey;
			var request = new RestRequest();

			request.RootElement = "artist";

			return Execute<Artist>(request, url);
		}

		public List<GhostArtist> GetSearchResult(string arg)
		{
			string url = address + methodArtistSearch + arg + "&api_key=" + apiKey;
			var request = new RestRequest();

			request.RootElement = "artistCollection";

			var client = new RestClient();
			client.BaseUrl = url;

			var response = client.Execute(request);
			var xmlString = response.Content.ToString();

			MatchCollection artistsSearchMatch = Regex.Matches(xmlString, @"<artist>(.|\n)*?</artist>");

			List<GhostArtist> artistList = new List<GhostArtist> ();
			foreach (var artistMatch in artistsSearchMatch) {
				string artistString = artistMatch.ToString ();
				Match artistNameMatch = Regex.Match(artistString, @"<name>(.|\n)*?</name>");
				string artistName = Regex.Replace(artistNameMatch.ToString(), @"</*name>", "").ToString();

				GhostArtist newArtist = new GhostArtist();
				newArtist.Name = artistName;
				artistList.Add(newArtist);
			}

			return artistList;
		}
	}
}

