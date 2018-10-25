using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace ConsoleApp1
{
	[DataContract]
	internal class Film
	{
		[DataMember]
		internal string Title;

		[DataMember]
		internal string Year;

		[DataMember]
		internal string Rated;

		[DataMember]
		internal string Released;

		[DataMember]
		internal string Runtime;

		[DataMember]
		internal string Genre;

		[DataMember]
		internal string Director;

		[DataMember]
		internal string Writer;

		[DataMember]
		internal string Actors;

		[DataMember]
		internal string Plot;

		[DataMember]
		internal string Language;

		[DataMember]
		internal string Country;

		[DataMember]
		internal string Awards;

		[DataMember]
		internal string Poster;

		// [DataMember]
		// internal string Ratings;

		[DataMember]
		internal string Metascore;

		[DataMember]
		internal string imdbRating;

		[DataMember]
		internal string imdbVotes;

		[DataMember]
		internal string imdbID;

		[DataMember]
		internal string Type;

		[DataMember]
		internal string DVD;

		[DataMember]
		internal string BoxOffice;

		[DataMember]
		internal string Production;

		[DataMember]
		internal string Website;

		[DataMember]
		internal string Response;
	}

	class Program
	{
		static HttpClient client = new HttpClient();

		static void Main(string[] args)
		{
			Console.Write("Enter film title: ");
			string title = Console.ReadLine();
			Film film = GetFilmAsync(title).Result;
			Console.WriteLine("Film title: {0}", film.Title);
			Console.ReadLine();			
		}

		static async Task<Film> GetFilmAsync(string title)
		{
			HttpResponseMessage response = await client.GetAsync(string.Format("http://www.omdbapi.com/?t={0}&apikey=b413c0e5", title));

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
