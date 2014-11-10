using System;
using RestSharp;
using System.Threading.Tasks;


namespace LastFM
{
	public class ArtistList
	{
		public ArtistList ()
		{
		}


	
		public void test(){
		
			var client = new RestClient();
			client.BaseUrl = "http://ws.audioscrobbler.com/";

			GetArtistsCollection ("cher",client);
		
		
		
		}

		public T GetArtistsCollection<T> (string artistName, RestClient client)
		{

					var request = GetRequest ("artist.search", artistName);
					var response = client.Execute<T>(request);
					var v = response.Data;
					return v;
				
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


	}
}

