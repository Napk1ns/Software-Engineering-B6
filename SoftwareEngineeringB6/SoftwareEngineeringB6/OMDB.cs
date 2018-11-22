using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineeringB6
{
	class OMDB : FilmDatabaseFacade
	{
		static HttpClient client = new HttpClient();

		static string url = "http://www.omdbapi.com/?t={0}&apikey=b413c0e5";

		public async Task<Film> GetFilmAsync(string title)
		{			
			HttpResponseMessage response = await client.GetAsync(string.Format(url, title));

			Film film = new Film();

			if (response.IsSuccessStatusCode)
			{
				string json = await response.Content.ReadAsStringAsync();
				MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
				DataContractJsonSerializer ser = new DataContractJsonSerializer(film.GetType());
				film = ser.ReadObject(ms) as Film;
				return film;
			}
			return film;
		}

		public async Task<Film> GetRandomFilmAsync()
		{
			Random r = new Random();
			string imdbID = "tt" + r.Next(10000000).ToString("D7");

			string url = "http://www.omdbapi.com/?i={0}&apikey=b413c0e5";

			HttpResponseMessage response = await client.GetAsync(string.Format(url, imdbID));
			Film film = new Film();
			if (response.IsSuccessStatusCode)
			{
				string json = await response.Content.ReadAsStringAsync();
				MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
				DataContractJsonSerializer ser = new DataContractJsonSerializer(film.GetType());
				film = ser.ReadObject(ms) as Film;
				return film;
			}
			return film;
		}
	}
}
