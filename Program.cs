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
			List<string> filmDatabases = new List<string> {
				"OMDB",
				"TMDB"
			};
			Console.WriteLine("Film databases:");
			for (int i = 0; i < filmDatabases.Count; i++)
			{
				Console.WriteLine(i+1 + ": " + filmDatabases[i]);
			}
			Console.Write("Choose a film database: ");
			string input = Console.ReadLine();
			int db;
			while (!int.TryParse(input, out db) || (db < 1 || db > filmDatabases.Count))
			{
				input = Console.ReadLine();
			}
			Console.Write("Enter film title: ");
			string title = Console.ReadLine();
			Film film = GetFilmAsync(title, filmDatabases[db-1]).Result;
			Console.WriteLine("Title: {0}, Year: {1}, Rating: {2}, Released: {3}, Runtime: {4}, Genre: {5}, Director: {6}, Writer: {7}, Actor: {8}, Plot: {9}, Language: {10}, Country: {11}, Awards: {12}, Poster: {13}, Metascore: {14}, IMDB Rating: {15}, IMDB Votes: {16}, IMDB ID: {17}, Type: {18}, DVD Release date: {19}, Box Office: {20}, Production: {21}, Website: {22}, Response: {23}",film.Title, film.Year, film.Rated, film.Released, film.Runtime, film.Genre, film.Director, film.Writer, film.Actors, film.Plot, film.Language, film.Country, film.Awards, film.Poster, film.Metascore, film.imdbRating, film.imdbVotes, film.imdbID, film.Type, film.DVD, film.BoxOffice, film.Production, film.Website, film.Response);
			Console.ReadLine();			
		}

		static async Task<Film> GetFilmAsync(string title, string db)
		{
			string url;
			switch (db)
			{
				case "TMDB":
					url = "https://api.themoviedb.org/3/search/movie?api_key=<<api_key>>&query={0}";
					break;
				default:
				case "OMDB":
					url = "http://www.omdbapi.com/?t={0}&apikey=b413c0e5";
					break;
			}
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
	}
}
