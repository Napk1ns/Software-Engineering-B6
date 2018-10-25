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
	}

	class Program
	{
		static HttpClient client = new HttpClient();

		static void Main(string[] args)
		{
			Console.WriteLine(GetFilmAsync("blade runner"));
			Console.ReadLine();			
		}

		static async Task<Film> GetFilmAsync(string title)
		{
			title.Replace(' ', '+');
			HttpResponseMessage response = await client.GetAsync(string.Format("http://www.omdbapi.com/?t={0}&apikey=b413c0e5", title));

			Film film = new Film();

			if (response.IsSuccessStatusCode)
			{
				string json = await response.Content.ReadAsStringAsync();
				Console.WriteLine(json);
				MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
				DataContractJsonSerializer ser = new DataContractJsonSerializer(film.GetType());
				film = ser.ReadObject(ms) as Film;
				Console.WriteLine("============================");
				Console.WriteLine(film.Title);
				return film;
			}
			return film;
		}
	}
}
